using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AngularAcessoriesBack.Dtos
{
    public class OrderDetailsDto
    {
        [Required]
        public string Name { get; set; } //non relational json style

        [Required]
        public string Address { get; set; }

        [Required]
        public string AdditionalInfo { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string Region { get; set; }

        [Required]
        public float TotalItemPrice { get; set; }

        [Required]
        public float ShippingFees { get; set; }

        [Required]
        public string DatePlaced { get; set; }

        [Required]
        public string PhoneNumber { get; set; }


        public string Status { get; set; }
    }
}
