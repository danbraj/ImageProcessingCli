using System.Drawing;

namespace ImageProcessingCli.Models.ImageProcessing
{
    abstract class ImageProcessing
    {
        protected Bitmap bitmap;
        
        protected ImageProcessing(Bitmap bitmap)
        {
            this.bitmap = bitmap;
        }

        public abstract Bitmap Convert();
    }
}