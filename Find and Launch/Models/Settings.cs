using Find_and_Launch.Abstract;
using Find_and_Launch.HabitsAnalysing;
using Find_and_Launch.Interfaces;
using Find_and_Launch.Settings;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Find_and_Launch.Models
{
    public class Settings : Model, ILaunchable
    {
        private string Command { get; }
        private string InformationUrl { get; }
        public string Category { get; }
        public string Path { get; }
        public string Description { get; }
        public ObservableCollection<SettingsQuestion> SettingsQuestions { get; }
        public string BeginNamePart { get; private set; }
        public string RequestNamePart { get; private set; }
        public string EndNamePart { get; private set; }

        public Settings(string request, string name)
        {
            Type = "Settings";

            Name = name;
            SeparateNameOnParts(request);

            switch (Name)
            {
                case "Settings":
                    Command = "ms-settings";
                    InformationUrl = @"https://support.microsoft.com/en-us/search?query=Settings%20in%20Windows%2010";
                    Category = "-";
                    Path = "Settings";
                    Description = "";
                    SettingsQuestions = new ObservableCollection<SettingsQuestion>();
                    break;

                case "Display":
                    Command = "ms-settings:display";
                    InformationUrl = @"https://support.microsoft.com/en-us/search?query=Display%20settings%20in%20Windows%2010";
                    Category = "System";
                    Path = "Settings > System > Display";
                    Description = "Most of the advanced display settings from previous versions of Windows are now available on the Display settings page.";
                    SettingsQuestions = new ObservableCollection<SettingsQuestion>()
                    {
                        new SettingsQuestion("Set up multiple monitors", "https://support.microsoft.com/en-us/help/4340331/windows-10-set-up-dual-monitors"),
                        new SettingsQuestion("Change screen brightness", "https://support.microsoft.com/en-us/help/4026946/windows-10-change-screen-brightness"),
                        new SettingsQuestion("Fix screen flickering", "https://support.microsoft.com/en-us/help/4026160/windows-10-fix-screen-flickering"),
                        new SettingsQuestion("Adjust font size", "https://support.microsoft.com/en-us/help/4028566/windows-10-change-the-size-of-text")
                    };
                    MediumImage = new BitmapImage(new Uri("/Images/Settings/Display.png", UriKind.Relative));
                    LargeImage = new BitmapImage(new Uri("/Images/Settings/Display.png", UriKind.Relative));
                    break;

                case "Night light settings":
                    Command = "ms-settings:nightlight";
                    InformationUrl = @"https://support.microsoft.com/en-us/search?query=Night%20light%20settings%20in%20Windows%2010";
                    Category = "System";
                    Path = "Settings > System > Display > Night light settings";
                    Description = "";
                    SettingsQuestions = new ObservableCollection<SettingsQuestion>();
                    break;
            }
        }

        public void Launch()
        {
            try { Process.Start(Command); } catch { }
            if (GlobalSettings.UseHabitsAnalysis == true && GlobalSettings.RememberOnLaunchment == true)
            {
                GlobalHabitsAnalyser.SettingsHabitsAnalyser.AddToHabitsAnalyser(this);
                GlobalHabitsAnalyser.AddRequestHabit(this);
            }
        }

        public void GetInformation()
        {
            Process.Start(InformationUrl);
        }

        private void SeparateNameOnParts(string request)
        {
            string lowercaseName = Name.ToLower();
            request = request.ToLower();
            for (int i = 0; i < (Name.Length - request.Length) + 1; i++)
            {
                if (lowercaseName.Substring(i, request.Length).Equals(request))
                {
                    BeginNamePart = Name.Substring(0, i);
                    RequestNamePart = Name.Substring(i, request.Length);
                    EndNamePart = Name.Substring(i + request.Length,
                        Name.Length - (BeginNamePart.Length + RequestNamePart.Length));
                    break;
                }
            }
        }
    }

    public class SettingsQuestion
    {
        public string Question { get; }
        public string AnswerUrl { get; }

        public SettingsQuestion(string question, string answerUrl)
        {
            Question = question;
            AnswerUrl = answerUrl;
        }

        public void GetAnswer()
        {
            Process.Start(AnswerUrl);
        }
    }
}
