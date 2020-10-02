using Find_and_Launch.Interfaces;
using Find_and_Launch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Find_and_Launch.Validators
{
    public class MathExpressionValidator : IValidator
    {
        public double? Result { get; private set; }

        public bool CheckWithSettings(object data)
        {
            return true;
        }

        public bool CompareWithRequest(string request, object data)
        {
            return true;
        }

        public bool Validate(string request, object data)
        {
            int openBracketsCount = 0;
            int closeBracketsCount = 0;
            int absoluteBarsCount = 0;

            foreach (char symbol in request)
            {
                switch (symbol)
                {
                    case '(': openBracketsCount++; break;
                    case ')': closeBracketsCount++; break;
                    case '|': absoluteBarsCount++; break;
                    default: continue;
                }
            }

            if (openBracketsCount != closeBracketsCount || absoluteBarsCount % 2 != 0)
                return false;

            try
            {
                MathExpression mathExpression = new MathExpression(request);
                Result = mathExpression.Result;
                return true;
            }
            catch { Result = null; return false; }
        }
    }
}
