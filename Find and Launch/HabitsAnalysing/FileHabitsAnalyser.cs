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
    public class FileHabitsAnalyser : HabitsAnalyser, IHabitsAnalyser
    {
        private readonly List<string> usedFileFullNames;

        public FileHabitsAnalyser() : base()
        {
            usedFileFullNames = new List<string>();
            LoadData();
        }

        public void LoadData()
        {
            FileStream fileStream = null;
            try
            {
                string applicationDataPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                string settingsPath = Path.Combine(applicationDataPath, @"Find and Launch\FileHabits.txt");
                fileStream = new FileStream(settingsPath, FileMode.Open, FileAccess.Read, FileShare.Read);
                StreamReader streamReader = new StreamReader(fileStream);
                while (streamReader.EndOfStream != true)
                    usedFileFullNames.Add(streamReader.ReadLine());
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

            ObservableCollection<Models.File> files = data as ObservableCollection<Models.File>;
            List<Models.File> usedFiles = new List<Models.File>();
            List<int> usedFilesIndexes = new List<int>();

            for (int i = 0; i < files.Count; i++)
            {
                for (int j = 0; j < usedFileFullNames.Count; j++)
                {
                    if (files[i].FullName.Equals(usedFileFullNames[j]))
                    {
                        usedFiles.Add(files[i]);
                        usedFilesIndexes.Add(j);
                        Application.Current.Dispatcher.Invoke(() =>
                        { files.Remove(files[i]); });
                        i--;
                        break;
                    }
                }
            }

            int currentIndex;
            Models.File currentFile;

            for (int i = 1; i < usedFilesIndexes.Count; i++)
            {
                currentIndex = usedFilesIndexes[i];
                currentFile = usedFiles[i];
                int j = i;
                while (j > 0 && currentIndex < usedFilesIndexes[j - 1])
                {
                    usedFilesIndexes[j] = usedFilesIndexes[j - 1];
                    usedFiles[j] = usedFiles[j - 1];
                    j--;
                }
                usedFilesIndexes[j] = currentIndex;
                usedFiles[j] = currentFile;
            }

            for (int i = usedFiles.Count - 1; i >= 0; i--)
            {
                Application.Current.Dispatcher.Invoke(() =>
                { files.Insert(0, usedFiles[i]); });
            }

            if (usedFiles.Count > 0)
            {
                DefiningObject = usedFiles[0];
                HabitsAreFound = true;
            }
        }

        public void AddToHabitsAnalyser(object data)
        {
            Models.File file = data as Models.File;
            usedFileFullNames.Remove(file.FullName);
            usedFileFullNames.Insert(0, file.FullName);
        }

        public void SaveData()
        {
            FileStream fileStream = null;
            try
            {
                string applicationDataPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                string settingsPath = Path.Combine(applicationDataPath, @"Find and Launch\FileHabits.txt");
                fileStream = new FileStream(settingsPath, FileMode.Open, FileAccess.Write, FileShare.Write);
                StreamWriter streamWriter = new StreamWriter(fileStream);
                foreach (string usedFileFullName in usedFileFullNames)
                    streamWriter.WriteLine(usedFileFullName);
                streamWriter.Close();
                fileStream.Close();
            }
            catch
            { fileStream.Close(); }
        }
    }
}
