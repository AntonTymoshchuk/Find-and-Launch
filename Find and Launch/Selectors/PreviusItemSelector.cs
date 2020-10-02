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
    public abstract class PreviusItemSelector
    {
        public static void SelectPreviusItem()
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
                if (mathExpressionListBox.SelectedIndex > 0)
                    mathExpressionListBox.SelectedIndex--;
                else if (mathExpressionListBox.SelectedIndex == 0)
                {
                    globalViewModel.MathExpressionListSelected = false;
                    if (globalViewModel.FileListOrder - globalViewModel.MathExpressionListOrder == -1 && globalViewModel.FileListVisibility == Visibility.Visible)
                    {
                        mathExpressionListBox.UnselectAll();
                        LastItemSelector.SelectLastFile();
                    }
                    else if (globalViewModel.FolderListOrder - globalViewModel.MathExpressionListOrder == -1 && globalViewModel.FolderListVisibility == Visibility.Visible)
                    {
                        mathExpressionListBox.UnselectAll();
                        LastItemSelector.SelectLastFolder();
                    }
                    else if (globalViewModel.MicrosoftStoreAppListOrder - globalViewModel.MathExpressionListOrder == -1 && globalViewModel.MicrosoftStoreAppListVisibility == Visibility.Visible)
                    {
                        mathExpressionListBox.UnselectAll();
                        LastItemSelector.SelectLastMicrosoftStoreApp();
                    }
                    else if (globalViewModel.ApplicationListOrder - globalViewModel.MathExpressionListOrder == -1 && globalViewModel.ApplicationListVisibility == Visibility.Visible)
                    {
                        mathExpressionListBox.UnselectAll();
                        LastItemSelector.SelectLastApplication();
                    }
                    else if (globalViewModel.SettingsListOrder - globalViewModel.MathExpressionListOrder == -1 && globalViewModel.SettingsListVisibility == Visibility.Visible)
                    {
                        mathExpressionListBox.UnselectAll();
                        LastItemSelector.SelectLastSettings();
                    }
                    else if (globalViewModel.SystemListOrder - globalViewModel.MathExpressionListOrder == -1 && globalViewModel.SystemListVisibility == Visibility.Visible)
                    {
                        mathExpressionListBox.UnselectAll();
                        LastItemSelector.SelectLastSystem();
                    }
                    else if (globalViewModel.WebServiceListOrder - globalViewModel.MathExpressionListOrder == -1 && globalViewModel.WebServiceListVisibility == Visibility.Visible)
                    {
                        mathExpressionListBox.UnselectAll();
                        LastItemSelector.SelectLastFolder();
                    }
                    else globalViewModel.MathExpressionListSelected = true;
                }
            }
            else if (globalViewModel.FileListSelected == true)
            {
                if (fileListBox.SelectedIndex > 0)
                    fileListBox.SelectedIndex--;
                else if (fileListBox.SelectedIndex == 0)
                {
                    globalViewModel.FileListSelected = false;
                    if (globalViewModel.MathExpressionListOrder - globalViewModel.FileListOrder == -1 && globalViewModel.MathExpressionListVisibility == Visibility.Visible)
                    {
                        fileListBox.UnselectAll();
                        LastItemSelector.SelectLastMathExpression();
                    }
                    else if (globalViewModel.FolderListOrder - globalViewModel.FileListOrder == -1 && globalViewModel.FolderListVisibility == Visibility.Visible)
                    {
                        fileListBox.UnselectAll();
                        LastItemSelector.SelectLastFolder();
                    }
                    else if (globalViewModel.MicrosoftStoreAppListOrder - globalViewModel.FileListOrder == -1 && globalViewModel.MicrosoftStoreAppListVisibility == Visibility.Visible)
                    {
                        fileListBox.UnselectAll();
                        LastItemSelector.SelectLastMicrosoftStoreApp();
                    }
                    else if (globalViewModel.ApplicationListOrder - globalViewModel.FileListOrder == -1 && globalViewModel.ApplicationListVisibility == Visibility.Visible)
                    {
                        fileListBox.UnselectAll();
                        LastItemSelector.SelectLastApplication();
                    }
                    else if (globalViewModel.SettingsListOrder - globalViewModel.FileListOrder == -1 && globalViewModel.SettingsListVisibility == Visibility.Visible)
                    {
                        fileListBox.UnselectAll();
                        LastItemSelector.SelectLastSettings();
                    }
                    else if (globalViewModel.SystemListOrder - globalViewModel.FileListOrder == -1 && globalViewModel.SystemListVisibility == Visibility.Visible)
                    {
                        fileListBox.UnselectAll();
                        LastItemSelector.SelectLastSystem();
                    }
                    else if (globalViewModel.WebServiceListOrder - globalViewModel.FileListOrder == -1 && globalViewModel.WebServiceListVisibility == Visibility.Visible)
                    {
                        fileListBox.UnselectAll();
                        LastItemSelector.SelectLastWebService();
                    }
                    else globalViewModel.FileListSelected = true;
                }
            }
            else if (globalViewModel.FolderListSelected == true)
            {
                if (folderListBox.SelectedIndex > 0)
                    folderListBox.SelectedIndex--;
                else if (folderListBox.SelectedIndex == 0)
                {
                    globalViewModel.FolderListSelected = false;
                    if (globalViewModel.MathExpressionListOrder - globalViewModel.FolderListOrder == -1 && globalViewModel.MathExpressionListVisibility == Visibility.Visible)
                    {
                        folderListBox.UnselectAll();
                        LastItemSelector.SelectLastMathExpression();
                    }
                    else if (globalViewModel.FileListOrder - globalViewModel.FolderListOrder == -1 && globalViewModel.FileListVisibility == Visibility.Visible)
                    {
                        folderListBox.UnselectAll();
                        LastItemSelector.SelectLastFile();
                    }
                    else if (globalViewModel.MicrosoftStoreAppListOrder - globalViewModel.FolderListOrder == -1 && globalViewModel.MicrosoftStoreAppListVisibility == Visibility.Visible)
                    {
                        folderListBox.UnselectAll();
                        LastItemSelector.SelectLastMicrosoftStoreApp();
                    }
                    else if (globalViewModel.ApplicationListOrder - globalViewModel.FolderListOrder == -1 && globalViewModel.ApplicationListVisibility == Visibility.Visible)
                    {
                        folderListBox.UnselectAll();
                        LastItemSelector.SelectLastApplication();
                    }
                    else if (globalViewModel.SettingsListOrder - globalViewModel.FolderListOrder == -1 && globalViewModel.SettingsListVisibility == Visibility.Visible)
                    {
                        folderListBox.UnselectAll();
                        LastItemSelector.SelectLastSettings();
                    }
                    else if (globalViewModel.SystemListOrder - globalViewModel.FolderListOrder == -1 && globalViewModel.SystemListVisibility == Visibility.Visible)
                    {
                        folderListBox.UnselectAll();
                        LastItemSelector.SelectLastSystem();
                    }
                    else if (globalViewModel.WebServiceListOrder - globalViewModel.FolderListOrder == -1 && globalViewModel.WebServiceListVisibility == Visibility.Visible)
                    {
                        folderListBox.UnselectAll();
                        LastItemSelector.SelectLastWebService();
                    }
                    else globalViewModel.FolderListSelected = true;
                }
            }
            else if (globalViewModel.MicrosoftStoreAppListSelected == true)
            {
                if (microsoftStoreAppListBox.SelectedIndex > 0)
                    microsoftStoreAppListBox.SelectedIndex--;
                else if (microsoftStoreAppListBox.SelectedIndex == 0)
                {
                    globalViewModel.MicrosoftStoreAppListSelected = false;
                    if (globalViewModel.MathExpressionListOrder - globalViewModel.MicrosoftStoreAppListOrder == -1 && globalViewModel.MathExpressionListVisibility == Visibility.Visible)
                    {
                        microsoftStoreAppListBox.UnselectAll();
                        LastItemSelector.SelectLastMathExpression();
                    }
                    else if (globalViewModel.FileListOrder - globalViewModel.MicrosoftStoreAppListOrder == -1 && globalViewModel.FileListVisibility == Visibility.Visible)
                    {
                        microsoftStoreAppListBox.UnselectAll();
                        LastItemSelector.SelectLastFile();
                    }
                    else if (globalViewModel.FolderListOrder - globalViewModel.MicrosoftStoreAppListOrder == -1 && globalViewModel.FolderListVisibility == Visibility.Visible)
                    {
                        microsoftStoreAppListBox.UnselectAll();
                        LastItemSelector.SelectLastFolder();
                    }
                    else if (globalViewModel.ApplicationListOrder - globalViewModel.MicrosoftStoreAppListOrder == -1 && globalViewModel.ApplicationListVisibility == Visibility.Visible)
                    {
                        microsoftStoreAppListBox.UnselectAll();
                        LastItemSelector.SelectLastApplication();
                    }
                    else if (globalViewModel.SettingsListOrder - globalViewModel.MicrosoftStoreAppListOrder == -1 && globalViewModel.SettingsListVisibility == Visibility.Visible)
                    {
                        microsoftStoreAppListBox.UnselectAll();
                        LastItemSelector.SelectLastSettings();
                    }
                    else if (globalViewModel.SystemListOrder - globalViewModel.MicrosoftStoreAppListOrder == -1 && globalViewModel.SystemListVisibility == Visibility.Visible)
                    {
                        microsoftStoreAppListBox.UnselectAll();
                        LastItemSelector.SelectLastSystem();
                    }
                    else if (globalViewModel.WebServiceListOrder - globalViewModel.MicrosoftStoreAppListOrder == -1 && globalViewModel.WebServiceListVisibility == Visibility.Visible)
                    {
                        microsoftStoreAppListBox.UnselectAll();
                        LastItemSelector.SelectLastWebService();
                    }
                    else globalViewModel.MicrosoftStoreAppListSelected = true;
                }
            }
            else if (globalViewModel.ApplicationListSelected == true)
            {
                if (applicationListBox.SelectedIndex > 0)
                    applicationListBox.SelectedIndex--;
                else if (applicationListBox.SelectedIndex == 0)
                {
                    globalViewModel.ApplicationListSelected = false;
                    if (globalViewModel.MathExpressionListOrder - globalViewModel.ApplicationListOrder == -1 && globalViewModel.MathExpressionListVisibility == Visibility.Visible)
                    {
                        applicationListBox.UnselectAll();
                        LastItemSelector.SelectLastMathExpression();
                    }
                    else if (globalViewModel.FileListOrder - globalViewModel.ApplicationListOrder == -1 && globalViewModel.FileListVisibility == Visibility.Visible)
                    {
                        applicationListBox.UnselectAll();
                        LastItemSelector.SelectLastFile();
                    }
                    else if (globalViewModel.FolderListOrder - globalViewModel.ApplicationListOrder == -1 && globalViewModel.FolderListVisibility == Visibility.Visible)
                    {
                        applicationListBox.UnselectAll();
                        LastItemSelector.SelectLastFolder();
                    }
                    else if (globalViewModel.MicrosoftStoreAppListOrder - globalViewModel.ApplicationListOrder == -1 && globalViewModel.MicrosoftStoreAppListVisibility == Visibility.Visible)
                    {
                        applicationListBox.UnselectAll();
                        LastItemSelector.SelectLastMicrosoftStoreApp();
                    }
                    else if (globalViewModel.SettingsListOrder - globalViewModel.ApplicationListOrder == -1 && globalViewModel.SettingsListVisibility == Visibility.Visible)
                    {
                        applicationListBox.UnselectAll();
                        LastItemSelector.SelectLastSettings();
                    }
                    else if (globalViewModel.SystemListOrder - globalViewModel.ApplicationListOrder == -1 && globalViewModel.SystemListVisibility == Visibility.Visible)
                    {
                        applicationListBox.UnselectAll();
                        LastItemSelector.SelectLastSystem();
                    }
                    else if (globalViewModel.WebServiceListOrder - globalViewModel.ApplicationListOrder == -1 && globalViewModel.WebServiceListVisibility == Visibility.Visible)
                    {
                        applicationListBox.UnselectAll();
                        LastItemSelector.SelectLastWebService();
                    }
                    else globalViewModel.ApplicationListSelected = true;
                }
            }
            else if (globalViewModel.SettingsListSelected == true)
            {
                if (settingsListBox.SelectedIndex > 0)
                    settingsListBox.SelectedIndex--;
                else
                {
                    globalViewModel.SettingsListSelected = false;
                    if (globalViewModel.MathExpressionListOrder - globalViewModel.SettingsListOrder == -1 && globalViewModel.MathExpressionListVisibility == Visibility.Visible)
                    {
                        settingsListBox.UnselectAll();
                        LastItemSelector.SelectLastMathExpression();
                    }
                    else if (globalViewModel.FileListOrder - globalViewModel.SettingsListOrder == -1 && globalViewModel.FileListVisibility == Visibility.Visible)
                    {
                        settingsListBox.UnselectAll();
                        LastItemSelector.SelectLastFile();
                    }
                    else if (globalViewModel.FolderListOrder - globalViewModel.SettingsListOrder == -1 && globalViewModel.FolderListVisibility == Visibility.Visible)
                    {
                        settingsListBox.UnselectAll();
                        LastItemSelector.SelectLastFolder();
                    }
                    else if (globalViewModel.MicrosoftStoreAppListOrder - globalViewModel.SettingsListOrder == -1 && globalViewModel.MicrosoftStoreAppListVisibility == Visibility.Visible)
                    {
                        settingsListBox.UnselectAll();
                        LastItemSelector.SelectLastMicrosoftStoreApp();
                    }
                    else if (globalViewModel.ApplicationListOrder - globalViewModel.SettingsListOrder == -1 && globalViewModel.ApplicationListVisibility == Visibility.Visible)
                    {
                        settingsListBox.UnselectAll();
                        LastItemSelector.SelectLastApplication();
                    }
                    else if (globalViewModel.SystemListOrder - globalViewModel.SettingsListOrder == -1 && globalViewModel.SystemListVisibility == Visibility.Visible)
                    {
                        settingsListBox.UnselectAll();
                        LastItemSelector.SelectLastSystem();
                    }
                    else if (globalViewModel.WebServiceListOrder - globalViewModel.SettingsListOrder == -1 && globalViewModel.WebServiceListVisibility == Visibility.Visible)
                    {
                        settingsListBox.UnselectAll();
                        LastItemSelector.SelectLastWebService();
                    }
                    else globalViewModel.SettingsListSelected = true;
                }
            }
            else if (globalViewModel.SystemListSelected == true)
            {
                if (systemsListBox.SelectedIndex > 0)
                    systemsListBox.SelectedIndex--;
                else if (systemsListBox.SelectedIndex == 0)
                {
                    globalViewModel.SystemListSelected = false;
                    if (globalViewModel.MathExpressionListOrder - globalViewModel.SystemListOrder == -1 && globalViewModel.MathExpressionListVisibility == Visibility.Visible)
                    {
                        systemsListBox.UnselectAll();
                        LastItemSelector.SelectLastMathExpression();
                    }
                    else if (globalViewModel.FileListOrder - globalViewModel.SystemListOrder == -1 && globalViewModel.FileListVisibility == Visibility.Visible)
                    {
                        systemsListBox.UnselectAll();
                        LastItemSelector.SelectLastFile();
                    }
                    else if (globalViewModel.FolderListOrder - globalViewModel.SystemListOrder == -1 && globalViewModel.FolderListVisibility == Visibility.Visible)
                    {
                        systemsListBox.UnselectAll();
                        LastItemSelector.SelectLastFolder();
                    }
                    else if (globalViewModel.MicrosoftStoreAppListOrder - globalViewModel.SystemListOrder == -1 && globalViewModel.MicrosoftStoreAppListVisibility == Visibility.Visible)
                    {
                        systemsListBox.UnselectAll();
                        LastItemSelector.SelectLastMicrosoftStoreApp();
                    }
                    else if (globalViewModel.ApplicationListOrder - globalViewModel.SystemListOrder == -1 && globalViewModel.ApplicationListVisibility == Visibility.Visible)
                    {
                        systemsListBox.UnselectAll();
                        LastItemSelector.SelectLastApplication();
                    }
                    else if (globalViewModel.SettingsListOrder - globalViewModel.SystemListOrder == -1 && globalViewModel.SettingsListVisibility == Visibility.Visible)
                    {
                        systemsListBox.UnselectAll();
                        LastItemSelector.SelectLastSettings();
                    }
                    else if (globalViewModel.WebServiceListOrder - globalViewModel.SystemListOrder == -1 && globalViewModel.WebServiceListVisibility == Visibility.Visible)
                    {
                        systemsListBox.UnselectAll();
                        LastItemSelector.SelectLastWebService();
                    }
                    else globalViewModel.SystemListSelected = true;
                }
            }
            else if (globalViewModel.WebServiceListSelected == true)
            {
                if (webServiceListBox.SelectedIndex > 0)
                    webServiceListBox.SelectedIndex--;
                else if (webServiceListBox.SelectedIndex == 0)
                {
                    globalViewModel.WebServiceListSelected = false;
                    if (globalViewModel.MathExpressionListOrder - globalViewModel.WebServiceListOrder == -1 && globalViewModel.MathExpressionListVisibility == Visibility.Visible)
                    {
                        webServiceListBox.UnselectAll();
                        LastItemSelector.SelectLastMathExpression();
                    }
                    else if (globalViewModel.FileListOrder - globalViewModel.WebServiceListOrder == -1 && globalViewModel.FileListVisibility == Visibility.Visible)
                    {
                        webServiceListBox.UnselectAll();
                        LastItemSelector.SelectLastFile();
                    }
                    else if (globalViewModel.FolderListOrder - globalViewModel.WebServiceListOrder == -1 && globalViewModel.FolderListVisibility == Visibility.Visible)
                    {
                        webServiceListBox.UnselectAll();
                        LastItemSelector.SelectLastFolder();
                    }
                    else if (globalViewModel.MicrosoftStoreAppListOrder - globalViewModel.WebServiceListOrder == -1 && globalViewModel.MicrosoftStoreAppListVisibility == Visibility.Visible)
                    {
                        webServiceListBox.UnselectAll();
                        LastItemSelector.SelectLastMicrosoftStoreApp();
                    }
                    else if (globalViewModel.ApplicationListOrder - globalViewModel.WebServiceListOrder == -1 && globalViewModel.ApplicationListVisibility == Visibility.Visible)
                    {
                        webServiceListBox.UnselectAll();
                        LastItemSelector.SelectLastApplication();
                    }
                    else if (globalViewModel.SettingsListOrder - globalViewModel.WebServiceListOrder == -1 && globalViewModel.SettingsListVisibility == Visibility.Visible)
                    {
                        webServiceListBox.UnselectAll();
                        LastItemSelector.SelectLastSettings();
                    }
                    else if (globalViewModel.SystemListOrder - globalViewModel.WebServiceListOrder == -1 && globalViewModel.SystemListVisibility == Visibility.Visible)
                    {
                        webServiceListBox.UnselectAll();
                        LastItemSelector.SelectLastSystem();
                    }
                    else globalViewModel.WebServiceListSelected = true;
                }
            }
        }
    }
}
