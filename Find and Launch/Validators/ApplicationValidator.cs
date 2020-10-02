using Find_and_Launch.Interfaces;
using Find_and_Launch.Settings;
using Microsoft.WindowsAPICodePack.Shell;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Find_and_Launch.Validators
{
    public class ApplicationValidator : IValidator
    {
        public bool CompareWithRequest(string request, object data)
        {
            FileInfo fileInfo = data as FileInfo;
            if (fileInfo.Extension.Equals(".lnk"))
            {
                string name = fileInfo.Name.Remove(fileInfo.Name.Length - 4, 4);
                switch (GlobalSettings.ComparementType)
                {
                    case ComparementType.ContainsRequest:
                        if (name.ToLower().Contains(request.ToLower()))
                            return true;
                        break;
                    case ComparementType.StartsWithRequest:
                        if (name.ToLower().StartsWith(request.ToLower()))
                            return true;
                        break;
                }
            }
            return false;
        }

        public bool CheckWithSettings(object data)
        {
            return true;
        }

        public bool Validate(string request, object data)
        {
            FileInfo fileInfo = data as FileInfo;
            if (fileInfo.Extension.Equals(".lnk"))
            {
                ShellFile inkShellFile = ShellFile.FromFilePath(fileInfo.FullName);
                FileInfo exeFileInfo = new FileInfo(inkShellFile.Properties.System.Link.TargetParsingPath.Value);
                return exeFileInfo.Exists;
            }
            return false;
        }
    }
}
