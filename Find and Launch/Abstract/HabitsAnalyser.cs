using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Find_and_Launch.Abstract
{
    public abstract class HabitsAnalyser
    {
        public object DefiningObject { get; protected set; }
        public bool HabitsAreFound { get; protected set; }

        public HabitsAnalyser()
        {
            DefiningObject = null;
            HabitsAreFound = false;
        }

        public void ReleaseHabitsAnalyser()
        {
            DefiningObject = null;
            HabitsAreFound = false;
        }
    }
}
