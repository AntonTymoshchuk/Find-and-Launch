using Find_and_Launch.Abstract;
using Find_and_Launch.HabitsAnalysing;
using Find_and_Launch.Interfaces;
using Find_and_Launch.Settings;
using Find_and_Launch.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Find_and_Launch.Algorithms
{
    public class GlobalAlgorithm : IAlgorithm 
    {
        private readonly GlobalViewModel globalViewModel;

        private readonly MathExpressionListViewModel mathExpressionListViewModel;
        private readonly FileListViewModel fileListViewModel;
        private readonly FolderListViewModel folderListViewModel;
        private readonly MicrosoftStoreAppsAlgorithm microsoftStoreAppsAlgorithm;
        private readonly ApplicationListViewModel applicationListViewModel;
        private readonly SettingsListViewModel settingsListViewModel;
        private readonly SystemListViewModel systemListViewModel;
        private readonly WebServiceListViewModel webServiceListViewModel;

        private readonly MathExpressionAlgorithm mathExpressionAlgorithm;
        private readonly FileAlgorithm fileAlgorithm;
        private readonly FolderAlgorithm folderAlgorithm;
        private readonly MicrosoftStoreAppsListViewModel microsoftStoreAppsListViewModel;
        private readonly ApplicationAlgorithm applicationAlgorithm;
        private readonly SettingsAlgorithm settingsAlgorithm;
        private readonly SystemAlgorithm systemAlgorithm;
        private readonly WebServiceAlgorithm webServiceAlgorithm;

        private Thread algorithmThread;

        public GlobalAlgorithm()
        {
            globalViewModel = (Application.Current.MainWindow as MainWindow).GlobalViewModel;

            mathExpressionListViewModel = (Application.Current.MainWindow as MainWindow).MathExpressionListViewModel;
            fileListViewModel = (Application.Current.MainWindow as MainWindow).FileListViewModel;
            folderListViewModel = (Application.Current.MainWindow as MainWindow).FolderListViewModel;
            microsoftStoreAppsListViewModel = (Application.Current.MainWindow as MainWindow).MicrosoftStoreAppsListViewModel;
            applicationListViewModel = (Application.Current.MainWindow as MainWindow).ApplicationListViewModel;
            settingsListViewModel = (Application.Current.MainWindow as MainWindow).SettingsListViewModel;
            systemListViewModel = (Application.Current.MainWindow as MainWindow).SystemListViewModel;
            webServiceListViewModel = (Application.Current.MainWindow as MainWindow).WebServiceListViewModel;

            mathExpressionAlgorithm = new MathExpressionAlgorithm();
            fileAlgorithm = new FileAlgorithm();
            folderAlgorithm = new FolderAlgorithm();
            microsoftStoreAppsAlgorithm = new MicrosoftStoreAppsAlgorithm();
            applicationAlgorithm = new ApplicationAlgorithm();
            settingsAlgorithm = new SettingsAlgorithm();
            systemAlgorithm = new SystemAlgorithm();
            webServiceAlgorithm = new WebServiceAlgorithm();
        }

        public void Clear()
        {
            if (algorithmThread != null && algorithmThread.IsAlive == true)
                algorithmThread.Abort();

            mathExpressionListViewModel.Clear();
            fileListViewModel.Clear();
            folderListViewModel.Clear();
            microsoftStoreAppsListViewModel.Clear();
            applicationListViewModel.Clear();
            settingsListViewModel.Clear();
            systemListViewModel.Clear();
            webServiceListViewModel.Clear();

            globalViewModel.Update();
        }

        public void Start(string request)
        {
            if (algorithmThread != null && algorithmThread.IsAlive == true)
                algorithmThread.Abort();
            algorithmThread = new Thread(StartAlgorithms)
            { IsBackground = true };
            algorithmThread.Start(request);
        }

        private void StartAlgorithms(object obj)
        {
            string request = Convert.ToString(obj);

            int startedAlgorithmsCount = 0, notResultedAlgorithms = 0;
            int minQueueIndex, minQueueValue;

            List<int> startQueue = new List<int>
            {
                GlobalSettings.MathExpressionAlgorithmQueue,
                GlobalSettings.FileAlgorithmQueue,
                GlobalSettings.FolderAlgorithmQueue,
                GlobalSettings.MicrosoftStoreAppsAlgorithmQueue,
                GlobalSettings.ApplicationAlgorithmQueue,
                GlobalSettings.SettingsAlgorithmQueue,
                GlobalSettings.SystemServiceAlgorithmQueue,
                GlobalSettings.GoogleWebServiceAlgorithmQueue
            };

            for (int i = 0; i < 8; i++)
            {
                minQueueIndex = i;
                minQueueValue = startQueue[i];
                for (int j = 0; j < 8; j++)
                {
                    if (i == j)
                        continue;
                    if (minQueueValue > startQueue[j] && startQueue[j] != -1)
                    {
                        minQueueValue = startQueue[j];
                        minQueueIndex = j;
                    }
                }
                startQueue[minQueueIndex] = -1;

                switch (minQueueIndex)
                {
                    case 0:
                        if (GlobalSettings.SearchForMathExpression == true)
                        {
                            mathExpressionAlgorithm.Start(request);
                            startedAlgorithmsCount++;
                            if (mathExpressionListViewModel.IsEmpty == true)
                                notResultedAlgorithms++;
                        }
                        break;
                    case 1:
                        if (GlobalSettings.SearchForFiles == true)
                        {
                            fileAlgorithm.Start(request);
                            startedAlgorithmsCount++;
                            if (fileListViewModel.IsEmpty == true)
                                notResultedAlgorithms++;
                        }
                        break;
                    case 2:
                        if (GlobalSettings.SearchForFolders == true)
                        {
                            folderAlgorithm.Start(request);
                            startedAlgorithmsCount++;
                            if (folderListViewModel.IsEmpty == true)
                                notResultedAlgorithms++;
                        }
                        break;
                    case 3:
                        if (GlobalSettings.SearchForMicrosoftStoreApps == true)
                        {
                            microsoftStoreAppsAlgorithm.Start(request);
                            startedAlgorithmsCount++;
                            if (microsoftStoreAppsListViewModel.IsEmpty == true)
                                notResultedAlgorithms++;
                        }
                        break;
                    case 4:
                        if (GlobalSettings.SearchForApplications == true)
                        {
                            applicationAlgorithm.Start(request);
                            startedAlgorithmsCount++;
                            if (applicationListViewModel.IsEmpty == true)
                                notResultedAlgorithms++;
                        }
                        break;
                    case 5:
                        if (GlobalSettings.SearchForSettings == true)
                        {
                            settingsAlgorithm.Start(request);
                            startedAlgorithmsCount++;
                            if (settingsListViewModel.IsEmpty == true)
                                notResultedAlgorithms++;
                        }
                        break;
                    case 6:
                        if (GlobalSettings.SearchForSystemServices == true)
                        {
                            systemAlgorithm.Start(request);
                            startedAlgorithmsCount++;
                            if (systemListViewModel.IsEmpty == true)
                                notResultedAlgorithms++;
                        }
                        break;
                    case 7:
                        if (GlobalSettings.SearchForGoogleWebServices == true)
                        {
                            webServiceAlgorithm.Start(request);
                            startedAlgorithmsCount++;
                            if (systemListViewModel.IsEmpty == true)
                                notResultedAlgorithms++;
                        }
                        break;
                }
            }

            if (GlobalSettings.UseHabitsAnalysis == true)
                GlobalHabitsAnalyser.AdaptViewByHabitsAnalysis(globalViewModel);

            //if (notResultedAlgorithms == startedAlgorithmsCount)
            //{
            //    globalViewModel.NotFoundReaction();
            //}

            Application.Current.Dispatcher.Invoke(() => { globalViewModel.ShowResult(); });
        }
    }
}
