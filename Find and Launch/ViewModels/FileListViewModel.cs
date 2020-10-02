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
    public class FileListViewModel : IListViewModel
    {
        private GlobalViewModel globalViewModel;

        public ObservableCollection<File> Files { get; }

        public bool IsEmpty
        {
            get
            {
                if (Files.Count == 0)
                    return true;
                return false;
            }
        }

        public FileListViewModel()
        {
            Files = new ObservableCollection<File>();
        }

        public void ConnectWithGlobalViewModel()
        {
            globalViewModel = (System.Windows.Application.Current.MainWindow as MainWindow).GlobalViewModel;
        }

        public void AddFile(File file)
        {
            Files.Add(file);
            if (Files.Count == 1)
                globalViewModel.ActivateFileList();
        }

        public void Clear()
        {
            Files.Clear();
        }
    }
}
