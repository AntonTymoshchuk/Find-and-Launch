using Find_and_Launch.Interfaces;
using Find_and_Launch.Settings;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Find_and_Launch.Validators
{
    public class FileValidator : IValidator
    {
        public bool CompareWithRequest(string request, object data)
        {
            FileInfo fileInfo = data as FileInfo;
            switch (GlobalSettings.ComparementType)
            {
                case ComparementType.ContainsRequest:
                    if (fileInfo.Name.ToLower().Contains(request.ToLower()))
                        return true;
                    break;
                case ComparementType.StartsWithRequest:
                    if (fileInfo.Name.ToLower().StartsWith(request.ToLower()))
                        return true;
                    break;
            }
            return false;
        }

        public bool CheckWithSettings(object data)
        {
            FileInfo fileInfo = data as FileInfo;
            if (fileInfo.Extension.Equals(".lnk") && GlobalSettings.IncludeInkFiles == false)
                return false;
            if (fileInfo.Attributes.HasFlag(FileAttributes.Hidden) && GlobalSettings.IncludeHiddenFiles == false)
                return false;
            return true;
        }

        public bool Validate(string request, object data)
        {
            return true;
        }
    }
}
