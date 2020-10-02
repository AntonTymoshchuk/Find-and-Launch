using Find_and_Launch.HabitsAnalysing;
using Find_and_Launch.Interfaces;
using Find_and_Launch.Settings;
using Find_and_Launch.Validators;
using Find_and_Launch.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Windows.ApplicationModel;
using Windows.Management.Deployment;

namespace Find_and_Launch.Algorithms
{
    public class MicrosoftStoreAppsAlgorithm : IAlgorithm
    {
        private readonly MicrosoftStoreAppsListViewModel microsoftStoreAppsListViewModel;
        private readonly MicrosoftStoreAppValidator microsoftStoreAppValidator;

        public MicrosoftStoreAppsAlgorithm()
        {
            microsoftStoreAppsListViewModel = (Application.Current.MainWindow as MainWindow).MicrosoftStoreAppsListViewModel;
            microsoftStoreAppValidator = new MicrosoftStoreAppValidator();
        }

        public void Start(string request)
        {
            PackageManager packageManager = new PackageManager();
            List<Package> packages = packageManager.FindPackagesForUser("").ToList();
            foreach (Package package in packages)
            {
                bool comparedWithRequest = microsoftStoreAppValidator.CompareWithRequest(request, package.Id.FullName);
                bool allowedBySettings = microsoftStoreAppValidator.CheckWithSettings(package.Id.FullName);
                bool validated = microsoftStoreAppValidator.Validate(request, package.Id.FullName);
                if (comparedWithRequest == true && allowedBySettings == true && validated == true)
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    { microsoftStoreAppsListViewModel.AddMicrosoftStoreApp(new Models.MicrosoftStoreApplication(request, package.Id.FullName)); });
                }
            }
            if (GlobalSettings.UseHabitsAnalysis == true && microsoftStoreAppsListViewModel.IsEmpty == false)
                GlobalHabitsAnalyser.MicrosoftStoreAppHabitsAnalyser.SortByHabitsAnalysis(microsoftStoreAppsListViewModel.MicrosoftStoreApplications);
        }
    }
}
