

using System.Collections.Generic;

namespace Cato.Shared.FaceApi
{
    public class FaceDetectionResult
    {

        public List<FaceDetection> Faces { get; set; }

        public int FaceCount
        {
            get { return Faces.Count; }
        }

        public FaceDetectionResult()
        {
            Faces = new List<FaceDetection>();
        }
    }
}
