using Find_and_Launch.Interfaces;
using Find_and_Launch.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Find_and_Launch.ViewModels
{
    public class MicrosoftStoreAppsListViewModel : IListViewModel
    {
        private GlobalViewModel globalViewModel;

        public ObservableCollection<MicrosoftStoreApplication> MicrosoftStoreApplications { get; }

        public bool IsEmpty
        {
            get
            {
                if (MicrosoftStoreApplications.Count == 0)
                    return true;
                return false;
            }
        }

        public MicrosoftStoreAppsListViewModel()
        {
            MicrosoftStoreApplications = new ObservableCollection<MicrosoftStoreApplication>();
        }

        public void ConnectWithGlobalViewModel()
        {
            globalViewModel = (System.Windows.Application.Current.MainWindow as MainWindow).GlobalViewModel;
        }

        public void AddMicrosoftStoreApp(MicrosoftStoreApplication microsoftStoreApplication)
        {
            MicrosoftStoreApplications.Add(microsoftStoreApplication);
            if (MicrosoftStoreApplications.Count == 1)
                globalViewModel.ActivateMicrosoftStoreAppList();
        }

        public void Clear()
        {
            MicrosoftStoreApplications.Clear();
        }
    }
}
