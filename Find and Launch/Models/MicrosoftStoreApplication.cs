using Find_and_Launch.Abstract;
using Find_and_Launch.HabitsAnalysing;
using Find_and_Launch.Interfaces;
using Find_and_Launch.MessageManager;
using Find_and_Launch.Settings;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media.Imaging;
using Windows.ApplicationModel;

namespace Find_and_Launch.Models
{
    public class MicrosoftStoreApplication : Model, ILaunchable
    {
        private string PackageFullName { get; }
        private string PackagePath { get; }
        public string Publisher { get; }
        public string Version { get; }
        public string Architecture { get; }
        public string Description { get; }
        public string BeginNamePart { get; private set; }
        public string RequestNamePart { get; private set; }
        public string EndNamePart { get; private set; }

        public MicrosoftStoreApplication() { }

        public MicrosoftStoreApplication(string request, string packageFullName)
        {
            PackageFullName = packageFullName;
            Type = "Microsoft Store app";

            OpenPackageInfoByFullName(PackageFullName, 0, out IntPtr packageInfoReference);
            if (packageInfoReference != IntPtr.Zero)
            {
                IntPtr infoBuffer = IntPtr.Zero;
                try
                {
                    int bufferLength = 0;
                    GetPackageInfo(packageInfoReference, 0x00000010, ref bufferLength, IntPtr.Zero, out int count);
                    if (bufferLength > 0)
                    {
                        IAppxFactory appxFactory = (IAppxFactory)new AppxFactory();
                        infoBuffer = Marshal.AllocHGlobal(bufferLength);
                        GetPackageInfo(packageInfoReference, 0x00000010, ref bufferLength, infoBuffer, out count);
                        for (int i = 0; i < count; i++)
                        {
                            PackageInfo packageInfo = (PackageInfo)Marshal.PtrToStructure(infoBuffer + i * Marshal.SizeOf(typeof(PackageInfo)), typeof(PackageInfo));
                            PackagePath = Marshal.PtrToStringUni(packageInfo.Path);
                            Version = new Version(packageInfo.PackageId.VersionMajor, packageInfo.PackageId.VersionMinor,
                                packageInfo.PackageId.VersionBuild, packageInfo.PackageId.VersionRevision).ToString();
                            Architecture = packageInfo.PackageId.ProcessorArchitecture.ToString();

                            string manifestPath = global::System.IO.Path.Combine(PackagePath, "AppXManifest.xml");
                            SHCreateStreamOnFileEx(manifestPath, 0x40, 0, false, IntPtr.Zero, out IStream stream);

                            if (stream != null)
                            {
                                IAppxManifestReader appxManifestReader = appxFactory.CreateManifestReader(stream);
                                IAppxManifestProperties appxManifestProperties = appxManifestReader.GetProperties();
                                if (appxManifestProperties != null)
                                {
                                    string manifestValue;

                                    Name = GetStringValue(appxManifestProperties, "DisplayName");
                                    manifestValue = GetResourceValue(Name);
                                    if (manifestValue != null)
                                        Name = manifestValue;

                                    Publisher = GetStringValue(appxManifestProperties, "PublisherDisplayName");
                                    manifestValue = GetResourceValue(Publisher);
                                    if (manifestValue != null)
                                        Publisher = manifestValue;

                                    Description = GetStringValue(appxManifestProperties, "Description");
                                    manifestValue = GetResourceValue(Description);
                                    if (manifestValue != null)
                                        Description = manifestValue;

                                    GetImageValue(GetStringValue(appxManifestProperties, "Logo"));
                                }
                                Marshal.ReleaseComObject(stream);
                            }
                        }
                        Marshal.ReleaseComObject(appxFactory);
                    }
                }
                finally
                {
                    if (infoBuffer != IntPtr.Zero)
                        Marshal.FreeHGlobal(infoBuffer);
                    ClosePackageInfo(packageInfoReference);
                }
            }

            SeparateNameOnParts(request);
        }

        private string GetStringValue(IAppxManifestProperties appxManifestProperties, string name)
        {
            appxManifestProperties.GetStringValue(name, out string value);
            return value;
        }

        private static bool GetBoolValue(IAppxManifestProperties appxManifestProperties, string name)
        {
            appxManifestProperties.GetBoolValue(name, out bool value);
            return value;
        }

        private string GetResourceValue(string resource)
        {
            if (string.IsNullOrWhiteSpace(resource))
                return null;

            string resourceScheme = "ms-resource:";
            if (resource.StartsWith(resourceScheme) != true)
                return null;

            string part = resource.Substring(resourceScheme.Length);
            string url;

            if (part.StartsWith("/"))
                url = resourceScheme + "//" + part;
            else
                url = resourceScheme + "///resources/" + part;

            string source = string.Format("@{{{0}? {1}}}", PackageFullName, url);
            StringBuilder stringBuilder = new StringBuilder(1024);
            int v = SHLoadIndirectString(source, stringBuilder, stringBuilder.Capacity, IntPtr.Zero);
            if (v != 0)
                return null;

            return stringBuilder.ToString();
        }

        private void GetImageValue(string data)
        {
            try
            {
                string manifestImagePath = Path.Combine(PackagePath, data);
                string[] pathParts = manifestImagePath.Split('\\');
                string imagesFolderPath = string.Empty;
                for (int i = 0; i < pathParts.Length - 1; i++)
                    imagesFolderPath += pathParts[i] + '\\';

                DirectoryInfo rootDirectoryInfo = new DirectoryInfo(imagesFolderPath);
                DirectoryInfo whiteImagesDirectoryInfo = null;
                DirectoryInfo[] directoryInfos = rootDirectoryInfo.GetDirectories();
                foreach (DirectoryInfo directoryInfo in directoryInfos)
                {
                    if (directoryInfo.Name.ToLower().Contains("black"))
                    {
                        whiteImagesDirectoryInfo = directoryInfo;
                        break;
                    }
                }

                if (whiteImagesDirectoryInfo != null)
                {
                    bool success = FindImagesInFolder(whiteImagesDirectoryInfo);
                    if (success == false)
                        FindImagesInFolder(rootDirectoryInfo);
                }
                else
                    FindImagesInFolder(rootDirectoryInfo);
            }
            catch { }
        }

        private bool FindImagesInFolder(DirectoryInfo directoryInfo)
        {
            MediumImage = null;
            LargeImage = null;

            FileInfo[] fileInfos = directoryInfo.GetFiles();

            List<Bitmap> targetSizeBitmaps = new List<Bitmap>();
            List<Bitmap> altformUnplatedBitmaps = new List<Bitmap>();

            foreach (FileInfo fileInfo in fileInfos)
            {
                if (fileInfo.Extension.Equals(".png"))
                {
                    if (fileInfo.Name.ToLower().Contains("targetsize"))
                        targetSizeBitmaps.Add(new Bitmap(fileInfo.FullName));
                    else if (fileInfo.Name.ToLower().Contains("altform-unplated"))
                        altformUnplatedBitmaps.Add(new Bitmap(fileInfo.FullName));
                }
            }

            Bitmap smallestTargetSizeBitmap = null;
            Bitmap largestTargetSizeBitmap = null;

            Bitmap smallestAltformUnplatedBitmap = null;
            Bitmap largestAltformUnplatedBitmap = null;

            if (targetSizeBitmaps.Count > 0)
            {
                smallestTargetSizeBitmap = targetSizeBitmaps[0];
                for (int i = 1; i < targetSizeBitmaps.Count; i++)
                {
                    if (smallestTargetSizeBitmap.Width > targetSizeBitmaps[i].Width)
                        smallestTargetSizeBitmap = targetSizeBitmaps[i];
                }

                largestTargetSizeBitmap = targetSizeBitmaps[0];
                for (int i = 1; i < targetSizeBitmaps.Count; i++)
                {
                    if (largestTargetSizeBitmap.Width < targetSizeBitmaps[i].Width)
                        largestTargetSizeBitmap = targetSizeBitmaps[i];
                }
            }

            if (altformUnplatedBitmaps.Count > 0)
            {
                smallestAltformUnplatedBitmap = altformUnplatedBitmaps[0];
                for (int i = 1; i < altformUnplatedBitmaps.Count; i++)
                {
                    if (smallestAltformUnplatedBitmap.Width > altformUnplatedBitmaps[i].Width)
                        smallestAltformUnplatedBitmap = altformUnplatedBitmaps[i];
                }

                largestAltformUnplatedBitmap = altformUnplatedBitmaps[0];
                for (int i = 1; i < altformUnplatedBitmaps.Count; i++)
                {
                    if (largestAltformUnplatedBitmap.Width < altformUnplatedBitmaps[i].Width)
                        largestAltformUnplatedBitmap = altformUnplatedBitmaps[i];
                }
            }

            if (smallestTargetSizeBitmap != null)
            {
                if (smallestAltformUnplatedBitmap != null)
                {
                    if (smallestTargetSizeBitmap.Width > smallestAltformUnplatedBitmap.Width)
                        MediumImage = BitmapToBitmapImage(smallestTargetSizeBitmap);
                    else if (smallestTargetSizeBitmap.Width < smallestAltformUnplatedBitmap.Width)
                        MediumImage = BitmapToBitmapImage(smallestAltformUnplatedBitmap);
                }
                else
                    MediumImage = BitmapToBitmapImage(smallestTargetSizeBitmap);
            }

            else if (smallestAltformUnplatedBitmap != null)
                MediumImage = BitmapToBitmapImage(smallestAltformUnplatedBitmap);

            if (largestTargetSizeBitmap != null)
            {
                if (largestAltformUnplatedBitmap != null)
                {
                    if (largestTargetSizeBitmap.Width < largestAltformUnplatedBitmap.Width)
                        LargeImage = BitmapToBitmapImage(largestTargetSizeBitmap);
                    else if (largestTargetSizeBitmap.Width > largestAltformUnplatedBitmap.Width)
                        LargeImage = BitmapToBitmapImage(largestAltformUnplatedBitmap);
                }
                else
                    LargeImage = BitmapToBitmapImage(largestTargetSizeBitmap);
            }

            else if (largestAltformUnplatedBitmap != null)
                LargeImage = BitmapToBitmapImage(largestAltformUnplatedBitmap);

            if (MediumImage != null && LargeImage != null)
                return true;
            return false;
        }

        private BitmapSource BitmapToBitmapImage(Bitmap bitmap)
        {
            BitmapSource i = Imaging.CreateBitmapSourceFromHBitmap(
                bitmap.GetHbitmap(),
                IntPtr.Zero,
                Int32Rect.Empty,
                BitmapSizeOptions.FromEmptyOptions());
            return i;
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
            string arguments = null;
            OpenPackageInfoByFullName(PackageFullName, 0, out IntPtr packageInfoReference);

            if (packageInfoReference != IntPtr.Zero)
            {
                try
                {
                    int length = 0;
                    GetPackageApplicationIds(packageInfoReference, ref length, null, out int count);

                    byte[] buffer = new byte[length];
                    GetPackageApplicationIds(packageInfoReference, ref length, buffer, out count);

                    string appUserModelId = Encoding.Unicode.GetString(buffer, IntPtr.Size * count, length - IntPtr.Size * count);
                    IApplicationActivationManager activation = (IApplicationActivationManager)new ApplicationActivationManager();
                    int hR = activation.ActivateApplication(appUserModelId, arguments ?? string.Empty, ActivateOptions.NoErrorUI, out uint pid);
                    if (hR < 0)
                        Marshal.ThrowExceptionForHR(hR);
                }
                catch
                { Message.Show("Application not found", MessageType.Error); }
                finally
                { ClosePackageInfo(packageInfoReference); }
            }

            if (GlobalSettings.UseHabitsAnalysis == true && GlobalSettings.RememberOnLaunchment == true)
            {
                GlobalHabitsAnalyser.MicrosoftStoreAppHabitsAnalyser.AddToHabitsAnalyser(this);
                GlobalHabitsAnalyser.AddRequestHabit(this);
            }
        }

        [Guid("45BA127D-10A8-46EA-8AB7-56EA9078943C"), ComImport]
        private class ApplicationActivationManager { }

        enum ActivateOptions
        {
            None = 0x00000000,  // No flags set
            DesignMode = 0x00000001,  // The application is being activated for design mode
            NoErrorUI = 0x00000002,  // Do not show an error dialog if the app fails to activate                                
            NoSplashScreen = 0x00000004,  // Do not show the splash screen when activating the app
        }

        [ComImport, InterfaceType(ComInterfaceType.InterfaceIsIUnknown), Guid("2e941141-7f97-4756-ba1d-9decde894a3d")]
        interface IApplicationActivationManager
        {
            int ActivateApplication([MarshalAs(UnmanagedType.LPWStr)] string appUserModelId, [MarshalAs(UnmanagedType.LPWStr)] string arguments,
                ActivateOptions options, out uint processId);
            int ActivateForFile([MarshalAs(UnmanagedType.LPWStr)] string appUserModelId, IntPtr pShelItemArray,
                [MarshalAs(UnmanagedType.LPWStr)] string verb, out uint processId);
            int ActivateForProtocol([MarshalAs(UnmanagedType.LPWStr)] string appUserModelId, IntPtr pShelItemArray,
                [MarshalAs(UnmanagedType.LPWStr)] string verb, out uint processId);
        }

        [DllImport("kernel32")]
        private static extern int OpenPackageInfoByFullName([MarshalAs(UnmanagedType.LPWStr)] string fullName, uint reserved, out IntPtr packageInfo);

        [DllImport("kernel32")]
        private static extern int GetPackageApplicationIds(IntPtr pir, ref int bufferLength, byte[] buffer, out int count);

        [Guid("5842a140-ff9f-4166-8f5c-62f5b7b0c781"), ComImport]
        private class AppxFactory { }

        [Guid("BEB94909-E451-438B-B5A7-D79E767B75D8"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        private interface IAppxFactory
        {
            void _VtblGap0_2();
            IAppxManifestReader CreateManifestReader(IStream inputStream);
        }

        [Guid("4E1BD148-55A0-4480-A3D1-15544710637C"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        private interface IAppxManifestReader
        {
            void _VtblGap0_1();
            IAppxManifestProperties GetProperties();
        }

        [Guid("03FAF64D-F26F-4B2C-AAF7-8FE7789B8BCA"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        private interface IAppxManifestProperties
        {
            [PreserveSig]
            int GetBoolValue([MarshalAs(UnmanagedType.LPWStr)]string name, out bool value);
            [PreserveSig]
            int GetStringValue([MarshalAs(UnmanagedType.LPWStr)] string name, [MarshalAs(UnmanagedType.LPWStr)] out string vaue);
        }

        [DllImport("shlwapi.dll", CharSet = CharSet.Unicode)]
        private static extern int SHLoadIndirectString(string pszSource, StringBuilder pszOutBuf, int cchOutBuf, IntPtr ppvReserved);

        [DllImport("shlwapi.dll", CharSet = CharSet.Unicode)]
        private static extern int SHCreateStreamOnFileEx(string fileName, int grfMode, int attributes, bool create, IntPtr reserved, out IStream stream);

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        private static extern int OpenPackageInfoByFullName(string packageFullName, int reserved, out IntPtr packageInfoReference);

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        private static extern int GetPackageInfo(IntPtr packageInfoReference, int packageConstant, ref int bufferLength, IntPtr buffer, out int count);

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        private static extern int ClosePackageInfo(IntPtr packageInfoReference);

        [StructLayout(LayoutKind.Sequential, Pack = 4)]
        private struct PackageInfo
        {
            public int Reserved;
            public int Flags;
            public IntPtr Path;
            public IntPtr FullName;
            public IntPtr FamilyName;
            public PackageId PackageId;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 4)]
        private struct PackageId
        {
            public int Reserved;
            public AppxProcessorArchitecture ProcessorArchitecture;
            public ushort VersionRevision;
            public ushort VersionBuild;
            public ushort VersionMinor;
            public ushort VersionMajor;
            public IntPtr Name;
            public IntPtr Publisher;
            public IntPtr ResourceId;
            public IntPtr PublisherId;
        }

        public enum AppxProcessorArchitecture
        {
            x86 = 0,
            Arm = 5,
            x64 = 9,
            Neutral = 11,
            Arm64 = 12
        }
    }
}
