using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cato.Web.Models
{
    public class PersonImageViewModel
    {
        public string PersonId { get; set; }
        public string ImageId { get; set; }
        public string ImageUrl { get; set; }
        public string FaceDetection { get; set; }    //FaceDetection
        public string FaceApiFaceId { get; set; }

        public PersonImageViewModel()
        {
            PersonId = string.Empty;
            ImageId = string.Empty;
            ImageUrl = string.Empty;
            FaceDetection = string.Empty;
            FaceApiFaceId = string.Empty;
        }

    }
}