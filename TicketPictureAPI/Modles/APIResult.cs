using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketPictureAPI.Modles
{
    public class APIResult
    {
        public bool IsSuccess { get; set; }
        public string Message {get;set;}
        public object Data { get; set; }

    }
}
