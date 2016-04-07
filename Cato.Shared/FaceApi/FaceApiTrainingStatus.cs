

namespace Cato.Shared.FaceApi
{
    public class FaceApiTrainingStatus
    {
        public string Status { get; set; }  // notstarted, running, succeeded, failed

        public string LastAction { get; set; }

        public string Message { get; set; }

        public FaceApiTrainingStatus()
        {
            Status = string.Empty;
            LastAction = string.Empty;
            Message = string.Empty;
        }

    }
}
