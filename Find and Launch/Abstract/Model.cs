using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Find_and_Launch.Abstract
{
    public abstract class Model
    {
        public string Type { get; protected set; }
        public string Name { get; protected set; }
        public BitmapSource MediumImage { get; protected set; }
        public BitmapSource LargeImage { get; protected set; }
    }
}
