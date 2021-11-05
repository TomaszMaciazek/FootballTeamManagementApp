using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Model.Dtos
{
    public class NewsDto
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public bool IsImportant { get; set; }
    }
}
