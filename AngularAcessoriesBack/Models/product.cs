using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using AngularAcessoriesBack.Data;
using Microsoft.EntityFrameworkCore;

namespace AngularAcessoriesBack.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string ImagePaths { get; set; }
        [Required]
        public float Price { get; set; }
        [Required]
        public int SalePercent { get; set; }
        [Required]
        public int QuantityAvailable { get; set; }
        [Required]
        public int TotalRatings { get; set; }
        [Required]
        public string Description { get; set; }

        public bool OnDisplay { get; set; }
        public bool FeaturedItem { get; set; }
        public List<Review> Reviews { get; set; }
        [NotMapped]
        public string[] ImagePathsArr
        {
            get
            {
                return ImagePaths.Split(';');
            }
            set
            {
                var _data = value;
                ImagePaths = string.Join(';', _data.Select(p => p.ToString()).ToArray());
            }
        }
    }
}
