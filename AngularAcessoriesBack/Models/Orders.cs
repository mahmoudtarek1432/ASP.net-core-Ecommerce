using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AngularAcessoriesBack.Models
{
    public class Orders
    {
        [Key]
        [Required]
        public int id { get; set; }

        [Required]
        public int OrderId { get; set; }

        [ForeignKey("User")]
        public string OrderClientId { get; set; }
        public CustomIdentityUser user { get; set; }

        [Required]
        public string FirstItemName { get; set; }

        [Required]
        public string FirstItemImage { get; set; }

        [Required]
        public string OrderStatus { get; set; }

        [Required]
        public string DatePlaced { get; set; }

        [Required]
        public int ItemsQuantity { get; set; }
    }
}
