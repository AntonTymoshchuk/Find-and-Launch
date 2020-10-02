using Find_and_Launch.Abstract;
using Find_and_Launch.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Find_and_Launch.HabitsAnalysing
{
    public class WebServicesHabitsAnalyser : HabitsAnalyser, IHabitsAnalyser
    {
        private readonly List<string> usedWebServicesInfos;

        public WebServicesHabitsAnalyser() : base()
        {
            usedWebServicesInfos = new List<string>();
            LoadData();
        }

        public void LoadData()
        {
            FileStream fileStream = null;
            try
            {
                string applicationDataPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                string settingsPath = Path.Combine(applicationDataPath, @"Find and Launch\WebServiceHabits.txt");
                fileStream = new FileStream(settingsPath, FileMode.Open, FileAccess.Read, FileShare.Read);
                StreamReader streamReader = new StreamReader(fileStream);
                while (streamReader.EndOfStream != true)
                    usedWebServicesInfos.Add(streamReader.ReadLine());
                streamReader.Close();
                fileStream.Close();
            }
            catch
            { fileStream.Close(); }
        }

        public void SortByHabitsAnalysis(object data)
        {
            DefiningObject = null;
            HabitsAreFound = false;

            List<string> usedWebServicesNames = new List<string>();
            List<string> usedWebServicesRequests = new List<string>();

            foreach (string usedWebServicesInfo in usedWebServicesInfos)
            {
                string[] infoArray = usedWebServicesInfo.Split('|');
                usedWebServicesNames.Add(infoArray[0]);
                usedWebServicesRequests.Add(infoArray[1]);
            }

            ObservableCollection<Models.WebService> webServices = data as ObservableCollection<Models.WebService>;
            List<Models.WebService> firstUsedWebServices = new List<Models.WebService>();
            List<int> firstUsedWebServicesIndexes = new List<int>();
            List<Models.WebService> usedWebServices = new List<Models.WebService>();
            List<Models.WebService> historyWebServices = new List<Models.WebService>();

            for (int i = 0; i < webServices.Count; i++)
            {
                for (int j = 0; j < usedWebServicesRequests.Count; j++)
                {
                    if (//!usedWebServicesRequests[j].ToLower().Equals(webServices[i].OriginalRequest.ToLower()) &&
                        (usedWebServicesRequests[j].ToLower().Contains(webServices[i].OriginalRequest.ToLower()) ||
                        webServices[i].OriginalRequest.ToLower().Contains(usedWebServicesRequests[j].ToLower())) &&
                        webServices[i].Name.Equals(usedWebServicesNames[j]))
                    {
                        firstUsedWebServices.Add(webServices[i]);
                        firstUsedWebServicesIndexes.Add(j);
                        Application.Current.Dispatcher.Invoke(() =>
                        { webServices.Remove(webServices[i]); });
                        i--;
                        break;
                    }
                }
            }

            int currentIndex;
            Models.WebService currentWebService;
            for (int i = 1; i < firstUsedWebServicesIndexes.Count; i++)
            {
                currentIndex = firstUsedWebServicesIndexes[i];
                currentWebService = firstUsedWebServices[i];
                int j = i;
                while (j > 0 && currentIndex < firstUsedWebServicesIndexes[j - 1])
                {
                    firstUsedWebServicesIndexes[j] = firstUsedWebServicesIndexes[j - 1];
                    firstUsedWebServices[j] = firstUsedWebServices[j - 1];
                    j--;
                }
                firstUsedWebServicesIndexes[j] = currentIndex;
                firstUsedWebServices[j] = currentWebService;
            }

            for (int i = 0; i < firstUsedWebServices.Count; i++)
            {
                usedWebServices.Add(firstUsedWebServices[i]);
                for (int j = 0; j < usedWebServicesRequests.Count; j++)
                {
                    if (//!usedWebServicesRequests[j].ToLower().Equals(firstUsedWebServices[i].OriginalRequest.ToLower()) &&
                        (usedWebServicesRequests[j].ToLower().Contains(firstUsedWebServices[i].OriginalRequest.ToLower()) ||
                        firstUsedWebServices[i].OriginalRequest.ToLower().Contains(usedWebServicesRequests[j].ToLower())) &&
                        firstUsedWebServices[i].Name.Equals(usedWebServicesNames[j]))
                    {
                        //Application.Current.Dispatcher.Invoke(() =>
                        //{ usedWebServices.Add(new Models.WebService(usedWebServicesRequests[j], firstUsedWebServices[i].Name)); });
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            Models.WebService webService = new Models.WebService(usedWebServicesRequests[j], firstUsedWebServices[i].Name);
                            usedWebServices.Add(webService);
                            historyWebServices.Add(webService);
                        });
                    }
                }
            }

            for (int i = 0; i < usedWebServices.Count; i++)
            {
                for (int j = 0; j < usedWebServices.Count; j++)
                {
                    if (i == j)
                        continue;
                    if (usedWebServices[i].Name.Equals(usedWebServices[j].Name))
                    {
                        if (usedWebServices[i].OriginalRequest.ToLower().Equals(usedWebServices[j].OriginalRequest.ToLower()))
                        {
                            usedWebServices.Remove(usedWebServices[j]);
                            if (i > 0)
                                i--;
                            j--;
                        }
                    }
                }
            }

            for (int i = 0; i < historyWebServices.Count; i++)
            {
                for (int j = 0; j < historyWebServices.Count; j++)
                {
                    if (i == j)
                        continue;
                    if (historyWebServices[i].Name.Equals(historyWebServices[j].Name))
                    {
                        if (historyWebServices[i].OriginalRequest.ToLower().Equals(historyWebServices[j].OriginalRequest.ToLower()))
                        {
                            historyWebServices.Remove(historyWebServices[j]);
                            if (i > 0)
                                i--;
                            j--;
                        }
                    }
                }
            }

            for (int i = usedWebServices.Count - 1; i >= 0; i--)
            {
                Application.Current.Dispatcher.Invoke(() =>
                { webServices.Insert(0, usedWebServices[i]); });
            }

            if (historyWebServices.Count > 0)
            {
                DefiningObject = historyWebServices[0];
                HabitsAreFound = true;
            }
        }

        public void AddToHabitsAnalyser(object data)
        {
            Models.WebService webService = data as Models.WebService;
            string webServiceInfo = webService.Name + '|' + webService.OriginalRequest;
            usedWebServicesInfos.Remove(webServiceInfo);
            usedWebServicesInfos.Insert(0, webServiceInfo);
        }

        public void SaveData()
        {
            FileStream fileStream = null;
            try
            {
                string applicationDataPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                string settingsPath = Path.Combine(applicationDataPath, @"Find and Launch\WebServiceHabits.txt");
                fileStream = new FileStream(settingsPath, FileMode.Open, FileAccess.Write, FileShare.Write);
                StreamWriter streamWriter = new StreamWriter(fileStream);
                foreach (string usedWebServiceInfo in usedWebServicesInfos)
                    streamWriter.WriteLine(usedWebServiceInfo);
                streamWriter.Close();
                fileStream.Close();
            }
            catch
            { fileStream.Close(); }
        }
    }
}
