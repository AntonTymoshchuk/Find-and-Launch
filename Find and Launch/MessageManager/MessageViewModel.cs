using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Find_and_Launch.MessageManager
{
    public class MessageViewModel : INotifyPropertyChanged
    {
        private BitmapSource image;
        private string message;

        public BitmapSource Image
        {
            get { return image; }
            set
            {
                image = value;
                OnPropertyChanged("Image");
            }
        }

        public string Message
        {
            get { return message; }
            set
            {
                message = value;
                OnPropertyChanged("Message");
            }
        }

        public void InitializeMessage(string message, MessageType messageType)
        {
            Message = message;
            switch (messageType)
            {
                case MessageType.Error:
                    Image = new BitmapImage(new Uri("/Images/Message/Error.png", UriKind.Relative));
                    break;
                case MessageType.Warning:
                    Image = new BitmapImage(new Uri("/Images/Message/Warning.png", UriKind.Relative));
                    break;
                case MessageType.Notification:
                    Image = new BitmapImage(new Uri("/Images/Message/Notification.png", UriKind.Relative));
                    break;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public enum MessageType
    {
        Error, Warning, Notification
    }
}
