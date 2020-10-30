using System.Drawing;
using System.Drawing.Imaging;

namespace ImageProcessingCli.Core.Command
{
  class SepiaCommand : Command
  {
    private string outcomeBitmapPath;

    public SepiaCommand(string bitmapPath, string outcomeBitmapPath) : base(bitmapPath)
    {
      this.outcomeBitmapPath = outcomeBitmapPath;
    }

    public override void Execute()
    {
      Bitmap bitmap = new Bitmap(bitmapPath);

      // var outcomeBitmap = pixelByPixelConvertMethod(bitmap);
      var outcomeBitmap = colorMatrixConvertMethod(bitmap);

      outcomeBitmap.Save(outcomeBitmapPath);
    }

    private Bitmap pixelByPixelConvertMethod(Bitmap bitmap)
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
          int tr = (int)(0.393 * r + 0.769 * g + 0.189 * b);
          int tg = (int)(0.349 * r + 0.686 * g + 0.168 * b);
          int tb = (int)(0.272 * r + 0.534 * g + 0.131 * b);
          newColor = Color.FromArgb(
              tr > 255 ? 255 : tr,
              tg > 255 ? 255 : tg,
              tb > 255 ? 255 : tb
          );
          bitmap.SetPixel(x, y, newColor);
        }
      }
      return bitmap;
    }

    private Bitmap colorMatrixConvertMethod(Bitmap bitmap)
    {
      // Bitmap newBitmap = new Bitmap(bitmap.Width, bitmap.Height); // NOTE: nowa taka bitmapa zajmuje dużo więcej mniejsca na dysku
      Bitmap newBitmap = (Bitmap)bitmap.Clone();
      Graphics g = Graphics.FromImage(newBitmap);

      float[][] colorMatrixElements = {
        new float[] {.393f, .349f, .272f, 0, 0},    // red scaling factor
        new float[] {.769f, .686f, .534f, 0, 0},    // green scaling factor
        new float[] {.189f, .168f, .131f, 0, 0},    // blue scaling factor
        new float[] {    0,     0,     0, 1, 0},    // alpha scaling factor
        new float[] {    0,     0,     0, 0, 1}     // three translations
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
  }
}