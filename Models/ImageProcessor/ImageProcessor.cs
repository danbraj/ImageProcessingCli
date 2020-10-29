using System.Drawing;

namespace ImageProcessingCli.Core.ImageProcessor
{
  abstract class ImageProcessor
  {
    protected Bitmap bitmap;

    protected ImageProcessor(Bitmap bitmap)
    {
      this.bitmap = bitmap;
    }

    public abstract Bitmap Convert();
  }
}