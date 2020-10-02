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
    public class SystemInfoViewModel : InfoViewModel, IInfoViewModel
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

        public SystemInfoViewModel() { }

        public void LoadInfo(object source)
        {
            Models.System system = source as Models.System;
            Type = system.Type;
            Name = system.Name;
            Description = system.Description;
            LargeImage = system.LargeImage;
        }
    }
}
