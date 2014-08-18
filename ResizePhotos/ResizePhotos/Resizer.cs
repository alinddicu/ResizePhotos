namespace ResizePhotos
{
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Imaging;

    public class Resizer
    {
        public Image ResizeImage(int newWidth, int newHeight, string stPhotoPath)
        {
            using (var sourcePhoto = Image.FromFile(stPhotoPath))
            {
                var sourceWidth = sourcePhoto.Width;
                var sourceHeight = sourcePhoto.Height;

                var nPercent = CalculateResizePercentage(sourceWidth, sourceHeight, newWidth, newHeight);

                return RedrawResizedImage(sourcePhoto, sourceWidth, sourceHeight, nPercent);
            }
        }

        private static float CalculateResizePercentage(int sourceWidth, int sourceHeight, int calcWidth, int calcHeight)
        {
            var resizePercentage = ((float)calcWidth / (float)sourceWidth);
            //Consider vertical pics
            if (sourceWidth < sourceHeight)
            {
                var buff = calcWidth;

                calcWidth = calcHeight;
                calcHeight = buff;

                resizePercentage = ((float)calcHeight / (float)sourceHeight);
            }
            return resizePercentage;
        }

        private static Image RedrawResizedImage(Image sourcePhoto, int sourceWidth, int sourceHeight, float nPercent)
        {
            var destWidth = (int)(sourceWidth * nPercent);
            var destHeight = (int)(sourceHeight * nPercent);
            using (var bitmapPhoto = new Bitmap(destWidth, destHeight, PixelFormat.Format24bppRgb))
            {
                bitmapPhoto.SetResolution(sourcePhoto.HorizontalResolution, sourcePhoto.VerticalResolution);

                var graphicsPhoto = Graphics.FromImage(bitmapPhoto);
                graphicsPhoto.Clear(Color.Black);
                graphicsPhoto.InterpolationMode = InterpolationMode.HighQualityBicubic;

                graphicsPhoto.DrawImage(sourcePhoto, new Rectangle(0, 0, destWidth, destHeight), new Rectangle(0, 0, sourceWidth, sourceHeight), GraphicsUnit.Pixel);
                
                return bitmapPhoto;
            }
        }
    }
}
