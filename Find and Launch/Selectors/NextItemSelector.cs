using Find_and_Launch.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Find_and_Launch.Selectors
{
    public abstract class NextItemSelector
    {
        public static void SelectNextItem()
        {
            GlobalViewModel globalViewModel = (Application.Current.MainWindow as MainWindow).GlobalViewModel;

            ListBox mathExpressionListBox = (Application.Current.MainWindow as MainWindow).MathExpressionListBox;
            ListBox fileListBox = (Application.Current.MainWindow as MainWindow).FileListBox;
            ListBox folderListBox = (Application.Current.MainWindow as MainWindow).FolderListBox;
            ListBox microsoftStoreAppListBox = (Application.Current.MainWindow as MainWindow).MicrosoftStoreAppListBox;
            ListBox applicationListBox = (Application.Current.MainWindow as MainWindow).ApplicationListBox;
            ListBox settingsListBox = (Application.Current.MainWindow as MainWindow).SettingsListBox;
            ListBox systemsListBox = (Application.Current.MainWindow as MainWindow).SystemsListBox;
            ListBox webServiceListBox = (Application.Current.MainWindow as MainWindow).WebServiceListBox;

            if (globalViewModel.MathExpressionListSelected == true)
            {
                if (mathExpressionListBox.SelectedIndex < mathExpressionListBox.Items.Count - 1)
                {
                    mathExpressionListBox.SelectedIndex++;
                    if (mathExpressionListBox.SelectedIndex < mathExpressionListBox.Items.Count - 1)
                        mathExpressionListBox.ScrollIntoView(mathExpressionListBox.Items[mathExpressionListBox.SelectedIndex + 1]);
                }
                else if (mathExpressionListBox.SelectedIndex == mathExpressionListBox.Items.Count - 1)
                {
                    globalViewModel.MathExpressionListSelected = false;
                    if (globalViewModel.FileListOrder - globalViewModel.MathExpressionListOrder == 1 && globalViewModel.FileListVisibility == Visibility.Visible)
                    {
                        mathExpressionListBox.UnselectAll();
                        FirstItemSelector.SelectFirstFile();
                    }
                    else if (globalViewModel.FolderListOrder - globalViewModel.MathExpressionListOrder == 1 && globalViewModel.FolderListVisibility == Visibility.Visible)
                    {
                        mathExpressionListBox.UnselectAll();
                        FirstItemSelector.SelectFirstFolder();
                    }
                    else if (globalViewModel.MicrosoftStoreAppListOrder - globalViewModel.MathExpressionListOrder == 1 && globalViewModel.MicrosoftStoreAppListVisibility == Visibility.Visible)
                    {
                        mathExpressionListBox.UnselectAll();
                        FirstItemSelector.SelectFirstMicrosoftStoreApp();
                    }
                    else if (globalViewModel.ApplicationListOrder - globalViewModel.MathExpressionListOrder == 1 && globalViewModel.ApplicationListVisibility == Visibility.Visible)
                    {
                        mathExpressionListBox.UnselectAll();
                        FirstItemSelector.SelectFirstApplication();
                    }
                    else if (globalViewModel.SettingsListOrder - globalViewModel.MathExpressionListOrder == 1 && globalViewModel.SettingsListVisibility == Visibility.Visible)
                    {
                        mathExpressionListBox.UnselectAll();
                        FirstItemSelector.SelectFirstSettings();
                    }
                    else if (globalViewModel.SystemListOrder - globalViewModel.MathExpressionListOrder == 1 && globalViewModel.SystemListVisibility == Visibility.Visible)
                    {
                        mathExpressionListBox.UnselectAll();
                        FirstItemSelector.SelectFirstSystem();
                    }
                    else if (globalViewModel.WebServiceListOrder - globalViewModel.MathExpressionListOrder == 1 && globalViewModel.WebServiceListVisibility == Visibility.Visible)
                    {
                        mathExpressionListBox.UnselectAll();
                        FirstItemSelector.SelectFirstFolder();
                    }
                    else globalViewModel.MathExpressionListSelected = true;
                }
            }
            else if (globalViewModel.FileListSelected == true)
            {
                if (fileListBox.SelectedIndex < fileListBox.Items.Count - 1)
                {
                    fileListBox.SelectedIndex++;
                    if (fileListBox.SelectedIndex < fileListBox.Items.Count - 1)
                        fileListBox.ScrollIntoView(fileListBox.Items[fileListBox.SelectedIndex + 1]);
                }
                else if (fileListBox.SelectedIndex == fileListBox.Items.Count - 1)
                {
                    globalViewModel.FileListSelected = false;
                    if (globalViewModel.MathExpressionListOrder - globalViewModel.FileListOrder == 1 && globalViewModel.MathExpressionListVisibility == Visibility.Visible)
                    {
                        fileListBox.UnselectAll();
                        FirstItemSelector.SelectFirstMathExpression();
                    }
                    else if (globalViewModel.FolderListOrder - globalViewModel.FileListOrder == 1 && globalViewModel.FolderListVisibility == Visibility.Visible)
                    {
                        fileListBox.UnselectAll();
                        FirstItemSelector.SelectFirstFolder();
                    }
                    else if (globalViewModel.MicrosoftStoreAppListOrder - globalViewModel.FileListOrder == 1 && globalViewModel.MicrosoftStoreAppListVisibility == Visibility.Visible)
                    {
                        fileListBox.UnselectAll();
                        FirstItemSelector.SelectFirstMicrosoftStoreApp();
                    }
                    else if (globalViewModel.ApplicationListOrder - globalViewModel.FileListOrder == 1 && globalViewModel.ApplicationListVisibility == Visibility.Visible)
                    {
                        fileListBox.UnselectAll();
                        FirstItemSelector.SelectFirstApplication();
                    }
                    else if (globalViewModel.SettingsListOrder - globalViewModel.FileListOrder == 1 && globalViewModel.SettingsListVisibility == Visibility.Visible)
                    {
                        fileListBox.UnselectAll();
                        FirstItemSelector.SelectFirstSettings();
                    }
                    else if (globalViewModel.SystemListOrder - globalViewModel.FileListOrder == 1 && globalViewModel.SystemListVisibility == Visibility.Visible)
                    {
                        fileListBox.UnselectAll();
                        FirstItemSelector.SelectFirstSystem();
                    }
                    else if (globalViewModel.WebServiceListOrder - globalViewModel.FileListOrder == 1 && globalViewModel.WebServiceListVisibility == Visibility.Visible)
                    {
                        fileListBox.UnselectAll();
                        FirstItemSelector.SelectFirstWebService();
                    }
                    else globalViewModel.FileListSelected = true;
                }
            }
            else if (globalViewModel.FolderListSelected == true)
            {
                if (folderListBox.SelectedIndex < folderListBox.Items.Count - 1)
                    folderListBox.SelectedIndex++;
                else if (folderListBox.SelectedIndex == folderListBox.Items.Count - 1)
                {
                    globalViewModel.FolderListSelected = false;
                    if (globalViewModel.MathExpressionListOrder - globalViewModel.FolderListOrder == 1 && globalViewModel.MathExpressionListVisibility == Visibility.Visible)
                    {
                        folderListBox.UnselectAll();
                        FirstItemSelector.SelectFirstMathExpression();
                    }
                    else if (globalViewModel.FileListOrder - globalViewModel.FolderListOrder == 1 && globalViewModel.FileListVisibility == Visibility.Visible)
                    {
                        folderListBox.UnselectAll();
                        FirstItemSelector.SelectFirstFile();
                    }
                    else if (globalViewModel.MicrosoftStoreAppListOrder - globalViewModel.FolderListOrder == 1 && globalViewModel.MicrosoftStoreAppListVisibility == Visibility.Visible)
                    {
                        folderListBox.UnselectAll();
                        FirstItemSelector.SelectFirstMicrosoftStoreApp();
                    }
                    else if (globalViewModel.ApplicationListOrder - globalViewModel.FolderListOrder == 1 && globalViewModel.ApplicationListVisibility == Visibility.Visible)
                    {
                        folderListBox.UnselectAll();
                        FirstItemSelector.SelectFirstApplication();
                    }
                    else if (globalViewModel.SettingsListOrder - globalViewModel.FolderListOrder == 1 && globalViewModel.SettingsListVisibility == Visibility.Visible)
                    {
                        folderListBox.UnselectAll();
                        FirstItemSelector.SelectFirstSettings();
                    }
                    else if (globalViewModel.SystemListOrder - globalViewModel.FolderListOrder == 1 && globalViewModel.SystemListVisibility == Visibility.Visible)
                    {
                        folderListBox.UnselectAll();
                        FirstItemSelector.SelectFirstSystem();
                    }
                    else if (globalViewModel.WebServiceListOrder - globalViewModel.FolderListOrder == 1 && globalViewModel.WebServiceListVisibility == Visibility.Visible)
                    {
                        folderListBox.UnselectAll();
                        FirstItemSelector.SelectFirstWebService();
                    }
                    else globalViewModel.FolderListSelected = true;
                }
            }
            else if (globalViewModel.MicrosoftStoreAppListSelected == true)
            {
                if (microsoftStoreAppListBox.SelectedIndex < microsoftStoreAppListBox.Items.Count - 1)
                    microsoftStoreAppListBox.SelectedIndex++;
                else if (microsoftStoreAppListBox.SelectedIndex == microsoftStoreAppListBox.Items.Count - 1)
                {
                    globalViewModel.MicrosoftStoreAppListSelected = false;
                    if (globalViewModel.MathExpressionListOrder - globalViewModel.MicrosoftStoreAppListOrder == 1 && globalViewModel.MathExpressionListVisibility == Visibility.Visible)
                    {
                        microsoftStoreAppListBox.UnselectAll();
                        FirstItemSelector.SelectFirstMathExpression();
                    }
                    else if (globalViewModel.FileListOrder - globalViewModel.MicrosoftStoreAppListOrder == 1 && globalViewModel.FileListVisibility == Visibility.Visible)
                    {
                        microsoftStoreAppListBox.UnselectAll();
                        FirstItemSelector.SelectFirstFile();
                    }
                    else if (globalViewModel.FolderListOrder - globalViewModel.MicrosoftStoreAppListOrder == 1 && globalViewModel.FolderListVisibility == Visibility.Visible)
                    {
                        microsoftStoreAppListBox.UnselectAll();
                        FirstItemSelector.SelectFirstFolder();
                    }
                    else if (globalViewModel.ApplicationListOrder - globalViewModel.MicrosoftStoreAppListOrder == 1 && globalViewModel.ApplicationListVisibility == Visibility.Visible)
                    {
                        microsoftStoreAppListBox.UnselectAll();
                        FirstItemSelector.SelectFirstApplication();
                    }
                    else if (globalViewModel.SettingsListOrder - globalViewModel.MicrosoftStoreAppListOrder == 1 && globalViewModel.SettingsListVisibility == Visibility.Visible)
                    {
                        microsoftStoreAppListBox.UnselectAll();
                        FirstItemSelector.SelectFirstSettings();
                    }
                    else if (globalViewModel.SystemListOrder - globalViewModel.MicrosoftStoreAppListOrder == 1 && globalViewModel.SystemListVisibility == Visibility.Visible)
                    {
                        microsoftStoreAppListBox.UnselectAll();
                        FirstItemSelector.SelectFirstSystem();
                    }
                    else if (globalViewModel.WebServiceListOrder - globalViewModel.MicrosoftStoreAppListOrder == 1 && globalViewModel.WebServiceListVisibility == Visibility.Visible)
                    {
                        microsoftStoreAppListBox.UnselectAll();
                        FirstItemSelector.SelectFirstWebService();
                    }
                    else globalViewModel.MicrosoftStoreAppListSelected = true;
                }
            }
            else if (globalViewModel.ApplicationListSelected == true)
            {
                if (applicationListBox.SelectedIndex < applicationListBox.Items.Count - 1)
                    applicationListBox.SelectedIndex++;
                else if (applicationListBox.SelectedIndex == applicationListBox.Items.Count - 1)
                {
                    globalViewModel.ApplicationListSelected = false;
                    if (globalViewModel.MathExpressionListOrder - globalViewModel.ApplicationListOrder == 1 && globalViewModel.MathExpressionListVisibility == Visibility.Visible)
                    {
                        applicationListBox.UnselectAll();
                        FirstItemSelector.SelectFirstMathExpression();
                    }
                    else if (globalViewModel.FileListOrder - globalViewModel.ApplicationListOrder == 1 && globalViewModel.FileListVisibility == Visibility.Visible)
                    {
                        applicationListBox.UnselectAll();
                        FirstItemSelector.SelectFirstFile();
                    }
                    else if (globalViewModel.FolderListOrder - globalViewModel.ApplicationListOrder == 1 && globalViewModel.FolderListVisibility == Visibility.Visible)
                    {
                        applicationListBox.UnselectAll();
                        FirstItemSelector.SelectFirstFolder();
                    }
                    else if (globalViewModel.MicrosoftStoreAppListOrder - globalViewModel.ApplicationListOrder == 1 && globalViewModel.MicrosoftStoreAppListVisibility == Visibility.Visible)
                    {
                        applicationListBox.UnselectAll();
                        FirstItemSelector.SelectFirstMicrosoftStoreApp();
                    }
                    else if (globalViewModel.SettingsListOrder - globalViewModel.ApplicationListOrder == 1 && globalViewModel.SettingsListVisibility == Visibility.Visible)
                    {
                        applicationListBox.UnselectAll();
                        FirstItemSelector.SelectFirstSettings();
                    }
                    else if (globalViewModel.SystemListOrder - globalViewModel.ApplicationListOrder == 1 && globalViewModel.SystemListVisibility == Visibility.Visible)
                    {
                        applicationListBox.UnselectAll();
                        FirstItemSelector.SelectFirstSystem();
                    }
                    else if (globalViewModel.WebServiceListOrder - globalViewModel.ApplicationListOrder == 1 && globalViewModel.WebServiceListVisibility == Visibility.Visible)
                    {
                        applicationListBox.UnselectAll();
                        FirstItemSelector.SelectFirstWebService();
                    }
                    else globalViewModel.ApplicationListSelected = true;
                }
            }
            else if (globalViewModel.SettingsListSelected == true)
            {
                if (settingsListBox.SelectedIndex < settingsListBox.Items.Count - 1)
                    settingsListBox.SelectedIndex++;
                else
                {
                    globalViewModel.SettingsListSelected = false;
                    if (globalViewModel.MathExpressionListOrder - globalViewModel.SettingsListOrder == 1 && globalViewModel.MathExpressionListVisibility == Visibility.Visible)
                    {
                        settingsListBox.UnselectAll();
                        FirstItemSelector.SelectFirstMathExpression();
                    }
                    else if (globalViewModel.FileListOrder - globalViewModel.SettingsListOrder == 1 && globalViewModel.FileListVisibility == Visibility.Visible)
                    {
                        settingsListBox.UnselectAll();
                        FirstItemSelector.SelectFirstFile();
                    }
                    else if (globalViewModel.FolderListOrder - globalViewModel.SettingsListOrder == 1 && globalViewModel.FolderListVisibility == Visibility.Visible)
                    {
                        settingsListBox.UnselectAll();
                        FirstItemSelector.SelectFirstFolder();
                    }
                    else if (globalViewModel.MicrosoftStoreAppListOrder - globalViewModel.SettingsListOrder == 1 && globalViewModel.MicrosoftStoreAppListVisibility == Visibility.Visible)
                    {
                        settingsListBox.UnselectAll();
                        FirstItemSelector.SelectFirstMicrosoftStoreApp();
                    }
                    else if (globalViewModel.ApplicationListOrder - globalViewModel.SettingsListOrder == 1 && globalViewModel.ApplicationListVisibility == Visibility.Visible)
                    {
                        settingsListBox.UnselectAll();
                        FirstItemSelector.SelectFirstApplication();
                    }
                    else if (globalViewModel.SystemListOrder - globalViewModel.SettingsListOrder == 1 && globalViewModel.SystemListVisibility == Visibility.Visible)
                    {
                        settingsListBox.UnselectAll();
                        FirstItemSelector.SelectFirstSystem();
                    }
                    else if (globalViewModel.WebServiceListOrder - globalViewModel.SettingsListOrder == 1 && globalViewModel.WebServiceListVisibility == Visibility.Visible)
                    {
                        settingsListBox.UnselectAll();
                        FirstItemSelector.SelectFirstWebService();
                    }
                    else globalViewModel.SettingsListSelected = true;
                }
            }
            else if (globalViewModel.SystemListSelected == true)
            {
                if (systemsListBox.SelectedIndex < systemsListBox.Items.Count - 1)
                    systemsListBox.SelectedIndex++;
                else if (systemsListBox.SelectedIndex == systemsListBox.Items.Count - 1)
                {
                    globalViewModel.SystemListSelected = false;
                    if (globalViewModel.MathExpressionListOrder - globalViewModel.SystemListOrder == 1 && globalViewModel.MathExpressionListVisibility == Visibility.Visible)
                    {
                        systemsListBox.UnselectAll();
                        FirstItemSelector.SelectFirstMathExpression();
                    }
                    else if (globalViewModel.FileListOrder - globalViewModel.SystemListOrder == 1 && globalViewModel.FileListVisibility == Visibility.Visible)
                    {
                        systemsListBox.UnselectAll();
                        FirstItemSelector.SelectFirstFile();
                    }
                    else if (globalViewModel.FolderListOrder - globalViewModel.SystemListOrder == 1 && globalViewModel.FolderListVisibility == Visibility.Visible)
                    {
                        systemsListBox.UnselectAll();
                        FirstItemSelector.SelectFirstFolder();
                    }
                    else if (globalViewModel.MicrosoftStoreAppListOrder - globalViewModel.SystemListOrder == 1 && globalViewModel.MicrosoftStoreAppListVisibility == Visibility.Visible)
                    {
                        systemsListBox.UnselectAll();
                        FirstItemSelector.SelectFirstMicrosoftStoreApp();
                    }
                    else if (globalViewModel.ApplicationListOrder - globalViewModel.SystemListOrder == 1 && globalViewModel.ApplicationListVisibility == Visibility.Visible)
                    {
                        systemsListBox.UnselectAll();
                        FirstItemSelector.SelectFirstApplication();
                    }
                    else if (globalViewModel.SettingsListOrder - globalViewModel.SystemListOrder == 1 && globalViewModel.SettingsListVisibility == Visibility.Visible)
                    {
                        systemsListBox.UnselectAll();
                        FirstItemSelector.SelectFirstSettings();
                    }
                    else if (globalViewModel.WebServiceListOrder - globalViewModel.SystemListOrder == 1 && globalViewModel.WebServiceListVisibility == Visibility.Visible)
                    {
                        systemsListBox.UnselectAll();
                        FirstItemSelector.SelectFirstWebService();
                    }
                    else globalViewModel.SystemListSelected = true;
                }
            }
            else if (globalViewModel.WebServiceListSelected == true)
            {
                if (webServiceListBox.SelectedIndex < webServiceListBox.Items.Count - 1)
                    webServiceListBox.SelectedIndex++;
                else if (webServiceListBox.SelectedIndex == webServiceListBox.Items.Count - 1)
                {
                    globalViewModel.WebServiceListSelected = false;
                    if (globalViewModel.MathExpressionListOrder - globalViewModel.WebServiceListOrder == 1 && globalViewModel.MathExpressionListVisibility == Visibility.Visible)
                    {
                        webServiceListBox.UnselectAll();
                        FirstItemSelector.SelectFirstMathExpression();
                    }
                    else if (globalViewModel.FileListOrder - globalViewModel.WebServiceListOrder == 1 && globalViewModel.FileListVisibility == Visibility.Visible)
                    {
                        webServiceListBox.UnselectAll();
                        FirstItemSelector.SelectFirstFile();
                    }
                    else if (globalViewModel.FolderListOrder - globalViewModel.WebServiceListOrder == 1 && globalViewModel.FolderListVisibility == Visibility.Visible)
                    {
                        webServiceListBox.UnselectAll();
                        FirstItemSelector.SelectFirstFolder();
                    }
                    else if (globalViewModel.MicrosoftStoreAppListOrder - globalViewModel.WebServiceListOrder == 1 && globalViewModel.MicrosoftStoreAppListVisibility == Visibility.Visible)
                    {
                        webServiceListBox.UnselectAll();
                        FirstItemSelector.SelectFirstMicrosoftStoreApp();
                    }
                    else if (globalViewModel.ApplicationListOrder - globalViewModel.WebServiceListOrder == 1 && globalViewModel.ApplicationListVisibility == Visibility.Visible)
                    {
                        webServiceListBox.UnselectAll();
                        FirstItemSelector.SelectFirstApplication();
                    }
                    else if (globalViewModel.SettingsListOrder - globalViewModel.WebServiceListOrder == 1 && globalViewModel.SettingsListVisibility == Visibility.Visible)
                    {
                        webServiceListBox.UnselectAll();
                        FirstItemSelector.SelectFirstSettings();
                    }
                    else if (globalViewModel.SystemListOrder - globalViewModel.WebServiceListOrder == 1 && globalViewModel.SystemListVisibility == Visibility.Visible)
                    {
                        webServiceListBox.UnselectAll();
                        FirstItemSelector.SelectFirstSystem();
                    }
                    else globalViewModel.WebServiceListSelected = true;
                }
            }
        }
    }
}
