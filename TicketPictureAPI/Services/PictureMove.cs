using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TicketPictureAPI.Interface;
using TicketPictureAPI.Modles;

namespace TicketPictureAPI.Services
{
    public class PictureMove : IPictureMove
    {
        /// <summary>
        /// 搬移照片
        /// </summary>
        /// <param name="picturePath">路徑相關資訊</param>
        /// <returns></returns>
        public APIResult TicketPictureMove(PicturePath picturePath)
        {
            try
            {
                File.Move(picturePath.SourcePath, picturePath.DestPath);
                return new APIResult
                {
                    IsSuccess = true,
                    Message = "Success",
                    Data = picturePath
                };
            }
            catch (Exception ex) {
                return new APIResult
                {
                    IsSuccess = false,
                    Message = ex.Message,
                    Data = picturePath
                };
            }
        }
    }
}
