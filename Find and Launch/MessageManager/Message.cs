using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Find_and_Launch.MessageManager
{
    public abstract class Message
    {
        public static void Show(string message, MessageType messageType)
        {
            MessageWindow messageWindow = new MessageWindow();
            messageWindow.ShowDialog(message, messageType);
        }
    }
}
