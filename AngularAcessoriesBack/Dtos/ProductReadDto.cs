using AngularAcessoriesBack.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AngularAcessoriesBack.Dtos
{
    public class ProductReadDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        public int SalePercent { get; set; }
        [Required]
        public int QuantityAvailable { get; set; }
        [Required]
        public int TotalRatings { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public List<Review> Reviews { get; set; }

        public string[] ImagePathsArr { get; set; }
    }
}
