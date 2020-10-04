﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AngularAcessoriesBack.Dtos
{
    public class AddProductToCart
    {
        [Required]
        public int ProductId { get; set; }
        
        [Required]
        public int InCartQuantity { get; set; }
    }
}
