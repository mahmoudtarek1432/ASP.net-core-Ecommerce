using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AngularAcessoriesBack.Models
{
    public class Categories
    {
        [Key]
        public int Id { get; set; }

        [Required]
        //alterntive key
        public string Category { get; set; }

        public List<CategoryValues> values { get; set; }

        public List<ProductFilters> productFilters { get; set; }
    }
}
