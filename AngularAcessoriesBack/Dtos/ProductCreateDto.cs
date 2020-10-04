using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AngularAcessoriesBack.Dtos
{
    public class ProductCreateDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string ImagePaths { get; set; }
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
        public bool FeaturedItem { get; set; }
        
        
        public string[] ImagePathsArr
        {
            get
            {
                if(ImagePaths == null) { return null;  }
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
