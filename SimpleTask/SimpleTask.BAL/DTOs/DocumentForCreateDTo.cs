using Microsoft.AspNetCore.Http;
using SimpleTask.Api.Attiebutes;

namespace SimpleTask.BAL.DTOs
{
    public class DocumentForCreateDTo
    {
        public string Name { get; set; }

        public string UserId { get; set; }
        public DateTime Due_date { get; set; }
        public int PriorityId { get; set; }

        [AllowedExtensionsAttributes(".pdf,.jpeg,.png,.mp4")]
        public List<IFormFile>? files { get; set; }
    }
}