using Find_and_Launch.Abstract;
using Find_and_Launch.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Find_and_Launch.HabitsAnalysing
{
    public class MathExpressionHabitsAnalyser : HabitsAnalyser, IHabitsAnalyser
    {
        private List<string> usedMathExpressionInfos;

        public MathExpressionHabitsAnalyser() : base()
        {
            usedMathExpressionInfos = new List<string>();
            LoadData();
        }

        public void LoadData()
        {
            FileStream fileStream = null;
            try
            {
                string applicationDataPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                string settingsPath = Path.Combine(applicationDataPath, @"Find and Launch\MathExpressionHabits.txt");
                fileStream = new FileStream(settingsPath, FileMode.Open, FileAccess.Read, FileShare.Read);
                StreamReader streamReader = new StreamReader(fileStream);
                while (streamReader.EndOfStream != true)
                    usedMathExpressionInfos.Add(streamReader.ReadLine());
                streamReader.Close();
                fileStream.Close();
            }
            catch
            { fileStream.Close(); }
        }

        public void SortByHabitsAnalysis(object data)
        {
            DefiningObject = null;
            HabitsAreFound = false;

            List<string> usedMathExpressionStrings = new List<string>();
            List<string> usedMathExpressionResults = new List<string>();

            for (int i = 0; i < usedMathExpressionInfos.Count; i++)
            {
                string[] mathExpressionInfos = usedMathExpressionInfos[i].Split('=');
                usedMathExpressionStrings.Add(mathExpressionInfos[0]);
                usedMathExpressionResults.Add(mathExpressionInfos[1]);
            }

            ObservableCollection<Models.MathExpression> mathExpressions = data as ObservableCollection<Models.MathExpression>;
            List<Models.MathExpression> usedMathExpressions = new List<Models.MathExpression>();
            List<int> usedMathExpresionIndexes = new List<int>();

            for (int i = 0; i < usedMathExpressionStrings.Count; i++)
            {
                if (usedMathExpressionStrings[i].ToLower().Contains(mathExpressions[0].Expression.ToLower()) ||
                    mathExpressions[0].Expression.ToLower().Contains(usedMathExpressionStrings[i].ToLower()))
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        usedMathExpressions.Add(new Models.MathExpression(usedMathExpressionStrings[i],
                            Convert.ToDouble(usedMathExpressionResults[i])));
                    });
                    usedMathExpresionIndexes.Add(i);
                }
            }

            int currentIndex;
            Models.MathExpression currentMathExpression;

            for (int i = 1; i < usedMathExpresionIndexes.Count; i++)
            {
                currentIndex = usedMathExpresionIndexes[i];
                currentMathExpression = usedMathExpressions[i];
                int j = i;
                while (j > 0 && currentIndex < usedMathExpresionIndexes[j - 1])
                {
                    usedMathExpresionIndexes[j] = usedMathExpresionIndexes[j - 1];
                    usedMathExpressions[j] = usedMathExpressions[j - 1];
                    j--;
                }
                usedMathExpresionIndexes[j] = currentIndex;
                usedMathExpressions[j] = currentMathExpression;
            }

            for (int i = 0; i < usedMathExpressions.Count; i++)
            {
                Application.Current.Dispatcher.Invoke(() =>
                { mathExpressions.Add(usedMathExpressions[i]); });
            }

            if (usedMathExpressions.Count > 0)
            {
                DefiningObject = usedMathExpressions[0];
                HabitsAreFound = true;
            }
        }

        public void AddToHabitsAnalyser(object data)
        {
            Models.MathExpression mathExpression = data as Models.MathExpression;
            string mathExpressionInfo = mathExpression.Expression + '=' + Convert.ToString(mathExpression.Result);
            usedMathExpressionInfos.Remove(mathExpressionInfo);
            usedMathExpressionInfos.Insert(0, mathExpressionInfo);
        }

        public void SaveData()
        {
            FileStream fileStream = null;
            try
            {
                string applicationDataPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                string settingsPath = Path.Combine(applicationDataPath, @"Find and Launch\MathExpressionHabits.txt");
                fileStream = new FileStream(settingsPath, FileMode.Open, FileAccess.Write, FileShare.Write);
                StreamWriter streamWriter = new StreamWriter(fileStream);
                foreach (string usedMathExpressionInfo in usedMathExpressionInfos)
                    streamWriter.WriteLine(usedMathExpressionInfo);
                streamWriter.Close();
                fileStream.Close();
            }
            catch
            { fileStream.Close(); }
        }
    }
}
