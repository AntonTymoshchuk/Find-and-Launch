using Find_and_Launch.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Find_and_Launch.History.Models
{
    public class ApplicationHistoryModel : IModelEntity
    {
        public string FullLnkName { get; set; }

        public ApplicationHistoryModel() { }

        public ApplicationHistoryModel(string applicationHistoryModelEntity)
        {
            FullLnkName = applicationHistoryModelEntity;
        }

        public string GetModelEntity()
        {
            return FullLnkName;
        }
    }
}
