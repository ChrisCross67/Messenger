using System;
using System.Runtime.InteropServices;

namespace Messenger.Utils
{
    class Win32
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct SHFILEINFO
        {
            public IntPtr hIcon;
            public IntPtr iIcon;
            public uint dwAttributes;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            public string szDisplayName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
            public string szTypeName;
        };

        [Flags]
        public enum SHGetFileInfoFlags
        {
            Icon = 0x000000100,     // get icon
            DisplayName = 0x000000200,     // get display name
            TypeName = 0x000000400,     // get type name
            Attributes = 0x000000800,     // get attributes
            IconLocation = 0x000001000,     // get icon location
            ExeType = 0x000002000,     // return exe type
            SysIconIndex = 0x000004000,     // get system icon index
            LinkOverlay = 0x000008000,     // put a link overlay on icon
            Selected = 0x000010000,     // show icon in selected state
            AttrSpecified = 0x000020000,     // get only specified attributes
            LargeIcon = 0x000000000,     // get large icon
            SmallIcon = 0x000000001,     // get small icon
            OpenIcon = 0x000000002,     // get open icon
            ShellIconSize = 0x000000004,     // get shell size icon
            PIDL = 0x000000008,     // pszPath is a pidl
            UseFileAttributes = 0x000000010      // use passed dwFileAttribute
        }

        // Flags that specify the file information to retrieve with SHGetFileInfo
        [Flags]
        public enum FileAttributeFlags
        {
            ReadOnly = 0x00000001,
            Hidden = 0x00000002,
            System = 0x00000004,
            Directory = 0x00000010,
            Archive = 0x00000020,
            Device = 0x00000040,
            Normal = 0x00000080,
            Temporary = 0x00000100,
            SparseFile = 0x00000200,
            ReparsePoint = 0x00000400,
            Compressed = 0x00000800,
            Offline = 0x00001000,
            NotConentIndexed = 0x00002000,
            Encrypted = 0x00004000
        }


        [DllImport("shell32.dll")]
        public static extern IntPtr SHGetFileInfo(
            string pszPath,
            FileAttributeFlags dwFileAttributes, 
            ref SHFILEINFO psfi,
            uint cbSizeFileInfo, 
            SHGetFileInfoFlags uFlags);

        public static IntPtr GetLargIcon(string path)
        {
            return GetIcon(path,
                FileAttributeFlags.Normal,
                SHGetFileInfoFlags.UseFileAttributes
                | SHGetFileInfoFlags.Icon
                | SHGetFileInfoFlags.LargeIcon);
        }

        public static IntPtr GetSmallIcon(string path)
        {
            return GetIcon(path,
                FileAttributeFlags.Normal,
                SHGetFileInfoFlags.UseFileAttributes
                | SHGetFileInfoFlags.Icon
                | SHGetFileInfoFlags.SmallIcon);
        }

        public static IntPtr GetFolderIcon()
        {
            return GetIcon("dicrectory",
                FileAttributeFlags.Directory,
                SHGetFileInfoFlags.UseFileAttributes
                | SHGetFileInfoFlags.Icon
                | SHGetFileInfoFlags.LargeIcon);
        }

        private static IntPtr GetIcon(string path,FileAttributeFlags attrFlags, SHGetFileInfoFlags dwFlag)
        {
            SHFILEINFO fi = new SHFILEINFO();
            int iTotal = (int)SHGetFileInfo(path, attrFlags, ref fi, 0, dwFlag);
            return fi.hIcon;
        }
    }
}
