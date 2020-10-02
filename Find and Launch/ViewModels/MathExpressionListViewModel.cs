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
    public class MathExpressionListViewModel : IListViewModel
    {
        private GlobalViewModel globalViewModel;

        public ObservableCollection<MathExpression> MathExpressions { get; }

        public bool IsEmpty
        {
            get
            {
                if (MathExpressions.Count == 0)
                    return true;
                return false;
            }
        }

        public MathExpressionListViewModel()
        {
            MathExpressions = new ObservableCollection<MathExpression>();
        }

        public void ConnectWithGlobalViewModel()
        {
            globalViewModel = (System.Windows.Application.Current.MainWindow as MainWindow).GlobalViewModel;
        }

        public void AddMathExpression(MathExpression mathExpression)
        {
            MathExpressions.Add(mathExpression);
            if (MathExpressions.Count == 1)
                globalViewModel.ActivateMathExpressionList();
        }

        public void Clear()
        {
            MathExpressions.Clear();
        }
    }
}
