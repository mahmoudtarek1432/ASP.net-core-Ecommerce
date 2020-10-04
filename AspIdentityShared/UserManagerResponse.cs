using System;
using System.Collections.Generic;
using System.Text;

namespace AspIdentity.Shared
{
    public class UserManagerResponse
    {
        public bool IsSuccessful { get; set; }

        public String Message { get; set; }

        public IEnumerable<string> Errors { get; set; }

        public DateTime? ExpireDate {get; set;}
    }
}
