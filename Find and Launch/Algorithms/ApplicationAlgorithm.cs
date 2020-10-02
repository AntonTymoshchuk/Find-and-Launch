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
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Find_and_Launch.Algorithms
{
    public class ApplicationAlgorithm : IAlgorithm
    {
        private readonly ApplicationListViewModel applicationListViewModel;
        private readonly ApplicationValidator applicationValidator;

        public ApplicationAlgorithm()
        {
            applicationListViewModel = (Application.Current.MainWindow as MainWindow).ApplicationListViewModel;
            applicationValidator = new ApplicationValidator();
        }

        public void Start(string request)
        {
            string commonStartMenu = Environment.GetFolderPath(Environment.SpecialFolder.CommonStartMenu);
            string userStartMenu = Environment.GetFolderPath(Environment.SpecialFolder.StartMenu);
            GetDirectoryApplications(new DirectoryInfo(commonStartMenu), request);
            GetDirectoryApplications(new DirectoryInfo(userStartMenu), request);
            if (GlobalSettings.UseHabitsAnalysis == true && applicationListViewModel.IsEmpty == false)
                GlobalHabitsAnalyser.ApplicationHabitsAnalyser.SortByHabitsAnalysis(applicationListViewModel.Applications);
        }

        private void GetDirectoryApplications(DirectoryInfo parentDirectoryInfo, string request)
        {
            FileInfo[] fileInfos = parentDirectoryInfo.GetFiles();
            foreach (FileInfo fileInfo in fileInfos)
            {
                bool comparedWithRequest = applicationValidator.CompareWithRequest(request, fileInfo);
                bool allowedBySettings = applicationValidator.CheckWithSettings(fileInfo);
                bool validated = applicationValidator.Validate(request, fileInfo);
                if (comparedWithRequest == true && allowedBySettings == true && validated == true)
                {
                    Application.Current.Dispatcher.Invoke(new Action(() =>
                    { applicationListViewModel.AddApplication(new Models.Application(request, fileInfo)); }));
                }
            }
            DirectoryInfo[] directoryInfos = parentDirectoryInfo.GetDirectories();
            foreach (DirectoryInfo directoryInfo in directoryInfos)
            {
                try { GetDirectoryApplications(directoryInfo, request); }
                catch { continue; }
            }
        }
    }
}
