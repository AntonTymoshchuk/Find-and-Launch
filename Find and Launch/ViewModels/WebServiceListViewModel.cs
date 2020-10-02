using Find_and_Launch.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Find_and_Launch.ViewModels
{
    public class WebServiceListViewModel : IListViewModel
    {
        private GlobalViewModel globalViewModel;

        public ObservableCollection<Models.WebService> WebServices { get; }

        public bool IsEmpty
        {
            get
            {
                if (WebServices.Count == 0)
                    return true;
                return false;
            }
        }

        public WebServiceListViewModel()
        {
            WebServices = new ObservableCollection<Models.WebService>();
        }

        public void ConnectWithGlobalViewModel()
        {
            globalViewModel = (System.Windows.Application.Current.MainWindow as MainWindow).GlobalViewModel;
        }

        public void AddWebService(Models.WebService webService)
        {
            WebServices.Add(webService);
            if (WebServices.Count == 1)
                globalViewModel.ActivateWebServiceList();
        }

        public void Clear()
        {
            WebServices.Clear();
        }
    }
}
