using System;
using System.Drawing;
using System.Text;

namespace GProject.Models
{
    /*
        NOTE:
        - kodowanie i dekodowanie odbywa się tylko na ostatnich bitach komponentów piksela
    */

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
            Color color;

            char[] chars = content.ToCharArray();
            int charIndex = 0;
            uint charValue = (uint)chars.Length;
            uint iCount = (uint)(chars.Length * 4) + 4;

            int bitsPairNumber = 0;
            bool[] charBits = new bool[16];
            
            int x = 0;
            int y = 0;
            for (uint i = 0; i < iCount; i++)
            {
                bitsPairNumber = (int)(i % 4);

                if (bitsPairNumber == 0)
                {
                    if (i > 0)
                    {
                        charValue = chars[charIndex];
                        charIndex++;
                    }

                    for (int f = 0; f < 16; f++)
                    {
                        uint m = (uint)(1 << f);
                        bool j = (charValue & m) == m;
                        charBits[f] = j;
                    }
                }

                color = bitmap.GetPixel(x, y);
                int[] c = new int[] { color.A, color.R, color.G, color.B };

                for (int k = 0; k < 4; k++)
                {
                    int index = 4 * bitsPairNumber + k;
                    c[k] = charBits[index] ? c[k] |= 1 : c[k] &= ~1;
                }

                bitmap.SetPixel(x, y, Color.FromArgb(c[0], c[1], c[2], c[3]));

                if (x == bitmap.Width - 1)
                {
                    x = 0;
                    y++;
                }
                else
                {
                    x++;
                }

                if (y == bitmap.Height - 1)
                {
                    return;
                }
            }
        }

        public static string DecodeTextFrom(Bitmap bitmap)
        {
            var sb = new StringBuilder();

            Color color;

            bool isFirstChar = true;
            int bitsPairNumber = 0;
            uint iCount = uint.MaxValue;
            uint charValue = 0;

            int x = 0;
            int y = 0;
            uint counter = 0;
            while (true)
            {
                bitsPairNumber = (int)counter % 4;

                color = bitmap.GetPixel(x, y);
                int[] c = new int[] { color.A, color.R, color.G, color.B };

                if (counter > 1 && bitsPairNumber == 0)
                {
                    if (isFirstChar)
                    {
                        iCount = charValue * 4 + 4;
                        isFirstChar = false;
                    }
                    else
                    {
                        sb.Append((char)charValue);
                    }
                    charValue = 0;
                }

                for (int k = 0; k < 4; k++)
                {
                    if ((c[k] & 1) == 1)
                    {
                        int index = 4 * bitsPairNumber + k;
                        charValue += (uint)(1 << index);
                    }
                }

                if (x == bitmap.Width - 1)
                {
                    x = 0;
                    y++;
                }
                else
                {
                    x++;
                }

                if (y >= bitmap.Height - 1 || counter >= iCount)
                {
                    break;
                }
                counter++;
            }
            return sb.ToString();
        }

        public static int CalculateTextMaxLengthToEncode(Bitmap bitmap)
        {
            return (int)(bitmap.Width * bitmap.Height / 4 - 12);
        }
    }
}