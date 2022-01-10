using System;

namespace App.Model.Dtos
{
    public class SimpleTestTemplateDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public double PassedMinimalValue { get; set; }
    }
}
