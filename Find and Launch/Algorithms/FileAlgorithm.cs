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
    public class FileAlgorithm : IAlgorithm
    {
        private readonly FileListViewModel fileListViewModel;
        private readonly FileValidator fileValidator;
        private readonly FolderValidator folderValidator;

        public FileAlgorithm()
        {
            fileListViewModel = (Application.Current.MainWindow as MainWindow).FileListViewModel;
            fileValidator = new FileValidator();
            folderValidator = new FolderValidator();
        }

        public void Start(string request)
        {
            foreach (string includedFolderPath in GlobalSettings.IncludedFoldersPaths)
                GetDirectoryFiles(new DirectoryInfo(includedFolderPath), request);
            if (GlobalSettings.UseHabitsAnalysis == true && fileListViewModel.IsEmpty == false)
                GlobalHabitsAnalyser.FileHabitsAnalyser.SortByHabitsAnalysis(fileListViewModel.Files);
        }

        private void GetDirectoryFiles(DirectoryInfo parentDirectoryInfo, string request)
        {
            FileInfo[] fileInfos = parentDirectoryInfo.GetFiles();
            foreach (FileInfo fileInfo in fileInfos)
            {
                bool comparedWithRequest = fileValidator.CompareWithRequest(request, fileInfo);
                bool allowedBySettings = fileValidator.CheckWithSettings(fileInfo);
                bool validated = fileValidator.Validate(request, fileInfo);
                if (comparedWithRequest == true && allowedBySettings == true && validated == true)
                {
                    Application.Current.Dispatcher.Invoke(new Action(() =>
                    { fileListViewModel.AddFile(new Models.File(request, fileInfo)); }));
                }
            }
            DirectoryInfo[] directoryInfos = parentDirectoryInfo.GetDirectories();
            foreach (DirectoryInfo directoryInfo in directoryInfos)
            {
                bool allowedBySettings = folderValidator.CheckWithSettings(directoryInfo);
                bool validated = folderValidator.Validate(request, directoryInfo);
                if (allowedBySettings == true && validated == true)
                    GetDirectoryFiles(directoryInfo, request);
            }
        }
    }
}
