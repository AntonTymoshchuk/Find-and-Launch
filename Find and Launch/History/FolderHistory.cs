using Find_and_Launch.History.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Find_and_Launch.History
{
    public class FolderHistory
    {
        public List<FolderHistoryModel> FolderHistoryModels { get; private set; }

        public FolderHistory()
        {
            FolderHistoryModels = new List<FolderHistoryModel>();
        }

        public void Save()
        {

        }
    }
}
