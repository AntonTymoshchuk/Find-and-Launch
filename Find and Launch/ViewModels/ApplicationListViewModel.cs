using Find_and_Launch.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Find_and_Launch.ViewModels
{
    public class ApplicationListViewModel : IListViewModel
    {
        private GlobalViewModel globalViewModel;

        public ObservableCollection<Models.Application> Applications { get; }

        public bool IsEmpty
        {
            get
            {
                if (Applications.Count == 0)
                    return true;
                return false;
            }
        }

        public ApplicationListViewModel()
        {
            Applications = new ObservableCollection<Models.Application>();
        }

        public void ConnectWithGlobalViewModel()
        {
            globalViewModel = (System.Windows.Application.Current.MainWindow as MainWindow).GlobalViewModel;
        }

        public void AddApplication(Models.Application application)
        {
            Applications.Add(application);
            if (Applications.Count == 1)
                globalViewModel.ActivateApplicationList();
        }

        public void Clear()
        {
            Applications.Clear();
        }
    }
}
