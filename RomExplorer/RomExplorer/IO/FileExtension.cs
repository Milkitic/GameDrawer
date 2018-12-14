using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace RomExplorer.IO
{
    public static class FileExtension
    {
        [DllImport("kernel32", SetLastError = true)]
        public static extern bool MoveFile(string lpExistingFileName, string lpNewFileName);
    }
}
