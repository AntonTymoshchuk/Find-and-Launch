using Find_and_Launch.Abstract;
using Find_and_Launch.Interfaces;
using Find_and_Launch.Settings;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Find_and_Launch.ViewModels
{
    public class FileInfoViewModel : InfoViewModel, IInfoViewModel
    {
        private string fullName;
        private string extension;
        private string owner;
        private string computer;
        private string attributes;
        private string creationTime;
        private string lastAccessTime;
        private string lastWriteTime;
        private string size;
        private double largeImageWidth;
        private double largeImageHeight;

        private Visibility lineVisibility;
        private Visibility extensionVisibility;
        private Visibility ownerVisibility;
        private Visibility computerVisibility;
        private Visibility attributesVisibility;
        private Visibility creationTimeVisibility;
        private Visibility lastAccessTimeVisibility;
        private Visibility lastWriteTimeVisibility;
        private Visibility sizeVisibility;

        public string FullName
        {
            get { return fullName; }
            set
            {
                fullName = value;
                OnPropertyChanged("FullName");
            }
        }

        public string Extension
        {
            get { return extension; }
            set
            {
                extension = value;
                OnPropertyChanged("Extension");
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

        public string Attributes
        {
            get { return attributes; }
            set
            {
                attributes = value;
                OnPropertyChanged("Attributes");
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

        public double LargeImageWidth
        {
            get { return largeImageWidth; }
            set
            {
                largeImageWidth = value;
                OnPropertyChanged("LargeImageWidth");
            }
        }

        public double LargeImageHeight
        {
            get { return largeImageHeight; }
            set
            {
                largeImageHeight = value;
                OnPropertyChanged("LargeImageHeight");
            }
        }

        public Visibility LineVisibility
        {
            get { return lineVisibility; }
            set
            {
                lineVisibility = value;
                OnPropertyChanged("LineVisibility");
            }
        }

        public Visibility ExtensionVisibility
        {
            get { return extensionVisibility; }
            set
            {
                extensionVisibility = value;
                OnPropertyChanged("ExtensionVisibility");
            }
        }

        public Visibility OwnerVisibility
        {
            get { return ownerVisibility; }
            set
            {
                ownerVisibility = value;
                OnPropertyChanged("OwnerVisibility");
            }
        }

        public Visibility ComputerVisibility
        {
            get { return computerVisibility; }
            set
            {
                computerVisibility = value;
                OnPropertyChanged("ComputerVisibility");
            }
        }

        public Visibility AttributesVisibility
        {
            get { return attributesVisibility; }
            set
            {
                attributesVisibility = value;
                OnPropertyChanged("AttributesVisibility");
            }
        }

        public Visibility CreationTimeVisibility
        {
            get { return creationTimeVisibility; }
            set
            {
                creationTimeVisibility = value;
                OnPropertyChanged("CreationTimeVisibility");
            }
        }

        public Visibility LastAccessTimeVisibility
        {
            get { return lastAccessTimeVisibility; }
            set
            {
                lastAccessTimeVisibility = value;
                OnPropertyChanged("LastAccessTimeVisibility");
            }
        }

        public Visibility LastWriteTimeVisibility
        {
            get { return lastWriteTimeVisibility; }
            set
            {
                lastWriteTimeVisibility = value;
                OnPropertyChanged("LastWriteTimeVisibility");
            }
        }

        public Visibility SizeVisibility
        {
            get { return sizeVisibility; }
            set
            {
                sizeVisibility = value;
                OnPropertyChanged("SizeVisibility");
            }
        }

        public FileInfoViewModel() { }

        public void LoadInfo(object source)
        {
            Models.File file = source as Models.File;
            Type = file.Type;
            Name = file.Name;
            FullName = file.FullName;
            Extension = file.Extension;
            Owner = file.Owner;
            Computer = file.Computer;
            Attributes = file.Attributes;
            CreationTime = file.CreationTime;
            LastAccessTime = file.LastAccessTime;
            LastWriteTime = file.LastWriteTime;
            Size = file.Size;
            LargeImage = file.LargeImage;

            if (LargeImage.Height > 100)
                LargeImageHeight = 100;
            else LargeImageHeight = LargeImage.Height;
            double k = LargeImage.Height / LargeImageHeight;
            LargeImageWidth = LargeImage.Width / k;
            if (LargeImageWidth > 360)
            {
                LargeImageWidth = 360;
                k = LargeImage.Width / LargeImageWidth;
                LargeImageHeight = LargeImage.Height / k;
            }

            ExtensionVisibility = GlobalSettings.FileInfoSettings.ExtensionVisibility;
            OwnerVisibility = GlobalSettings.FileInfoSettings.OwnerVisibility;
            ComputerVisibility = GlobalSettings.FileInfoSettings.ComputerVisibility;
            AttributesVisibility = GlobalSettings.FileInfoSettings.AttributesVisibility;
            CreationTimeVisibility = GlobalSettings.FileInfoSettings.CreationTimeVisibility;
            LastAccessTimeVisibility = GlobalSettings.FileInfoSettings.LastAccessTimeVisibility;
            LastWriteTimeVisibility = GlobalSettings.FileInfoSettings.LastWriteTimeVisibility;
            SizeVisibility = GlobalSettings.FileInfoSettings.SizeVisibility;
            LineVisibility = GlobalSettings.FileInfoSettings.LineVisibility;
        }
    }
}
