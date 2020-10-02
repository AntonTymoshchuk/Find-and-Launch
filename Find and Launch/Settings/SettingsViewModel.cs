using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Find_and_Launch.Settings
{
    public class SettingsViewModel : INotifyPropertyChanged
    {
        private bool searchForFiles;
        private bool searchForFolders;
        private bool searchForApplications;
        private bool searchForSettings;
        private bool searchForSystemServices;
        private bool searchForGoogleServices;

        public bool SearchForFiles
        {
            get { return searchForFiles; }
            set
            {
                searchForFiles = value;
                GlobalSettings.SearchForFiles = value;
                OnPropertyChanged("SearchForFiles");
            }
        }

        public bool SearchForFolders
        {
            get { return searchForFolders; }
            set
            {
                searchForFolders = value;
                GlobalSettings.SearchForFolders = value;
                OnPropertyChanged("SearchForFolders");
            }
        }

        public bool SearchForApplications
        {
            get { return searchForApplications; }
            set
            {
                searchForApplications = value;
                GlobalSettings.SearchForApplications = value;
                OnPropertyChanged("SearchForApplications");
            }
        }

        public bool SearchForSettings
        {
            get { return searchForSettings; }
            set
            {
                searchForSettings = value;
                GlobalSettings.SearchForSettings = value;
                OnPropertyChanged("SearchForSettings");
            }
        }

        public bool SearchForSystemServices
        {
            get { return searchForSystemServices; }
            set
            {
                searchForSystemServices = value;
                GlobalSettings.SearchForSystemServices = value;
                OnPropertyChanged("SearchForSystemServices");
            }
        }

        public bool SearchForGoogleServices
        {
            get { return searchForGoogleServices; }
            set
            {
                searchForGoogleServices = value;
                GlobalSettings.SearchForGoogleWebServices = value;
                OnPropertyChanged("SearchForGoogleServices");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public SettingsViewModel()
        {
            SearchForFiles = GlobalSettings.SearchForFiles;
            SearchForFolders = GlobalSettings.SearchForFolders;
            SearchForApplications = GlobalSettings.SearchForApplications;
            SearchForSettings = GlobalSettings.SearchForSettings;
            SearchForSystemServices = GlobalSettings.SearchForSystemServices;
            SearchForGoogleServices = GlobalSettings.SearchForGoogleWebServices;
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
