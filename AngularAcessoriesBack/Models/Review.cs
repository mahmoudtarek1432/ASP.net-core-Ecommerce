using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AngularAcessoriesBack.Models
{
    public class Review
    {
        [Required]
        [ForeignKey("Id")]
        public int ProductId { get; set; }//forgin key

        [Required]
        [ForeignKey("User")]
        public string UserId { get; set; }
        public CustomIdentityUser User { get; set; }

        [Key]
        public int ReviewId { get; set; }//primary key

        [Required]
        public string Name { get; set; }
        [Required]
        public int Rating { get; set; }
        [Required]
        public string Comment { get; set; }
        [Required]
        public string Date { get; set; }
    }
}
