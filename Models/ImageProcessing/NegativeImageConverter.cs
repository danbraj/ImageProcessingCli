using System.Drawing;
using System.Drawing.Imaging;

namespace GProject.Models.ImageProcessing
{
    class NegativeImageConverter : ImageProcessing
    {
        public NegativeImageConverter(Bitmap bitmap) : base(bitmap) {}

        public override Bitmap Convert()
        {
            return this.PixelByPixelConvertMethod();
        }

        private Bitmap ColorMatrixConvertMethod()
        {
            Bitmap newBitmap = new Bitmap(bitmap.Width, bitmap.Height);

            Graphics g = Graphics.FromImage(newBitmap);

            float[][] colorMatrixElements = {
                new float[] { -1,  0,  0,  0,  0},    // red scaling factor
                new float[] {  0, -1,  0,  0,  0},    // green scaling factor
                new float[] {  0,  0, -1,  0,  0},    // blue scaling factor
                new float[] {  0,  0,  0,  1,  0},    // alpha scaling factor
                new float[] {  1,  1,  1,  0,  1}     // three translations
            };

            ColorMatrix colorMatrix = new ColorMatrix(colorMatrixElements);

            ImageAttributes imageAttributes = new ImageAttributes();
            imageAttributes.SetColorMatrix(colorMatrix);

            g.DrawImage(
                bitmap,
                new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                0,
                0,
                bitmap.Width,
                bitmap.Height,
                GraphicsUnit.Pixel,
                imageAttributes
            );
            g.Dispose();

            return newBitmap;
        }

        private Bitmap PixelByPixelConvertMethod()
        {
            Color oldColor, newColor;

            for (int y = 0; y < bitmap.Height; y++)
            {
                for (int x = 0; x < bitmap.Width; x++)
                {
                    oldColor = bitmap.GetPixel(x, y);
                    newColor = Color.FromArgb((byte)~oldColor.R, (byte)~oldColor.G, (byte)~oldColor.B);
                    bitmap.SetPixel(x, y, newColor);
                }
            }
            return bitmap;
        }
    }
}