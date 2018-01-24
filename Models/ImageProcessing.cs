using System;
using System.Drawing;
using System.Text;

namespace GProject.Models
{
    class ImageProcessing
    {
        public static void ConvertToNegative(Bitmap bitmap)
        {
            Color color;

            for (int y = 0; y < bitmap.Height; y++)
            {
                for (int x = 0; x < bitmap.Width; x++)
                {
                    color = bitmap.GetPixel(x, y);
                    byte r = (byte)(255 - color.R);
                    byte g = (byte)(255 - color.G);
                    byte b = (byte)(255 - color.B);
                    bitmap.SetPixel(x, y, Color.FromArgb(r, g, b));
                }
            }
        }

        public static void ConvertToGray(Bitmap bitmap)
        {
            Color color;

            for (int y = 0; y < bitmap.Height; y++)
            {
                for (int x = 0; x < bitmap.Width; x++)
                {
                    color = bitmap.GetPixel(x, y);
                    byte r = color.R;
                    byte g = color.G;
                    byte b = color.B;
                    byte gray = (byte)((r + g + b) / 3);
                    r = g = b = gray;
                    bitmap.SetPixel(x, y, Color.FromArgb(gray, gray, gray));
                }
            }
        }

        public static void ConvertToSepia(Bitmap bitmap)
        {
            Color color;

            for (int y = 0; y < bitmap.Height; y++)
            {
                for (int x = 0; x < bitmap.Width; x++)
                {
                    color = bitmap.GetPixel(x, y);
                    byte r = color.R;
                    byte g = color.G;
                    byte b = color.B;
                    int tr = (int)(0.393 * r + 0.769 * g + 0.189 * b);
                    int tg = (int)(0.349 * r + 0.686 * g + 0.168 * b);
                    int tb = (int)(0.272 * r + 0.534 * g + 0.131 * b);
                    bitmap.SetPixel(x, y, Color.FromArgb(
                        tr > 255 ? 255 : tr,
                        tg > 255 ? 255 : tg,
                        tb > 255 ? 255 : tb
                    ));
                }
            }
        }

        public static void EncodeTextIn(Bitmap bitmap, string content)
        {
            // TODO: to implement encode function
        }

        public static string DecodeTextFrom(Bitmap bitmap)
        {
            // TODO: to implement decode function
            return string.Empty;
        }
    }
}