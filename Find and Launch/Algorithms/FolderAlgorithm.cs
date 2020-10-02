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
    public class FolderAlgorithm : IAlgorithm
    {
        private readonly FolderListViewModel folderListViewModel;
        private readonly FolderValidator folderValidator;

        public FolderAlgorithm()
        {
            folderListViewModel = (Application.Current.MainWindow as MainWindow).FolderListViewModel;
            folderValidator = new FolderValidator();
        }

        public void Start(string request)
        {
            foreach (string includedFolderPath in GlobalSettings.IncludedFoldersPaths)
                GetDirectoryFolders(new DirectoryInfo(includedFolderPath), request);
            if (GlobalSettings.UseHabitsAnalysis == true && folderListViewModel.IsEmpty == false)
                GlobalHabitsAnalyser.FolderHabitsAnalyser.SortByHabitsAnalysis(folderListViewModel.Folders);
        }

        private void GetDirectoryFolders(DirectoryInfo parentDirectoryInfo, string request)
        {
            DirectoryInfo[] directoryInfos = parentDirectoryInfo.GetDirectories();
            foreach (DirectoryInfo directoryInfo in directoryInfos)
            {
                bool comparedWithRequest = folderValidator.CompareWithRequest(request, directoryInfo);
                bool allowedBySettings = folderValidator.CheckWithSettings(directoryInfo);
                bool validated = folderValidator.Validate(request, directoryInfo);
                if (comparedWithRequest == true && allowedBySettings == true && validated == true)
                {
                    Application.Current.Dispatcher.Invoke(new Action(() =>
                    { folderListViewModel.AddFolder(new Models.Folder(request, directoryInfo)); }));
                }
                try { GetDirectoryFolders(directoryInfo, request); }
                catch { continue; }
            }
        }
    }
}
