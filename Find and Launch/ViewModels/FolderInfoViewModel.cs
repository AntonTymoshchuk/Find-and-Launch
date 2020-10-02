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
    public class FolderInfoViewModel : InfoViewModel, IInfoViewModel
    {
        private string fullName;
        private string owner;
        private string computer;
        private string attributes;
        private string creationTime;
        private string lastAccessTime;
        private string lastWriteTime;
        private string elements;
        private string size;

        public string FullName
        {
            get { return fullName; }
            set
            {
                fullName = value;
                OnPropertyChanged("FullName");
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

        public string Elements
        {
            get { return elements; }
            set
            {
                elements = value;
                OnPropertyChanged("Elements");
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

        public FolderInfoViewModel() { }

        public void LoadInfo(object source)
        {
            Models.Folder folder = source as Models.Folder;
            Type = folder.Type;
            Name = folder.Name;
            FullName = folder.FullName;
            Owner = folder.Owner;
            Computer = folder.Computer;
            Attributes = folder.Attributes;
            CreationTime = folder.CreationTime;
            LastAccessTime = folder.LastAccessTime;
            LastWriteTime = folder.LastWriteTime;
            Elements = folder.Elements;
            Size = folder.Size;
            LargeImage = folder.LargeImage;
        }
    }
}
