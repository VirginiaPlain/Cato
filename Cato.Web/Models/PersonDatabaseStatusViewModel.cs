using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cato.Web.Models
{
    public class PersonDatabaseStatusViewModel
    {

        public string Status { get; set; }  // notstarted, running, succeeded, failed

        public string StatusColour { get; set; }

        public string LastAction { get; set; }

        public string Message { get; set; }

        public PersonDatabaseStatusViewModel()
        {
            Status = string.Empty;
            StatusColour = "black";
            LastAction = string.Empty;
            Message = string.Empty;
        }

    }
}