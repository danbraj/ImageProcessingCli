using System.Drawing;
using GProject.Enums;

namespace GProject.Models
{
    class Arguments
    {
        public Command Command { get; set; }
        public string BitmapFile { get; set; }
        public string Extension { get; set; }

        public string GetFullBitmapFile()
        {
            return $"{this.BitmapFile}.{this.Extension}";
        }

        public string GetChangedBitmapFile(string change)
        {
            return $"{this.BitmapFile}_{change}.{this.Extension}";
        }
    }
}