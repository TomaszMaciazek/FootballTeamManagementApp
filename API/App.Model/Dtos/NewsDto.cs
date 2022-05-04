using System;

namespace App.Model.Dtos
{
    public class NewsDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public bool IsImportant { get; set; }
    }
}
