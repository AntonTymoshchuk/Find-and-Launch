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
    public class FolderListViewModel : IListViewModel
    {
        private GlobalViewModel globalViewModel;

        public ObservableCollection<Folder> Folders { get; }

        public bool IsEmpty
        {
            get
            {
                if (Folders.Count == 0)
                    return true;
                return false;
            }
        }

        public FolderListViewModel()
        {
            Folders = new ObservableCollection<Folder>();
        }

        public void ConnectWithGlobalViewModel()
        {
            globalViewModel = (System.Windows.Application.Current.MainWindow as MainWindow).GlobalViewModel;
        }

        public void AddFolder(Folder folder)
        {
            Folders.Add(folder);
            if (Folders.Count == 1)
                globalViewModel.ActivateFolderList();
        }

        public void Clear()
        {
            Folders.Clear();
        }
    }
}
