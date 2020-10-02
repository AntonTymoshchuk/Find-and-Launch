using Find_and_Launch.Algorithms;
using Find_and_Launch.Selectors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Find_and_Launch.ViewModels
{
    public class GlobalViewModel : INotifyPropertyChanged
    {
        private int mathExpressionListOrder;
        private int fileListOrder;
        private int folderListOrder;
        private int microsoftStoreAppListOrder;
        private int applicationListOrder;
        private int settingsListOrder;
        private int systemListOrder;
        private int webServiceListOrder;

        private Visibility mathExpressionListVisibility;
        private Visibility mathExpressionInfoVisibility;

        private Visibility fileListVisibility;
        private Visibility fileInfoVisibility;

        private Visibility folderListVisibility;
        private Visibility folderInfoVisibility;

        private Visibility microsoftStoreAppListVisibility;
        private Visibility microsoftStoreAppInfoVisibility;

        private Visibility applicationListVisibility;
        private Visibility applicationInfoVisibility;

        private Visibility settingsListVisibility;
        private Visibility settingsInfoVisibility;

        private Visibility systemListVisibility;
        private Visibility systemInfoVisibility;

        private Visibility webServiceListVisibility;
        private Visibility webServiceInfoVisibility;

        private Visibility notFoundIllustrationVisibility;
        private Visibility resultVisibility;

        private string postRequestString;


        public int MathExpressionListOrder
        {
            get { return mathExpressionListOrder; }
            set
            {
                mathExpressionListOrder = value;
                OnPropertyChanged("MathExpressionListOrder");
            }
        }

        public int FileListOrder
        {
            get { return fileListOrder; }
            set
            {
                fileListOrder = value;
                OnPropertyChanged("FileListOrder");
            }
        }

        public int FolderListOrder
        {
            get { return folderListOrder; }
            set
            {
                folderListOrder = value;
                OnPropertyChanged("FolderListOrder");
            }
        }

        public int MicrosoftStoreAppListOrder
        {
            get { return microsoftStoreAppListOrder; }
            set
            {
                microsoftStoreAppListOrder = value;
                OnPropertyChanged("MicrosoftStoreAppListOrder");
            }
        }

        public int ApplicationListOrder
        {
            get { return applicationListOrder; }
            set
            {
                applicationListOrder = value;
                OnPropertyChanged("ApplicationListOrder");
            }
        }

        public int SettingsListOrder
        {
            get { return settingsListOrder; }
            set
            {
                settingsListOrder = value;
                OnPropertyChanged("SettingsListOrder");
            }
        }

        public int SystemListOrder
        {
            get { return systemListOrder; }
            set
            {
                systemListOrder = value;
                OnPropertyChanged("SystemListOrder");
            }
        }

        public int WebServiceListOrder
        {
            get { return webServiceListOrder; }
            set
            {
                webServiceListOrder = value;
                OnPropertyChanged("WebServiceListOrder");
            }
        }


        public bool MathExpressionListAviliable { get; set; }

        public bool FileListAviliable { get; set; }

        public bool FolderListAviliable { get; set; }

        public bool MicrosoftStoreAppListAviliable { get; set; }
        
        public bool ApplicationListAviliable { get; set; }

        public bool SettingsListAviliable { get; set; }

        public bool SystemListAviliable { get; set; }

        public bool WebServiceListAviliable { get; set; }


        public bool MathExpressionListSelected { get; set; }

        public bool FileListSelected { get; set; }

        public bool FolderListSelected { get; set; }

        public bool MicrosoftStoreAppListSelected { get; set; }

        public bool ApplicationListSelected { get; set; }

        public bool SettingsListSelected { get; set; }

        public bool SystemListSelected { get; set; }

        public bool WebServiceListSelected { get; set; }


        public Visibility MathExpressionListVisibility
        {
            get { return mathExpressionListVisibility; }
            set
            {
                mathExpressionListVisibility = value;
                OnPropertyChanged("MathExpressionListVisibility");
            }
        }

        public Visibility MathExpressionInfoVisibility
        {
            get { return mathExpressionInfoVisibility; }
            set
            {
                mathExpressionInfoVisibility = value;
                OnPropertyChanged("MathExpressionInfoVisibility");
            }
        }


        public Visibility FileListVisibility
        {
            get { return fileListVisibility; }
            set
            {
                fileListVisibility = value;
                OnPropertyChanged("FileListVisibility");
            }
        }

        public Visibility FileInfoVisibility
        {
            get { return fileInfoVisibility; }
            set
            {
                fileInfoVisibility = value;
                OnPropertyChanged("FileInfoVisibility");
            }
        }


        public Visibility FolderListVisibility
        {
            get { return folderListVisibility; }
            set
            {
                folderListVisibility = value;
                OnPropertyChanged("FolderListVisibility");
            }
        }

        public Visibility FolderInfoVisibility
        {
            get { return folderInfoVisibility; }
            set
            {
                folderInfoVisibility = value;
                OnPropertyChanged("FolderInfoVisibility");
            }
        }


        public Visibility MicrosoftStoreAppListVisibility
        {
            get { return microsoftStoreAppListVisibility; }
            set
            {
                microsoftStoreAppListVisibility = value;
                OnPropertyChanged("MicrosoftStoreAppListVisibility");
            }
        }

        public Visibility MicrosoftStoreAppInfoVisibility
        {
            get { return microsoftStoreAppInfoVisibility; }
            set
            {
                microsoftStoreAppInfoVisibility = value;
                OnPropertyChanged("MicrosoftStoreAppInfoVisibility");
            }
        }


        public Visibility ApplicationListVisibility
        {
            get { return applicationListVisibility; }
            set
            {
                applicationListVisibility = value;
                OnPropertyChanged("ApplicationListVisibility");
            }
        }

        public Visibility ApplicationInfoVisibility
        {
            get { return applicationInfoVisibility; }
            set
            {
                applicationInfoVisibility = value;
                OnPropertyChanged("ApplicationInfoVisibility");
            }
        }


        public Visibility SettingsListVisibility
        {
            get { return settingsListVisibility; }
            set
            {
                settingsListVisibility = value;
                OnPropertyChanged("SettingsListVisibility");
            }
        }

        public Visibility SettingsInfoVisibility
        {
            get { return settingsInfoVisibility; }
            set
            {
                settingsInfoVisibility = value;
                OnPropertyChanged("SettingsInfoVisibility");
            }
        }


        public Visibility SystemListVisibility
        {
            get { return systemListVisibility; }
            set
            {
                systemListVisibility = value;
                OnPropertyChanged("SystemListVisibility");
            }
        }

        public Visibility SystemInfoVisibility
        {
            get { return systemInfoVisibility; }
            set
            {
                systemInfoVisibility = value;
                OnPropertyChanged("SystemInfoVisibility");
            }
        }


        public Visibility WebServiceListVisibility
        {
            get { return webServiceListVisibility; }
            set
            {
                webServiceListVisibility = value;
                OnPropertyChanged("WebServiceListVisibility");
            }
        }

        public Visibility WebServiceInfoVisibility
        {
            get { return webServiceInfoVisibility; }
            set
            {
                webServiceInfoVisibility = value;
                OnPropertyChanged("WebServiceInfoVisibility");
            }
        }


        public Visibility NotFoundIllustrationVisibility
        {
            get { return notFoundIllustrationVisibility; }
            set
            {
                notFoundIllustrationVisibility = value;
                OnPropertyChanged("NotFoundIllustrationVisibility");
            }
        }

        public Visibility ResultVisibility
        {
            get { return resultVisibility; }
            set
            {
                resultVisibility = value;
                OnPropertyChanged("ResultVisibility");
            }
        }

        public string PostRequestString
        {
            get { return postRequestString; }
            set
            {
                postRequestString = value;
                OnPropertyChanged("PostRequestString");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public GlobalViewModel()
        {
            Update();
        }

        public void Update()
        {
            MathExpressionListOrder = -1;
            FileListOrder = -1;
            FolderListOrder = -1;
            MicrosoftStoreAppListOrder = -1;
            ApplicationListOrder = -1;
            SettingsListOrder = -1;
            SystemListOrder = -1;
            WebServiceListOrder = -1;

            MathExpressionListAviliable = false;
            FileListAviliable = false;
            FolderListAviliable = false;
            MicrosoftStoreAppListAviliable = false;
            ApplicationListAviliable = false;
            SettingsListAviliable = false;
            SystemListAviliable = false;
            WebServiceListAviliable = false;

            MathExpressionListSelected = false;
            FileListSelected = false;
            FolderListSelected = false;
            MicrosoftStoreAppListSelected = false;
            ApplicationListSelected = false;
            SettingsListSelected = false;
            SystemListSelected = false;
            WebServiceListSelected = false;

            MathExpressionListVisibility = Visibility.Collapsed;
            MathExpressionInfoVisibility = Visibility.Collapsed;

            FileListVisibility = Visibility.Collapsed;
            FileInfoVisibility = Visibility.Collapsed;

            FolderListVisibility = Visibility.Collapsed;
            FolderInfoVisibility = Visibility.Collapsed;

            MicrosoftStoreAppListVisibility = Visibility.Collapsed;
            MicrosoftStoreAppInfoVisibility = Visibility.Collapsed;

            ApplicationListVisibility = Visibility.Collapsed;
            ApplicationInfoVisibility = Visibility.Collapsed;

            SettingsListVisibility = Visibility.Collapsed;
            SettingsInfoVisibility = Visibility.Collapsed;

            SystemListVisibility = Visibility.Collapsed;
            SystemInfoVisibility = Visibility.Collapsed;

            WebServiceListVisibility = Visibility.Collapsed;
            WebServiceInfoVisibility = Visibility.Collapsed;

            NotFoundIllustrationVisibility = Visibility.Collapsed;
            ResultVisibility = Visibility.Collapsed;
            //firstItemInfoShown = false;
            //lastListOrder = -1;
        }

        public void LaunchSelectedItem()
        {
            ListBox mathExpressionListBox = (Application.Current.MainWindow as MainWindow).MathExpressionListBox;
            ListBox fileListBox = (Application.Current.MainWindow as MainWindow).FileListBox;
            ListBox folderListBox = (Application.Current.MainWindow as MainWindow).FolderListBox;
            ListBox microsoftStoreAppListBox = (Application.Current.MainWindow as MainWindow).MicrosoftStoreAppListBox;
            ListBox applicationListBox = (Application.Current.MainWindow as MainWindow).ApplicationListBox;
            ListBox settingsListBox = (Application.Current.MainWindow as MainWindow).SettingsListBox;
            ListBox systemsListBox = (Application.Current.MainWindow as MainWindow).SystemsListBox;
            ListBox webServiceListBox = (Application.Current.MainWindow as MainWindow).WebServiceListBox;

            if (MathExpressionListSelected == true)
                (mathExpressionListBox.SelectedItem as Models.MathExpression).Launch();
            else if (FileListSelected == true)
                (fileListBox.SelectedItem as Models.File).Launch();
            else if (FolderListSelected == true)
                (folderListBox.SelectedItem as Models.Folder).Launch();
            else if (MicrosoftStoreAppListSelected == true)
                (microsoftStoreAppListBox.SelectedItem as Models.MicrosoftStoreApplication).Launch();
            else if (ApplicationListSelected == true)
                (applicationListBox.SelectedItem as Models.Application).Launch();
            else if (SettingsListSelected == true)
                (settingsListBox.SelectedItem as Models.Settings).Launch();
            else if (SystemListSelected == true)
                (systemsListBox.SelectedItem as Models.System).Launch();
            else if (WebServiceListSelected == true)
                (webServiceListBox.SelectedItem as Models.WebService).Launch();
        }

        public void ActivateMathExpressionList()
        {
            MathExpressionListAviliable = true;
            MathExpressionListVisibility = Visibility.Visible;
        }

        public void ActivateFileList()
        {
            FileListAviliable = true;
            FileListVisibility = Visibility.Visible;
        }

        public void ActivateFolderList()
        {
            FolderListAviliable = true;
            FolderListVisibility = Visibility.Visible;
        }

        public void ActivateMicrosoftStoreAppList()
        {
            MicrosoftStoreAppListAviliable = true;
            MicrosoftStoreAppListVisibility = Visibility.Visible;
        }

        public void ActivateApplicationList()
        {
            ApplicationListAviliable = true;
            ApplicationListVisibility = Visibility.Visible;
        }

        public void ActivateSettingsList()
        {
            SettingsListAviliable = true;
            SettingsListVisibility = Visibility.Visible;
        }

        public void ActivateSystemServiceList()
        {
            SystemListAviliable = true;
            SystemListVisibility = Visibility.Visible;
        }

        public void ActivateWebServiceList()
        {
            WebServiceListAviliable = true;
            WebServiceListVisibility = Visibility.Visible;
        }

        public void ShowResult()
        {
            ResultVisibility = Visibility.Visible;
            Application.Current.MainWindow.UpdateLayout();

            Application.Current.Dispatcher.Invoke(() =>
            { (Application.Current.MainWindow as MainWindow).RequestTextBox.Tag = "Filled"; });

            string[] modelNames = new string[] {
                "MathExpression",
                "File",
                "Folder",
                "MicrosoftStoreApp",
                "Application",
                "Settings",
                "System",
                "WebService" };

            int[] modelIndexes = new int[]
            {
                MathExpressionListOrder,
                FileListOrder,
                FolderListOrder,
                MicrosoftStoreAppListOrder,
                ApplicationListOrder,
                SettingsListOrder,
                SystemListOrder,
                WebServiceListOrder
            };

            int currentIndex;
            string currentName;
            for (int i = 1; i < modelIndexes.Length; i++)
            {
                currentIndex = modelIndexes[i];
                currentName = modelNames[i];
                int j = i;
                while (j > 0 && currentIndex < modelIndexes[j - 1])
                {
                    modelIndexes[j] = modelIndexes[j - 1];
                    modelNames[j] = modelNames[j - 1];
                    j--;
                }
                modelIndexes[j] = currentIndex;
                modelNames[j] = currentName;
            }

            for (int i = 0; i < modelIndexes.Length; i++)
            {
                if (modelIndexes[i] > -1)
                {
                    switch (modelNames[i])
                    {
                        case "MathExpression":
                            FirstItemSelector.SelectFirstMathExpression();
                            break;
                        case "File":
                            FirstItemSelector.SelectFirstFile();
                            break;
                        case "Folder":
                            FirstItemSelector.SelectFirstFolder();
                            break;
                        case "MicrosoftStoreApp":
                            FirstItemSelector.SelectFirstMicrosoftStoreApp();
                            break;
                        case "Application":
                            FirstItemSelector.SelectFirstApplication();
                            break;
                        case "Settings":
                            FirstItemSelector.SelectFirstSettings();
                            break;
                        case "System":
                            FirstItemSelector.SelectFirstSystem();
                            break;
                        case "WebService":
                            FirstItemSelector.SelectFirstWebService();
                            break;
                    }
                    break;
                }
            }

            Application.Current.Dispatcher.Invoke(() =>
            { (Application.Current.MainWindow as MainWindow).GlobalScrollViewer.ScrollToTop(); });
        }

        public void NotFoundReaction()
        {
            ResultVisibility = Visibility.Collapsed;
            NotFoundIllustrationVisibility = Visibility.Visible;
        }

        public void SetPostRequestString(Models.MathExpression mathExpression)
        {
            PostRequestString = $" = {mathExpression.Result}";
        }

        public void SetPostRequestString(Models.File file)
        {
            string request = (Application.Current.MainWindow as MainWindow).RequestTextBox.Text;
            if (file.Name.ToLower().StartsWith(request.ToLower()))
                PostRequestString = file.Name.Substring(request.Length, file.Name.Length - request.Length);
            else if (file.Name.ToLower().Contains(request.ToLower()))
                PostRequestString = $" − {file.Name}";
        }

        public void SetPostRequestString(Models.MicrosoftStoreApplication microsoftStoreApplication)
        {
            string request = (Application.Current.MainWindow as MainWindow).RequestTextBox.Text;
            if (microsoftStoreApplication.Name.ToLower().StartsWith(request.ToLower()))
                PostRequestString = microsoftStoreApplication.Name.Substring(request.Length, microsoftStoreApplication.Name.Length - request.Length);
            else if (microsoftStoreApplication.Name.ToLower().Contains(request.ToLower()))
                PostRequestString = $" − {microsoftStoreApplication.Name}";
        }
    }
}
