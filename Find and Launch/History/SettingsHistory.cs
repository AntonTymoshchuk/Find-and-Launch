using Find_and_Launch.History.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Find_and_Launch.History
{
    public class SettingsHistory
    {
        public List<SettingsHistoryModel> SettingsHistoryModels { get; private set; }

        public SettingsHistory()
        {
            SettingsHistoryModels = new List<SettingsHistoryModel>();
        }

        public void Save()
        {

        }
    }
}
