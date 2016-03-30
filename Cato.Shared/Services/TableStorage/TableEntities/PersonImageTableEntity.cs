using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Table;

namespace Cato.Shared.Services.TableStorage.TableEntities
{
    public class PersonImageTableEntity : TableEntity
    {
        public string PersonId { get; set; }
        public string ImageId { get; set; }
        public string FaceDetection { get; set; }
        public string FaceApiFaceId { get; set; }
    }
}
