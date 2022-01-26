using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TicketPictureAPI.Modles
{
    public class PicturePath
    {
        [Required]
        public string SourcePath { get; set; }
        [Required]
        public string DestPath { get; set; }
        public string PictureName { get; set; }
    }
}
