using Find_and_Launch.Abstract;
using Find_and_Launch.HabitsAnalysing;
using Find_and_Launch.Interfaces;
using Find_and_Launch.MessageManager;
using Find_and_Launch.Settings;
using IWshRuntimeLibrary;
using Microsoft.WindowsAPICodePack.Shell;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media.Imaging;

namespace Find_and_Launch.Models
{
    public class File : Model, ILaunchable
    {
        public string FullName { get; }
        public string Extension { get; }
        public string Owner { get; }
        public string Computer { get; }
        public string Attributes { get; }
        public string CreationTime { get; }
        public string LastAccessTime { get; }
        public string LastWriteTime { get; }
        public string Size { get; private set; }
        public string BeginNamePart { get; private set; }
        public string RequestNamePart { get; private set; }
        public string EndNamePart { get; private set; }

        public File() { }

        public File(string request, FileInfo fileInfo)
        {
            Type = "File";

            if (fileInfo.Exists == true)
            {
                try { Name = fileInfo.Name; }
                catch { Name = string.Empty; }
                try { FullName = fileInfo.FullName; }
                catch { FullName = ErrorContent.ErrorText; }
                try { Extension = fileInfo.Extension.TrimStart('.').ToUpper(); }
                catch { Extension = ErrorContent.ErrorText; }
                try { Attributes = fileInfo.Attributes.ToString(); }
                catch { Attributes = ErrorContent.ErrorText; }
                try { CreationTime = Convert.ToString(fileInfo.CreationTime); }
                catch { CreationTime = ErrorContent.ErrorText; }
                try { LastAccessTime = Convert.ToString(fileInfo.LastAccessTime); }
                catch { LastAccessTime = ErrorContent.ErrorText; }
                try { LastWriteTime = Convert.ToString(fileInfo.LastWriteTime); }
                catch { LastWriteTime = ErrorContent.ErrorText; }
                if (Extension.Equals(string.Empty))
                    Extension = "FILE";
                try { CalculateSize(fileInfo.Length); } catch { Size = ErrorContent.ErrorText; }
                SeparateNameOnParts(request);

                try
                {
                    ShellFile shellFile = ShellFile.FromFilePath(FullName);
                    try { Owner = shellFile.Properties.System.FileOwner.Value; }
                    catch { Owner = ErrorContent.ErrorText; }
                    try { Computer = shellFile.Properties.System.ComputerName.Value; }
                    catch { Computer = ErrorContent.ErrorText; }
                    try { MediumImage = shellFile.Thumbnail.MediumBitmapSource; }
                    catch { MediumImage = ErrorContent.MediumErrorImage; }
                    try { LargeImage = shellFile.Thumbnail.LargeBitmapSource; }
                    catch { LargeImage = ErrorContent.LargeErrorImage; }
                }
                catch
                {
                    Owner = ErrorContent.ErrorText;
                    Computer = ErrorContent.ErrorText;
                    MediumImage = ErrorContent.MediumErrorImage;
                    LargeImage = ErrorContent.LargeErrorImage;
                }
            }
            else
            {
                Name = ErrorContent.ErrorText;
                FullName = ErrorContent.ErrorText;
                Extension = ErrorContent.ErrorText;
                Attributes = ErrorContent.ErrorText;
                CreationTime = ErrorContent.ErrorText;
                LastAccessTime = ErrorContent.ErrorText;
                LastWriteTime = ErrorContent.ErrorText;
                Size = ErrorContent.ErrorText;
                BeginNamePart = ErrorContent.ErrorText;
                Owner = ErrorContent.ErrorText;
                Computer = ErrorContent.ErrorText;
                MediumImage = ErrorContent.MediumErrorImage;
                LargeImage = ErrorContent.LargeErrorImage;
            }
        }

        private void CalculateSize(long length)
        {
            if (length < 1000)
                Size = $"{length} B";
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
                if (BeginNamePart.Length > EndNamePart.Length - (Extension.Length + 1))
                {
                    int newBeginNamePartLength = 24 - (RequestNamePart.Length + EndNamePart.Length + 3);
                    if (newBeginNamePartLength < 6)
                    {
                        if (BeginNamePart.Length > 6)
                            BeginNamePart = "..." + BeginNamePart.Substring(BeginNamePart.Length - 3, 3);
                        int newEndNamePartLength = 24 - (BeginNamePart.Length + RequestNamePart.Length + Extension.Length + 3);
                        if (newEndNamePartLength < Extension.Length + 3)
                        {
                            if (EndNamePart.Length > Extension.Length + 3 && EndNamePart.Length > Extension.Length + 1)
                                EndNamePart = "..." + Extension.ToLower();
                            if (RequestNamePart.Length > 18)
                                RequestNamePart = RequestNamePart.Substring(0, 15) + "...";
                        }
                        else
                        {
                            EndNamePart = EndNamePart.Substring(0, newEndNamePartLength) + "..." + Extension.ToLower();
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
                    int newEndNamePartLength = 24 - (BeginNamePart.Length + RequestNamePart.Length + Extension.Length + 3);
                    if (newEndNamePartLength < Extension.Length + 3)
                    {
                        if (EndNamePart.Length > Extension.Length + 3 && EndNamePart.Length > Extension.Length + 1)
                            EndNamePart = "..." + Extension.ToLower();
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
                        EndNamePart = EndNamePart.Substring(0, newEndNamePartLength) + "..." + Extension.ToLower();
                    }
                }
            }
        }

        public void Launch()
        {
            try
            {
                Process.Start(FullName);
                if (GlobalSettings.UseHabitsAnalysis == true && GlobalSettings.RememberOnLaunchment == true)
                {
                    GlobalHabitsAnalyser.FileHabitsAnalyser.AddToHabitsAnalyser(this);
                    GlobalHabitsAnalyser.AddRequestHabit(this);
                }
            }
            catch { Message.Show("File not found", MessageType.Error); }
        }

        public void OpenFileLocation()
        {
            try
            {
                FileInfo fileInfo = new FileInfo(FullName);
                Process.Start(fileInfo.Directory.FullName);
            }
            catch { Message.Show("File not found", MessageType.Error); }
        }

        public void CopyPath()
        {
            Clipboard.SetText(FullName);
        }

        public void Copy()
        {
            Clipboard.SetFileDropList(new StringCollection() { FullName });
        }

        public void CreateShortcut()
        {
            try
            {
                FileInfo fileInfo = new FileInfo(FullName);
                if (fileInfo.Exists==false)
                {
                    Message.Show("File not found", MessageType.Error);
                    return;
                }
                WshShell wshShell = new WshShell();
                string lnkName = Name.Substring(0, Name.Length - (Extension.Length + 1)) + " - Shortcut.lnk";
                string lnkFullName = Path.Combine(fileInfo.Directory.FullName, lnkName);
                IWshShortcut wshShortcut = (IWshShortcut)wshShell.CreateShortcut(lnkFullName);
                wshShortcut.Description = Name + " - Shortcut";
                wshShortcut.TargetPath = FullName;
                wshShortcut.Save();
            }
            catch { Message.Show("Unknown error", MessageType.Error); }
        }
    }
}
