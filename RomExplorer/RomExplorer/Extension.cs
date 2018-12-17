using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace RomExplorer
{
    public static class Extension
    {
        // Returns the human-readable file size for an arbitrary, 64-bit file size 
        // The default format is "0.### XB", e.g. "4.2 KB" or "1.434 GB"
        public static string GetBytesReadable(long length)
        {
            // Get absolute value
            length = length < 0 ? -length : length;
            // Determine the suffix and readable value
            string suffix;
            double readable;
            if (length >= 0x1000000000000000) // Exabyte
            {
                suffix = "EB";
                readable = (length >> 50);
            }
            else if (length >= 0x4000000000000) // Petabyte
            {
                suffix = "PB";
                readable = (length >> 40);
            }
            else if (length >= 0x10000000000) // Terabyte
            {
                suffix = "TB";
                readable = (length >> 30);
            }
            else if (length >= 0x40000000) // Gigabyte
            {
                suffix = "GB";
                readable = (length >> 20);
            }
            else if (length >= 0x100000) // Megabyte
            {
                suffix = "MB";
                readable = (length >> 10);
            }
            else if (length >= 0x400) // Kilobyte
            {
                suffix = "KB";
                readable = length;
            }
            else
            {
                return length.ToString("0 B"); // Byte
            }
            // Divide by 1024 to get fractional value
            readable = (readable / 1024);
            // Return formatted number with suffix
            return readable.ToString("0.## ") + suffix;
        }
    }

    public static class ColorExtension
    {
        public static Color? GetChromeColor()
        {
            var hr1 = DwmIsCompositionEnabled(out var isEnabled);
            if (hr1 != 0 || !isEnabled) // 0 means S_OK.
                return null;

            DWMCOLORIZATIONPARAMS parameters;
            try
            {
                // This API is undocumented and so may become unusable in future versions of OSes.
                var hr2 = DwmGetColorizationParameters(out parameters);
                if (hr2 != 0) // 0 means S_OK.
                    return null;
            }
            catch
            {
                return null;
            }

            // Convert colorization color parameter to Color ignoring alpha channel.
            var targetColor = Color.FromArgb(
                (byte)(parameters.colorizationColor >> 16),
                (byte)(parameters.colorizationColor >> 8),
                (byte)parameters.colorizationColor);

            // Prepare base gray color.
            var baseColor = Color.FromArgb(217, 217, 217);

            // Blend the two colors using colorization color balance parameter.
            return BlendColor(targetColor, baseColor, (double)(100 - parameters.colorizationColorBalance));
        }

        private static Color BlendColor(Color color1, Color color2, double color2Perc)
        {
            if ((color2Perc < 0) || (100 < color2Perc))
                throw new ArgumentOutOfRangeException("color2Perc");

            return Color.FromArgb(
                BlendColorChannel(color1.R, color2.R, color2Perc),
                BlendColorChannel(color1.G, color2.G, color2Perc),
                BlendColorChannel(color1.B, color2.B, color2Perc));
        }

        private static byte BlendColorChannel(double channel1, double channel2, double channel2Perc)
        {
            var buff = channel1 + (channel2 - channel1) * channel2Perc / 100D;
            return Math.Min((byte)Math.Round(buff), (byte)255);
        }

        [DllImport("Dwmapi.dll")]
        private static extern int DwmIsCompositionEnabled([MarshalAs(UnmanagedType.Bool)] out bool pfEnabled);

        [DllImport("Dwmapi.dll", EntryPoint = "#127")] // Undocumented API
        private static extern int DwmGetColorizationParameters(out DWMCOLORIZATIONPARAMS parameters);

        [StructLayout(LayoutKind.Sequential)]
        private struct DWMCOLORIZATIONPARAMS
        {
            public uint colorizationColor;
            public uint colorizationAfterglow;
            public uint colorizationColorBalance; // Ranging from 0 to 100
            public uint colorizationAfterglowBalance;
            public uint colorizationBlurBalance;
            public uint colorizationGlassReflectionIntensity;
            public uint colorizationOpaqueBlend;
        }
    }
}