using Find_and_Launch.HabitsAnalysing;
using Find_and_Launch.Interfaces;
using Find_and_Launch.Selectors;
using Find_and_Launch.Settings;
using Find_and_Launch.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Find_and_Launch.Algorithms
{
    public class WebServiceAlgorithm : IAlgorithm
    {
        private readonly WebServiceListViewModel webServiceListViewModel;

        public WebServiceAlgorithm()
        {
            webServiceListViewModel = (Application.Current.MainWindow as MainWindow).WebServiceListViewModel;
        }

        public void Start(string request)
        {
            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                AddWebService(new Models.WebService(request, "Google Search"));
                AddWebService(new Models.WebService(request, "Google Images"));
                AddWebService(new Models.WebService(request, "YouTube"));
            }));
            if (GlobalSettings.UseHabitsAnalysis == true)
                GlobalHabitsAnalyser.WebServicesHabitsAnalyser.SortByHabitsAnalysis(webServiceListViewModel.WebServices);
        }

        private void AddWebService(Models.WebService webService)
        {
            webServiceListViewModel.AddWebService(webService);
        }
    }
}
