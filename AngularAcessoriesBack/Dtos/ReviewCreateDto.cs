using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AngularAcessoriesBack.Dtos
{
    public class ReviewCreateDto
    {
        [Required]
        public int ProductId { get; set; }//forgin key

        [Required]
        public int Rating { get; set; }

        [Required]
        public string Comment { get; set; }

        [Required]
        public string Date { get; set; }
    }
}
