using System;
using System.IO;
using Cato.Shared.Models;
using Cato.Shared.Services.TableStorage;
using Cato.Shared.Services.TableStorage.TableEntities;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using Microsoft.WindowsAzure.Storage.Blob;

namespace Cato.Shared.Services.PersonStore
{
    public class AzurePersonStore : IPersonStore
    {
        private const string AZURE_BLOB_NAME = "personimages";

        private readonly CloudStorageAccount _storageAccount;

        public AzurePersonStore(CloudStorageAccount storageAcc)
        {
            _storageAccount = storageAcc;
        }

        public void SavePerson(Person person)
        {
            // Add person
            var entity = Map(person);
            CloudTableClient tableClient = _storageAccount.CreateCloudTableClient();
            CloudTable table = tableClient.GetTableReference(TableNames.PERSON_TABLE);
            TableOperation insertOp = TableOperation.Insert(entity);
            table.Execute(insertOp);
            // Add FaceApiPerson for lookup
            var lookup = MapLookup(person);
            TableOperation insertOp2 = TableOperation.Insert(lookup);
            table.Execute(insertOp2);
        }

        public void SavePersonImage(string imageId, Stream imageStream, string contentType)
        {
            CloudBlobClient blobClient = _storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference(AZURE_BLOB_NAME);
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(imageId);
            blockBlob.Properties.ContentType = contentType;
            blockBlob.UploadFromStream(imageStream);
        }

        private PersonTableEntity Map(Person person)
        {
            var pte = new PersonTableEntity()
            {
                PartitionKey = "person",
                RowKey = person.PersonId,
                Name = person.Name,
                FriendlyName = person.FriendlyName,
                PersonId = person.PersonId,
                FaceApiPersonId = person.FaceApiPersonId
            };
            return pte;
        }

        private FaceApiPersonTableEntity MapLookup(Person person)
        {
            var e = new FaceApiPersonTableEntity
            {
                PartitionKey = "apiperson",
                RowKey = person.FaceApiPersonId,
                FaceApiPersonId = person.FaceApiPersonId,
                PersonId = person.PersonId
            };
            return e;
        }

    }


}
