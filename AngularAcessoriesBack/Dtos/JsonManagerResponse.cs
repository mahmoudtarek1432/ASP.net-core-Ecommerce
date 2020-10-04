using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AngularAcessoriesBack.Dtos
{
    public class JsonManagerResponse <T>
    {
        [Required]
        public bool IsSuccessful { get; set; }

        [Required]
        public string Message { get; set; }

        public T ResponseObject { get; set; }
    }
}
