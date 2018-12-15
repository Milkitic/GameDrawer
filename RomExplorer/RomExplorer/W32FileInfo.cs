using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

// ReSharper disable UnusedMember.Global
// ReSharper disable InconsistentNaming
// ReSharper disable IdentifierTypo
// ReSharper disable FieldCanBeMadeReadOnly.Global
// ReSharper disable MemberCanBePrivate.Global

namespace RomExplorer
{
    public class W32FileInfo
    {
        [DllImport("shell32.dll", EntryPoint = "SHGetFileInfo")]
        public static extern int GetFileInfo(string pszPath, int dwFileAttributes,
            ref FileInformation psfi, int cbFileInfo, int uFlags);

        [StructLayout(LayoutKind.Sequential)]
        public struct FileInformation
        {
            public IntPtr hIcon;
            public int iIcon;
            public int dwAttributes;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            public string szDisplayName;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
            public string szTypeName;
        }

        public enum FileAttributeFlags : uint
        {
            FILE_ATTRIBUTE_READONLY = 0x00000001,
            FILE_ATTRIBUTE_HIDDEN = 0x00000002,
            FILE_ATTRIBUTE_SYSTEM = 0x00000004,
            FILE_ATTRIBUTE_DIRECTORY = 0x00000010,
            FILE_ATTRIBUTE_ARCHIVE = 0x00000020,
            FILE_ATTRIBUTE_DEVICE = 0x00000040,
            FILE_ATTRIBUTE_NORMAL = 0x00000080,
            FILE_ATTRIBUTE_TEMPORARY = 0x00000100,
            FILE_ATTRIBUTE_SPARSE_FILE = 0x00000200,
            FILE_ATTRIBUTE_REPARSE_POINT = 0x00000400,
            FILE_ATTRIBUTE_COMPRESSED = 0x00000800,
            FILE_ATTRIBUTE_OFFLINE = 0x00001000,
            FILE_ATTRIBUTE_NOT_CONTENT_INDEXED = 0x00002000,
            FILE_ATTRIBUTE_ENCRYPTED = 0x00004000
        }

        public enum GetFileInfoFlags : uint
        {
            SHGFI_ICON = 0x000000100, // get icon 
            SHGFI_DISPLAYNAME = 0x000000200, // get display name 
            SHGFI_TYPENAME = 0x000000400, // get type name 
            SHGFI_ATTRIBUTES = 0x000000800, // get attributes 
            SHGFI_ICONLOCATION = 0x000001000, // get icon location 
            SHGFI_EXETYPE = 0x000002000, // return exe type 
            SHGFI_SYSICONINDEX = 0x000004000, // get system icon index 
            SHGFI_LINKOVERLAY = 0x000008000, // put a link overlay on icon 
            SHGFI_SELECTED = 0x000010000, // show icon in selected state 
            SHGFI_ATTR_SPECIFIED = 0x000020000, // get only specified attributes 
            SHGFI_LARGEICON = 0x000000000, // get large icon 
            SHGFI_SMALLICON = 0x000000001, // get small icon 
            SHGFI_OPENICON = 0x000000002, // get open icon 
            SHGFI_SHELLICONSIZE = 0x000000004, // get shell size icon 
            SHGFI_PIDL = 0x000000008, // pszPath is a pidl 
            SHGFI_USEFILEATTRIBUTES = 0x000000010, // use passed dwFileAttribute 
            SHGFI_ADDOVERLAYS = 0x000000020, // apply the appropriate overlays 
            SHGFI_OVERLAYINDEX = 0x000000040 // Get the index of the overlay 
        }
        
        public static Icon GetLargeIcon(string path)
        {
            FileInformation info = new FileInformation();

            GetFileInfo(path, 0, ref info, Marshal.SizeOf(info),
                // ReSharper disable once BitwiseOperatorOnEnumWithoutFlags
                (int)(GetFileInfoFlags.SHGFI_ICON | GetFileInfoFlags.SHGFI_LARGEICON));
            try
            {
                return Icon.FromHandle(info.hIcon);
            }
            catch
            {
                return null;
            }
        }
    }
}
