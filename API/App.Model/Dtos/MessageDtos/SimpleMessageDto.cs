using System;

namespace App.Model.Dtos
{
    public class SimpleMessageDto
    {
        public Guid Id { get; set; }
        public string Topic { get; set; }
        public string Content { get; set; }
        public DateTime? SendDate { get; set; }
    }
}
