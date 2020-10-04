using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AngularAcessoriesBack.Dtos
{
    public class OrderCreateDto
    {

        [Required]
        public string DatePlaced { get; set; }

        [Required]
        public int ItemsQuantity { get; set; }

        [Required]
        public OrderProductCreateDto[] products { get; set; }

        [Required]
        public string OrderStatus { get; set; }

        [Required]
        public OrderDetailsDto orderDetails { get; set; }
    }
}
