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
    public class SettingsHabitsAnalyser : HabitsAnalyser, IHabitsAnalyser
    {
        private readonly List<string> usedSettingsNames;

        public SettingsHabitsAnalyser() : base()
        {
            usedSettingsNames = new List<string>();
            LoadData();
        }

        public void LoadData()
        {
            FileStream fileStream = null;
            try
            {
                string applicationDataPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                string settingsPath = Path.Combine(applicationDataPath, @"Find and Launch\SettingsHabits.txt");
                fileStream = new FileStream(settingsPath, FileMode.Open, FileAccess.Read, FileShare.Read);
                StreamReader streamReader = new StreamReader(fileStream);
                while (streamReader.EndOfStream != true)
                    usedSettingsNames.Add(streamReader.ReadLine());
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

            ObservableCollection<Models.Settings> settings = data as ObservableCollection<Models.Settings>;
            List<Models.Settings> usedSettings = new List<Models.Settings>();
            List<int> usedSettingsIndexes = new List<int>();

            for (int i = 0; i < settings.Count; i++)
            {
                for (int j = 0; j < usedSettingsNames.Count; j++)
                {
                    if (settings[i].Name.Equals(usedSettingsNames[j]))
                    {
                        usedSettings.Add(settings[i]);
                        usedSettingsIndexes.Add(j);
                        Application.Current.Dispatcher.Invoke(() =>
                        { settings.Remove(settings[i]); });
                        i--;
                        break;
                    }
                }
            }

            int currentIndex;
            Models.Settings currentSettings;

            for (int i = 1; i < usedSettingsIndexes.Count; i++)
            {
                currentIndex = usedSettingsIndexes[i];
                currentSettings = usedSettings[i];
                int j = i;
                while (j > 0 && currentIndex < usedSettingsIndexes[j - 1])
                {
                    usedSettingsIndexes[j] = usedSettingsIndexes[j - 1];
                    usedSettings[j] = usedSettings[j - 1];
                    j--;
                }
                usedSettingsIndexes[j] = currentIndex;
                usedSettings[j] = currentSettings;
            }

            for (int i = usedSettings.Count - 1; i >= 0; i--)
            {
                Application.Current.Dispatcher.Invoke(() =>
                { settings.Insert(0, usedSettings[i]); });
            }

            if (usedSettings.Count > 0)
            {
                DefiningObject = usedSettings[0];
                HabitsAreFound = true;
            }
        }

        public void AddToHabitsAnalyser(object data)
        {
            Models.Settings settings = data as Models.Settings;
            usedSettingsNames.Remove(settings.Name);
            usedSettingsNames.Insert(0, settings.Name);
        }

        public void SaveData()
        {
            FileStream fileStream = null;
            try
            {
                string applicationDataPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                string settingsPath = Path.Combine(applicationDataPath, @"Find and Launch\SettingsHabits.txt");
                fileStream = new FileStream(settingsPath, FileMode.Open, FileAccess.Write, FileShare.Write);
                StreamWriter streamWriter = new StreamWriter(fileStream);
                foreach (string usedSettingsName in usedSettingsNames)
                    streamWriter.WriteLine(usedSettingsName);
                streamWriter.Close();
                fileStream.Close();
            }
            catch
            { fileStream.Close(); }
        }
    }
}
