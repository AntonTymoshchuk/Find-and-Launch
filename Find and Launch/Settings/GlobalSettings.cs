using Find_and_Launch.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Find_and_Launch.Settings
{
    public abstract class GlobalSettings
    {
        public static bool SearchForMathExpression { get; set; }
        public static bool SearchForFiles { get; set; }
        public static bool SearchForFolders { get; set; }
        public static bool SearchForMicrosoftStoreApps { get; set; }
        public static bool SearchForApplications { get; set; }
        public static bool SearchForSettings { get; set; }
        public static bool SearchForSystemServices { get; set; }
        public static bool SearchForGoogleWebServices { get; set; }
        public static int MathExpressionAlgorithmQueue { get; set; }
        public static int FileAlgorithmQueue { get; set; }
        public static int FolderAlgorithmQueue { get; set; }
        public static int MicrosoftStoreAppsAlgorithmQueue { get; set; }
        public static int ApplicationAlgorithmQueue { get; set; }
        public static int SettingsAlgorithmQueue { get; set; }
        public static int SystemServiceAlgorithmQueue { get; set; }
        public static int GoogleWebServiceAlgorithmQueue { get; set; }
        public static bool UseHabitsAnalysis { get; set; }
        public static bool RememberOnLaunchment { get; set; }
        public static bool RememberOnActivation { get; set; }
        public static bool IncludeInkFiles { get; set; }
        public static bool IncludeHiddenFiles { get; set; }
        public static bool IncludeHiddenFolders { get; set; }
        public static ComparementType ComparementType { get; set; }
        public static bool UseImageFavicon { get; set; }
        public static Theme Theme { get; set; }
        public static FileInfoSettings FileInfoSettings { get; private set; }
        public static List<string> IncludedFoldersPaths { get; set; }
        public static List<string> ExcludedFolderPaths { get; set; }

        public static void Read()
        {
            FileStream fileStream = null;
            try
            {
                string applicationDataPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                string settingsPath = Path.Combine(applicationDataPath, @"Find and Launch\Settings.txt");
                fileStream = new FileStream(settingsPath, FileMode.Open, FileAccess.Read, FileShare.Read);

                byte[] bytes = new byte[fileStream.Length];
                fileStream.Read(bytes, 0, Convert.ToInt32(fileStream.Length));
                List<char> chars = new List<char>();
                foreach (byte b in bytes)
                    chars.Add(Convert.ToChar(b));
                char[] charArray = chars.ToArray();
                string encodedText = string.Empty;
                foreach (char c in charArray)
                    encodedText += c;
                bytes = Convert.FromBase64String(encodedText);
                chars = new List<char>();
                foreach (byte b in bytes)
                    chars.Add(Convert.ToChar(b));
                charArray = chars.ToArray();
                string settingsText = string.Empty;
                foreach (char c in charArray)
                    settingsText += c;

                string[] settings = settingsText.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

                SearchForMathExpression = Convert.ToBoolean(settings[0]);
                SearchForFiles = Convert.ToBoolean(settings[1]);
                SearchForFolders = Convert.ToBoolean(settings[2]);
                SearchForMicrosoftStoreApps = Convert.ToBoolean(settings[3]);
                SearchForApplications = Convert.ToBoolean(settings[4]);
                SearchForSettings = Convert.ToBoolean(settings[5]);
                SearchForSystemServices = Convert.ToBoolean(settings[6]);
                SearchForGoogleWebServices = Convert.ToBoolean(settings[7]);

                MathExpressionAlgorithmQueue = Convert.ToInt32(settings[8]);
                FileAlgorithmQueue = Convert.ToInt32(settings[9]);
                FolderAlgorithmQueue = Convert.ToInt32(settings[10]);
                MicrosoftStoreAppsAlgorithmQueue = Convert.ToInt32(settings[11]);
                ApplicationAlgorithmQueue = Convert.ToInt32(settings[12]);
                SettingsAlgorithmQueue = Convert.ToInt32(settings[13]);
                SystemServiceAlgorithmQueue = Convert.ToInt32(settings[14]);
                GoogleWebServiceAlgorithmQueue = Convert.ToInt32(settings[15]);

                UseHabitsAnalysis = Convert.ToBoolean(settings[16]);
                RememberOnLaunchment = Convert.ToBoolean(settings[17]);
                RememberOnActivation = Convert.ToBoolean(settings[18]);
                IncludeInkFiles = Convert.ToBoolean(settings[19]);
                IncludeHiddenFiles = Convert.ToBoolean(settings[20]);
                IncludeHiddenFolders = Convert.ToBoolean(settings[21]);
                ComparementType = (ComparementType)Convert.ToInt32(settings[22]);
                UseImageFavicon = Convert.ToBoolean(settings[23]);
                Theme = (Theme)Convert.ToInt32(settings[24]);

                FileInfoSettings = new FileInfoSettings(settings);

                IncludedFoldersPaths = new List<string>();
                int currentIndex = 33;
                for (int i = currentIndex; i < settings.Length; i++)
                {
                    if (settings[i].Equals("!"))
                        break;
                    IncludedFoldersPaths.Add(settings[i]);
                    currentIndex++;
                }
                currentIndex++;

                ExcludedFolderPaths = new List<string>();
                for (int i = currentIndex; i < settings.Length; i++)
                    ExcludedFolderPaths.Add(settings[i]);

                fileStream.Close();
            }
            catch
            {
                // Error message

                fileStream.Close();

                SearchForMathExpression = true;
                SearchForFiles = true;
                SearchForFolders = true;
                SearchForMicrosoftStoreApps = true;
                SearchForApplications = true;
                SearchForSettings = true;
                SearchForSystemServices = true;
                SearchForGoogleWebServices = true;

                MathExpressionAlgorithmQueue = 0;
                FileAlgorithmQueue = 1;
                FolderAlgorithmQueue = 2;
                MicrosoftStoreAppsAlgorithmQueue = 3;
                ApplicationAlgorithmQueue = 4;
                SettingsAlgorithmQueue = 5;
                SystemServiceAlgorithmQueue = 6;
                GoogleWebServiceAlgorithmQueue = 7;

                UseHabitsAnalysis = true;
                RememberOnLaunchment = true;
                RememberOnActivation = false;
                IncludeInkFiles = false;
                IncludeHiddenFiles = false;
                IncludeHiddenFolders = false;

                ComparementType = ComparementType.ContainsRequest;
                UseImageFavicon = false;
                Theme = Theme.White;

                FileInfoSettings = new FileInfoSettings();

                IncludedFoldersPaths = new List<string>
                {Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)};

                ExcludedFolderPaths = new List<string>();

                Save();
            }
        }

        public static void Save()
        {
            string settingsText =
                Convert.ToString(SearchForMathExpression) + "\r\n" +
                Convert.ToString(SearchForFiles) + "\r\n" +
                Convert.ToString(SearchForFolders) + "\r\n" +
                Convert.ToString(SearchForMicrosoftStoreApps) + "\r\n" +
                Convert.ToString(SearchForApplications) + "\r\n" +
                Convert.ToString(SearchForSettings) + "\r\n" +
                Convert.ToString(SearchForSystemServices) + "\r\n" +
                Convert.ToString(SearchForGoogleWebServices) + "\r\n" +
                Convert.ToString(MathExpressionAlgorithmQueue) + "\r\n" +
                Convert.ToString(FileAlgorithmQueue) + "\r\n" +
                Convert.ToString(FolderAlgorithmQueue) + "\r\n" +
                Convert.ToString(MicrosoftStoreAppsAlgorithmQueue) + "\r\n" +
                Convert.ToString(ApplicationAlgorithmQueue) + "\r\n" +
                Convert.ToString(SettingsAlgorithmQueue) + "\r\n" +
                Convert.ToString(SystemServiceAlgorithmQueue) + "\r\n" +
                Convert.ToString(GoogleWebServiceAlgorithmQueue) + "\r\n" +
                Convert.ToString(UseHabitsAnalysis) + "\r\n" +
                Convert.ToString(RememberOnLaunchment) + "\r\n" +
                Convert.ToString(RememberOnActivation) + "\r\n" +
                Convert.ToString(IncludeInkFiles) + "\r\n" +
                Convert.ToString(IncludeHiddenFiles) + "\r\n" +
                Convert.ToString(IncludeHiddenFolders) + "\r\n" +
                Convert.ToString((int)ComparementType) + "\r\n" +
                Convert.ToString(UseImageFavicon) + "\r\n" +
                FileInfoSettings.Save() + "\r\n" +
                Convert.ToString((int)Theme) + "\r\n";
            foreach (string includedFolderPath in IncludedFoldersPaths)
                settingsText += includedFolderPath + "\r\n";
            foreach (string excludedFilderPath in ExcludedFolderPaths)
                settingsText += excludedFilderPath + "\r\n";

            char[] chars = settingsText.ToCharArray();
            List<byte> bytes = new List<byte>();
            foreach (char symbol in chars)
                bytes.Add(Convert.ToByte(symbol));
            byte[] byteArr = bytes.ToArray();
            string encodedString = Convert.ToBase64String(byteArr);

            FileStream fileStream = null;
            StreamWriter streamWriter = null;
            try
            {
                string applicationDataPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                string settingsPath = Path.Combine(applicationDataPath, @"Find and Launch\Settings.txt");
                fileStream = new FileStream(settingsPath, FileMode.Open, FileAccess.Write, FileShare.Write);
                streamWriter = new StreamWriter(fileStream);
                streamWriter.Write(encodedString);

                streamWriter.Close();
                fileStream.Close();
            }
            catch
            {
                // Error message

                streamWriter.Close();
                fileStream.Close();
            }
        }
    }

    public enum Theme
    {
        White, Red, Green, Blue, Gold, Pink, Purple, Orange, Brown, Black
    }

    public enum ComparementType
    {
        ContainsRequest, StartsWithRequest
    }
}
