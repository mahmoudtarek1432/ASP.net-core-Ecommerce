using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AngularAcessoriesBack.Models
{
    // joining table for many to many relation between productfilters and categoryValues
    public class ProductFiltersValues
    {
        [Key]
        public int Id { get; set; }

        public int productFilterId { get; set; }
        public ProductFilters productFilters { get; set; }


        public string Value { get; set; }
        public CategoryValues categoryValues { get; set; }
    }
}
