using Find_and_Launch.Settings;
using Find_and_Launch.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Find_and_Launch.HabitsAnalysing
{
    public abstract class GlobalHabitsAnalyser
    {
        private static List<string> globalHabitsInfos;

        public static MathExpressionHabitsAnalyser MathExpressionHabitsAnalyser { get; private set; }
        public static FileHabitsAnalyser FileHabitsAnalyser { get; private set; }
        public static FolderHabitsAnalyser FolderHabitsAnalyser { get; private set; }
        public static MicrosoftStoreAppHabitsAnalyser MicrosoftStoreAppHabitsAnalyser { get; private set; }
        public static ApplicationHabitsAnalyser ApplicationHabitsAnalyser { get; private set; }
        public static SettingsHabitsAnalyser SettingsHabitsAnalyser { get; private set; }
        public static SystemHabitsAnalyser SystemHabitsAnalyser { get; private set; }
        public static WebServicesHabitsAnalyser WebServicesHabitsAnalyser { get; private set; }

        public static void LoadData()
        {
            globalHabitsInfos = new List<string>();
            FileStream fileStream = null;
            try
            {
                string applicationDataPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                string settingsPath = Path.Combine(applicationDataPath, @"Find and Launch\GlobalHabits.txt");
                fileStream = new FileStream(settingsPath, FileMode.Open, FileAccess.Read, FileShare.Read);
                StreamReader streamReader = new StreamReader(fileStream);
                while (streamReader.EndOfStream != true)
                    globalHabitsInfos.Add(streamReader.ReadLine());
                streamReader.Close();
                fileStream.Close();
            }
            catch
            { fileStream.Close(); }

            MathExpressionHabitsAnalyser = new MathExpressionHabitsAnalyser();
            FileHabitsAnalyser = new FileHabitsAnalyser();
            FolderHabitsAnalyser = new FolderHabitsAnalyser();
            MicrosoftStoreAppHabitsAnalyser = new MicrosoftStoreAppHabitsAnalyser();
            ApplicationHabitsAnalyser = new ApplicationHabitsAnalyser();
            SettingsHabitsAnalyser = new SettingsHabitsAnalyser();
            SystemHabitsAnalyser = new SystemHabitsAnalyser();
            WebServicesHabitsAnalyser = new WebServicesHabitsAnalyser();
        }

        public static void AdaptViewByHabitsAnalysis(GlobalViewModel globalViewModel)
        {
            List<string> usedModelNames = new List<string>();
            List<string> usedModelParameters = new List<string>();

            for (int i = 0; i < globalHabitsInfos.Count; i++)
            {
                string[] habitInfos = globalHabitsInfos[i].Split('|');
                usedModelNames.Add(habitInfos[0]);
                usedModelParameters.Add(habitInfos[1]);
            }

            string[] modelNames = new string[] {
                "MathExpression",
                "File",
                "Folder",
                "MicrosoftStoreApp",
                "Application",
                "Settings",
                "System",
                "WebService" };

            List<string> currentlyUsedModelNames = new List<string>();
            List<string> currentlyUsedModelParameters = new List<string>();
            List<int> currentlyUsedModelIndexes = new List<int>();

            if (MathExpressionHabitsAnalyser.HabitsAreFound == true)
            {
                currentlyUsedModelNames.Add(modelNames[0]);
                currentlyUsedModelParameters.Add(
                    (MathExpressionHabitsAnalyser.DefiningObject as Models.MathExpression).Expression);
            }
            if (FileHabitsAnalyser.HabitsAreFound == true)
            {
                currentlyUsedModelNames.Add(modelNames[1]);
                currentlyUsedModelParameters.Add(
                    (FileHabitsAnalyser.DefiningObject as Models.File).FullName);
            }
            if (FolderHabitsAnalyser.HabitsAreFound == true)
            {
                currentlyUsedModelNames.Add(modelNames[2]);
                currentlyUsedModelParameters.Add(
                    (FolderHabitsAnalyser.DefiningObject as Models.Folder).FullName);
            }
            if (MicrosoftStoreAppHabitsAnalyser.HabitsAreFound == true)
            {
                currentlyUsedModelNames.Add(modelNames[3]);
                currentlyUsedModelParameters.Add(
                    (MicrosoftStoreAppHabitsAnalyser.DefiningObject as Models.MicrosoftStoreApplication).Name);
            }
            if (ApplicationHabitsAnalyser.HabitsAreFound == true)
            {
                currentlyUsedModelNames.Add(modelNames[4]);
                currentlyUsedModelParameters.Add(
                    (ApplicationHabitsAnalyser.DefiningObject as Models.Application).FullLnkName);
            }
            if (SettingsHabitsAnalyser.HabitsAreFound == true)
            {
                currentlyUsedModelNames.Add(modelNames[5]);
                currentlyUsedModelParameters.Add(
                    (SettingsHabitsAnalyser.DefiningObject as Models.Settings).Name);
            }
            if (SystemHabitsAnalyser.HabitsAreFound == true)
            {
                currentlyUsedModelNames.Add(modelNames[6]);
                currentlyUsedModelParameters.Add(
                    (SystemHabitsAnalyser.DefiningObject as Models.System).OriginalRequest);
            }
            if (WebServicesHabitsAnalyser.HabitsAreFound == true)
            {
                currentlyUsedModelNames.Add(modelNames[7]);
                currentlyUsedModelParameters.Add(
                    (WebServicesHabitsAnalyser.DefiningObject as Models.WebService).OriginalRequest);
            }

            for (int i = 0; i < currentlyUsedModelNames.Count; i++)
            {
                for (int j = 0; j < usedModelNames.Count; j++)
                {
                    if (currentlyUsedModelNames[i].Equals(usedModelNames[j]) &&
                        currentlyUsedModelParameters[i].ToLower().Equals(usedModelParameters[j].ToLower()))
                    {
                        currentlyUsedModelIndexes.Add(j);
                    }
                }
            }

            int interimIndex;
            string interimModelName;
            for (int i = 1; i < currentlyUsedModelIndexes.Count; i++)
            {
                interimIndex = currentlyUsedModelIndexes[i];
                interimModelName = currentlyUsedModelNames[i];
                int j = i;
                while (j > 0 && interimIndex < currentlyUsedModelIndexes[j - 1])
                {
                    currentlyUsedModelIndexes[j] = currentlyUsedModelIndexes[j - 1];
                    currentlyUsedModelNames[j] = currentlyUsedModelNames[j - 1];
                    j--;
                }
                currentlyUsedModelIndexes[j] = interimIndex;
                currentlyUsedModelNames[j] = interimModelName;
            }

            int position = 0;
            for (int i = 0; i < currentlyUsedModelNames.Count; i++)
            {
                switch (currentlyUsedModelNames[i])
                {
                    case "MathExpression":
                        globalViewModel.MathExpressionListOrder = position;
                        position++;
                        break;
                    case "File":
                        globalViewModel.FileListOrder = position;
                        position++;
                        break;
                    case "Folder":
                        globalViewModel.FolderListOrder = position;
                        position++;
                        break;
                    case "MicrosoftStoreApp":
                        globalViewModel.MicrosoftStoreAppListOrder = position;
                        position++;
                        break;
                    case "Application":
                        globalViewModel.ApplicationListOrder = position;
                        position++;
                        break;
                    case "Settings":
                        globalViewModel.SettingsListOrder = position;
                        position++;
                        break;
                    case "System":
                        globalViewModel.SystemListOrder = position;
                        position++;
                        break;
                    case "WebService":
                        globalViewModel.WebServiceListOrder = position;
                        position++;
                        break;
                }
            }

            if (currentlyUsedModelNames.Count < modelNames.Length)
            {
                List<string> currentlyNotUsedModelNames = new List<string>();
                List<int> currentlyNotUsedModelPriorities = new List<int>();

                if (MathExpressionHabitsAnalyser.HabitsAreFound == false && globalViewModel.MathExpressionListAviliable == true)
                {
                    if (GlobalSettings.SearchForMathExpression == true)
                    {
                        currentlyNotUsedModelNames.Add(modelNames[0]);
                        currentlyNotUsedModelPriorities.Add(GlobalSettings.MathExpressionAlgorithmQueue);
                    }
                }
                if (FileHabitsAnalyser.HabitsAreFound == false && globalViewModel.FileListAviliable == true)
                {
                    if (GlobalSettings.SearchForFiles == true)
                    {
                        currentlyNotUsedModelNames.Add(modelNames[1]);
                        currentlyNotUsedModelPriorities.Add(GlobalSettings.FileAlgorithmQueue);
                    }
                }
                if (FolderHabitsAnalyser.HabitsAreFound == false && globalViewModel.FolderListAviliable == true)
                {
                    if (GlobalSettings.SearchForFolders == true)
                    {
                        currentlyNotUsedModelNames.Add(modelNames[2]);
                        currentlyNotUsedModelPriorities.Add(GlobalSettings.FolderAlgorithmQueue);
                    }
                }
                if (MicrosoftStoreAppHabitsAnalyser.HabitsAreFound == false && globalViewModel.MicrosoftStoreAppListAviliable == true)
                {
                    if (GlobalSettings.SearchForMicrosoftStoreApps == true)
                    {
                        currentlyNotUsedModelNames.Add(modelNames[3]);
                        currentlyNotUsedModelPriorities.Add(GlobalSettings.MicrosoftStoreAppsAlgorithmQueue);
                    }
                }
                if (ApplicationHabitsAnalyser.HabitsAreFound == false && globalViewModel.ApplicationListAviliable == true)
                {
                    if (GlobalSettings.SearchForApplications == true)
                    {
                        currentlyNotUsedModelNames.Add(modelNames[4]);
                        currentlyNotUsedModelPriorities.Add(GlobalSettings.ApplicationAlgorithmQueue);
                    }
                }
                if (SettingsHabitsAnalyser.HabitsAreFound == false && globalViewModel.SettingsListAviliable == true)
                {
                    if (GlobalSettings.SearchForSettings == true)
                    {
                        currentlyNotUsedModelNames.Add(modelNames[5]);
                        currentlyNotUsedModelPriorities.Add(GlobalSettings.SettingsAlgorithmQueue);
                    }
                }
                if (SystemHabitsAnalyser.HabitsAreFound == false && globalViewModel.SystemListAviliable == true)
                {
                    if (GlobalSettings.SearchForSystemServices == true)
                    {
                        currentlyNotUsedModelNames.Add(modelNames[6]);
                        currentlyNotUsedModelPriorities.Add(GlobalSettings.SystemServiceAlgorithmQueue);
                    }
                }
                if (WebServicesHabitsAnalyser.HabitsAreFound == false && globalViewModel.WebServiceListAviliable == true)
                {
                    if (GlobalSettings.SearchForGoogleWebServices == true)
                    {
                        currentlyNotUsedModelNames.Add(modelNames[7]);
                        currentlyNotUsedModelPriorities.Add(GlobalSettings.GoogleWebServiceAlgorithmQueue);
                    }
                }

                int interimPriority;
                string interimNotUsedModelName;
                for (int i = 1; i < currentlyNotUsedModelPriorities.Count; i++)
                {
                    interimPriority = currentlyNotUsedModelPriorities[i];
                    interimNotUsedModelName = currentlyNotUsedModelNames[i];
                    int j = i;
                    while (j > 0 && interimPriority < currentlyNotUsedModelPriorities[j - 1])
                    {
                        currentlyNotUsedModelPriorities[j] = currentlyNotUsedModelPriorities[j - 1];
                        currentlyNotUsedModelNames[j] = currentlyNotUsedModelNames[j - 1];
                        j--;
                    }
                    currentlyNotUsedModelPriorities[j] = interimPriority;
                    currentlyNotUsedModelNames[j] = interimNotUsedModelName;
                }

                for (int i = 0; i < currentlyNotUsedModelNames.Count; i++)
                {
                    switch (currentlyNotUsedModelNames[i])
                    {
                        case "MathExpression":
                            globalViewModel.MathExpressionListOrder = position;
                            position++;
                            break;
                        case "File":
                            globalViewModel.FileListOrder = position;
                            position++;
                            break;
                        case "Folder":
                            globalViewModel.FolderListOrder = position;
                            position++;
                            break;
                        case "MicrosoftStoreApp":
                            globalViewModel.MicrosoftStoreAppListOrder = position;
                            position++;
                            break;
                        case "Application":
                            globalViewModel.ApplicationListOrder = position;
                            position++;
                            break;
                        case "Settings":
                            globalViewModel.SettingsListOrder = position;
                            position++;
                            break;
                        case "System":
                            globalViewModel.SystemListOrder = position;
                            position++;
                            break;
                        case "WebService":
                            globalViewModel.WebServiceListOrder = position;
                            position++;
                            break;
                    }
                }
            }

            ReleaseHabitsAnalysers();
        }

        public static void ReleaseHabitsAnalysers()
        {
            MathExpressionHabitsAnalyser.ReleaseHabitsAnalyser();
            FileHabitsAnalyser.ReleaseHabitsAnalyser();
            FolderHabitsAnalyser.ReleaseHabitsAnalyser();
            MicrosoftStoreAppHabitsAnalyser.ReleaseHabitsAnalyser();
            ApplicationHabitsAnalyser.ReleaseHabitsAnalyser();
            SettingsHabitsAnalyser.ReleaseHabitsAnalyser();
            SystemHabitsAnalyser.ReleaseHabitsAnalyser();
            WebServicesHabitsAnalyser.ReleaseHabitsAnalyser();
        }

        public static void AddRequestHabit(Models.MathExpression mathExpression)
        {
            string habitInfo = $"MathExpression|{mathExpression.Expression}";
            globalHabitsInfos.Remove(habitInfo);
            globalHabitsInfos.Insert(0, habitInfo);
        }

        public static void AddRequestHabit(Models.File file)
        {
            string habitInfo = $"File|{file.FullName}";
            globalHabitsInfos.Remove(habitInfo);
            globalHabitsInfos.Insert(0, habitInfo);
        }

        public static void AddRequestHabit(Models.Folder folder)
        {
            string habitInfo = $"Folder|{folder.FullName}";
            globalHabitsInfos.Remove(habitInfo);
            globalHabitsInfos.Insert(0, habitInfo);
        }

        public static void AddRequestHabit(Models.MicrosoftStoreApplication microsoftStoreApplication)
        {
            string habitInfo = $"MicrosoftStoreApp|{microsoftStoreApplication.Name}";
            globalHabitsInfos.Remove(habitInfo);
            globalHabitsInfos.Insert(0, habitInfo);
        }

        public static void AddRequestHabit(Models.Application application)
        {
            string habitInfo = $"Application|{application.FullLnkName}";
            globalHabitsInfos.Remove(habitInfo);
            globalHabitsInfos.Insert(0, habitInfo);
        }

        public static void AddRequestHabit(Models.Settings settings)
        {
            string habitInfo = $"Settings|{settings.Name}";
            globalHabitsInfos.Remove(habitInfo);
            globalHabitsInfos.Insert(0, habitInfo);
        }

        public static void AddRequestHabit(Models.System system)
        {
            string habitInfo = $"System|{system.OriginalRequest}";
            globalHabitsInfos.Remove(habitInfo);
            globalHabitsInfos.Insert(0, habitInfo);
        }

        public static void AddRequestHabit(Models.WebService webService)
        {
            string habitInfo = $"WebService|{webService.OriginalRequest}";
            globalHabitsInfos.Remove(habitInfo);
            globalHabitsInfos.Insert(0, habitInfo);
        }

        public static void SaveData()
        {
            FileStream fileStream = null;
            try
            {
                string applicationDataPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                string settingsPath = Path.Combine(applicationDataPath, @"Find and Launch\GlobalHabits.txt");
                fileStream = new FileStream(settingsPath, FileMode.Open, FileAccess.Write, FileShare.Write);
                StreamWriter streamWriter = new StreamWriter(fileStream);
                foreach (string globalHabitsInfo in globalHabitsInfos)
                    streamWriter.WriteLine(globalHabitsInfo);
                streamWriter.Close();
                fileStream.Close();
            }
            catch
            { fileStream.Close(); }

            MathExpressionHabitsAnalyser.SaveData();
            FileHabitsAnalyser.SaveData();
            FolderHabitsAnalyser.SaveData();
            MicrosoftStoreAppHabitsAnalyser.SaveData();
            ApplicationHabitsAnalyser.SaveData();
            SettingsHabitsAnalyser.SaveData();
            SystemHabitsAnalyser.SaveData();
            WebServicesHabitsAnalyser.SaveData();
        }
    }
}
