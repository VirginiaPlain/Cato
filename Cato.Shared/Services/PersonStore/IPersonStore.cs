using System.Collections.Generic;
using Cato.Shared.Models;
using System.IO;

namespace Cato.Shared.Services.PersonStore
{
    public interface IPersonStore
    {

        void SavePerson(Person person);

        Person GetPerson(string personId);

        IEnumerable<Person> GetPeople();

        /// <summary>
        /// Saves the blob
        /// </summary>
        /// <param name="imageId"></param>
        /// <param name="imageStream"></param>
        /// <param name="contentType"></param>
        void SavePersonImage(string imageId, Stream imageStream, string contentType);
        /// <summary>
        /// Saves the image information to table
        /// </summary>
        /// <param name="personImage"></param>
        void SavePersonImageData(PersonImage personImage);
    }
}
