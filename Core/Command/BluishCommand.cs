using System.Drawing;

namespace ImageProcessingCli.Core.Command
{
  class BluishCommand : Command
  {
    private string outcomeBitmapPath;

    public BluishCommand(string bitmapPath, string outcomeBitmapPath) : base(bitmapPath)
    {
      this.outcomeBitmapPath = outcomeBitmapPath;
    }

    public override void Execute()
    { // TODO: improve bluish algorithm
      Bitmap bitmap = new Bitmap(bitmapPath);
      Color oldColor, newColor;

      for (int y = 0; y < bitmap.Height; y++)
      {
        for (int x = 0; x < bitmap.Width; x++)
        {
          oldColor = bitmap.GetPixel(x, y);
          byte r = oldColor.R;
          byte g = oldColor.G;
          byte b = oldColor.B;
          int tr = (int)(0.272 * r + 0.534 * g + 0.131 * b);
          int tg = (int)(0.349 * r + 0.686 * g + 0.168 * b);
          int tb = (int)(0.393 * r + 0.769 * g + 0.189 * b);
          newColor = Color.FromArgb(
              tr > 255 ? 255 : tr,
              tg > 255 ? 255 : tg,
              tb > 255 ? 255 : tb
          );
          bitmap.SetPixel(x, y, newColor);
        }
      }
      
      bitmap.Save(outcomeBitmapPath);
    }
  }
}