using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketPictureAPI.Modles;

namespace TicketPictureAPI.Interface
{
    public interface IPictureMove
    {
        APIResult TicketPictureMove(PicturePath picturePath);
    }
}
