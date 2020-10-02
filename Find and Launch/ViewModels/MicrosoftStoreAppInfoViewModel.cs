using Find_and_Launch.Abstract;
using Find_and_Launch.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Find_and_Launch.ViewModels
{
    public class MicrosoftStoreAppInfoViewModel : InfoViewModel, IInfoViewModel
    {
        private string publisher;
        private string version;
        private string architecture;
        private string description;

        public string Publisher
        {
            get { return publisher; }
            set
            {
                publisher = value;
                OnPropertyChanged("Publisher");
            }
        }

        public string Version
        {
            get { return version; }
            set
            {
                version = value;
                OnPropertyChanged("Version");
            }
        }

        public string Architecture
        {
            get { return architecture; }
            set
            {
                architecture = value;
                OnPropertyChanged("Architecture");
            }
        }

        public string Description
        {
            get { return description; }
            set
            {
                description = value;
                OnPropertyChanged("Description");
            }
        }

        public MicrosoftStoreAppInfoViewModel() { }

        public void LoadInfo(object source)
        {
            Models.MicrosoftStoreApplication microsoftStoreApplication = source as Models.MicrosoftStoreApplication;
            Type = microsoftStoreApplication.Type;
            Name = microsoftStoreApplication.Name;
            Publisher = microsoftStoreApplication.Publisher;
            Version = microsoftStoreApplication.Version;
            Architecture = microsoftStoreApplication.Architecture;
            Description = microsoftStoreApplication.Description;
            LargeImage = microsoftStoreApplication.LargeImage;
        }
    }
}
