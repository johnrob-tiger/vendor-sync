using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsi.Vendors.DataSync.Domain.Interfaces;

namespace Tsi.Vendors.DataSync.Domain.Entities.MailBoxes
{
    public interface IMailBoxRespository : IAsyncRepository<MailBox>
    {
    }
}
