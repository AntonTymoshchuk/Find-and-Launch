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
    public class ApplicationHabitsAnalyser : HabitsAnalyser, IHabitsAnalyser
    {
        private readonly List<string> usedApplicationFullLnkNames;

        public ApplicationHabitsAnalyser() : base()
        {
            usedApplicationFullLnkNames = new List<string>();
            LoadData();
        }

        public void LoadData()
        {
            FileStream fileStream = null;
            try
            {
                string applicationDataPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                string settingsPath = Path.Combine(applicationDataPath, @"Find and Launch\ApplicationHabits.txt");
                fileStream = new FileStream(settingsPath, FileMode.Open, FileAccess.Read, FileShare.Read);
                StreamReader streamReader = new StreamReader(fileStream);
                while (streamReader.EndOfStream != true)
                    usedApplicationFullLnkNames.Add(streamReader.ReadLine());
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

            ObservableCollection<Models.Application> applications = data as ObservableCollection<Models.Application>;
            List<Models.Application> usedApplications = new List<Models.Application>();
            List<int> usedApplicationIndexes = new List<int>();

            for (int i = 0; i < applications.Count; i++)
            {
                for (int j = 0; j < usedApplicationFullLnkNames.Count; j++)
                {
                    if (applications[i].FullLnkName.Equals(usedApplicationFullLnkNames[j]))
                    {
                        usedApplications.Add(applications[i]);
                        usedApplicationIndexes.Add(j);
                        Application.Current.Dispatcher.Invoke(() =>
                        { applications.Remove(applications[i]); });
                        i--;
                        break;
                    }
                }
            }

            int currentIndex;
            Models.Application currentApplication;
            for (int i = 1; i < usedApplicationIndexes.Count; i++)
            {
                currentIndex = usedApplicationIndexes[i];
                currentApplication = usedApplications[i];
                int j = i;
                while (j > 0 && currentIndex < usedApplicationIndexes[j - 1])
                {
                    usedApplicationIndexes[j] = usedApplicationIndexes[j - 1];
                    usedApplications[j] = usedApplications[j - 1];
                    j--;
                }
                usedApplicationIndexes[j] = currentIndex;
                usedApplications[j] = currentApplication;
            }

            for (int i = usedApplications.Count - 1; i >= 0; i--)
            {
                Application.Current.Dispatcher.Invoke(() =>
                { applications.Insert(0, usedApplications[i]); });
            }

            if (usedApplications.Count > 0)
            {
                DefiningObject = usedApplications[0];
                HabitsAreFound = true;
            }
        }

        public void AddToHabitsAnalyser(object data)
        {
            Models.Application application = data as Models.Application;
            usedApplicationFullLnkNames.Remove(application.FullLnkName);
            usedApplicationFullLnkNames.Insert(0, application.FullLnkName);
        }

        public void SaveData()
        {
            FileStream fileStream = null;
            try
            {
                string applicationDataPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                string settingsPath = Path.Combine(applicationDataPath, @"Find and Launch\ApplicationHabits.txt");
                fileStream = new FileStream(settingsPath, FileMode.Open, FileAccess.Write, FileShare.Write);
                StreamWriter streamWriter = new StreamWriter(fileStream);
                foreach (string usedApplicationFullLnkName in usedApplicationFullLnkNames)
                    streamWriter.WriteLine(usedApplicationFullLnkName);
                streamWriter.Close();
                fileStream.Close();
            }
            catch
            { fileStream.Close(); }
        }
    }
}
