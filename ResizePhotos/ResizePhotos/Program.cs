using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;

namespace ResizePhotos
{
    class Program
    {
        //private const string SRC_DIR = @"C:\Temp\photos\source\";
        //private const string DEST_DIR = @"C:\Temp\photos\dest\";
        private const string SRC_DIR = @"C:\Temp\test\source\";
        private const string DEST_DIR = @"C:\Temp\test\dest\";
        private const int DEST_WIDTH = 600;
        private const int DEST_HEIGHT = 300;

        static void Main(string[] args)
        {
            string[] filePaths = Directory.GetFiles(SRC_DIR, "*.PNG", SearchOption.AllDirectories);

            Resizer resizer = new Resizer();
            Image bmPhoto = null;

            foreach (string file in filePaths)
            {
                bmPhoto = resizer.ResizeImage(DEST_WIDTH, DEST_HEIGHT, file);

                string name = file.Replace("source", "dest").Replace(Path.GetExtension(file), ".PNG");

                bmPhoto.Save(name);
            }

            // del /s *.JPG
        }
    }
}
