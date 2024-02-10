using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTask.BAL.DTOs
{
    public class DocumentForReturnDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string UserId { get; set; }
        public string UserEmail { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime DueDate { get; set; }
        public string Priorty { get; set; }

        public List<string> DocumentFiles { get; set; }
    }
}