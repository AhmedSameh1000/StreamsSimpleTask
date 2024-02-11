using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTask.BAL.DTOs
{
    public class DocumentFileToReturn
    {
        public int id { get; set; }
        public string filePath { get; set; }
        public string FileName { get; set; }
    }
}