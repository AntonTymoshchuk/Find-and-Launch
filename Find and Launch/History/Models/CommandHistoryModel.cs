using Find_and_Launch.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Find_and_Launch.History.Models
{
    public class CommandHistoryModel : IModelEntity
    {
        public string Name { get; set; }

        public CommandHistoryModel() { }

        public CommandHistoryModel(string commandHistoryModelEntity)
        {
            Name = commandHistoryModelEntity;
        }

        public string GetModelEntity()
        {
            return Name;
        }
    }
}
