using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Find_and_Launch.Selectors
{
    public abstract class FirstItemSelector
    {
        public static void SelectFirstMathExpression()
        {
            (Application.Current.MainWindow as MainWindow).GlobalViewModel.MathExpressionListSelected = true;
            (Application.Current.MainWindow as MainWindow).MathExpressionListBox.Focus();
            (Application.Current.MainWindow as MainWindow).MathExpressionListBox.SelectedIndex = 0;
        }

        public static void SelectFirstFile()
        {
            (Application.Current.MainWindow as MainWindow).GlobalViewModel.FileListSelected = true;
            (Application.Current.MainWindow as MainWindow).FileListBox.Focus();
            (Application.Current.MainWindow as MainWindow).FileListBox.SelectedIndex = 0;
        }

        public static void SelectFirstFolder()
        {
            (Application.Current.MainWindow as MainWindow).GlobalViewModel.FolderListSelected = true;
            (Application.Current.MainWindow as MainWindow).FolderListBox.Focus();
            (Application.Current.MainWindow as MainWindow).FolderListBox.SelectedIndex = 0;
        }

        public static void SelectFirstMicrosoftStoreApp()
        {
            (Application.Current.MainWindow as MainWindow).GlobalViewModel.MicrosoftStoreAppListSelected = true;
            (Application.Current.MainWindow as MainWindow).MicrosoftStoreAppListBox.Focus();
            (Application.Current.MainWindow as MainWindow).MicrosoftStoreAppListBox.SelectedIndex = 0;
        }

        public static void SelectFirstApplication()
        {
            (Application.Current.MainWindow as MainWindow).GlobalViewModel.ApplicationListSelected = true;
            (Application.Current.MainWindow as MainWindow).ApplicationListBox.Focus();
            (Application.Current.MainWindow as MainWindow).ApplicationListBox.SelectedIndex = 0;
        }

        public static void SelectFirstSettings()
        {
            (Application.Current.MainWindow as MainWindow).GlobalViewModel.SettingsListSelected = true;
            (Application.Current.MainWindow as MainWindow).SettingsListBox.Focus();
            (Application.Current.MainWindow as MainWindow).SettingsListBox.SelectedIndex = 0;
        }

        public static void SelectFirstSystem()
        {
            (Application.Current.MainWindow as MainWindow).GlobalViewModel.SystemListSelected = true;
            (Application.Current.MainWindow as MainWindow).SystemsListBox.Focus();
            (Application.Current.MainWindow as MainWindow).SystemsListBox.SelectedIndex = 0;
        }

        public static void SelectFirstWebService()
        {
            (Application.Current.MainWindow as MainWindow).GlobalViewModel.WebServiceListSelected = true;
            (Application.Current.MainWindow as MainWindow).WebServiceListBox.Focus();
            (Application.Current.MainWindow as MainWindow).WebServiceListBox.SelectedIndex = 0;
        }
    }
}
