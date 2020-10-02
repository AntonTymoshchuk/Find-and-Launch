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
    public class FolderHabitsAnalyser : HabitsAnalyser, IHabitsAnalyser
    {
        private readonly List<string> usedFolderFullNames;

        public FolderHabitsAnalyser() : base()
        {
            usedFolderFullNames = new List<string>();
            LoadData();
        }

        public void LoadData()
        {
            FileStream fileStream = null;
            try
            {
                string applicationDataPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                string settingsPath = Path.Combine(applicationDataPath, @"Find and Launch\FolderHabits.txt");
                fileStream = new FileStream(settingsPath, FileMode.Open, FileAccess.Read, FileShare.Read);
                StreamReader streamReader = new StreamReader(fileStream);
                while (streamReader.EndOfStream != true)
                    usedFolderFullNames.Add(streamReader.ReadLine());
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

            ObservableCollection<Models.Folder> folders = data as ObservableCollection<Models.Folder>;
            List<Models.Folder> usedFolders = new List<Models.Folder>();
            List<int> usedFoldersIndexes = new List<int>();

            for (int i = 0; i < folders.Count; i++)
            {
                for (int j = 0; j < usedFolderFullNames.Count; j++)
                {
                    if (folders[i].FullName.Equals(usedFolderFullNames[j]))
                    {
                        usedFolders.Add(folders[i]);
                        usedFoldersIndexes.Add(j);
                        Application.Current.Dispatcher.Invoke(() =>
                        { folders.Remove(folders[i]); });
                        i--;
                        break;
                    }
                }
            }

            int currentIndex;
            Models.Folder currentFolder;

            for (int i = 1; i < usedFolders.Count; i++)
            {
                currentIndex = usedFoldersIndexes[i];
                currentFolder = usedFolders[i];
                int j = i;
                while (j > 0 && currentIndex < usedFoldersIndexes[j - 1])
                {
                    usedFoldersIndexes[j] = usedFoldersIndexes[j - 1];
                    usedFolders[j] = usedFolders[j - 1];
                    j--;
                }
                usedFoldersIndexes[j] = currentIndex;
                usedFolders[j] = currentFolder;
            }

            for (int i = usedFolders.Count - 1; i >= 0; i--)
            {
                Application.Current.Dispatcher.Invoke(() =>
                { folders.Insert(0, usedFolders[i]); });
            }

            if (usedFolders.Count > 0)
            {
                DefiningObject = usedFolders[0];
                HabitsAreFound = true;
            }
        }

        public void AddToHabitsAnalyser(object data)
        {
            Models.Folder folder = data as Models.Folder;
            usedFolderFullNames.Remove(folder.FullName);
            usedFolderFullNames.Insert(0, folder.FullName);
        }

        public void SaveData()
        {
            FileStream fileStream = null;
            try
            {
                string applicationDataPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                string settingsPath = Path.Combine(applicationDataPath, @"Find and Launch\FolderHabits.txt");
                fileStream = new FileStream(settingsPath, FileMode.Open, FileAccess.Write, FileShare.Write);
                StreamWriter streamWriter = new StreamWriter(fileStream);
                foreach (string usedFolderFullName in usedFolderFullNames)
                    streamWriter.WriteLine(usedFolderFullName);
                streamWriter.Close();
                fileStream.Close();
            }
            catch
            { fileStream.Close(); }
        }
    }
}
