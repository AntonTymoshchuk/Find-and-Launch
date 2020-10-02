using Find_and_Launch.Abstract;
using Find_and_Launch.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Find_and_Launch.ViewModels
{
    public class MathExpressionInfoViewModel : InfoViewModel, IInfoViewModel
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

        public MathExpressionInfoViewModel() { }

        public void LoadInfo(object source)
        {
            Models.MathExpression mathExpression = source as Models.MathExpression;
            Type = mathExpression.Type;
            Name = mathExpression.Name;
            Description = mathExpression.Description;
            LargeImage = mathExpression.LargeImage;
        }
    }
}
