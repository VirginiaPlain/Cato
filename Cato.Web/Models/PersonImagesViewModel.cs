using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cato.Web.Models
{
    public class PersonImagesViewModel
    {

        public string PersonId { get; set; }
        public string Name { get; set; }
        public string FriendlyName { get; set; }

        public List<PersonImageViewModel> Images { get; set; } 

        public PersonImagesViewModel()
        {
            PersonId = string.Empty;
            Name = string.Empty;
            FriendlyName = string.Empty;
            Images = new List<PersonImageViewModel>();
        }
    }
}