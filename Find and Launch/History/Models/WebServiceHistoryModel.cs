using Find_and_Launch.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Find_and_Launch.History.Models
{
    public class WebServiceHistoryModel : IModelEntity
    {
        public string Request { get; set; }
        public string ServiceName { get; set; }

        public WebServiceHistoryModel() { }

        public WebServiceHistoryModel(string webServiceHistoryModelEntity)
        {
            string[] webServiceHistoryModelProperties = webServiceHistoryModelEntity.Split('|');
            Request = webServiceHistoryModelProperties[0];
            ServiceName = webServiceHistoryModelProperties[1];
        }

        public string GetModelEntity()
        {
            return $"{Request}|{ServiceName}";
        }
    }
}
