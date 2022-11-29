using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tsi.Vendors.DataSync.Application.History
{
    public interface IHistoryService
    {
        HistoryDto GetHistory();
    }
}
