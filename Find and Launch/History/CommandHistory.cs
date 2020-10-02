using Find_and_Launch.History.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Find_and_Launch.History
{
    public class CommandHistory
    {
        public List<CommandHistoryModel> CommandHistoryModels { get; private set; }

        public CommandHistory()
        {
            CommandHistoryModels = new List<CommandHistoryModel>();
        }

        public void Save()
        {

        }
    }
}
