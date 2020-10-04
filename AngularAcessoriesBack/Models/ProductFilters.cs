using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AngularAcessoriesBack.Models
{
    //database table.. contains the product's filters
    public class ProductFilters
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Category { get; set; }
        public Categories category { get; set; }

        [Required]
        //foreignkey for an alterntive key
        public string Value { get; set; }
        public List<ProductFiltersValues> productFiltersValues { get; set; }

        [Required]
        [ForeignKey("product")]
        public int productId { get; set; }
        public Product product { get; set; }


    }
}
