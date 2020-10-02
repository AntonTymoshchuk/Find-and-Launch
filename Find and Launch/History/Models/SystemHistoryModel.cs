using Find_and_Launch.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Find_and_Launch.History.Models
{
    public class SystemHistoryModel : IModelEntity
    {
        public string Command { get; set; }
        public string ApplicationName { get; set; }

        public SystemHistoryModel() { }

        public SystemHistoryModel(string systemHistoryModelEntity)
        {
            string[] systemHistotyModelProperties = systemHistoryModelEntity.Split('|');
            Command = systemHistotyModelProperties[0];
            ApplicationName = systemHistotyModelProperties[1];
        }

        public string GetModelEntity()
        {
            return $"{Command}|{ApplicationName}";
        }
    }
}
