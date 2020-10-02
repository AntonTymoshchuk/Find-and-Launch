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
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows.Media.Imaging;

namespace Find_and_Launch.Models
{
    public class Folder : Model, ILaunchable
    {
        public string FullName { get; }
        public string Owner { get; }
        public string Computer { get; }
        public string Attributes { get; }
        public string CreationTime { get; }
        public string LastAccessTime { get; }
        public string LastWriteTime { get; }
        public string Elements { get; private set; }
        public string Size { get; private set; }
        public string BeginNamePart { get; private set; }
        public string RequestNamePart { get; private set; }
        public string EndNamePart { get; private set; }

        public Folder(string request, DirectoryInfo directoryInfo)
        {
            Type = "Folder";

            if (directoryInfo.Exists == true)
            {
                try { Name = directoryInfo.Name; }
                catch { Name = string.Empty; }
                try { FullName = directoryInfo.FullName; }
                catch { FullName = ErrorContent.ErrorText; }
                try { Attributes = directoryInfo.Attributes.ToString(); }
                catch { Attributes = ErrorContent.ErrorText; }
                try { CreationTime = Convert.ToString(directoryInfo.CreationTime); }
                catch { CreationTime = ErrorContent.ErrorText; }
                try { LastAccessTime = Convert.ToString(directoryInfo.LastAccessTime); }
                catch { LastAccessTime = ErrorContent.ErrorText; }
                try { LastWriteTime = Convert.ToString(directoryInfo.LastWriteTime); }
                catch { LastWriteTime = ErrorContent.ErrorText; }
                CalculateElements(directoryInfo);
                CalculateSize();
                SeparateNameOnParts(request);

                try
                {
                    ShellFolder shellFolder = ShellObject.FromParsingName(FullName) as ShellFolder;
                    try { Owner = shellFolder.Properties.System.FileOwner.Value; }
                    catch { Owner = ErrorContent.ErrorText; }
                    try { Computer = shellFolder.Properties.System.ComputerName.Value; }
                    catch { Computer = ErrorContent.ErrorText; }
                    try { MediumImage = shellFolder.Thumbnail.MediumBitmapSource; }
                    catch { MediumImage = ErrorContent.MediumErrorImage; }
                    try { LargeImage = shellFolder.Thumbnail.LargeBitmapSource; }
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
                Attributes = ErrorContent.ErrorText;
                CreationTime = ErrorContent.ErrorText;
                LastAccessTime = ErrorContent.ErrorText;
                LastWriteTime = ErrorContent.ErrorText;
                Elements = ErrorContent.ErrorText;
                BeginNamePart = ErrorContent.ErrorText;
                Owner = ErrorContent.ErrorText;
                Computer = ErrorContent.ErrorText;
                MediumImage = ErrorContent.MediumErrorImage;
                LargeImage = ErrorContent.LargeErrorImage;
            }
        }

        public void CalculateSize()
        {
            Thread sizeCalculationThread = new Thread(() =>
            {
                long length = 0;
                DirectoryInfo directoryInfo = new DirectoryInfo(FullName);
                try { CalculateFolderSize(directoryInfo, ref length); }
                catch
                {
                    //global::System.Windows.Application.Current.Dispatcher.Invoke(new Action(() =>
                    //{ textBlock.Text = ErrorContent.ErrorText; }));
                    return;
                }
                string size = null;

                if (length < 1000)
                    size = $"{length} B";
                else if (length >= 1000 && length < 1000000)
                    size = $"{Math.Round(Convert.ToDouble(length) / 1000, 2)} KB";
                else if (length >= 1000000 && length < 1000000000)
                    size = $"{Math.Round(Convert.ToDouble(length) / 1000000, 2)} MB";
                else if (length >= 1000000000 && length < 1000000000000)
                    size = $"{Math.Round(Convert.ToDouble(length) / 1000000000, 2)} GB";
                else if (length >= 1000000000000 && length < 1000000000000000)
                    size = $"{Math.Round(Convert.ToDouble(length) / 1000000000000, 2)} TB";
                Size = size;
                //global::System.Windows.Application.Current.Dispatcher.Invoke(new Action(() =>
                //{ textBlock.Text = size; }));
            })
            { IsBackground = true };
            sizeCalculationThread.Start();
        }

        private void CalculateFolderSize(DirectoryInfo directoryInfo, ref long length)
        {
            try
            {
                FileInfo[] fileInfos = directoryInfo.GetFiles();
                for (int i = 0; i < fileInfos.Length; i++)
                    length += fileInfos[i].Length;
                DirectoryInfo[] directoryInfos = directoryInfo.GetDirectories();
                for (int i = 0; i < directoryInfos.Length; i++)
                    CalculateFolderSize(directoryInfos[i], ref length);
            }
            catch { return; }
        }

        private void CalculateElements(DirectoryInfo directoryInfo)
        {
            int folders = directoryInfo.GetDirectories().Length;
            int files = directoryInfo.GetFiles().Length;
            string folderString = "folders";
            if (folders == 1)
                folderString = "folder";
            string fileString = "files";
            if (files == 1)
                fileString = "file";
            string folderExpression = $"{folders} {folderString}";
            if (folders == 0)
                folderExpression = string.Empty;
            string fileExpression = $"{files} {fileString}";
            if (files == 0)
                fileExpression = string.Empty;
            string separator = " and ";
            if (folders == 0 || files == 0)
                separator = string.Empty;
            if (folders == 0 && files == 0)
                separator = "Empty";
            Elements = $"{folderExpression}{separator}{fileExpression}";
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
                Process.Start(FullName);
                if (GlobalSettings.UseHabitsAnalysis == true && GlobalSettings.RememberOnLaunchment == true)
                {
                    GlobalHabitsAnalyser.FolderHabitsAnalyser.AddToHabitsAnalyser(this);
                    GlobalHabitsAnalyser.AddRequestHabit(this);
                }
            }
            catch { Message.Show("Folder not found", MessageType.Error); }
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
                DirectoryInfo directoryInfo = new DirectoryInfo(FullName);
                if (directoryInfo.Exists==false)
                {
                    Message.Show("Folder not found", MessageType.Error);
                    return;
                }
                WshShell wshShell = new WshShell();
                string lnkFullName = FullName + " - Shortcut.lnk";
                IWshShortcut wshShortcut = (IWshShortcut)wshShell.CreateShortcut(lnkFullName);
                wshShortcut.Description = Name + " - Shortcut";
                wshShortcut.TargetPath = FullName;
                wshShortcut.Save();
            }
            catch { Message.Show("Unknown error", MessageType.Error); }
        }
    }
}
