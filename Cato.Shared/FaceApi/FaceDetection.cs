using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cato.Shared.FaceApi
{
    public class FaceDetection
    {
        public string FaceId { get; set; }
        public int Top { get; set; }
        public int Left { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        //
        public string IdentifiedFaceId { get; set; }
        public string IdentifiedFaceName { get; set; }

        public FaceDetection()
        {
            FaceId = string.Empty;
            Top = 0;
            Left = 0;
            Width = 0;
            Height = 0;
            IdentifiedFaceId = string.Empty;
            IdentifiedFaceName = string.Empty;
        }

    }
}
