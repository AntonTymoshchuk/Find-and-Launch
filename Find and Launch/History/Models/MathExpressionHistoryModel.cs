using Find_and_Launch.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Find_and_Launch.History.Models
{
    public class MathExpressionHistoryModel : IModelEntity
    {
        public string Expression { get; set; }

        public MathExpressionHistoryModel() { }

        public MathExpressionHistoryModel(string mathExpressionHistoryModelEntity)
        {
            Expression = mathExpressionHistoryModelEntity;
        }

        public string GetModelEntity()
        {
            return Expression;
        }
    }
}
