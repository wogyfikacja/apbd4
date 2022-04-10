using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace cw4.Models
{
    public class Animal
    {
        public int IdAnimal { get; set; }
        [MinLength(10)]
        [MaxLength(100)]
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string Area { get; set; }
    }
}