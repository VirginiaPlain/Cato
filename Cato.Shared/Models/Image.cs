using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cato.Shared.Models
{
    public class Image
    {

        public string ImageId { get; set; }
        public int FaceCount { get; set; }
        public string FaceDetectionResult { get; set; }


        public Image()
        {
            ImageId = string.Empty;
            FaceCount = 0;
            FaceDetectionResult = string.Empty;
        }

    }
}
