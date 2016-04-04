using System;
using System.Collections.Generic;
using System.IO;
using Cato.Shared.Models;
using Cato.Shared.Services.TableStorage;
using Cato.Shared.Services.TableStorage.TableEntities;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using Microsoft.WindowsAzure.Storage.Blob;
using Newtonsoft.Json;

namespace Cato.Shared.Services.PersonStore
{
    public class AzurePersonStore : IPersonStore
    {
        
        private const string AZURE_BLOB_NAME = "personimages";

        private readonly CloudStorageAccount _storageAccount;

        public AzurePersonStore(CloudStorageAccount storageAcc)
        {
            _storageAccount = storageAcc;
            Initialise();
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

        public Person GetPerson(string personId)
        {
            CloudTableClient tableClient = _storageAccount.CreateCloudTableClient();
            CloudTable table = tableClient.GetTableReference(TableNames.PERSON_TABLE);
            TableOperation retrieveOp = TableOperation.Retrieve<PersonTableEntity>("person", personId);
            var res = table.Execute(retrieveOp);
            var te = res.Result as PersonTableEntity;
            return Map(te);
        }

        public IEnumerable<Person> GetPeople()
        {
            CloudTableClient tableClient = _storageAccount.CreateCloudTableClient();
            CloudTable table = tableClient.GetTableReference(TableNames.PERSON_TABLE);
            TableQuery<PersonTableEntity> query = new TableQuery<PersonTableEntity>()
                                                        .Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, "person"));
            var result = table.ExecuteQuery(query);

            var list = new List<Person>();
            foreach (PersonTableEntity e in result)
            {
                var p = Map(e);
                list.Add(p);
            }

            return list;
        } 

        /// <summary>
        /// Saves the image blob
        /// </summary>
        /// <param name="imageId"></param>
        /// <param name="imageStream"></param>
        /// <param name="contentType"></param>
        public void SavePersonImage(string imageId, Stream imageStream, string contentType)
        {
            CloudBlobClient blobClient = _storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference(AZURE_BLOB_NAME);
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(imageId);
            blockBlob.Properties.ContentType = contentType;
            blockBlob.UploadFromStream(imageStream);
        }

        /// <summary>
        /// Saves the image blob data to table storage
        /// </summary>
        public void SavePersonImageData(PersonImage personImage)
        {
            var entity = Map(personImage);
            CloudTableClient tableClient = _storageAccount.CreateCloudTableClient();
            CloudTable table = tableClient.GetTableReference(TableNames.PERSON_TABLE);
            TableOperation insertOp = TableOperation.Insert(entity);
            table.Execute(insertOp);
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

        private Person Map(PersonTableEntity pte)
        {
            var p = new Person()
            {
                PersonId = pte.PersonId,
                Name = pte.Name,
                FriendlyName = pte.FriendlyName,
                FaceApiPersonId = pte.FaceApiPersonId
            };
            return p;
        }

        private PersonImageTableEntity Map(PersonImage personImage)
        {
            var e = new PersonImageTableEntity()
            {
                PartitionKey = personImage.PersonId,
                RowKey = personImage.ImageId,
                PersonId = personImage.PersonId,
                ImageId = personImage.ImageId,
                FaceDetection = personImage.FaceDetection,
                FaceApiFaceId = personImage.FaceApiFaceId
            };
            return e;
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


        private void Initialise()
        {
            CloudTableClient tableClient = _storageAccount.CreateCloudTableClient();
            CloudTable table = tableClient.GetTableReference(TableNames.PERSON_TABLE);

            table.CreateIfNotExists();
        }


    }


}
