using System;
using System.IO;
using Microsoft.Win32;

namespace TransportComp.Services
{

    public class UploadPhotos
    {
        static string sourceImagePath = null;
        static string targetImagePath = null;
        readonly static string dirName = AppDomain.CurrentDomain.BaseDirectory + "Images\\";
        private readonly static DirectoryInfo directory = new DirectoryInfo(dirName);
        static bool isAllowCopy = false;

        public static string GetImagePath()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files|*.jpg|*.jpeg|*.png";
            if (openFileDialog.ShowDialog() == true) sourceImagePath = openFileDialog.FileName;
            if (sourceImagePath == string.Empty) return null;

            if (sourceImagePath != null)
            {
                isAllowCopy = true;
                return targetImagePath = $"{dirName}{Path.GetFileName(sourceImagePath)}";
            }
            else
            {
                isAllowCopy = false;
                return targetImagePath = null;
            }
        }

        public static void CopyImage()
        {
            if (isAllowCopy)
            {
                if (!directory.Exists)
                    directory.Create();
                if (sourceImagePath != null && targetImagePath != null)
                    File.Copy(sourceImagePath, targetImagePath);
            }
        }
    }
}