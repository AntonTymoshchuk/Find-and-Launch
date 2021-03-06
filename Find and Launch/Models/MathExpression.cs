﻿using Find_and_Launch.Abstract;
using Find_and_Launch.HabitsAnalysing;
using Find_and_Launch.Interfaces;
using Find_and_Launch.Settings;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Find_and_Launch.Models
{
    public class MathExpression : Model, ILaunchable
    {
        private readonly List<Brackets> brackets;
        private readonly List<Absolute> absolutes;
        private readonly List<string> subExpressions;
        private readonly List<ExpressionType> expressionTypes;
        private readonly List<string> logicalOrder;
        private readonly List<double> numbers;
        private readonly List<char> actions;
        private readonly List<string> functions;

        public string Description { get; }
        public string InformationUrl { get; }
        public string Expression { get; }
        public double Result { get; }

        public MathExpression(string expression)
        {
            Name = "Calculator";
            Description = "................";
            Expression = expression;
            MediumImage = new BitmapImage(new Uri("/Images/Calculator/MathMediumImage.png", UriKind.Relative));
            LargeImage = new BitmapImage(new Uri("/Images/Calculator/MathLargeImage.png", UriKind.Relative));

            brackets = new List<Brackets>();
            absolutes = new List<Absolute>();
            subExpressions = new List<string>();
            expressionTypes = new List<ExpressionType>();
            logicalOrder = new List<string>();
            numbers = new List<double>();
            actions = new List<char>();
            functions = new List<string>();

            string[] expressionParts = expression.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
            expression = string.Empty;
            foreach (string expressionPart in expressionParts)
                expression += expressionPart;

            BuildLogicalTree(expression, 0);
            Result = Calculate();
        }

        public MathExpression(string expression, double result)
        {
            Name = "Calculator";
            Description = "................";
            Expression = expression;
            MediumImage = new BitmapImage(new Uri("/Images/Calculator/MathMediumImage.png", UriKind.Relative));
            LargeImage = new BitmapImage(new Uri("/Images/Calculator/MathLargeImage.png", UriKind.Relative));
            Result = result;
        }

        private void BuildLogicalTree(string expression, int startIndex)
        {
            string localExpression = string.Empty;
            for (int i = startIndex; i < expression.Length; i++)
            {
                switch (expression[i])
                {
                    case '(':
                        if (localExpression.Equals(string.Empty) == false)
                        {
                            subExpressions.Add(localExpression);
                            localExpression = string.Empty;
                        }
                        else logicalOrder.Add("Farg");

                        Brackets bracketsPair = new Brackets();
                        i = bracketsPair.BuildLogicalTree(expression, i + 1);
                        brackets.Add(bracketsPair);
                        expressionTypes.Add(ExpressionType.BracketsPair);
                        break;
                    case '|':
                        bool absoluteOpen = false;
                        switch (expression[i + 1])
                        {
                            case '(': absoluteOpen = true; break;
                            case '|': absoluteOpen = true; break;
                            case '-': absoluteOpen = true; break;
                            case 's': absoluteOpen = true; break;
                            case 'c': absoluteOpen = true; break;
                            case 't': absoluteOpen = true; break;
                            case 'a': absoluteOpen = true; break;
                            case 'l': absoluteOpen = true; break;
                            case 'e': absoluteOpen = true; break;
                            case 'f': absoluteOpen = true; break;
                            case 'd': absoluteOpen = true; break;
                            case 'r': absoluteOpen = true; break;
                            default:
                                if (char.IsNumber(expression[i + 1]))
                                    absoluteOpen = true;
                                break;
                        }

                        if (absoluteOpen == true)
                        {
                            if (localExpression.Equals(string.Empty) == false)
                                subExpressions.Add(localExpression);
                            localExpression = string.Empty;

                            Absolute absolute = new Absolute();
                            i = absolute.BuildLogicalTree(expression, i + 1);
                            absolutes.Add(absolute);
                            expressionTypes.Add(ExpressionType.Absolute);
                        }
                        else
                            throw new Exception();
                        break;
                    default:
                        if (expressionTypes.Count == 0 || expressionTypes[expressionTypes.Count - 1] != ExpressionType.Expression)
                            expressionTypes.Add(ExpressionType.Expression);
                        localExpression += expression[i];
                        break;
                }
            }
            if (localExpression.Equals(string.Empty) == false)
                subExpressions.Add(localExpression);
        }

        private void BuildLogicalOrder()
        {
            int bracketsPairIndex = 0;
            int absoluteIndex = 0;
            int subExpressionIndex = 0;

            string completeNumber = string.Empty;
            bool numberCompleted;
            bool isNegative;
            int startIndex;

            foreach (ExpressionType expressionType in expressionTypes)
            {
                switch (expressionType)
                {
                    case ExpressionType.BracketsPair:
                        logicalOrder.Add($"B{bracketsPairIndex}");
                        bracketsPairIndex++;
                        break;
                    case ExpressionType.Absolute:
                        logicalOrder.Add($"S{absoluteIndex}");
                        absoluteIndex++;
                        break;
                    case ExpressionType.Expression:
                        string lowercaseSubExpression = subExpressions[subExpressionIndex].ToLower();
                        bool containsFunction = false;
                        string functionName = string.Empty;

                        if (lowercaseSubExpression.EndsWith("sin"))
                        {
                            lowercaseSubExpression = lowercaseSubExpression.Remove(lowercaseSubExpression.Length - 3, 3);
                            containsFunction = true;
                            functionName = "sin";
                        }
                        else if (lowercaseSubExpression.EndsWith("cos"))
                        {
                            lowercaseSubExpression = lowercaseSubExpression.Remove(lowercaseSubExpression.Length - 3, 3);
                            containsFunction = true;
                            functionName = "cos";
                        }
                        else if (lowercaseSubExpression.EndsWith("tg"))
                        {
                            lowercaseSubExpression = lowercaseSubExpression.Remove(lowercaseSubExpression.Length - 2, 2);
                            containsFunction = true;
                            functionName = "tg";
                        }
                        else if (lowercaseSubExpression.EndsWith("ctg"))
                        {
                            lowercaseSubExpression = lowercaseSubExpression.Remove(lowercaseSubExpression.Length - 3, 3);
                            containsFunction = true;
                            functionName = "ctg";
                        }
                        else if (lowercaseSubExpression.EndsWith("arcsin"))
                        {
                            lowercaseSubExpression = lowercaseSubExpression.Remove(lowercaseSubExpression.Length - 6, 6);
                            containsFunction = true;
                            functionName = "arcsin";
                        }
                        else if (lowercaseSubExpression.EndsWith("arccos"))
                        {
                            lowercaseSubExpression = lowercaseSubExpression.Remove(lowercaseSubExpression.Length - 6, 6);
                            containsFunction = true;
                            functionName = "arccos";
                        }
                        else if (lowercaseSubExpression.EndsWith("arctg"))
                        {
                            lowercaseSubExpression = lowercaseSubExpression.Remove(lowercaseSubExpression.Length - 5, 5);
                            containsFunction = true;
                            functionName = "arctg";
                        }
                        else if (lowercaseSubExpression.EndsWith("arcctg"))
                        {
                            lowercaseSubExpression = lowercaseSubExpression.Remove(lowercaseSubExpression.Length - 6, 6);
                            containsFunction = true;
                            functionName = "arcctg";
                        }
                        else if (lowercaseSubExpression.EndsWith("sinh"))
                        {
                            lowercaseSubExpression = lowercaseSubExpression.Remove(lowercaseSubExpression.Length - 4, 4);
                            containsFunction = true;
                            functionName = "sinh";
                        }
                        else if (lowercaseSubExpression.EndsWith("cosh"))
                        {
                            lowercaseSubExpression = lowercaseSubExpression.Remove(lowercaseSubExpression.Length - 4, 4);
                            containsFunction = true;
                            functionName = "cosh";
                        }
                        else if (lowercaseSubExpression.EndsWith("tgh"))
                        {
                            lowercaseSubExpression = lowercaseSubExpression.Remove(lowercaseSubExpression.Length - 3, 3);
                            containsFunction = true;
                            functionName = "tgh";
                        }
                        else if (lowercaseSubExpression.EndsWith("ctgh"))
                        {
                            lowercaseSubExpression = lowercaseSubExpression.Remove(lowercaseSubExpression.Length - 4, 4);
                            containsFunction = true;
                            functionName = "ctgh";
                        }
                        else if (lowercaseSubExpression.EndsWith("ln"))
                        {
                            lowercaseSubExpression = lowercaseSubExpression.Remove(lowercaseSubExpression.Length - 2, 2);
                            containsFunction = true;
                            functionName = "ln";
                        }
                        else if (lowercaseSubExpression.EndsWith("exp"))
                        {
                            lowercaseSubExpression = lowercaseSubExpression.Remove(lowercaseSubExpression.Length - 3, 3);
                            containsFunction = true;
                            functionName = "exp";
                        }
                        else if (lowercaseSubExpression.EndsWith("truncate"))
                        {
                            lowercaseSubExpression = lowercaseSubExpression.Remove(lowercaseSubExpression.Length - 8, 8);
                            containsFunction = true;
                            functionName = "truncate";
                        }
                        else if (lowercaseSubExpression.EndsWith("sqrt"))
                        {
                            lowercaseSubExpression = lowercaseSubExpression.Remove(lowercaseSubExpression.Length - 4, 4);
                            containsFunction = true;
                            functionName = "sqrt";
                        }
                        else if (lowercaseSubExpression.EndsWith("abs"))
                        {
                            lowercaseSubExpression = lowercaseSubExpression.Remove(lowercaseSubExpression.Length - 3, 3);
                            containsFunction = true;
                            functionName = "abs";
                        }
                        else if (lowercaseSubExpression.EndsWith("ceiling"))
                        {
                            lowercaseSubExpression = lowercaseSubExpression.Remove(lowercaseSubExpression.Length - 7, 7);
                            containsFunction = true;
                            functionName = "ceiling";
                        }
                        else if (lowercaseSubExpression.EndsWith("floor"))
                        {
                            lowercaseSubExpression = lowercaseSubExpression.Remove(lowercaseSubExpression.Length - 5, 5);
                            containsFunction = true;
                            functionName = "floor";
                        }
                        else if (lowercaseSubExpression.EndsWith("rad"))
                        {
                            lowercaseSubExpression = lowercaseSubExpression.Remove(lowercaseSubExpression.Length - 3, 3);
                            containsFunction = true;
                            functionName = "rad";
                        }
                        else if (lowercaseSubExpression.EndsWith("deg"))
                        {
                            lowercaseSubExpression = lowercaseSubExpression.Remove(lowercaseSubExpression.Length - 3, 3);
                            containsFunction = true;
                            functionName = "deg";
                        }

                        if (lowercaseSubExpression.Length > 0)
                        {
                            isNegative = false;
                            startIndex = 0;

                            if (lowercaseSubExpression[0] == '-')
                            {
                                completeNumber += '-';
                                isNegative = true;
                                startIndex = 1;
                            }

                            for (int i = startIndex; i < lowercaseSubExpression.Length; i++)
                            {
                                if (isNegative == false)
                                    completeNumber = string.Empty;
                                isNegative = false;
                                numberCompleted = false;

                                if (char.IsNumber(lowercaseSubExpression[i]))
                                {
                                    completeNumber += lowercaseSubExpression[i];

                                    for (int j = i + 1; j < lowercaseSubExpression.Length; j++)
                                    {
                                        if (char.IsNumber(lowercaseSubExpression[j]))
                                            completeNumber += lowercaseSubExpression[j];
                                        else
                                        {
                                            switch (lowercaseSubExpression[j])
                                            {
                                                case '.':
                                                    completeNumber += '.';
                                                    break;
                                                case ',':
                                                    completeNumber += '.';
                                                    break;
                                                default:
                                                    numberCompleted = true;
                                                    break;
                                            }
                                        }

                                        if (numberCompleted == true)
                                        {
                                            i = j - 1;
                                            break;
                                        }

                                        if (j == lowercaseSubExpression.Length - 1)
                                            i = j;
                                    }

                                    logicalOrder.Add($"N{completeNumber}");
                                }
                                else
                                {
                                    switch (lowercaseSubExpression[i])
                                    {
                                        case '+':
                                            logicalOrder.Add("A+");
                                            break;
                                        case '-':
                                            logicalOrder.Add("A-");
                                            break;
                                        case '*':
                                            logicalOrder.Add("A*");
                                            break;
                                        case '/':
                                            logicalOrder.Add("A/");
                                            break;
                                        case '%':
                                            logicalOrder.Add("A%");
                                            break;
                                        case '^':
                                            logicalOrder.Add("A^");
                                            break;
                                        case '√':
                                            logicalOrder.Add("A√");
                                            break;
                                        case 'r':
                                            logicalOrder.Add("Ar");
                                            break;
                                        case '㏒':
                                            logicalOrder.Add("A㏒");
                                            break;
                                        case 'l':
                                            logicalOrder.Add("Al");
                                            break;
                                        default:
                                            string symbolAsString = string.Empty;
                                            symbolAsString += lowercaseSubExpression[i];
                                            if (symbolAsString.Equals("p") || symbolAsString.Equals("e"))
                                                logicalOrder.Add($"C{symbolAsString}");
                                            break;
                                    }
                                }
                            }
                        }

                        if (containsFunction == true)
                            logicalOrder.Add($"F{functionName}");
                        else logicalOrder.Add("Farg");

                        subExpressionIndex++;
                        break;
                }
            }
        }

        private double Calculate()
        {
            BuildLogicalOrder();
            foreach (string logicalOrderItem in logicalOrder)
            {
                switch (logicalOrderItem[0])
                {
                    case 'N':
                        numbers.Add(Convert.ToDouble(logicalOrderItem.TrimStart('N')));
                        break;
                    case 'A':
                        actions.Add(logicalOrderItem[1]);
                        break;
                    case 'F':
                        functions.Add(logicalOrderItem.Substring(1, logicalOrderItem.Length - 1));
                        break;
                    case 'B':
                        double result = brackets[Convert.ToInt32(logicalOrderItem.TrimStart('B'))].Calculate();

                        switch (functions[0])
                        {
                            case "arg":
                                numbers.Add(result);
                                break;
                            case "sin":
                                numbers.Add(Math.Sin(result));
                                break;
                            case "cos":
                                numbers.Add(Math.Cos(result));
                                break;
                            case "tg":
                                numbers.Add(Math.Tan(result));
                                break;
                            case "ctg":
                                numbers.Add(Math.Cos(result) / Math.Sin(result));
                                break;
                            case "arcsin":
                                numbers.Add(Math.Asin(result));
                                break;
                            case "arccos":
                                numbers.Add(Math.Acos(result));
                                break;
                            case "arctg":
                                numbers.Add(Math.Atan(result));
                                break;
                            case "arcctg":
                                numbers.Add(Math.Acos(result) / Math.Asin(result));
                                break;
                            case "sinh":
                                numbers.Add(Math.Sinh(result));
                                break;
                            case "cosh":
                                numbers.Add(Math.Cosh(result));
                                break;
                            case "tgh":
                                numbers.Add(Math.Tanh(result));
                                break;
                            case "ctgh":
                                numbers.Add(Math.Cosh(result) / Math.Sinh(result));
                                break;
                            case "ln":
                                numbers.Add(Math.Log10(result));
                                break;
                            case "exp":
                                numbers.Add(Math.Exp(result));
                                break;
                            case "truncate":
                                numbers.Add(Math.Truncate(result));
                                break;
                            case "sqrt":
                                numbers.Add(Math.Sqrt(result));
                                break;
                            case "abs":
                                numbers.Add(Math.Abs(result));
                                break;
                            case "ceiling":
                                numbers.Add(Math.Ceiling(result));
                                break;
                            case "floor":
                                numbers.Add(Math.Floor(result));
                                break;
                            case "rad":
                                numbers.Add(result * Math.PI / 180);
                                break;
                            case "deg":
                                numbers.Add(result * 180 / Math.PI);
                                break;
                        }

                        functions.RemoveAt(0);
                        break;
                    case 'S':
                        numbers.Add(absolutes[Convert.ToInt32(logicalOrderItem.TrimStart('S'))].Calculate());
                        break;
                    case 'C':
                        switch (logicalOrderItem[1])
                        {
                            case 'p':
                                numbers.Add(Math.PI);
                                break;
                            case 'e':
                                numbers.Add(Math.E);
                                break;
                        }
                        break;
                }
            }

            int index = 0;
            if (actions.Count > 0)
            {
                bool firstPriorityActionsAviliable = true;
                while (firstPriorityActionsAviliable == true)
                {
                    switch (actions[index])
                    {
                        case '^':
                            numbers[index] = Math.Pow(numbers[index], numbers[index + 1]);
                            numbers.RemoveAt(index + 1);
                            actions.RemoveAt(index);
                            index = 0;
                            break;
                        case '√':
                            numbers[index] = Math.Sqrt(numbers[index]);
                            actions.RemoveAt(index);
                            index = 0;
                            break;
                        case 'r':
                            numbers[index] = Math.Sqrt(numbers[index]);
                            actions.RemoveAt(index);
                            index = 0;
                            break;
                        case '㏒':
                            numbers[index] = Math.Log(numbers[index], numbers[index + 1]);
                            numbers.RemoveAt(index + 1);
                            actions.RemoveAt(index);
                            index = 0;
                            break;
                        case 'l':
                            numbers[index] = Math.Log(numbers[index], numbers[index + 1]);
                            numbers.RemoveAt(index + 1);
                            actions.RemoveAt(index);
                            index = 0;
                            break;
                        default:
                            index++;
                            break;
                    }

                    int firstPriorityActionsCount = 0;
                    for (int i = 0; i < actions.Count; i++)
                    {
                        switch (actions[i])
                        {
                            case '^':
                                firstPriorityActionsCount++;
                                break;
                            case '√':
                                firstPriorityActionsCount++;
                                break;
                            case 'r':
                                firstPriorityActionsCount++;
                                break;
                            case '㏒':
                                firstPriorityActionsCount++;
                                break;
                            case 'l':
                                firstPriorityActionsCount++;
                                break;
                        }
                    }
                    if (firstPriorityActionsCount == 0)
                        firstPriorityActionsAviliable = false;
                }
            }

            if (actions.Count > 0)
            {
                index = 0;
                bool secondPriorityActionsAviliable = true;
                while (secondPriorityActionsAviliable == true)
                {
                    switch (actions[index])
                    {
                        case '*':
                            numbers[index] *= numbers[index + 1];
                            numbers.RemoveAt(index + 1);
                            actions.RemoveAt(index);
                            index = 0;
                            break;
                        case '/':
                            numbers[index] /= numbers[index + 1];
                            numbers.RemoveAt(index + 1);
                            actions.RemoveAt(index);
                            index = 0;
                            break;
                        case '%':
                            numbers[index] = numbers[index] / 100 * numbers[index + 1];
                            numbers.RemoveAt(index + 1);
                            actions.RemoveAt(index);
                            index = 0;
                            break;
                        default:
                            index++;
                            break;
                    }

                    int secondPriorityActionsCount = 0;
                    for (int i = 0; i < actions.Count; i++)
                    {
                        switch (actions[i])
                        {
                            case '*':
                                secondPriorityActionsCount++;
                                break;
                            case '/':
                                secondPriorityActionsCount++;
                                break;
                            case '%':
                                secondPriorityActionsCount++;
                                break;
                        }
                    }
                    if (secondPriorityActionsCount == 0)
                        secondPriorityActionsAviliable = false;
                }
            }

            if (actions.Count > 0)
            {
                index = 0;
                bool thirdPriorityActionsAviliable = true;
                while (thirdPriorityActionsAviliable == true)
                {
                    switch (actions[index])
                    {
                        case '+':
                            numbers[index] += numbers[index + 1];
                            numbers.RemoveAt(index + 1);
                            actions.RemoveAt(index);
                            index = 0;
                            break;
                        case '-':
                            numbers[index] -= numbers[index + 1];
                            numbers.RemoveAt(index + 1);
                            actions.RemoveAt(index);
                            index = 0;
                            break;
                        default:
                            index++;
                            break;
                    }

                    int thirdPriorityActionsCount = 0;
                    for (int i = 0; i < actions.Count; i++)
                    {
                        switch (actions[i])
                        {
                            case '+':
                                thirdPriorityActionsCount++;
                                break;
                            case '-':
                                thirdPriorityActionsCount++;
                                break;
                        }
                    }
                    if (thirdPriorityActionsCount == 0)
                        thirdPriorityActionsAviliable = false;
                }
            }

            return numbers[0];
        }

        public void Launch()
        {
            MessageManager.Message.Show("Math expression", MessageManager.MessageType.Notification);
            if (GlobalSettings.UseHabitsAnalysis == true && GlobalSettings.RememberOnLaunchment == true)
            {
                GlobalHabitsAnalyser.MathExpressionHabitsAnalyser.AddToHabitsAnalyser(this);
                GlobalHabitsAnalyser.AddRequestHabit(this);
            }
        }

        public void GetInformation()
        {
            Process.Start(InformationUrl);
        }
    }

    public enum ExpressionType
    {
        Expression, BracketsPair, Absolute
    }
}
