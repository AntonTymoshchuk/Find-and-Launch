using Find_and_Launch.Abstract;
using Find_and_Launch.HabitsAnalysing;
using Find_and_Launch.Interfaces;
using Find_and_Launch.MessageManager;
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
    public class System : Model, ILaunchable
    {
        private string Request { get; set; }
        private string ApplicationName { get; }
        public string OriginalRequest { get; }
        public string DisplayRequest { get; }
        public string Description { get; }
        private string InformationUrl { get; }

        public System(string request, string name)
        {
            Type = "System service";
            Name = name;
            OriginalRequest = request;

            switch (Name)
            {
                case "Command Prompt":
                    ApplicationName = "cmd.exe";
                    if (request.Length > 24 - (Name.Length + 3))
                        DisplayRequest = request.Substring(0, 24 - (Name.Length + 6)) + "...";
                    else DisplayRequest = request;
                    Request = $"/k{request}";
                    Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged.";
                    InformationUrl = "https://support.microsoft.com/en-us/search?query=command%20prompt%20in%20Windows%2010";
                    MediumImage = new BitmapImage(new Uri("/Images/Systems/Console.png", UriKind.Relative));
                    LargeImage = new BitmapImage(new Uri("/Images/Systems/Console.png", UriKind.Relative));
                    break;
                case "Windows PowerShell":
                    ApplicationName = @"C:\Users\aatym\AppData\Roaming\Microsoft\Windows\Start Menu\Programs\Windows PowerShell\Windows PowerShell.lnk";
                    if (request.Length > 24 - (Name.Length + 3))
                        DisplayRequest = request.Substring(0, 24 - (Name.Length + 6)) + "...";
                    else DisplayRequest = request;
                    Request = $"-NoExit {request}";
                    Description = "...";
                    InformationUrl = "...";
                    MediumImage = new BitmapImage(new Uri("/Images/Systems/PowerShell.png", UriKind.Relative));
                    LargeImage = new BitmapImage(new Uri("/Images/Systems/PowerShell.png", UriKind.Relative));
                    break;
                case "Launch":
                    ApplicationName = null;
                    if (request.Length > 24 - (Name.Length + 3))
                        DisplayRequest = request.Substring(0, 24 - (Name.Length + 6)) + "...";
                    else DisplayRequest = request;
                    Request = request;
                    Description = "...";
                    InformationUrl = "...";
                    MediumImage = new BitmapImage(new Uri("/Images/Systems/Launch.png", UriKind.Relative));
                    LargeImage = new BitmapImage(new Uri("/Images/Systems/Launch.png", UriKind.Relative));
                    break;
            }
        }

        public void Launch()
        {
            try
            {
                ProcessStartInfo processStartInfo = null;
                if (ApplicationName == null)
                {
                    processStartInfo = new ProcessStartInfo()
                    {
                        WorkingDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                        FileName = Request
                    };
                    Process.Start(processStartInfo);
                }
                else
                {
                    processStartInfo = new ProcessStartInfo()
                    {
                        WorkingDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                        FileName = ApplicationName,
                        Arguments = Request
                    };
                    Process.Start(processStartInfo);
                }
                if (GlobalSettings.UseHabitsAnalysis == true && GlobalSettings.RememberOnLaunchment == true)
                {
                    GlobalHabitsAnalyser.SystemHabitsAnalyser.AddToHabitsAnalyser(this);
                    GlobalHabitsAnalyser.AddRequestHabit(this);
                }
            }
            catch { Message.Show("Error", MessageType.Error); }
        }

        public void RunAsAdministrator()
        {
            try
            {
                ProcessStartInfo processStartInfo = null;
                if (ApplicationName == null)
                {
                    processStartInfo = new ProcessStartInfo()
                    {
                        WorkingDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                        FileName = Request,
                        Verb = "runas"
                    };
                    try { Process.Start(processStartInfo); }
                    catch
                    {
                        processStartInfo = new ProcessStartInfo()
                        {
                            WorkingDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                            FileName = Request
                        };
                        Process.Start(processStartInfo);
                    }
                }
                else
                {
                    processStartInfo = new ProcessStartInfo()
                    {
                        WorkingDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                        FileName = ApplicationName,
                        Arguments = Request,
                        Verb = "runas"
                    };
                    Process.Start(processStartInfo);
                }
            }
            catch { Message.Show("Error", MessageType.Error); }
        }

        public void GetInformation()
        {
            Process.Start(InformationUrl);
        }
    }
}
