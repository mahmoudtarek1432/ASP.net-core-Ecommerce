using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AngularAcessoriesBack.Dtos
{
    public class OrderReadDto
    {

        [Required]
        public int OrderId { get; set; }

        [Required]
        public string OrderStatus { get; set; }

        [Required]
        public string DatePlaced { get; set; }

        [Required]
        public int ItemsQuantity { get; set; }

        [Required]
        public string FirstItemName { get; set; }

        [Required]
        public string FirstItemImage { get; set; }
    }
}
