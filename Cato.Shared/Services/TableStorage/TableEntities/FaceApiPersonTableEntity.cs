using Microsoft.WindowsAzure.Storage.Table;



namespace Cato.Shared.Services.TableStorage.TableEntities
{
    
    public class FaceApiPersonTableEntity : TableEntity
    {
        public string FaceApiPersonId { get; set; }
        public string PersonId { get; set; }
    }
}
