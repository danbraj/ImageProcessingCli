using System.Drawing;

namespace ImageProcessingCli.Models.ImageProcessing
{
    class GrayscaleImageConverter : ImageProcessing
    {
        public GrayscaleImageConverter(Bitmap bitmap) : base(bitmap) {}

        public override Bitmap Convert()
        {
            Color oldColor, newColor;

            for (int y = 0; y < bitmap.Height; y++)
            {
                for (int x = 0; x < bitmap.Width; x++)
                {
                    oldColor = bitmap.GetPixel(x, y);
                    byte r = oldColor.R;
                    byte g = oldColor.G;
                    byte b = oldColor.B;
                    byte gray = (byte)((r + g + b) / 3);
                    r = g = b = gray;
                    newColor = Color.FromArgb(gray, gray, gray);
                    bitmap.SetPixel(x, y, newColor);
                }
            }
            return bitmap;
        }
    }
}