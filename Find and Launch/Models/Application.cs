using Find_and_Launch.Abstract;
using Find_and_Launch.HabitsAnalysing;
using Find_and_Launch.Interfaces;
using Find_and_Launch.MessageManager;
using Find_and_Launch.Settings;
using IWshRuntimeLibrary;
using Microsoft.Win32;
using Microsoft.WindowsAPICodePack.Shell;
using Shell32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Find_and_Launch.Models
{
    public class Application : Model, ILaunchable
    {
        public string Version { get; }
        public string Description { get; }
        public string Language { get; }
        public string Publisher { get; }
        public string FullLnkName { get; }
        public string FullExeName { get; }
        public string Owner { get; }
        public string Computer { get; }
        public string CreationTime { get; }
        public string LastAccessTime { get; }
        public string LastWriteTime { get; }
        public string Size { get; private set; }
        public string BeginNamePart { get; private set; }
        public string RequestNamePart { get; private set; }
        public string EndNamePart { get; private set; }

        public Application(string request, FileInfo inkInfo)
        {
            Type = "Application";

            if (inkInfo.Exists == true)
            {
                try { Name = inkInfo.Name.Remove(inkInfo.Name.Length - 4, 4); }
                catch { Name = string.Empty; }
                try { FullLnkName = inkInfo.FullName; }
                catch { FullLnkName = ErrorContent.ErrorText; }
                SeparateNameOnParts(request);

                try
                {
                    ShellFile inkShellFile = ShellFile.FromFilePath(inkInfo.FullName);
                    try { FullExeName = inkShellFile.Properties.System.Link.TargetParsingPath.Value; }
                    catch { FullExeName = ErrorContent.ErrorText; }

                    FileInfo exeInfo = new FileInfo(FullExeName);
                    try { CreationTime = Convert.ToString(exeInfo.CreationTime); }
                    catch { CreationTime = ErrorContent.ErrorText; }
                    try { LastAccessTime = Convert.ToString(exeInfo.LastAccessTime); }
                    catch { LastAccessTime = ErrorContent.ErrorText; }
                    try { LastWriteTime = Convert.ToString(exeInfo.LastWriteTime); }
                    catch { LastWriteTime = ErrorContent.ErrorText; }
                    try { CalculateSize(exeInfo.Length); }
                    catch { Size = ErrorContent.ErrorText; }

                    try
                    {
                        ShellFile exeShellFile = ShellFile.FromFilePath(FullExeName);
                        try { Version = exeShellFile.Properties.System.FileVersion.Value; }
                        catch { Version = ErrorContent.ErrorText; }
                        try { Description = exeShellFile.Properties.System.FileDescription.Value; }
                        catch { Description = ErrorContent.ErrorText; }
                        try { Language = exeShellFile.Properties.System.Language.Value; }
                        catch { Language = ErrorContent.ErrorText; }
                        try { Publisher = exeShellFile.Properties.System.Company.Value; }
                        catch { Publisher = ErrorContent.ErrorText; }
                        try { Owner = exeShellFile.Properties.System.FileOwner.Value; }
                        catch { Owner = ErrorContent.ErrorText; }
                        try { Computer = exeShellFile.Properties.System.ComputerName.Value; }
                        catch { Computer = ErrorContent.ErrorText; }
                        try { MediumImage = exeShellFile.Thumbnail.MediumBitmapSource; }
                        catch { MediumImage = ErrorContent.MediumErrorImage; }
                        try { LargeImage = exeShellFile.Thumbnail.LargeBitmapSource; }
                        catch { LargeImage = ErrorContent.LargeErrorImage; }
                    }
                    catch
                    {
                        Version = ErrorContent.ErrorText;
                        Description = ErrorContent.ErrorText;
                        Language = ErrorContent.ErrorText;
                        Publisher = ErrorContent.ErrorText;
                        Owner = ErrorContent.ErrorText;
                        Computer = ErrorContent.ErrorText;
                        MediumImage = ErrorContent.MediumErrorImage;
                        LargeImage = ErrorContent.LargeErrorImage;
                    }
                }
                catch
                {
                    FullExeName = ErrorContent.ErrorText;
                    CreationTime = ErrorContent.ErrorText;
                    LastAccessTime = ErrorContent.ErrorText;
                    LastWriteTime = ErrorContent.ErrorText;
                    Size = ErrorContent.ErrorText;

                    Version = ErrorContent.ErrorText;
                    Description = ErrorContent.ErrorText;
                    Language = ErrorContent.ErrorText;
                    Publisher = ErrorContent.ErrorText;
                    Owner = ErrorContent.ErrorText;
                    Computer = ErrorContent.ErrorText;
                    MediumImage = ErrorContent.MediumErrorImage;
                    LargeImage = ErrorContent.LargeErrorImage;
                }
            }
        }

        private void CalculateSize(long length)
        {
            if (length < 1000)
                Size = $"{ length} B";
            else if (length >= 1000 && length < 1000000)
                Size = $"{Math.Round(Convert.ToDouble(length) / 1000, 2)} KB";
            else if (length >= 1000000 && length < 1000000000)
                Size = $"{Math.Round(Convert.ToDouble(length) / 1000000, 2)} MB";
            else if (length >= 1000000000 && length < 1000000000000)
                Size = $"{Math.Round(Convert.ToDouble(length) / 1000000000, 2)} GB";
            else if (length >= 1000000000000 && length < 1000000000000000)
                Size = $"{Math.Round(Convert.ToDouble(length) / 1000000000000, 2)} TB";
        }

        private void SeparateNameOnParts(string request)
        {
            if (Name.Equals(string.Empty))
                BeginNamePart = ErrorContent.ErrorText;
            else
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
                PrepareDisplayName();
            }
        }

        private void PrepareDisplayName()
        {
            int displayNameLength = (BeginNamePart.Length + RequestNamePart.Length + EndNamePart.Length);
            if (displayNameLength > 24)
            {
                if (BeginNamePart.Length > EndNamePart.Length)
                {
                    int newBeginNamePartLength = 24 - (RequestNamePart.Length + EndNamePart.Length + 3);
                    if (newBeginNamePartLength < 6)
                    {
                        if (BeginNamePart.Length > 6)
                            BeginNamePart = "..." + BeginNamePart.Substring(BeginNamePart.Length - 3, 3);
                        int newEndNamePartLength = 24 - (BeginNamePart.Length + RequestNamePart.Length + 6);
                        if (newEndNamePartLength < 6)
                        {
                            if (EndNamePart.Length > 6)
                                EndNamePart = "..." + EndNamePart.Substring(EndNamePart.Length - 3, 3);
                            if (RequestNamePart.Length > 18)
                                RequestNamePart = RequestNamePart.Substring(0, 15) + "...";
                        }
                        else
                        {
                            EndNamePart = EndNamePart.Substring(0, newEndNamePartLength) + "..." + EndNamePart.Substring(EndNamePart.Length - 3, 3);
                        }
                    }
                    else
                    {
                        int start = BeginNamePart.Length - newBeginNamePartLength;
                        if (start > 0)
                            BeginNamePart = "..." + BeginNamePart.Substring(start, newBeginNamePartLength);
                    }
                }
                else
                {
                    int newEndNamePartLength = 24 - (BeginNamePart.Length + RequestNamePart.Length + 6);
                    if (newEndNamePartLength < 6)
                    {
                        EndNamePart = "..." + EndNamePart.Substring(EndNamePart.Length - 3, 3);
                        int newBeginNamePartLength = 24 - (RequestNamePart.Length + EndNamePart.Length + 3);
                        if (newBeginNamePartLength < 6)
                        {
                            if (BeginNamePart.Length > 6)
                                BeginNamePart = "..." + BeginNamePart.Substring(BeginNamePart.Length - 3, 3);
                            if (RequestNamePart.Length > 18)
                                RequestNamePart = RequestNamePart.Substring(0, 15) + "...";
                        }
                        else
                        {
                            int start = BeginNamePart.Length - newBeginNamePartLength;
                            if (start > 0)
                                BeginNamePart = "..." + BeginNamePart.Substring(start, newBeginNamePartLength);
                        }
                    }
                    else
                    {
                        EndNamePart = EndNamePart.Substring(0, newEndNamePartLength) + "..." + EndNamePart.Substring(EndNamePart.Length - 3, 3);
                    }
                }
            }
        }

        public void Launch()
        {
            try
            {
                Process.Start(FullLnkName);
                if (GlobalSettings.UseHabitsAnalysis == true && GlobalSettings.RememberOnLaunchment == true)
                {
                    GlobalHabitsAnalyser.ApplicationHabitsAnalyser.AddToHabitsAnalyser(this);
                    GlobalHabitsAnalyser.AddRequestHabit(this);
                }
            }
            catch { Message.Show("Application not found", MessageType.Error); }
        }

        public void RunAsAdministrator()
        {
            ProcessStartInfo processStartInfo = new ProcessStartInfo()
            {
                UseShellExecute = true,
                WorkingDirectory = Environment.CurrentDirectory,
                FileName = FullLnkName,
                Verb = "runas"
            };
            try { Process.Start(processStartInfo); }
            catch { Message.Show("Error", MessageType.Error); }
        }

        public void OpenFileLocation()
        {
            try
            {
                FileInfo fileInfo = new FileInfo(FullLnkName);
                Process.Start(fileInfo.Directory.FullName);
            }
            catch { Message.Show("Application not found", MessageType.Error); }
        }

        public void CopyPath()
        {
            Clipboard.SetText(FullLnkName);
        }
    }
}
