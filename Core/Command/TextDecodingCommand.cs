using System.Drawing;
using System.Text;
using ImageProcessingCli.Core.Logger;

namespace ImageProcessingCli.Core.Command
{
  class TextDecodingCommand : Command
  {
    public TextDecodingCommand(string bitmapPath) : base(bitmapPath) { }

    public override void Execute()
    {
      // Bitmap bitmap = new Bitmap(Image.FromFile(bitmapPath));
      Bitmap bitmap = new Bitmap(bitmapPath);

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

      string message = sb.ToString();
      LoggerFacade.Info($"Odkodowana wiadomość:\n\n{message}\n");
    }
  }
}