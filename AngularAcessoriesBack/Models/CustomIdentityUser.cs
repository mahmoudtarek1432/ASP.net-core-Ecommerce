using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using Newtonsoft.Json;

namespace AngularAcessoriesBack.Models
{
    public class CustomIdentityUser : IdentityUser
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public string Address { get; set; }

        public string AdditionalInfo { get; set; }

        public string City { get; set; }

        public string Region { get; set; }

        public string SavedItems { get; set; } //contain id of products saved

        public string RecentlyViewed { get; set; } //contain id of 10 last viewed products

        [NotMapped]
        public string[] SavedItemsArr
        {
            get
            {
                if (SavedItems != null)
                {
                    return SavedItems.Split(";");
                }
                return new string [0];
            }
            set
            {
                var _data = value;
                if(value.Length > 0)
                {
                    SavedItems = (_data[0] == "") ? null : string.Join(";", _data.Select(d => d.ToString()).ToArray());
                    return;
                }
                SavedItems = null;
                
                
            }
        }

        [NotMapped]
        public string[] RecentlyViewedArr
        {
            get
            {
                if(RecentlyViewed != null)
                {
                    return RecentlyViewed.Split(";");
                }
                return new string[0]; 
            }
            set
            {
                var _data = value;
                if(_data.Length > 10)
                {
                    Queue<string> _list = new Queue<string>(_data.ToList());
                    _list.Dequeue();
                    RecentlyViewed = string.Join(";", _list.Select(d => d.ToString()).ToArray());
                }
                else if(_data.Length <= 0) { RecentlyViewed = null; }
                RecentlyViewed = string.Join(";", _data.Select(d => d.ToString()).ToArray());
            }
        }
    }
}
