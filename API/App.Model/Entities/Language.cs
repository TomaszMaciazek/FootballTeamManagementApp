using App.Model.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace App.Model.Entities
{
    public class Language
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }

        [Required]
        public string Code { get; set; }
        public ICollection<Translation> Translations { get; set; }
    }
}
