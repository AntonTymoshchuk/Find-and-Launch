using Find_and_Launch.Abstract;
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
    public class WebService : Model, ILaunchable
    {
        private string RequestUrl { get; }
        private string InformationUrl { get; }
        public string OriginalRequest { get; }
        public string DisplayRequest { get; }
        public string Description { get; }

        public WebService(string request, string name)
        {
            Name = name;
            OriginalRequest = request;
            DisplayRequest = request;

            switch (Name)
            {
                case "Google Search":
                    RequestUrl = $@"https://www.google.com/search?q={request}";
                    InformationUrl = @"https://en.wikipedia.org/wiki/Google_Search";
                    //if (request.Length > 24 - (Name.Length + 3))
                    //    DisplayRequest = request.Substring(0, 24 - (Name.Length + 6)) + "...";
                    //else DisplayRequest = request;
                    Type = "Google";
                    Description = @"Google Search, also referred to as Google Web Search or simply Google, is a web search engine developed by Google LLC. It is the most used search engine on the World Wide Web across all platforms, with 92.74% market share as of October 2018, handling more than 3.5 billion searches each day.";
                    MediumImage = new BitmapImage(new Uri("/Images/WebServices/Google/GoogleSearch.png", UriKind.Relative));
                    LargeImage = new BitmapImage(new Uri("/Images/WebServices/Google/GoogleSearch.png", UriKind.Relative));
                    break;
                case "Google Images":
                    RequestUrl = $@"https://www.google.com/search?q={request}&tbm=isch";
                    InformationUrl = @"https://en.wikipedia.org/wiki/Google_Images";
                    //if (request.Length > 24 - (Name.Length + 3))
                    //    DisplayRequest = request.Substring(0, 24 - (Name.Length + 6)) + "...";
                    //else DisplayRequest = request;
                    Type = "Google";
                    Description = @"Google Images is a search service owned by Google that allows users to search the Web for image content. It was introduced on July 12, 2001 due to a demand for pictures of Jennifer Lopez's green Versace dress that the regular Google search couldn't handle. In 2011, reverse image search functionality was added to it.";
                    MediumImage = new BitmapImage(new Uri("/Images/WebServices/Google/GoogleImages.png", UriKind.Relative));
                    LargeImage = new BitmapImage(new Uri("/Images/WebServices/Google/GoogleImages.png", UriKind.Relative));
                    break;
                case "YouTube":
                    RequestUrl = $@"https://www.youtube.com/results?search_query={request}";
                    InformationUrl = @"https://en.wikipedia.org/wiki/YouTube";
                    //if (request.Length > 24 - (Name.Length + 3))
                    //    DisplayRequest = request.Substring(0, 24 - (Name.Length + 6)) + "...";
                    //else DisplayRequest = request;
                    Type = "Google";
                    Description = @"YouTube is an American video-sharing website headquartered in San Bruno, California. Three former PayPal employees—Chad Hurley, Steve Chen, and Jawed Karim—created the service in February 2005. Google bought the site in November 2006 for US$1.65 billion; YouTube now operates as one of Google's subsidiaries.";
                    MediumImage = new BitmapImage(new Uri("/Images/WebServices/Google/YouTube.png", UriKind.Relative));
                    LargeImage = new BitmapImage(new Uri("/Images/WebServices/Google/YouTube.png", UriKind.Relative));
                    break;
            }
        }

        public void Launch()
        {
            Process.Start(RequestUrl);
            if (GlobalSettings.UseHabitsAnalysis == true && GlobalSettings.RememberOnLaunchment)
            {
                GlobalHabitsAnalyser.WebServicesHabitsAnalyser.AddToHabitsAnalyser(this);
                GlobalHabitsAnalyser.AddRequestHabit(this);
            }
        }

        public void GetInformation()
        {
            Process.Start(InformationUrl);
        }
    }
}
