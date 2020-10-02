using Find_and_Launch.Algorithms;
using Find_and_Launch.HabitsAnalysing;
using Find_and_Launch.Models;
using Find_and_Launch.Selectors;
using Find_and_Launch.Settings;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Find_and_Launch
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly GlobalAlgorithm globalAlgorithm;
        private bool alreadySelected = false;

        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
            GlobalSettings.Read();
            GlobalHabitsAnalyser.LoadData();
            globalAlgorithm = new GlobalAlgorithm();
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            MathExpressionListViewModel.ConnectWithGlobalViewModel();
            FileListViewModel.ConnectWithGlobalViewModel();
            FolderListViewModel.ConnectWithGlobalViewModel();
            MicrosoftStoreAppsListViewModel.ConnectWithGlobalViewModel();
            ApplicationListViewModel.ConnectWithGlobalViewModel();
            SettingsListViewModel.ConnectWithGlobalViewModel();
            SystemListViewModel.ConnectWithGlobalViewModel();
            WebServiceListViewModel.ConnectWithGlobalViewModel();
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            BackgroundBorder.Height = 48;
            RequestTextBox.Focus();
        }

        private void Window_Deactivated(object sender, EventArgs e)
        {
            GlobalHabitsAnalyser.SaveData();
            //Close();
        }

        private void RequestTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            RequestTextBoxContextMenuPopup.IsOpen = false;
            FileListBoxContextMenuPopup.IsOpen = false;
            switch (e.Key)
            {
                case Key.Down:
                    NextItemSelector.SelectNextItem();
                    break;
                case Key.Up:
                    PreviusItemSelector.SelectPreviusItem();
                    break;
                case Key.Enter:
                    GlobalViewModel.LaunchSelectedItem();
                    break;
            }
        }

        private void RequestTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            RequestTextBoxContextMenuPopup.IsOpen = false;
            FileListBoxContextMenuPopup.IsOpen = false;
            RequestTextBox.Tag = "Empty";
            if (RequestTextBox.Text.Equals(string.Empty))
            {
                GlobalViewModel.PostRequestString = "Search";
                globalAlgorithm.Clear();
            }
            else
            {
                globalAlgorithm.Clear();
                globalAlgorithm.Start(RequestTextBox.Text);
            }
        }

        private void ListBoxItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            GlobalViewModel.LaunchSelectedItem();
        }

        private void RequestTextBox_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            RequestTextBoxContextMenuPopup.IsOpen = true;
        }

        private void SelectAllButton_Click(object sender, RoutedEventArgs e)
        {
            RequestTextBox.SelectAll();
            RequestTextBox.Focus();
            RequestTextBoxContextMenuPopup.IsOpen = false;
        }

        private void CutButton_Click(object sender, RoutedEventArgs e)
        {
            RequestTextBox.SelectAll();
            RequestTextBox.Cut();
            RequestTextBox.Focus();
            RequestTextBoxContextMenuPopup.IsOpen = false;
        }

        private void CopyButton_Click(object sender, RoutedEventArgs e)
        {
            RequestTextBox.SelectAll();
            RequestTextBox.Copy();
            RequestTextBox.Select(RequestTextBox.Text.Length, 0);
            RequestTextBox.Focus();
            RequestTextBoxContextMenuPopup.IsOpen = false;
        }

        private void PasteButton_Click(object sender, RoutedEventArgs e)
        {
            RequestTextBox.Paste();
            RequestTextBox.Focus();
            RequestTextBoxContextMenuPopup.IsOpen = false;
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            RequestTextBox.Text = string.Empty;
            RequestTextBox.Focus();
            RequestTextBoxContextMenuPopup.IsOpen = false;
        }

        private void UndoButton_Click(object sender, RoutedEventArgs e)
        {
            if (RequestTextBox.CanUndo == true)
                RequestTextBox.Undo();
            RequestTextBox.Focus();
            RequestTextBoxContextMenuPopup.IsOpen = false;
        }

        private void RedoButton_Click(object sender, RoutedEventArgs e)
        {
            if (RequestTextBox.CanRedo == true)
                RequestTextBox.Redo();
            RequestTextBox.Focus();
            RequestTextBoxContextMenuPopup.IsOpen = false;
        }

        #region MathExpression
        private void MathExpressionListBox_GotFocus(object sender, RoutedEventArgs e)
        {
            InfoModelSelector.ShowMathExpressionInfo();
            if (alreadySelected == true)
            {
                alreadySelected = false;
                RequestTextBox.Focus();
            }
        }

        private void MathExpressionListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            alreadySelected = false;
            if (MathExpressionListBox.Items.Count > 0 && MathExpressionListBox.SelectedItems.Count == 1)
            {
                MathExpression mathExpression = MathExpressionListBox.SelectedItem as MathExpression;
                GlobalViewModel.SetPostRequestString(mathExpression);
                MathExpressionInfoViewModel.LoadInfo(mathExpression);
                ListModelSelector.SelectMathExpressionList();
                if (GlobalSettings.UseHabitsAnalysis == true && GlobalSettings.RememberOnActivation == true)
                {
                    GlobalHabitsAnalyser.MathExpressionHabitsAnalyser.AddToHabitsAnalyser(mathExpression);
                    GlobalHabitsAnalyser.AddRequestHabit(mathExpression);
                }
            }
            RequestTextBox.Focus();
        }

        private void MathExpressionHyperlinkTextBlock_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            (MathExpressionListBox.SelectedItem as MathExpression).GetInformation();
        }
        #endregion

        #region Files
        private void FileListBox_GotFocus(object sender, RoutedEventArgs e)
        {
            InfoModelSelector.ShowFileInfo();
            if (alreadySelected == true)
            {
                alreadySelected = false;
                RequestTextBox.Focus();
            }
        }

        private void FileListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            alreadySelected = false;
            if (FileListBox.Items.Count > 0 && FileListBox.SelectedItems.Count == 1)
            {
                File file = FileListBox.SelectedItem as File;
                GlobalViewModel.SetPostRequestString(file);
                FileInfoViewModel.LoadInfo(file);
                ListModelSelector.SelectFileList();
                if (GlobalSettings.UseHabitsAnalysis == true && GlobalSettings.RememberOnActivation == true)
                {
                    GlobalHabitsAnalyser.FileHabitsAnalyser.AddToHabitsAnalyser(file);
                    GlobalHabitsAnalyser.AddRequestHabit(file);
                }
            }
            RequestTextBox.Focus();
        }

        private void Border_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            FileListBoxContextMenuPopup.IsOpen = true;
            RequestTextBox.Focus();
        }

        private void OpenFileButton_Click(object sender, RoutedEventArgs e)
        {
            (FileListBox.SelectedItem as Models.File).Launch();
            RequestTextBox.Focus();
            FileListBoxContextMenuPopup.IsOpen = false;
        }

        private void OpenFileLocationFileButton_Click(object sender, RoutedEventArgs e)
        {
            (FileListBox.SelectedItem as Models.File).OpenFileLocation();
            RequestTextBox.Focus();
            FileListBoxContextMenuPopup.IsOpen = false;
        }

        private void CopyPathFileButton_Click(object sender, RoutedEventArgs e)
        {
            (FileListBox.SelectedItem as Models.File).CopyPath();
            RequestTextBox.Focus();
            FileListBoxContextMenuPopup.IsOpen = false;
        }

        private void CopyFileButton_Click(object sender, RoutedEventArgs e)
        {
            (FileListBox.SelectedItem as Models.File).Copy();
            RequestTextBox.Focus();
            FileListBoxContextMenuPopup.IsOpen = false;
        }

        private void CreateShortcutFileButton_Click(object sender, RoutedEventArgs e)
        {
            (FileListBox.SelectedItem as Models.File).CreateShortcut();
            RequestTextBox.Focus();
            FileListBoxContextMenuPopup.IsOpen = false;
        }
        #endregion

        #region Folders
        private void FolderListBox_GotFocus(object sender, RoutedEventArgs e)
        {
            InfoModelSelector.ShowFolderInfo();
        }

        private void FolderListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (FolderListBox.Items.Count > 0 && FolderListBox.SelectedItems.Count == 1)
            {
                Folder folder = FolderListBox.SelectedItem as Folder;
                FolderInfoViewModel.LoadInfo(folder);
                ListModelSelector.SelectFolderList();
                if (GlobalSettings.UseHabitsAnalysis == true && GlobalSettings.RememberOnActivation == true)
                {
                    GlobalHabitsAnalyser.FolderHabitsAnalyser.AddToHabitsAnalyser(folder);
                    GlobalHabitsAnalyser.AddRequestHabit(folder);
                }
            }
            RequestTextBox.Focus();
        }
        #endregion

        #region Microsoft Store apps
        private void MicrosoftStoreAppListBox_GotFocus(object sender, RoutedEventArgs e)
        {
            InfoModelSelector.ShowMicrosoftStoreAppInfo();
            if (alreadySelected == true)
            {
                alreadySelected = false;
                RequestTextBox.Focus();
            }
        }

        private void MicrosoftStoreAppListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            alreadySelected = false;
            if (MicrosoftStoreAppListBox.Items.Count > 0 && MicrosoftStoreAppListBox.SelectedItems.Count == 1)
            {
                MicrosoftStoreApplication microsoftStoreApplication = MicrosoftStoreAppListBox.SelectedItem as MicrosoftStoreApplication;
                GlobalViewModel.SetPostRequestString(microsoftStoreApplication);
                MicrosoftStoreAppInfoViewModel.LoadInfo(microsoftStoreApplication);
                ListModelSelector.SelectMicrosoftStoreAppList();
                if (GlobalSettings.UseHabitsAnalysis == true && GlobalSettings.RememberOnActivation == true)
                {
                    GlobalHabitsAnalyser.MicrosoftStoreAppHabitsAnalyser.AddToHabitsAnalyser(microsoftStoreApplication);
                    GlobalHabitsAnalyser.AddRequestHabit(microsoftStoreApplication);
                }
            }
            RequestTextBox.Focus();
        }
        #endregion

        #region Applications
        private void ApplicationListBox_GotFocus(object sender, RoutedEventArgs e)
        {
            InfoModelSelector.ShowApplicationInfo();
        }

        private void ApplicationListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ApplicationListBox.Items.Count > 0 && ApplicationListBox.SelectedItems.Count == 1)
            {
                Models.Application application = ApplicationListBox.SelectedItem as Models.Application;
                ApplicationInfoViewModel.LoadInfo(application);
                ListModelSelector.SelectApplicationList();
                if (GlobalSettings.UseHabitsAnalysis == true && GlobalSettings.RememberOnActivation == true)
                {
                    GlobalHabitsAnalyser.ApplicationHabitsAnalyser.AddToHabitsAnalyser(application);
                    GlobalHabitsAnalyser.AddRequestHabit(application);
                }
            }
            RequestTextBox.Focus();
        }
        #endregion

        #region Settings
        private void SettingsListBox_GotFocus(object sender, RoutedEventArgs e)
        {
            InfoModelSelector.ShowSettingsInfo();
        }

        private void SettingsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SettingsListBox.Items.Count > 0 && SettingsListBox.SelectedItems.Count == 1)
            {
                Models.Settings settings = SettingsListBox.SelectedItem as Models.Settings;
                SettingsInfoViewModel.LoadInfo(settings);
                ListModelSelector.SelectSettingsList();
                if (GlobalSettings.UseHabitsAnalysis == true && GlobalSettings.RememberOnActivation == true)
                {
                    GlobalHabitsAnalyser.SettingsHabitsAnalyser.AddToHabitsAnalyser(settings);
                    GlobalHabitsAnalyser.AddRequestHabit(settings);
                }
            }
            RequestTextBox.Focus();
        }

        private void SettingsHyperlinkTextBlock_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            (SettingsListBox.SelectedItem as Models.Settings).GetInformation();
        }

        private void SettingsQuestionsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            (SettingsQuestionsListBox.SelectedItem as Models.SettingsQuestion).GetAnswer();
        }
        #endregion

        #region Systems
        private void SystemsListBox_GotFocus(object sender, RoutedEventArgs e)
        {
            InfoModelSelector.ShowSystemInfo();
        }

        private void SystemsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SystemsListBox.Items.Count > 0 && SystemsListBox.SelectedItems.Count == 1)
            {
                Models.System system = SystemsListBox.SelectedItem as Models.System;
                SystemInfoViewModel.LoadInfo(system);
                ListModelSelector.SelectSystemList();
                if (GlobalSettings.UseHabitsAnalysis == true && GlobalSettings.RememberOnActivation == true)
                {
                    GlobalHabitsAnalyser.SystemHabitsAnalyser.AddToHabitsAnalyser(system);
                    GlobalHabitsAnalyser.AddRequestHabit(system);
                }
            }
            RequestTextBox.Focus();
        }

        private void SystemHyperlinkTextBlock_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            (SystemsListBox.SelectedItem as Models.System).GetInformation();
        }
        #endregion

        #region WebServices
        private void WebServiceListBox_GotFocus(object sender, RoutedEventArgs e)
        {
            InfoModelSelector.ShowWebServiceInfo();
        }

        private void WebServiceListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (WebServiceListBox.Items.Count > 0 && WebServiceListBox.SelectedItems.Count == 1)
            {
                WebService webService = WebServiceListBox.SelectedItem as WebService;
                ListModelSelector.SelectWebServiceList();
                WebServiceInfoViewModel.LoadInfo(webService);
                if (GlobalSettings.UseHabitsAnalysis == true && GlobalSettings.RememberOnActivation == true)
                {
                    GlobalHabitsAnalyser.WebServicesHabitsAnalyser.AddToHabitsAnalyser(webService);
                    GlobalHabitsAnalyser.AddRequestHabit(webService);
                }
            }
            RequestTextBox.Focus();
        }

        private void WebServiceHyperlinkTextBlock_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            (WebServiceListBox.SelectedItem as WebService).GetInformation();
        }
        #endregion

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Border border = sender as Border;
            SolidColorBrush solidColorBrush = new SolidColorBrush(Colors.LightGray);
            SolidColorBrush solidColorBrush1 = (SolidColorBrush)border.Background;
            if (solidColorBrush1.Color == solidColorBrush.Color)
                alreadySelected = true;
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            SettingsWindow settingsWindow = new SettingsWindow();
            settingsWindow.Show();
        }
    }
}
