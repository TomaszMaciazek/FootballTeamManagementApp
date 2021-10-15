using App.Model.Entities.Common;
using System;
using System.ComponentModel.DataAnnotations;

namespace App.Model.Entities
{
    public class Translation
    {
        public Guid Id { get; set; }
        [Required]
        public string Key { get; set; }

        [Required]
        public string Value { get; set; }

        public Language Language { get; set; }
    }
}
