using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Find_and_Launch
{
    public abstract class ErrorContent
    {
        public static string ErrorText { get; } = "No information";
        public static BitmapImage MediumErrorImage
        {
            get { return new BitmapImage(new Uri("/Images/Error/MediumErrorImage.png", UriKind.Relative)); }
        }
        public static BitmapImage LargeErrorImage
        {
            get { return new BitmapImage(new Uri("/Images/Error/LargeErrorImage.png", UriKind.Relative)); }
        }
    }
}
