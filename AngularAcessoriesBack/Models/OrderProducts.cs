using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AngularAcessoriesBack.Models
{
    public class OrderProducts
    {
        [Key]
        public int Id{ get; set; }

        [ForeignKey("order")]
        [Required]
        public int OrderId { get; set; }
        public Orders Order { get; set; }

        [ForeignKey("product")]
        [Required]
        public int ProductId { get; set; }
        public Product product { get; set; }

        [Required]
        public float Price { get; set; }

        [Required]
        public int Quantity { get; set; }
    }
}
