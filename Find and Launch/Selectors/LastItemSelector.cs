using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Find_and_Launch.Selectors
{
    public abstract class LastItemSelector
    {
        public static void SelectLastMathExpression()
        {
            (Application.Current.MainWindow as MainWindow).GlobalViewModel.MathExpressionListSelected = true;
            int lastIndex = (Application.Current.MainWindow as MainWindow).MathExpressionListBox.Items.Count - 1;
            (Application.Current.MainWindow as MainWindow).MathExpressionListBox.Focus();
            (Application.Current.MainWindow as MainWindow).MathExpressionListBox.SelectedIndex = lastIndex;
        }

        public static void SelectLastFile()
        {
            (Application.Current.MainWindow as MainWindow).GlobalViewModel.FileListSelected = true;
            int lastIndex = (Application.Current.MainWindow as MainWindow).FileListBox.Items.Count - 1;
            (Application.Current.MainWindow as MainWindow).FileListBox.Focus();
            (Application.Current.MainWindow as MainWindow).FileListBox.SelectedIndex = lastIndex;
        }

        public static void SelectLastFolder()
        {
            (Application.Current.MainWindow as MainWindow).GlobalViewModel.FolderListSelected = true;
            int lastIndex = (Application.Current.MainWindow as MainWindow).FolderListBox.Items.Count - 1;
            (Application.Current.MainWindow as MainWindow).FolderListBox.Focus();
            (Application.Current.MainWindow as MainWindow).FolderListBox.SelectedIndex = lastIndex;
        }

        public static void SelectLastMicrosoftStoreApp()
        {
            (Application.Current.MainWindow as MainWindow).GlobalViewModel.MicrosoftStoreAppListSelected = true;
            int lastIndex = (Application.Current.MainWindow as MainWindow).MicrosoftStoreAppListBox.Items.Count - 1;
            (Application.Current.MainWindow as MainWindow).MicrosoftStoreAppListBox.Focus();
            (Application.Current.MainWindow as MainWindow).MicrosoftStoreAppListBox.SelectedIndex = lastIndex;
        }

        public static void SelectLastApplication()
        {
            (Application.Current.MainWindow as MainWindow).GlobalViewModel.ApplicationListSelected = true;
            int lastIndex = (Application.Current.MainWindow as MainWindow).ApplicationListBox.Items.Count - 1;
            (Application.Current.MainWindow as MainWindow).ApplicationListBox.Focus();
            (Application.Current.MainWindow as MainWindow).ApplicationListBox.SelectedIndex = lastIndex;
        }

        public static void SelectLastSettings()
        {
            (Application.Current.MainWindow as MainWindow).GlobalViewModel.SettingsListSelected = true;
            int lastIndex = (Application.Current.MainWindow as MainWindow).SettingsListBox.Items.Count - 1;
            (Application.Current.MainWindow as MainWindow).SettingsListBox.Focus();
            (Application.Current.MainWindow as MainWindow).SettingsListBox.SelectedIndex = lastIndex;
        }

        public static void SelectLastSystem()
        {
            (Application.Current.MainWindow as MainWindow).GlobalViewModel.SystemListSelected = true;
            int lastIndex = (Application.Current.MainWindow as MainWindow).SystemsListBox.Items.Count - 1;
            (Application.Current.MainWindow as MainWindow).SystemsListBox.Focus();
            (Application.Current.MainWindow as MainWindow).SystemsListBox.SelectedIndex = lastIndex;
        }

        public static void SelectLastWebService()
        {
            (Application.Current.MainWindow as MainWindow).GlobalViewModel.WebServiceListSelected = true;
            int lastIndex = (Application.Current.MainWindow as MainWindow).WebServiceListBox.Items.Count - 1;
            (Application.Current.MainWindow as MainWindow).WebServiceListBox.Focus();
            (Application.Current.MainWindow as MainWindow).WebServiceListBox.SelectedIndex = lastIndex;
        }
    }
}
