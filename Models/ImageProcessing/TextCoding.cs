using System;
using System.Drawing;
using System.Text;

namespace ImageProcessingCli.Models.ImageProcessing
{
    // TODO: to improve and refactor
    class TextCoding
    {
        public bool IsOutRange { get; private set; }
        
        private Bitmap bitmap;

        public TextCoding(Bitmap bitmap)
        {
            this.bitmap = bitmap;
            this.IsOutRange = false;
        }

        public Bitmap GetBitmap()
        {
            return this.bitmap;
        }

        public void Encode(string content)
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
                    this.IsOutRange = true;
                    return;
                }
            }
        }

        public string Decode()
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
    }
}