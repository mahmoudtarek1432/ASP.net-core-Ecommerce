﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AngularAcessoriesBack.Dtos
{
    public class MinifiedProductReadDto
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
        public string[] ImagePathsArr { get; set; }
    }
}
