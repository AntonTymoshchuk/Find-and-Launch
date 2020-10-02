using Find_and_Launch.History.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Find_and_Launch.History
{
    public class FileHistory
    {
        public List<FileHistoryModel> FileHistoryModels { get; private set; }

        public FileHistory()
        {
            FileHistoryModels = new List<FileHistoryModel>();
        }

        public void Save()
        {

        }
    }
}
