namespace ResizePhotos
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Drawing.Imaging;
    using System.Drawing.Drawing2D;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class Resizer
    {
        public Image ResizeImage(int newWidth, int newHeight, string stPhotoPath)
        {
            Image imgPhoto = Image.FromFile(stPhotoPath);

            int sourceWidth = imgPhoto.Width;
            int sourceHeight = imgPhoto.Height;

            float nPercent = 0;
            
            int calcWidth = newWidth;
            int calcHeight = newHeight;

            nPercent = ((float)calcWidth / (float)sourceWidth);
            //Consider vertical pics
            if (sourceWidth < sourceHeight)
            {
                int buff = calcWidth;

                calcWidth = calcHeight;
                calcHeight = buff;

                nPercent = ((float)calcHeight / (float)sourceHeight);
            }

            int destWidth = (int)(sourceWidth * nPercent);
            int destHeight = (int)(sourceHeight * nPercent);

            Bitmap bmPhoto = new Bitmap(destWidth, destHeight, PixelFormat.Format24bppRgb);

            bmPhoto.SetResolution(imgPhoto.HorizontalResolution, imgPhoto.VerticalResolution);

            Graphics grPhoto = Graphics.FromImage(bmPhoto);
            grPhoto.Clear(Color.Black);
            grPhoto.InterpolationMode = InterpolationMode.HighQualityBicubic;

            grPhoto.DrawImage(imgPhoto, new Rectangle(0, 0, destWidth, destHeight), new Rectangle(0, 0, sourceWidth, sourceHeight), GraphicsUnit.Pixel);

            grPhoto.Dispose();
            imgPhoto.Dispose();
            return bmPhoto;
        }
    }
}
