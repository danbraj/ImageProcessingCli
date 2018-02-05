using System.Drawing;

namespace GProject.Models.ImageProcessing
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