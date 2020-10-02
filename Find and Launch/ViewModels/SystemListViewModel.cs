using Find_and_Launch.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Find_and_Launch.ViewModels
{
    public class SystemListViewModel : IListViewModel
    {
        private GlobalViewModel globalViewModel;

        public ObservableCollection<Models.System> Systems { get; }

        public bool IsEmpty
        {
            get
            {
                if (Systems.Count == 0)
                    return true;
                return false;
            }
        }

        public SystemListViewModel()
        {
            Systems = new ObservableCollection<Models.System>();
        }

        public void ConnectWithGlobalViewModel()
        {
            globalViewModel = (System.Windows.Application.Current.MainWindow as MainWindow).GlobalViewModel;
        }

        public void AddSystemService(Models.System system)
        {
            Systems.Add(system);
            if (Systems.Count == 1)
                globalViewModel.ActivateSystemServiceList();
        }

        public void Clear()
        {
            Systems.Clear();
        }
    }
}
