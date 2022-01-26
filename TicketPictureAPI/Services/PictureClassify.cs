using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TicketPictureAPI.Interface;
using TicketPictureAPI.Modles;

namespace TicketPictureAPI.Services
{
    public class PictureClassify : IPictureClassify
    {
        private readonly ITicketConvert _ticketConvertcs;
        public PictureClassify(ITicketConvert ticketConvert) 
        {
            _ticketConvertcs = ticketConvert;
        }
        public APIResult GetClassifyPath(string ticktNo)
        {
            var pattern = "(\\w{1})(\\w{1})(\\d{2})(\\w{2})(\\d{3})(\\d{2})(\\d{4})";
            var ticketInfo = new Regex(pattern);
            var match = ticketInfo.Match(ticktNo);
            try
            {
                string year = _ticketConvertcs.ConvertYear(match.Groups[1].Value);
                string month = _ticketConvertcs.ConvertMonth(match.Groups[2].Value);
                return new APIResult
                {
                    IsSuccess = true,
                    Message = "Success",
                    Data = $@"\{year}\{month}\{match.Groups[3].Value}\{match.Groups[4]}\{match.Groups[6].Value}\{match.Groups[5].Value}"
                };

            }
            catch (Exception ex)
            {
                return new APIResult
                {
                    IsSuccess = false,
                    Message = ex.Message,
                    Data = ""
                };
            }
        }
       
    }
}
