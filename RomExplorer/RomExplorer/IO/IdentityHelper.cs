using RomExplorer.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RomExplorer.IO
{
    public static class IdentityHelper
    {
        public static string GetRelativePath(string fileName)
        {
            var baseUri = new Uri(App.Config.GamePath, UriKind.Absolute);
            var fileUri = new Uri(fileName, UriKind.Absolute);

            return baseUri.MakeRelativeUri(fileUri).OriginalString;
        }
    }
}
