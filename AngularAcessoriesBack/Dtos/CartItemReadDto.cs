using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AngularAcessoriesBack.Dtos
{
    //dto for object sent to client -- removes user id for security reasons
    public class CartItemReadDto
    {
        [Required]
        public string ItemName { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        public int InCartQuantity { get; set; }

        [Required]
        public string Image { get; set; }

        [Required]
        public float UnitePrice { get; set; }
    }
}
