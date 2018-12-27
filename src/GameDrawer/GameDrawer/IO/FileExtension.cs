using Microsoft.Win32;
using System;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;

namespace GameDrawer.IO
{
    public static class FileExtension
    {
        [DllImport("kernel32", SetLastError = true)]
        public static extern bool MoveFile(string lpExistingFileName, string lpNewFileName);

        public static string ValidateFileName(string originName)
        {
            var trimStr = originName?.Trim();
            if (string.IsNullOrEmpty(trimStr))
            {
                throw new ArgumentNullException(trimStr, @"路径不能为空。");
            }

            var chars = Path.GetInvalidFileNameChars();
            foreach (var k in trimStr)
            {
                if (chars.Contains(k))
                {
                    throw new ArgumentException(@"路径中不允许有非法字符。", k.ToString());
                }
            }

            return trimStr;
        }
        public static bool TryValidateFileName(string originName, out string trueName)
        {
            try
            {
                trueName = ValidateFileName(originName);
                return true;
            }
            catch (Exception e)
            {
                trueName = null;
                return false;
            }
        }

        public static string BrowseIcon()
        {
            OpenFileDialog fbd = new OpenFileDialog
            {
                Title = @"请选择一个图片",
                Filter = @"所有支持的图片类型|*.jpg;*.png;*.bmp;*.jpeg"
            };
            var result = fbd.ShowDialog();
            return result == true ? fbd.FileName : null;
        }
        public static string Browse()
        {
            OpenFileDialog fbd = new OpenFileDialog
            {
                Title = @"请选择一个文件",
                Filter = @"所有文件|*.*"
            };
            var result = fbd.ShowDialog();
            return result == true ? fbd.FileName : null;
        }
    }
}
