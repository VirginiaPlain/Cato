

namespace Cato.Shared.Models
{
    public class PersonImage
    {
        public string ImageId { get; set; }
        public int FaceCount { get; set; }
        public string FaceData { get; set; }
        public string FaceId { get; set; }

        public PersonImage()
        {
            ImageId = string.Empty;
            FaceCount = 0;
            FaceData = string.Empty;
            FaceId = string.Empty;
        }
    }
}
