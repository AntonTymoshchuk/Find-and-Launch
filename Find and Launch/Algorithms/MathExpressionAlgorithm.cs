using Find_and_Launch.HabitsAnalysing;
using Find_and_Launch.Interfaces;
using Find_and_Launch.Models;
using Find_and_Launch.Settings;
using Find_and_Launch.Validators;
using Find_and_Launch.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Find_and_Launch.Algorithms
{
    public class MathExpressionAlgorithm : IAlgorithm
    {
        private readonly MathExpressionListViewModel mathExpressionListViewModel;

        private readonly MathExpressionValidator mathExpressionValidator;

        public MathExpressionAlgorithm()
        {
            mathExpressionListViewModel = (System.Windows.Application.Current.MainWindow as MainWindow).MathExpressionListViewModel;
            mathExpressionValidator = new MathExpressionValidator();
        }

        public void Start(string request)
        {
            if (mathExpressionValidator.Validate(request, null) == true)
            {
                System.Windows.Application.Current.Dispatcher.Invoke(() =>
                { mathExpressionListViewModel.AddMathExpression(new MathExpression(request, Convert.ToDouble(mathExpressionValidator.Result))); });
            }
            if (GlobalSettings.UseHabitsAnalysis == true && mathExpressionListViewModel.IsEmpty == false)
                GlobalHabitsAnalyser.MathExpressionHabitsAnalyser.SortByHabitsAnalysis(mathExpressionListViewModel.MathExpressions);
        }
    }
}
