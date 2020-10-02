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
    public class SystemHabitsAnalyser : HabitsAnalyser, IHabitsAnalyser
    {
        private readonly List<string> usedSystemInfos;

        public SystemHabitsAnalyser() : base()
        {
            usedSystemInfos = new List<string>();
            LoadData();
        }

        public void LoadData()
        {
            FileStream fileStream = null;
            try
            {
                string applicationDataPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                string settingsPath = Path.Combine(applicationDataPath, @"Find and Launch\SystemHabits.txt");
                fileStream = new FileStream(settingsPath, FileMode.Open, FileAccess.Read, FileShare.Read);
                StreamReader streamReader = new StreamReader(fileStream);
                while (streamReader.EndOfStream != true)
                    usedSystemInfos.Add(streamReader.ReadLine());
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

            List<string> usedSystemNames = new List<string>();
            List<string> usedSystemsRequests = new List<string>();

            foreach (string usedSystemInfo in usedSystemInfos)
            {
                string[] infoArray = usedSystemInfo.Split('|');
                usedSystemNames.Add(infoArray[0]);
                usedSystemsRequests.Add(infoArray[1]);
            }

            ObservableCollection<Models.System> systems = data as ObservableCollection<Models.System>;
            List<Models.System> firstUsedSystems = new List<Models.System>();
            List<int> firstUsedSystemIndexes = new List<int>();
            List<Models.System> usedSystems = new List<Models.System>();
            List<Models.System> historySystems = new List<Models.System>();

            for (int i = 0; i < systems.Count; i++)
            {
                for (int j = 0; j < usedSystemsRequests.Count; j++)
                {
                    if (//!usedSystemsRequests[j].ToLower().Equals(systems[i].OriginalRequest.ToLower()) &&
                        (usedSystemsRequests[j].ToLower().Contains(systems[i].OriginalRequest.ToLower()) ||
                        systems[i].OriginalRequest.ToLower().Contains(usedSystemsRequests[j].ToLower())) &&
                        systems[i].Name.Equals(usedSystemNames[j]))
                    {
                        firstUsedSystems.Add(systems[i]);
                        firstUsedSystemIndexes.Add(j);
                        Application.Current.Dispatcher.Invoke(() =>
                        { systems.Remove(systems[i]); });
                        i--;
                        break;
                    }
                }
            }

            int currentIndex;
            Models.System currentSystem;

            for (int i = 1; i < firstUsedSystemIndexes.Count; i++)
            {
                currentIndex = firstUsedSystemIndexes[i];
                currentSystem = firstUsedSystems[i];
                int j = i;
                while (j > 0 && currentIndex < firstUsedSystemIndexes[j - 1])
                {
                    firstUsedSystemIndexes[j] = firstUsedSystemIndexes[j - 1];
                    firstUsedSystems[j] = firstUsedSystems[j - 1];
                    j--;
                }
                firstUsedSystemIndexes[j] = currentIndex;
                firstUsedSystems[j] = currentSystem;
            }

            for (int i = 0; i < firstUsedSystems.Count; i++)
            {
                usedSystems.Add(firstUsedSystems[i]);
                for (int j = 0; j < usedSystemsRequests.Count; j++)
                {
                    if (//!usedSystemsRequests[j].ToLower().Equals(firstUsedSystems[i].OriginalRequest.ToLower()) &&
                        (usedSystemsRequests[j].ToLower().Contains(firstUsedSystems[i].OriginalRequest.ToLower()) ||
                        firstUsedSystems[i].OriginalRequest.ToLower().Contains(usedSystemsRequests[j].ToLower())) &&
                        firstUsedSystems[i].Name.Equals(usedSystemNames[j]))
                    {
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            Models.System system = new Models.System(usedSystemsRequests[j], firstUsedSystems[i].Name);
                            usedSystems.Add(system);
                            historySystems.Add(system);
                        });
                        //Application.Current.Dispatcher.Invoke(() =>
                        //{ usedSystems.Add(new Models.System(usedSystemsRequests[j], firstUsedSystems[i].Name)); });
                    }
                }
            }

            for (int i = 0; i < usedSystems.Count; i++)
            {
                for (int j = 0; j < usedSystems.Count; j++)
                {
                    if (i == j)
                        continue;
                    if (usedSystems[i].Name.Equals(usedSystems[j].Name))
                    {
                        if (usedSystems[i].OriginalRequest.ToLower().Equals(usedSystems[j].OriginalRequest.ToLower()))
                        {
                            usedSystems.Remove(usedSystems[j]);
                            if (i > 0)
                                i--;
                            j--;
                        }
                    }
                }
            }

            for (int i = 0; i < historySystems.Count; i++)
            {
                for (int j = 0; j < historySystems.Count; j++)
                {
                    if (i == j)
                        continue;
                    if (historySystems[i].Name.Equals(historySystems[j].Name))
                    {
                        if (historySystems[i].OriginalRequest.ToLower().Equals(historySystems[j].OriginalRequest.ToLower()))
                        {
                            historySystems.Remove(historySystems[j]);
                            if (i > 0)
                                i--;
                            j--;
                        }
                    }
                }
            }

            for (int i = usedSystems.Count - 1; i >= 0; i--)
            {
                Application.Current.Dispatcher.Invoke(() =>
                { systems.Insert(0, usedSystems[i]); });
            }

            if (historySystems.Count > 0)
            {
                DefiningObject = historySystems[0];
                HabitsAreFound = true;
            }
        }

        public void AddToHabitsAnalyser(object data)
        {
            Models.System system = data as Models.System;
            string systemInfo = system.Name + '|' + system.OriginalRequest;
            usedSystemInfos.Remove(systemInfo);
            usedSystemInfos.Insert(0, systemInfo);
        }

        public void SaveData()
        {
            FileStream fileStream = null;
            try
            {
                string applicationDataPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                string settingsPath = Path.Combine(applicationDataPath, @"Find and Launch\SystemHabits.txt");
                fileStream = new FileStream(settingsPath, FileMode.Open, FileAccess.Write, FileShare.Write);
                StreamWriter streamWriter = new StreamWriter(fileStream);
                foreach (string usedSystemInfo in usedSystemInfos)
                    streamWriter.WriteLine(usedSystemInfo);
                streamWriter.Close();
                fileStream.Close();
            }
            catch
            { fileStream.Close(); }
        }
    }
}
