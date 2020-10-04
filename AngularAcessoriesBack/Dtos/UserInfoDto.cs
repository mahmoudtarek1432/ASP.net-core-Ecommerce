using AngularAcessoriesBack.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AngularAcessoriesBack.Dtos
{
    public class UserInfoDto
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string Address { get; set; }

        public string AdditionalInfo { get; set; }

        public string City { get; set; }

        public string Region { get; set; }

        public string Email { get; set; }
        
        public List<CartItem> UserCart { get; set; }

        
    }
}
