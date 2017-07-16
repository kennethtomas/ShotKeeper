using System;
using System.IO;
using Xamarin.Forms;
using ShotKeeper.Droid;
using ShotKeeper.Interfaces;

[assembly: Dependency(typeof(FileHelper))]
namespace ShotKeeper.Droid
{
    public class FileHelper : IFileHelper
    {
        public string GetLocalFilePath(string filename)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            return Path.Combine(path, filename);
        }
    }
}