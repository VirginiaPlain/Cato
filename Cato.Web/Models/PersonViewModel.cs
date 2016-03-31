using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cato.Web.Models
{
    public class PersonViewModel
    {

        public string PersonId { get; set; }
        public string Name { get; set; }
        public string FriendlyName { get; set; }
        public string FaceApiPersonId { get; set; }

    }
}