using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TicketPictureAPI.Interface;
using TicketPictureAPI.Modles;

namespace TicketPictureAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TicketPictureController : ControllerBase
    {
        private readonly ILogger<TicketPictureController> _logger;
        private readonly IPictureClassify _pictureClassify;
        private readonly IConfiguration _configuration;
        private readonly IPictureMove _pictureMove;
        public TicketPictureController(ILogger<TicketPictureController> logger, IPictureClassify pictureClassify, IConfiguration configuration, IPictureMove pictureMove)
        {
            _logger = logger;
            _pictureClassify = pictureClassify;
            _configuration = configuration;
            _pictureMove = pictureMove;
        }

        /// <summary>
        /// 取得停車單分類結果
        /// </summary>
        /// <param name="ticketNo">來源單號</param>
        /// <returns>目的地</returns>
        [HttpPost]
        public APIResult GetClassifyPath([FromBody] TicketInfo jsonString)
        {
            return _pictureClassify.GetClassifyPath(jsonString.TicketNo);
        }

        /// <summary>
        /// 取得來源與目的路徑
        /// </summary>
        /// <param name="ticketNo">來源單號</param>
        /// <returns>目的地</returns>
        [HttpGet]
        public APIResult GetPicturePath()
        {
            return new APIResult
            {
                IsSuccess = true,
                Message = "Success",
                Data = new PicturePath
                {
                    SourcePath = _configuration.GetValue<string>("PicturePath:SourcePath"),
                    DestPath = _configuration.GetValue<string>("PicturePath:DestPath")
                }
            }; 
        }

         /// <summary>
         /// 取得現有廠商資料夾
         /// </summary>
         /// <returns></returns>
        [HttpGet]
        public APIResult GetDirectory() 
        {
            var sourcePath = _configuration.GetValue<string>("PicturePath:SourcePath");
            string[] dirs = Directory.GetDirectories(sourcePath, "", SearchOption.TopDirectoryOnly);
            return new APIResult
            {
                IsSuccess = true,
                Message = "Success",
                Data = dirs
            };
        }

        /// <summary>
        /// 停車單照片分類
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public APIResult StartClassify()
        {        
            var sourcePath = _configuration.GetValue<string>("PicturePath:SourcePath");
            var destPath = _configuration.GetValue<string>("PicturePath:DestPath");

            //搬移結果
            List<object> data = new();

            //第一層(廠商資料夾)
            string[] dirs = Directory.GetDirectories(sourcePath, "", SearchOption.TopDirectoryOnly);

            foreach (var dir in dirs)
            {
                //第二層(日期資料夾)
                string[] dirsLevel2 = Directory.GetDirectories(dir, "???????", SearchOption.TopDirectoryOnly);

                foreach (var die2 in dirsLevel2)
                {
                    //取得資料夾內所有檔案
                    var pictures = Directory.EnumerateFiles(die2);

                    foreach (var item in pictures)
                    {
                        var ticketNo = Path.GetFileName(item).Substring(0, 15);
                        var pictureName = Path.GetFileName(item);
                        var classifyPath = _pictureClassify.GetClassifyPath(ticketNo).Data;
                        var newDestPath = $@"{destPath}{classifyPath}";

                        Directory.CreateDirectory(newDestPath);

                        var pictureInfo = new PicturePath
                        {
                            SourcePath = item,
                            DestPath = $@"{newDestPath}\{pictureName}",
                            PictureName = Path.GetFileName(item)
                        };
                        var result = _pictureMove.TicketPictureMove(pictureInfo);

                        _logger.LogInformation($@"{result.IsSuccess}-{result.Message}-{pictureInfo.PictureName}({pictureInfo.SourcePath}=>{pictureInfo.DestPath})");
                        data.Add(result);
                    }
   
                }
            }
        
            return new APIResult
            {
                IsSuccess = true,
                Message = "Success",
                Data = data
            };
        }
    }
}
