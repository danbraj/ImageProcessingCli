using System.Drawing;
using System.Drawing.Imaging;
using ImageProcessingCli.Core.Logger;

namespace ImageProcessingCli.Core.Command
{
  class TextCodingCommand : Command
  {
    public bool IsOutRange { get; private set; }

    private string outcomeBitmapPath;
    private string textToCoding;

    public TextCodingCommand(string bitmapPath, string outcomeBitmapPath, string textToCoding) : base(bitmapPath)
    {
      this.outcomeBitmapPath = outcomeBitmapPath;
      this.textToCoding = textToCoding;
      this.IsOutRange = false;
    }

    public override void Execute()
    {
      LoggerFacade.Highlight($"Długość tekstu do zakodowania: {this.textToCoding.Length}");
      
      Bitmap bitmap = new Bitmap(Image.FromFile(bitmapPath));

      Color color;

      char[] chars = this.textToCoding.ToCharArray();
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

      bitmap.Save(outcomeBitmapPath, ImageFormat.Png); // NOTE: jpg is also saved as bitmap
      if (this.IsOutRange)
      {
        LoggerFacade.Warn($"Ostrzeżenie: Zabrakło wolnych bitów do zakodowania!");
      }
    }
  }
}