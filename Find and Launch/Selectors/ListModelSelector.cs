using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Find_and_Launch.Selectors
{
    public abstract class ListModelSelector
    {
        public static void SelectMathExpressionList()
        {
            if ((Application.Current.MainWindow as MainWindow).GlobalViewModel.MathExpressionListSelected == false)
            {
                ListBox fileListBox = (Application.Current.MainWindow as MainWindow).FileListBox;
                ListBox folderListBox = (Application.Current.MainWindow as MainWindow).FolderListBox;
                ListBox microsoftStoreAppListBox = (Application.Current.MainWindow as MainWindow).MicrosoftStoreAppListBox;
                ListBox applicationListBox = (Application.Current.MainWindow as MainWindow).ApplicationListBox;
                ListBox settingsListBox = (Application.Current.MainWindow as MainWindow).SettingsListBox;
                ListBox systemsListBox = (Application.Current.MainWindow as MainWindow).SystemsListBox;
                ListBox webServiceListBox = (Application.Current.MainWindow as MainWindow).WebServiceListBox;

                (Application.Current.MainWindow as MainWindow).GlobalViewModel.MathExpressionListSelected = true;
                (Application.Current.MainWindow as MainWindow).GlobalViewModel.FileListSelected = false;
                (Application.Current.MainWindow as MainWindow).GlobalViewModel.FolderListSelected = false;
                (Application.Current.MainWindow as MainWindow).GlobalViewModel.MicrosoftStoreAppListSelected = false;
                (Application.Current.MainWindow as MainWindow).GlobalViewModel.ApplicationListSelected = false;
                (Application.Current.MainWindow as MainWindow).GlobalViewModel.SettingsListSelected = false;
                (Application.Current.MainWindow as MainWindow).GlobalViewModel.SystemListSelected = false;
                (Application.Current.MainWindow as MainWindow).GlobalViewModel.WebServiceListSelected = false;

                fileListBox.UnselectAll();
                folderListBox.UnselectAll();
                microsoftStoreAppListBox.UnselectAll();
                applicationListBox.UnselectAll();
                settingsListBox.UnselectAll();
                systemsListBox.UnselectAll();
                webServiceListBox.UnselectAll();
            }
        }

        public static void SelectFileList()
        {
            if ((Application.Current.MainWindow as MainWindow).GlobalViewModel.FileListSelected == false)
            {
                ListBox mathExpressionListBox = (Application.Current.MainWindow as MainWindow).MathExpressionListBox;
                ListBox folderListBox = (Application.Current.MainWindow as MainWindow).FolderListBox;
                ListBox microsoftStoreAppListBox = (Application.Current.MainWindow as MainWindow).MicrosoftStoreAppListBox;
                ListBox applicationListBox = (Application.Current.MainWindow as MainWindow).ApplicationListBox;
                ListBox settingsListBox = (Application.Current.MainWindow as MainWindow).SettingsListBox;
                ListBox systemsListBox = (Application.Current.MainWindow as MainWindow).SystemsListBox;
                ListBox webServiceListBox = (Application.Current.MainWindow as MainWindow).WebServiceListBox;

                (Application.Current.MainWindow as MainWindow).GlobalViewModel.MathExpressionListSelected = false;
                (Application.Current.MainWindow as MainWindow).GlobalViewModel.FileListSelected = true;
                (Application.Current.MainWindow as MainWindow).GlobalViewModel.FolderListSelected = false;
                (Application.Current.MainWindow as MainWindow).GlobalViewModel.MicrosoftStoreAppListSelected = false;
                (Application.Current.MainWindow as MainWindow).GlobalViewModel.ApplicationListSelected = false;
                (Application.Current.MainWindow as MainWindow).GlobalViewModel.SettingsListSelected = false;
                (Application.Current.MainWindow as MainWindow).GlobalViewModel.SystemListSelected = false;
                (Application.Current.MainWindow as MainWindow).GlobalViewModel.WebServiceListSelected = false;

                mathExpressionListBox.UnselectAll();
                folderListBox.UnselectAll();
                microsoftStoreAppListBox.UnselectAll();
                applicationListBox.UnselectAll();
                settingsListBox.UnselectAll();
                systemsListBox.UnselectAll();
                webServiceListBox.UnselectAll();
            }
        }

        public static void SelectFolderList()
        {
            if ((Application.Current.MainWindow as MainWindow).GlobalViewModel.FolderListSelected == false)
            {
                ListBox mathExpressionListBox = (Application.Current.MainWindow as MainWindow).MathExpressionListBox;
                ListBox fileListBox = (Application.Current.MainWindow as MainWindow).FileListBox;
                ListBox microsoftStoreAppListBox = (Application.Current.MainWindow as MainWindow).MicrosoftStoreAppListBox;
                ListBox applicationListBox = (Application.Current.MainWindow as MainWindow).ApplicationListBox;
                ListBox settingsListBox = (Application.Current.MainWindow as MainWindow).SettingsListBox;
                ListBox systemsListBox = (Application.Current.MainWindow as MainWindow).SystemsListBox;
                ListBox webServiceListBox = (Application.Current.MainWindow as MainWindow).WebServiceListBox;

                (Application.Current.MainWindow as MainWindow).GlobalViewModel.MathExpressionListSelected = false;
                (Application.Current.MainWindow as MainWindow).GlobalViewModel.FileListSelected = false;
                (Application.Current.MainWindow as MainWindow).GlobalViewModel.FolderListSelected = true;
                (Application.Current.MainWindow as MainWindow).GlobalViewModel.MicrosoftStoreAppListSelected = false;
                (Application.Current.MainWindow as MainWindow).GlobalViewModel.ApplicationListSelected = false;
                (Application.Current.MainWindow as MainWindow).GlobalViewModel.SettingsListSelected = false;
                (Application.Current.MainWindow as MainWindow).GlobalViewModel.SystemListSelected = false;
                (Application.Current.MainWindow as MainWindow).GlobalViewModel.WebServiceListSelected = false;

                mathExpressionListBox.UnselectAll();
                fileListBox.UnselectAll();
                microsoftStoreAppListBox.UnselectAll();
                applicationListBox.UnselectAll();
                settingsListBox.UnselectAll();
                systemsListBox.UnselectAll();
                webServiceListBox.UnselectAll();
            }
        }

        public static void SelectMicrosoftStoreAppList()
        {
            if ((Application.Current.MainWindow as MainWindow).GlobalViewModel.ApplicationListSelected == false)
            {
                ListBox mathExpressionListBox = (Application.Current.MainWindow as MainWindow).MathExpressionListBox;
                ListBox fileListBox = (Application.Current.MainWindow as MainWindow).FileListBox;
                ListBox folderListBox = (Application.Current.MainWindow as MainWindow).FolderListBox;
                ListBox applicationListBox = (Application.Current.MainWindow as MainWindow).ApplicationListBox;
                ListBox settingsListBox = (Application.Current.MainWindow as MainWindow).SettingsListBox;
                ListBox systemsListBox = (Application.Current.MainWindow as MainWindow).SystemsListBox;
                ListBox webServiceListBox = (Application.Current.MainWindow as MainWindow).WebServiceListBox;

                (Application.Current.MainWindow as MainWindow).GlobalViewModel.MathExpressionListSelected = false;
                (Application.Current.MainWindow as MainWindow).GlobalViewModel.FileListSelected = false;
                (Application.Current.MainWindow as MainWindow).GlobalViewModel.FolderListSelected = false;
                (Application.Current.MainWindow as MainWindow).GlobalViewModel.MicrosoftStoreAppListSelected = true;
                (Application.Current.MainWindow as MainWindow).GlobalViewModel.ApplicationListSelected = false;
                (Application.Current.MainWindow as MainWindow).GlobalViewModel.SettingsListSelected = false;
                (Application.Current.MainWindow as MainWindow).GlobalViewModel.SystemListSelected = false;
                (Application.Current.MainWindow as MainWindow).GlobalViewModel.WebServiceListSelected = false;

                mathExpressionListBox.UnselectAll();
                fileListBox.UnselectAll();
                folderListBox.UnselectAll();
                applicationListBox.UnselectAll();
                settingsListBox.UnselectAll();
                systemsListBox.UnselectAll();
                webServiceListBox.UnselectAll();
            }
        }

        public static void SelectApplicationList()
        {
            if ((Application.Current.MainWindow as MainWindow).GlobalViewModel.ApplicationListSelected == false)
            {
                ListBox mathExpressionListBox = (Application.Current.MainWindow as MainWindow).MathExpressionListBox;
                ListBox fileListBox = (Application.Current.MainWindow as MainWindow).FileListBox;
                ListBox folderListBox = (Application.Current.MainWindow as MainWindow).FolderListBox;
                ListBox microsoftStoreAppListBox = (Application.Current.MainWindow as MainWindow).MicrosoftStoreAppListBox;
                ListBox settingsListBox = (Application.Current.MainWindow as MainWindow).SettingsListBox;
                ListBox systemsListBox = (Application.Current.MainWindow as MainWindow).SystemsListBox;
                ListBox webServiceListBox = (Application.Current.MainWindow as MainWindow).WebServiceListBox;

                (Application.Current.MainWindow as MainWindow).GlobalViewModel.MathExpressionListSelected = false;
                (Application.Current.MainWindow as MainWindow).GlobalViewModel.FileListSelected = false;
                (Application.Current.MainWindow as MainWindow).GlobalViewModel.FolderListSelected = false;
                (Application.Current.MainWindow as MainWindow).GlobalViewModel.MicrosoftStoreAppListSelected = false;
                (Application.Current.MainWindow as MainWindow).GlobalViewModel.ApplicationListSelected = true;
                (Application.Current.MainWindow as MainWindow).GlobalViewModel.SettingsListSelected = false;
                (Application.Current.MainWindow as MainWindow).GlobalViewModel.SystemListSelected = false;
                (Application.Current.MainWindow as MainWindow).GlobalViewModel.WebServiceListSelected = false;

                mathExpressionListBox.UnselectAll();
                fileListBox.UnselectAll();
                folderListBox.UnselectAll();
                microsoftStoreAppListBox.UnselectAll();
                settingsListBox.UnselectAll();
                systemsListBox.UnselectAll();
                webServiceListBox.UnselectAll();
            }
        }

        public static void SelectSettingsList()
        {
            if ((Application.Current.MainWindow as MainWindow).GlobalViewModel.SettingsListSelected == false)
            {
                ListBox mathExpressionListBox = (Application.Current.MainWindow as MainWindow).MathExpressionListBox;
                ListBox fileListBox = (Application.Current.MainWindow as MainWindow).FileListBox;
                ListBox folderListBox = (Application.Current.MainWindow as MainWindow).FolderListBox;
                ListBox microsoftStoreAppListBox = (Application.Current.MainWindow as MainWindow).MicrosoftStoreAppListBox;
                ListBox applicationListBox = (Application.Current.MainWindow as MainWindow).ApplicationListBox;
                ListBox systemsListBox = (Application.Current.MainWindow as MainWindow).SystemsListBox;
                ListBox webServiceListBox = (Application.Current.MainWindow as MainWindow).WebServiceListBox;

                (Application.Current.MainWindow as MainWindow).GlobalViewModel.MathExpressionListSelected = false;
                (Application.Current.MainWindow as MainWindow).GlobalViewModel.FileListSelected = false;
                (Application.Current.MainWindow as MainWindow).GlobalViewModel.FolderListSelected = false;
                (Application.Current.MainWindow as MainWindow).GlobalViewModel.MicrosoftStoreAppListSelected = false;
                (Application.Current.MainWindow as MainWindow).GlobalViewModel.ApplicationListSelected = false;
                (Application.Current.MainWindow as MainWindow).GlobalViewModel.SettingsListSelected = true;
                (Application.Current.MainWindow as MainWindow).GlobalViewModel.SystemListSelected = false;
                (Application.Current.MainWindow as MainWindow).GlobalViewModel.WebServiceListSelected = false;

                mathExpressionListBox.UnselectAll();
                fileListBox.UnselectAll();
                folderListBox.UnselectAll();
                microsoftStoreAppListBox.UnselectAll();
                applicationListBox.UnselectAll();
                systemsListBox.UnselectAll();
                webServiceListBox.UnselectAll();
            }
        }

        public static void SelectSystemList()
        {
            if ((Application.Current.MainWindow as MainWindow).GlobalViewModel.SystemListSelected == false)
            {
                ListBox mathExpressionListBox = (Application.Current.MainWindow as MainWindow).MathExpressionListBox;
                ListBox fileListBox = (Application.Current.MainWindow as MainWindow).FileListBox;
                ListBox folderListBox = (Application.Current.MainWindow as MainWindow).FolderListBox;
                ListBox microsoftStoreAppListBox = (Application.Current.MainWindow as MainWindow).MicrosoftStoreAppListBox;
                ListBox applicationListBox = (Application.Current.MainWindow as MainWindow).ApplicationListBox;
                ListBox settingsListBox = (Application.Current.MainWindow as MainWindow).SettingsListBox;
                ListBox webServiceListBox = (Application.Current.MainWindow as MainWindow).WebServiceListBox;

                (Application.Current.MainWindow as MainWindow).GlobalViewModel.MathExpressionListSelected = false;
                (Application.Current.MainWindow as MainWindow).GlobalViewModel.FileListSelected = false;
                (Application.Current.MainWindow as MainWindow).GlobalViewModel.FolderListSelected = false;
                (Application.Current.MainWindow as MainWindow).GlobalViewModel.MicrosoftStoreAppListSelected = false;
                (Application.Current.MainWindow as MainWindow).GlobalViewModel.ApplicationListSelected = false;
                (Application.Current.MainWindow as MainWindow).GlobalViewModel.SettingsListSelected = false;
                (Application.Current.MainWindow as MainWindow).GlobalViewModel.SystemListSelected = true;
                (Application.Current.MainWindow as MainWindow).GlobalViewModel.WebServiceListSelected = false;

                mathExpressionListBox.UnselectAll();
                fileListBox.UnselectAll();
                folderListBox.UnselectAll();
                microsoftStoreAppListBox.UnselectAll();
                applicationListBox.UnselectAll();
                settingsListBox.UnselectAll();
                webServiceListBox.UnselectAll();
            }
        }

        public static void SelectWebServiceList()
        {
            if ((Application.Current.MainWindow as MainWindow).GlobalViewModel.WebServiceListSelected == false)
            {
                ListBox mathExpressionListBox = (Application.Current.MainWindow as MainWindow).MathExpressionListBox;
                ListBox fileListBox = (Application.Current.MainWindow as MainWindow).FileListBox;
                ListBox folderListBox = (Application.Current.MainWindow as MainWindow).FolderListBox;
                ListBox microsoftStoreAppListBox = (Application.Current.MainWindow as MainWindow).MicrosoftStoreAppListBox;
                ListBox applicationListBox = (Application.Current.MainWindow as MainWindow).ApplicationListBox;
                ListBox settingsListBox = (Application.Current.MainWindow as MainWindow).SettingsListBox;
                ListBox systemsListBox = (Application.Current.MainWindow as MainWindow).SystemsListBox;

                (Application.Current.MainWindow as MainWindow).GlobalViewModel.MathExpressionListSelected = false;
                (Application.Current.MainWindow as MainWindow).GlobalViewModel.FileListSelected = false;
                (Application.Current.MainWindow as MainWindow).GlobalViewModel.FolderListSelected = false;
                (Application.Current.MainWindow as MainWindow).GlobalViewModel.MicrosoftStoreAppListSelected = false;
                (Application.Current.MainWindow as MainWindow).GlobalViewModel.ApplicationListSelected = false;
                (Application.Current.MainWindow as MainWindow).GlobalViewModel.SettingsListSelected = false;
                (Application.Current.MainWindow as MainWindow).GlobalViewModel.SystemListSelected = false;
                (Application.Current.MainWindow as MainWindow).GlobalViewModel.WebServiceListSelected = true;

                mathExpressionListBox.UnselectAll();
                fileListBox.UnselectAll();
                folderListBox.UnselectAll();
                microsoftStoreAppListBox.UnselectAll();
                applicationListBox.UnselectAll();
                settingsListBox.UnselectAll();
                systemsListBox.UnselectAll();
            }
        }
    }
}
