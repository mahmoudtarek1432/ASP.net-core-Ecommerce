using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AngularAcessoriesBack.Models
{
    public class OrderDetails
    {
        [Key]
        public int id { get; set; }

        [ForeignKey("order")]
        [Required]
        public int OrderId { get; set; }
        public Orders Order { get; set; }

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
        public string PhoneNumber { get; set; }

        [Required]
        public float ShippingFees { get; set; }

        public string DatePlaced { get; set; }

        public string DateConfirmed { get; set; }

        public string DateShipped { get; set; }

        public string DateDelivered { get; set; }
    }
}
