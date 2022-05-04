using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Model.ViewModels.Queries
{
    public class TestTemplateQuery : PaginationQuery
    {
        public Guid? AuthorId { get; set; }
        public string Title { get; set; }
    }
}
