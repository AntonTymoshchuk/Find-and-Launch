using Find_and_Launch.Abstract;
using Find_and_Launch.HabitsAnalysing;
using Find_and_Launch.History;
using Find_and_Launch.Interfaces;
using Find_and_Launch.Selectors;
using Find_and_Launch.Settings;
using Find_and_Launch.Validators;
using Find_and_Launch.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Find_and_Launch.Algorithms
{
    public class SettingsAlgorithm : IAlgorithm
    {
        private readonly SettingsListViewModel settingsListViewModel;
        private readonly SettingsValidator settingsValidator;

        private List<string> SettingsNames { get; set; }

        public SettingsAlgorithm()
        {
            settingsListViewModel = (Application.Current.MainWindow as MainWindow).SettingsListViewModel;
            settingsValidator = new SettingsValidator();

            SettingsNames = new List<string>()
            {
                "Settings", "Display", "Night light settings"
            };
        }

        public void Start(string request)
        {
            foreach (string settingsName in SettingsNames)
            {
                bool comparedWithRequest = settingsValidator.CompareWithRequest(request, settingsName);
                if (comparedWithRequest == true)
                {
                    Application.Current.Dispatcher.Invoke(new Action(() =>
                    { settingsListViewModel.AddSettings(new Models.Settings(request, settingsName)); }));
                }
            }
            if (GlobalSettings.UseHabitsAnalysis == true && settingsListViewModel.IsEmpty == false)
                GlobalHabitsAnalyser.SettingsHabitsAnalyser.SortByHabitsAnalysis(settingsListViewModel.Settings);
        }
    }
}
