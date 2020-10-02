using Find_and_Launch.Abstract;
using Find_and_Launch.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Find_and_Launch.ViewModels
{
    public class WebServiceInfoViewModel : InfoViewModel, IInfoViewModel
    {
        private string description;

        public string Description
        {
            get { return description; }
            set
            {
                description = value;
                OnPropertyChanged("Description");
            }
        }

        public WebServiceInfoViewModel() { }

        public void LoadInfo(object source)
        {
            Models.WebService webService = source as Models.WebService;
            Type = webService.Type;
            Name = webService.Name;
            Description = webService.Description;
            LargeImage = webService.LargeImage;
        }
    }
}
