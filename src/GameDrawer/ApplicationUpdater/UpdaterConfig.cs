﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Milky.ApplicationUpdater
{
    public static class UpdaterConfig
    {
        public static string SavePath { get; set; }
        public static string RequestUri { get; set; }
        public static string AssertName { get; set; }
        public static string CurrentVersion { get; set; }
    }
}
