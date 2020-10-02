using Find_and_Launch.Abstract;
using Find_and_Launch.Interfaces;
using Find_and_Launch.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Find_and_Launch.ViewModels
{
    public class SettingsInfoViewModel : InfoViewModel, IInfoViewModel
    {
        private string category;
        private string path;
        private string description;
        private ObservableCollection<SettingsQuestion> settingsQuestions;

        public string Category
        {
            get { return category; }
            set
            {
                category = value;
                OnPropertyChanged("Category");
            }
        }

        public string Path
        {
            get { return path; }
            set
            {
                path = value;
                OnPropertyChanged("Path");
            }
        }

        public string Description
        {
            get { return description; }
            set
            {
                description = value;
                OnPropertyChanged("Description");
            }
        }

        public ObservableCollection<SettingsQuestion> SettingsQuestions
        {
            get { return settingsQuestions; }
            set
            {
                settingsQuestions = value;
                OnPropertyChanged("SettingsQuestions");
            }
        }

        public SettingsInfoViewModel() { }

        public void LoadInfo(object source)
        {
            Models.Settings settings = source as Models.Settings;
            Type = settings.Type;
            Name = settings.Name;
            Category = settings.Category;
            Path = settings.Path;
            Description = settings.Description;
            SettingsQuestions = settings.SettingsQuestions;
            LargeImage = settings.LargeImage;
        }
    }
}
