using Find_and_Launch.Interfaces;
using Find_and_Launch.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel;

namespace Find_and_Launch.Validators
{
    public class MicrosoftStoreAppValidator : IValidator
    {
        public bool CompareWithRequest(string request, object data)
        {
            string packageFullName = data as string;
            string name = string.Empty;

            OpenPackageInfoByFullName(packageFullName, 0, out IntPtr packageInfoReference);
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
                            string packagePath = Marshal.PtrToStringUni(packageInfo.Path);

                            string manifestPath = global::System.IO.Path.Combine(packagePath, "AppXManifest.xml");
                            SHCreateStreamOnFileEx(manifestPath, 0x40, 0, false, IntPtr.Zero, out IStream stream);

                            if (stream != null)
                            {
                                IAppxManifestReader appxManifestReader = appxFactory.CreateManifestReader(stream);
                                IAppxManifestProperties appxManifestProperties = appxManifestReader.GetProperties();
                                if (appxManifestProperties != null)
                                {
                                    string manifestValue;

                                    name = GetStringValue(appxManifestProperties, "DisplayName");
                                    manifestValue = GetResourceValue(packageFullName, name);
                                    if (manifestValue != null)
                                        name = manifestValue;
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

            if (name != null)
            {
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
            string packageFullName = data as string;
            string name = string.Empty;

            OpenPackageInfoByFullName(packageFullName, 0, out IntPtr packageInfoReference);
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
                            string packagePath = Marshal.PtrToStringUni(packageInfo.Path);

                            string manifestPath = global::System.IO.Path.Combine(packagePath, "AppXManifest.xml");
                            SHCreateStreamOnFileEx(manifestPath, 0x40, 0, false, IntPtr.Zero, out IStream stream);

                            if (stream != null)
                            {
                                IAppxManifestReader appxManifestReader = appxFactory.CreateManifestReader(stream);
                                IAppxManifestProperties appxManifestProperties = appxManifestReader.GetProperties();
                                if (appxManifestProperties != null)
                                {
                                    string manifestValue;

                                    name = GetStringValue(appxManifestProperties, "DisplayName");
                                    manifestValue = GetResourceValue(packageFullName, name);
                                    if (manifestValue != null)
                                        name = manifestValue;
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

            if (name != null)
            {
                if (name.StartsWith("ms-resource:"))
                    return false;
                return true;
            }
            return false;
        }

        private string GetStringValue(IAppxManifestProperties appxManifestProperties, string name)
        {
            appxManifestProperties.GetStringValue(name, out string value);
            return value;
        }

        private string GetResourceValue(string packageFullName, string resource)
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

            string source = string.Format("@{{{0}? {1}}}", packageFullName, url);
            StringBuilder stringBuilder = new StringBuilder(1024);
            int v = SHLoadIndirectString(source, stringBuilder, stringBuilder.Capacity, IntPtr.Zero);
            if (v != 0)
                return null;

            return stringBuilder.ToString();
        }

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
