﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AngularAcessoriesBack.Dtos
{
    public class BannerReadDto
    {
        [Required]
        public int BannerId { get; set; }
        [Required]
        public string Image { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Header { get; set; }
        [Required]
        public string Body { get; set; }

    }
}
