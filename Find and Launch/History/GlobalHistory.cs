using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Find_and_Launch.History
{
    public abstract class GlobalHistory
    {
        public static CommandHistory CommandHistory { get; private set; }
        public static FileHistory FileHistory { get; private set; }
        public static FolderHistory FolderHistory { get; private set; }
        public static ApplicationHistory ApplicationHistory { get; private set; }
        public static SettingsHistory SettingsHistory { get; private set; }

        public static void Read()
        {
            CommandHistory = new CommandHistory();
            FileHistory = new FileHistory();
            FolderHistory = new FolderHistory();
            ApplicationHistory = new ApplicationHistory();
            SettingsHistory = new SettingsHistory();
        }

        public static void Save()
        {
            CommandHistory.Save();
            FileHistory.Save();
            FolderHistory.Save();
            ApplicationHistory.Save();
            SettingsHistory.Save();
        }
    }
}
