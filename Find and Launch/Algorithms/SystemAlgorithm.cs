using Find_and_Launch.HabitsAnalysing;
using Find_and_Launch.Interfaces;
using Find_and_Launch.Selectors;
using Find_and_Launch.Settings;
using Find_and_Launch.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Find_and_Launch.Algorithms
{
    public class SystemAlgorithm : IAlgorithm
    {
        private readonly SystemListViewModel systemListViewModel;

        public SystemAlgorithm()
        {
            systemListViewModel = (Application.Current.MainWindow as MainWindow).SystemListViewModel;
        }

        public void Start(string request)
        {
            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                AddSystemService(new Models.System(request, "Command Prompt"));
                AddSystemService(new Models.System(request, "Windows PowerShell"));
                AddSystemService(new Models.System(request, "Launch"));
            }));
            if (GlobalSettings.UseHabitsAnalysis == true)
                GlobalHabitsAnalyser.SystemHabitsAnalyser.SortByHabitsAnalysis(systemListViewModel.Systems);
        }

        private void AddSystemService(Models.System system)
        {
            systemListViewModel.AddSystemService(system);
        }
    }
}
