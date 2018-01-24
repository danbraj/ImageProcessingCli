using System.Drawing;
using GProject.Enums;

namespace GProject.Models
{
    class Arguments
    {
        public Command Command { get; }
        public string BitmapFile { get; }
        public string Extension { get; }

        public Arguments(Command command, string bitmapFile, string extension)
        {
            this.Command = command;
            this.BitmapFile = bitmapFile;
            this.Extension = extension;
        }

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