using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketPictureAPI.Interface
{
    public interface ITicketConvert
    {
        string ConvertYear(string yearValue);
        string ConvertMonth(string monthValue);
    }
}
