using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AngularAcessoriesBack.Models
{
    public class CategoryValues
    {
        [Key]
        public int Id { get; set; }


        [Required]
        //foreignkey for an alterntive key
        public string Category { get; set; }
        public Categories category { get; set; }

        [Required]
        public string Value { get; set; }

        public List<ProductFiltersValues> productFiltersValues { get; set; }
    }
}
