using Cato.Shared.Models;
using System.IO;

namespace Cato.Shared.Services.PersonStore
{
    public interface IPersonStore
    {

        void SavePerson(Person person);

        void SavePersonImage(string imageId, Stream imageStream, string contentType);

    }
}
