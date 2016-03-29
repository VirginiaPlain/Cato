

namespace Cato.Shared.Models
{
    public class Person
    {

        public string PersonId { get; set; }
        public string Name { get; set; }
        public string FriendlyName { get; set; }
        public string FaceApiPersonId { get; set; }

        public Person()
        {
            PersonId = string.Empty;
            Name = string.Empty;
            FriendlyName = string.Empty;
            FaceApiPersonId = string.Empty;
        }

    }
}
