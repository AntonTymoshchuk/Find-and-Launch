using Find_and_Launch.Interfaces;
using Find_and_Launch.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Find_and_Launch.Validators
{
    public class SettingsValidator : IValidator
    {
        public bool CompareWithRequest(string request, object data)
        {
            string settingsName = Convert.ToString(data);
            switch (GlobalSettings.ComparementType)
            {
                case ComparementType.ContainsRequest:
                    if (settingsName.ToLower().Contains(request.ToLower()))
                        return true;
                    break;
                case ComparementType.StartsWithRequest:
                    if (settingsName.ToLower().StartsWith(request.ToLower()))
                        return true;
                    break;
            }
            return false;
        }

        public bool CheckWithSettings(object data)
        {
            return true;
        }

        public bool Validate(string request, object data)
        {
            return true;
        }
    }
}
