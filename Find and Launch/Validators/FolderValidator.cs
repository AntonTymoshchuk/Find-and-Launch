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
    public class FolderValidator : IValidator
    {
        public bool CompareWithRequest(string request, object data)
        {
            DirectoryInfo directoryInfo = data as DirectoryInfo;
            switch (GlobalSettings.ComparementType)
            {
                case ComparementType.ContainsRequest:
                    if (directoryInfo.Name.ToLower().Contains(request.ToLower()))
                        return true;
                    break;
                case ComparementType.StartsWithRequest:
                    if (directoryInfo.Name.ToLower().StartsWith(request.ToLower()))
                        return true;
                    break;
            }
            return false;
        }

        public bool CheckWithSettings(object data)
        {
            DirectoryInfo directoryInfo = data as DirectoryInfo;
            foreach (string excludedFolderPath in GlobalSettings.ExcludedFolderPaths)
            {
                if (directoryInfo.FullName.Equals(excludedFolderPath))
                    return false;
            }
            bool isHidden = false;
            if (directoryInfo.Attributes.HasFlag(FileAttributes.Hidden))
                isHidden = true;
            if (isHidden == true && GlobalSettings.IncludeHiddenFolders == false)
                return false;
            return true;
        }

        public bool Validate(string request, object data)
        {
            DirectoryInfo directoryInfo = data as DirectoryInfo;
            try { directoryInfo.GetFileSystemInfos(); return true; }
            catch { return false; }
        }
    }
}
