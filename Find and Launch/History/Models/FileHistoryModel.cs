using Find_and_Launch.Abstract;
using Find_and_Launch.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Find_and_Launch.History.Models
{
    public class FileHistoryModel : IModelEntity
    {
        public string FullName { get; set; }

        public FileHistoryModel() { }

        public FileHistoryModel(string fileHistoryModelEntity)
        {
            FullName = fileHistoryModelEntity;
        }

        public string GetModelEntity()
        {
            return FullName;
        }
    }
}
