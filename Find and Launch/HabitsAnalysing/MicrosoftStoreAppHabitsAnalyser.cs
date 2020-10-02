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
    public class MicrosoftStoreAppHabitsAnalyser : HabitsAnalyser, IHabitsAnalyser
    {
        private readonly List<string> usedMicrosoftStoreAppNames;

        public MicrosoftStoreAppHabitsAnalyser() : base()
        {
            usedMicrosoftStoreAppNames = new List<string>();
            LoadData();
        }

        public void LoadData()
        {
            FileStream fileStream = null;
            try
            {
                string applicationDataPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                string settingsPath = Path.Combine(applicationDataPath, @"Find and Launch\MicrosoftStoreAppHabits.txt");
                fileStream = new FileStream(settingsPath, FileMode.Open, FileAccess.Read, FileShare.Read);
                StreamReader streamReader = new StreamReader(fileStream);
                while (streamReader.EndOfStream != true)
                    usedMicrosoftStoreAppNames.Add(streamReader.ReadLine());
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

            ObservableCollection<Models.MicrosoftStoreApplication> microsoftStoreApplications = data as ObservableCollection<Models.MicrosoftStoreApplication>;
            List<Models.MicrosoftStoreApplication> usedMicrosoftStoreApplications = new List<Models.MicrosoftStoreApplication>();
            List<int> usedMicrosoftStoreAppIndexes = new List<int>();

            for (int i = 0; i < microsoftStoreApplications.Count; i++)
            {
                for (int j = 0; j < usedMicrosoftStoreAppNames.Count; j++)
                {
                    if (microsoftStoreApplications[i].Name.Equals(usedMicrosoftStoreAppNames[j]))
                    {
                        usedMicrosoftStoreApplications.Add(microsoftStoreApplications[i]);
                        usedMicrosoftStoreAppIndexes.Add(j);
                        Application.Current.Dispatcher.Invoke(() =>
                        { microsoftStoreApplications.Remove(microsoftStoreApplications[i]); });
                        i--;
                        break;
                    }
                }
            }

            int currentIndex;
            Models.MicrosoftStoreApplication currentMicrosoftStoreApp;

            for (int i = 1; i < usedMicrosoftStoreAppIndexes.Count; i++)
            {
                currentIndex = usedMicrosoftStoreAppIndexes[i];
                currentMicrosoftStoreApp = usedMicrosoftStoreApplications[i];
                int j = i;
                while (j > 0 && currentIndex < usedMicrosoftStoreAppIndexes[j - 1])
                {
                    usedMicrosoftStoreAppIndexes[j] = usedMicrosoftStoreAppIndexes[j - 1];
                    usedMicrosoftStoreApplications[j] = usedMicrosoftStoreApplications[j - 1];
                    j--;
                }
                usedMicrosoftStoreAppIndexes[j] = currentIndex;
                usedMicrosoftStoreApplications[j] = currentMicrosoftStoreApp;
            }

            for (int i = usedMicrosoftStoreApplications.Count - 1; i >= 0; i--)
            {
                Application.Current.Dispatcher.Invoke(() =>
                { microsoftStoreApplications.Insert(0, usedMicrosoftStoreApplications[i]); });
            }

            if (usedMicrosoftStoreApplications.Count > 0)
            {
                DefiningObject = usedMicrosoftStoreApplications[0];
                HabitsAreFound = true;
            }
        }

        public void AddToHabitsAnalyser(object data)
        {
            Models.MicrosoftStoreApplication microsoftStoreApplication = data as Models.MicrosoftStoreApplication;
            usedMicrosoftStoreAppNames.Remove(microsoftStoreApplication.Name);
            usedMicrosoftStoreAppNames.Insert(0, microsoftStoreApplication.Name);
        }

        public void SaveData()
        {
            FileStream fileStream = null;
            try
            {
                string applicationDataPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                string settingsPath = Path.Combine(applicationDataPath, @"Find and Launch\MicrosoftStoreAppHabits.txt");
                fileStream = new FileStream(settingsPath, FileMode.Open, FileAccess.Write, FileShare.Write);
                StreamWriter streamWriter = new StreamWriter(fileStream);
                foreach (string usedMicrosoftStoreAppName in usedMicrosoftStoreAppNames)
                    streamWriter.WriteLine(usedMicrosoftStoreAppName);
                streamWriter.Close();
                fileStream.Close();
            }
            catch
            { fileStream.Close(); }
        }
    }
}
