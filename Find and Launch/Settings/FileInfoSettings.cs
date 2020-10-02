using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Find_and_Launch.Settings
{
    public class FileInfoSettings
    {
        public Visibility ExtensionVisibility { get; set; }
        public Visibility OwnerVisibility { get; set; }
        public Visibility ComputerVisibility { get; set; }
        public Visibility AttributesVisibility { get; set; }
        public Visibility CreationTimeVisibility { get; set; }
        public Visibility LastAccessTimeVisibility { get; set; }
        public Visibility LastWriteTimeVisibility { get; set; }
        public Visibility SizeVisibility { get; set; }

        public Visibility LineVisibility
        {
            get
            {
                int visibleInfoCount = 0;
                if (ExtensionVisibility == Visibility.Visible)
                    visibleInfoCount++;
                if (OwnerVisibility == Visibility.Visible)
                    visibleInfoCount++;
                if (AttributesVisibility == Visibility.Visible)
                    visibleInfoCount++;
                if (CreationTimeVisibility == Visibility.Visible)
                    visibleInfoCount++;
                if (LastAccessTimeVisibility == Visibility.Visible)
                    visibleInfoCount++;
                if (LastWriteTimeVisibility == Visibility.Visible)
                    visibleInfoCount++;
                if (SizeVisibility == Visibility.Visible)
                    visibleInfoCount++;
                if (visibleInfoCount == 0)
                    return Visibility.Collapsed;
                return Visibility.Visible;
            }
        }

        public FileInfoSettings()
        {
            ExtensionVisibility = Visibility.Visible;
            OwnerVisibility = Visibility.Visible;
            ComputerVisibility = Visibility.Visible;
            AttributesVisibility = Visibility.Visible;
            CreationTimeVisibility = Visibility.Visible;
            LastAccessTimeVisibility = Visibility.Visible;
            LastWriteTimeVisibility = Visibility.Visible;
            SizeVisibility = Visibility.Visible;
        }

        public FileInfoSettings(string[] settings)
        {
            ExtensionVisibility = (Visibility)Convert.ToInt32(settings[25]);
            OwnerVisibility = (Visibility)Convert.ToInt32(settings[26]);
            ComputerVisibility = (Visibility)Convert.ToInt32(settings[27]);
            AttributesVisibility = (Visibility)Convert.ToInt32(settings[28]);
            CreationTimeVisibility = (Visibility)Convert.ToInt32(settings[29]);
            LastAccessTimeVisibility = (Visibility)Convert.ToInt32(settings[30]);
            LastWriteTimeVisibility = (Visibility)Convert.ToInt32(settings[31]);
            SizeVisibility = (Visibility)Convert.ToInt32(settings[32]);
        }

        public string Save()
        {
            string fileInfoSettings;
            fileInfoSettings = Convert.ToString((int)ExtensionVisibility) + "\r\n" +
                Convert.ToString((int)OwnerVisibility) + "\r\n" +
                Convert.ToString((int)ComputerVisibility) + "\r\n" +
                Convert.ToString((int)AttributesVisibility) + "\r\n" +
                Convert.ToString((int)CreationTimeVisibility) + "\r\n" +
                Convert.ToString((int)LastAccessTimeVisibility) + "\r\n" +
                Convert.ToString((int)LastWriteTimeVisibility) + "\r\n" +
                Convert.ToString((int)SizeVisibility);
            return fileInfoSettings;
        }
    }
}
