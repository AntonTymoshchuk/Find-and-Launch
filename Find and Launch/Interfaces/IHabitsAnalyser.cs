using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Find_and_Launch.Interfaces
{
    public interface IHabitsAnalyser
    {
        void LoadData();
        void SortByHabitsAnalysis(object data);
        void AddToHabitsAnalyser(object data);
        void SaveData();
    }
}
