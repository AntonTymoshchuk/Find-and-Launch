using Find_and_Launch.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Find_and_Launch.ViewModels
{
    public class SettingsListViewModel : IListViewModel
    {
        private GlobalViewModel globalViewModel;

        public ObservableCollection<Models.Settings> Settings { get; }

        public bool IsEmpty
        {
            get
            {
                if (Settings.Count == 0)
                    return true;
                return false;
            }
        }

        public SettingsListViewModel()
        {
            Settings = new ObservableCollection<Models.Settings>();
        }

        public void ConnectWithGlobalViewModel()
        {
            globalViewModel = (System.Windows.Application.Current.MainWindow as MainWindow).GlobalViewModel;
        }

        public void AddSettings(Models.Settings settings)
        {
            Settings.Add(settings);
            if (Settings.Count == 1)
                globalViewModel.ActivateSettingsList();
        }

        public void Clear()
        {
            Settings.Clear();
        }
    }
}
