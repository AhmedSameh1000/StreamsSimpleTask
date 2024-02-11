using Microsoft.AspNetCore.Http;
using SimpleTask.Api.Attiebutes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTask.BAL.DTOs
{
    public class FileForCreateDTO
    {
        public int DocumentId { get; set; }

        [AllowedExtensionsAttributes(".pdf,.jpeg,.png,.mp4")]
        public List<IFormFile> files { get; set; }
    }
}