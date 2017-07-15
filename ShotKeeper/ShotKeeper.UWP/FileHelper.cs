using Xamarin.Forms;
using ShotKeeper.Interfaces;
using System.IO;
using Windows.Storage;
using ShotKeeper.UWP;

[assembly: Xamarin.Forms.Dependency(typeof(FileHelper))]
namespace ShotKeeper.UWP
{
    public class FileHelper : IFileHelper
    {
        public string GetLocalFilePath(string filename)
        {
            return Path.Combine(ApplicationData.Current.LocalFolder.Path, filename);
        }
    }
}
