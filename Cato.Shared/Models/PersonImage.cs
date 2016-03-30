

namespace Cato.Shared.Models
{
    public class PersonImage
    {
        public string PersonId { get; set; }
        public string ImageId { get; set; }
        public string FaceDetection { get; set; }    //FaceDetection
        public string FaceApiFaceId { get; set; }

        public PersonImage()
        {
            PersonId = string.Empty;
            ImageId = string.Empty;
            FaceDetection = string.Empty;
            FaceApiFaceId = string.Empty;
        }
    }
}
