using System.Drawing;

namespace ImageProcessingCli.Core.Command
{
  class NegativeCommand : Command
  {
    private string outcomeBitmapPath;

    public NegativeCommand(string bitmapPath, string outcomeBitmapPath) : base(bitmapPath)
    {
      this.outcomeBitmapPath = outcomeBitmapPath;
    }

    public override void Execute()
    {
      Bitmap bitmap = new Bitmap(bitmapPath);
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

      bitmap.Save(outcomeBitmapPath);
    }
  }
}