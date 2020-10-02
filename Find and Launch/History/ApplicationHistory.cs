using Find_and_Launch.History.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Find_and_Launch.History
{
    public class ApplicationHistory
    {
        public List<ApplicationHistoryModel> ApplicationHistoryModels { get; private set; }

        public ApplicationHistory()
        {
            ApplicationHistoryModels = new List<ApplicationHistoryModel>();
        }

        public void Save()
        {

        }
    }
}
