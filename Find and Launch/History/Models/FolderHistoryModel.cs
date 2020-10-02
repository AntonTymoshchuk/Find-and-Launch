using Find_and_Launch.Abstract;
using Find_and_Launch.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Find_and_Launch.History.Models
{
    public class FolderHistoryModel : IModelEntity
    {
        public string FullName { get; set; }

        public FolderHistoryModel() { }

        public FolderHistoryModel(string folderHistoryModelEntity)
        {
            FullName = folderHistoryModelEntity;
        }

        public string GetModelEntity()
        {
            return FullName;
        }
    }
}
