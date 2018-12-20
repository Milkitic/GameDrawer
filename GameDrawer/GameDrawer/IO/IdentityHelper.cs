using System.IO;

namespace GameDrawer.IO
{
    public static class IdentityHelper
    {
        public static string GetRelativePath(string filePath)
        {
            var dir = new FileInfo(filePath).Directory?.Parent?.Name;
            return dir + Path.GetFileNameWithoutExtension(filePath);
            //var baseUri = new Uri(App.Config.GameDirectory, UriKind.Absolute);
            //var fileUri = new Uri(fileName, UriKind.Absolute);

            //return baseUri.MakeRelativeUri(fileUri).ToString();
        }
    }
}
