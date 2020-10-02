using Find_and_Launch.Abstract;
using Find_and_Launch.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Find_and_Launch.ViewModels
{
    public class ApplicationInfoViewModel : InfoViewModel, IInfoViewModel
    {
        private string version;
        private string description;
        private string language;
        private string publisher;
        private string fullLnkName;
        private string fullExeName;
        private string owner;
        private string computer;
        private string creationTime;
        private string lastAccessTime;
        private string lastWriteTime;
        private string size;

        public string Version
        {
            get { return version; }
            set
            {
                version = value;
                OnPropertyChanged("Version");
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

        public string Language
        {
            get { return language; }
            set
            {
                language = value;
                OnPropertyChanged("Language");
            }
        }

        public string Publisher
        {
            get { return publisher; }
            set
            {
                publisher = value;
                OnPropertyChanged("Publisher");
            }
        }

        public string FullLnkName
        {
            get { return fullLnkName; }
            set
            {
                fullLnkName = value;
                OnPropertyChanged("FullInkName");
            }
        }

        public string FullExeName
        {
            get { return fullExeName; }
            set
            {
                fullExeName = value;
                OnPropertyChanged("FullExeName");
            }
        }

        public string Owner
        {
            get { return owner; }
            set
            {
                owner = value;
                OnPropertyChanged("Owner");
            }
        }

        public string Computer
        {
            get { return computer; }
            set
            {
                computer = value;
                OnPropertyChanged("Computer");
            }
        }

        public string CreationTime
        {
            get { return creationTime; }
            set
            {
                creationTime = value;
                OnPropertyChanged("CreationTime");
            }
        }

        public string LastAccessTime
        {
            get { return lastAccessTime; }
            set
            {
                lastAccessTime = value;
                OnPropertyChanged("LastAccessTime");
            }
        }

        public string LastWriteTime
        {
            get { return lastWriteTime; }
            set
            {
                lastWriteTime = value;
                OnPropertyChanged("LastWriteTime");
            }
        }

        public string Size
        {
            get { return size; }
            set
            {
                size = value;
                OnPropertyChanged("Size");
            }
        }

        public ApplicationInfoViewModel() { }

        public void LoadInfo(object source)
        {
            Models.Application application = source as Models.Application;
            Type = application.Type;
            Name = application.Name;
            Version = application.Version;
            Description = application.Description;
            Language = application.Language;
            Publisher = application.Publisher;
            FullLnkName = application.FullLnkName;
            FullExeName = application.FullExeName;
            Owner = application.Owner;
            Computer = application.Computer;
            CreationTime = application.CreationTime;
            LastAccessTime = application.LastAccessTime;
            LastWriteTime = application.LastWriteTime;
            Size = application.Size;
            LargeImage = application.LargeImage;
        }
    }
}
