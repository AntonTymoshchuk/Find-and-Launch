using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Find_and_Launch.Selectors
{
    public abstract class InfoModelSelector
    {
        public static void ShowMathExpressionInfo()
        {
            (Application.Current.MainWindow as MainWindow).GlobalViewModel.MathExpressionInfoVisibility = Visibility.Visible;
            (Application.Current.MainWindow as MainWindow).GlobalViewModel.FileInfoVisibility = Visibility.Collapsed;
            (Application.Current.MainWindow as MainWindow).GlobalViewModel.FolderInfoVisibility = Visibility.Collapsed;
            (Application.Current.MainWindow as MainWindow).GlobalViewModel.MicrosoftStoreAppInfoVisibility = Visibility.Collapsed;
            (Application.Current.MainWindow as MainWindow).GlobalViewModel.ApplicationInfoVisibility = Visibility.Collapsed;
            (Application.Current.MainWindow as MainWindow).GlobalViewModel.SettingsInfoVisibility = Visibility.Collapsed;
            (Application.Current.MainWindow as MainWindow).GlobalViewModel.SystemInfoVisibility = Visibility.Collapsed;
            (Application.Current.MainWindow as MainWindow).GlobalViewModel.WebServiceInfoVisibility = Visibility.Collapsed;
        }

        public static void ShowFileInfo()
        {
            (Application.Current.MainWindow as MainWindow).GlobalViewModel.MathExpressionInfoVisibility = Visibility.Collapsed;
            (Application.Current.MainWindow as MainWindow).GlobalViewModel.FileInfoVisibility = Visibility.Visible;
            (Application.Current.MainWindow as MainWindow).GlobalViewModel.FolderInfoVisibility = Visibility.Collapsed;
            (Application.Current.MainWindow as MainWindow).GlobalViewModel.MicrosoftStoreAppInfoVisibility = Visibility.Collapsed;
            (Application.Current.MainWindow as MainWindow).GlobalViewModel.ApplicationInfoVisibility = Visibility.Collapsed;
            (Application.Current.MainWindow as MainWindow).GlobalViewModel.SettingsInfoVisibility = Visibility.Collapsed;
            (Application.Current.MainWindow as MainWindow).GlobalViewModel.SystemInfoVisibility = Visibility.Collapsed;
            (Application.Current.MainWindow as MainWindow).GlobalViewModel.WebServiceInfoVisibility = Visibility.Collapsed;
        }

        public static void ShowFolderInfo()
        {
            (Application.Current.MainWindow as MainWindow).GlobalViewModel.MathExpressionInfoVisibility = Visibility.Collapsed;
            (Application.Current.MainWindow as MainWindow).GlobalViewModel.FileInfoVisibility = Visibility.Collapsed;
            (Application.Current.MainWindow as MainWindow).GlobalViewModel.FolderInfoVisibility = Visibility.Visible;
            (Application.Current.MainWindow as MainWindow).GlobalViewModel.MicrosoftStoreAppInfoVisibility = Visibility.Collapsed;
            (Application.Current.MainWindow as MainWindow).GlobalViewModel.ApplicationInfoVisibility = Visibility.Collapsed;
            (Application.Current.MainWindow as MainWindow).GlobalViewModel.SettingsInfoVisibility = Visibility.Collapsed;
            (Application.Current.MainWindow as MainWindow).GlobalViewModel.SystemInfoVisibility = Visibility.Collapsed;
            (Application.Current.MainWindow as MainWindow).GlobalViewModel.WebServiceInfoVisibility = Visibility.Collapsed;
        }

        public static void ShowMicrosoftStoreAppInfo()
        {
            (Application.Current.MainWindow as MainWindow).GlobalViewModel.MathExpressionInfoVisibility = Visibility.Collapsed;
            (Application.Current.MainWindow as MainWindow).GlobalViewModel.FileInfoVisibility = Visibility.Collapsed;
            (Application.Current.MainWindow as MainWindow).GlobalViewModel.FolderInfoVisibility = Visibility.Collapsed;
            (Application.Current.MainWindow as MainWindow).GlobalViewModel.MicrosoftStoreAppInfoVisibility = Visibility.Visible;
            (Application.Current.MainWindow as MainWindow).GlobalViewModel.ApplicationInfoVisibility = Visibility.Collapsed;
            (Application.Current.MainWindow as MainWindow).GlobalViewModel.SettingsInfoVisibility = Visibility.Collapsed;
            (Application.Current.MainWindow as MainWindow).GlobalViewModel.SystemInfoVisibility = Visibility.Collapsed;
            (Application.Current.MainWindow as MainWindow).GlobalViewModel.WebServiceInfoVisibility = Visibility.Collapsed;
        }

        public static void ShowApplicationInfo()
        {
            (Application.Current.MainWindow as MainWindow).GlobalViewModel.MathExpressionInfoVisibility = Visibility.Collapsed;
            (Application.Current.MainWindow as MainWindow).GlobalViewModel.FileInfoVisibility = Visibility.Collapsed;
            (Application.Current.MainWindow as MainWindow).GlobalViewModel.FolderInfoVisibility = Visibility.Collapsed;
            (Application.Current.MainWindow as MainWindow).GlobalViewModel.MicrosoftStoreAppInfoVisibility = Visibility.Collapsed;
            (Application.Current.MainWindow as MainWindow).GlobalViewModel.ApplicationInfoVisibility = Visibility.Visible;
            (Application.Current.MainWindow as MainWindow).GlobalViewModel.SettingsInfoVisibility = Visibility.Collapsed;
            (Application.Current.MainWindow as MainWindow).GlobalViewModel.SystemInfoVisibility = Visibility.Collapsed;
            (Application.Current.MainWindow as MainWindow).GlobalViewModel.WebServiceInfoVisibility = Visibility.Collapsed;
        }

        public static void ShowSettingsInfo()
        {
            (Application.Current.MainWindow as MainWindow).GlobalViewModel.MathExpressionInfoVisibility = Visibility.Collapsed;
            (Application.Current.MainWindow as MainWindow).GlobalViewModel.FileInfoVisibility = Visibility.Collapsed;
            (Application.Current.MainWindow as MainWindow).GlobalViewModel.FolderInfoVisibility = Visibility.Collapsed;
            (Application.Current.MainWindow as MainWindow).GlobalViewModel.MicrosoftStoreAppInfoVisibility = Visibility.Collapsed;
            (Application.Current.MainWindow as MainWindow).GlobalViewModel.ApplicationInfoVisibility = Visibility.Collapsed;
            (Application.Current.MainWindow as MainWindow).GlobalViewModel.SettingsInfoVisibility = Visibility.Visible;
            (Application.Current.MainWindow as MainWindow).GlobalViewModel.SystemInfoVisibility = Visibility.Collapsed;
            (Application.Current.MainWindow as MainWindow).GlobalViewModel.WebServiceInfoVisibility = Visibility.Collapsed;
        }

        public static void ShowSystemInfo()
        {
            (Application.Current.MainWindow as MainWindow).GlobalViewModel.MathExpressionInfoVisibility = Visibility.Collapsed;
            (Application.Current.MainWindow as MainWindow).GlobalViewModel.FileInfoVisibility = Visibility.Collapsed;
            (Application.Current.MainWindow as MainWindow).GlobalViewModel.FolderInfoVisibility = Visibility.Collapsed;
            (Application.Current.MainWindow as MainWindow).GlobalViewModel.MicrosoftStoreAppInfoVisibility = Visibility.Collapsed;
            (Application.Current.MainWindow as MainWindow).GlobalViewModel.ApplicationInfoVisibility = Visibility.Collapsed;
            (Application.Current.MainWindow as MainWindow).GlobalViewModel.SettingsInfoVisibility = Visibility.Collapsed;
            (Application.Current.MainWindow as MainWindow).GlobalViewModel.SystemInfoVisibility = Visibility.Visible;
            (Application.Current.MainWindow as MainWindow).GlobalViewModel.WebServiceInfoVisibility = Visibility.Collapsed;
        }

        public static void ShowWebServiceInfo()
        {
            (Application.Current.MainWindow as MainWindow).GlobalViewModel.MathExpressionInfoVisibility = Visibility.Collapsed;
            (Application.Current.MainWindow as MainWindow).GlobalViewModel.FileInfoVisibility = Visibility.Collapsed;
            (Application.Current.MainWindow as MainWindow).GlobalViewModel.FolderInfoVisibility = Visibility.Collapsed;
            (Application.Current.MainWindow as MainWindow).GlobalViewModel.MicrosoftStoreAppInfoVisibility = Visibility.Collapsed;
            (Application.Current.MainWindow as MainWindow).GlobalViewModel.ApplicationInfoVisibility = Visibility.Collapsed;
            (Application.Current.MainWindow as MainWindow).GlobalViewModel.SettingsInfoVisibility = Visibility.Collapsed;
            (Application.Current.MainWindow as MainWindow).GlobalViewModel.SystemInfoVisibility = Visibility.Collapsed;
            (Application.Current.MainWindow as MainWindow).GlobalViewModel.WebServiceInfoVisibility = Visibility.Visible;
        }
    }
}
