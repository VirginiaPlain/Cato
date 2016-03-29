using Microsoft.WindowsAzure.Storage.Table;


namespace Cato.Shared.Services.TableStorage.TableEntities
{
    
    public class PersonTableEntity : TableEntity
    {
        public string PersonId { get; set; }
        public string Name { get; set; }
        public string FriendlyName { get; set; }
        public string FaceApiPersonId { get; set; }
    }
}
