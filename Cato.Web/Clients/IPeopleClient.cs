using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cato.Web.Models;

namespace Cato.Web.Clients
{
    public interface IPeopleClient
    {

        Task<IEnumerable<PersonViewModel>> GetPeopleAsync();

    }
}
