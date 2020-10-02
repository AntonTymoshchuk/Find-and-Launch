using Find_and_Launch.Abstract;
using Find_and_Launch.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Find_and_Launch.History.Models
{
    public class SettingsHistoryModel : IModelEntity
    {
        public string Name { get; set; }

        public SettingsHistoryModel() { }

        public SettingsHistoryModel(string settingsHistoryModelEntity)
        {
            Name = settingsHistoryModelEntity;
        }

        public string GetModelEntity()
        {
            return Name;
        }
    }
}
