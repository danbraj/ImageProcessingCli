using System.Drawing;
using GProject.Models.Enums;

namespace GProject.Models.ArgumentsParser
{
    class ArgumentsParsingResult
    {
        public Command Command { get; }
        public string BitmapFile { get; }
        public string OutcomeBitmapFile { get; }
        public string TextToEncoding { get; }

        public ArgumentsParsingResult(Command command, string bitmapFile, string extension, string textToEncoding = "")
        {
            this.Command = command;
            this.BitmapFile = $"{bitmapFile}.{extension}";
            this.OutcomeBitmapFile = $"{bitmapFile}_modified.{extension}";
            this.TextToEncoding = textToEncoding;
        }
    }
}