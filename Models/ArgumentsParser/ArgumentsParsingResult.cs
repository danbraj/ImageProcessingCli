using System.Drawing;
using ImageProcessingCli.Models.Enums;

namespace ImageProcessingCli.Models.ArgumentsParser
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
            this.OutcomeBitmapFile = $"{bitmapFile}.m.{extension}";
            this.TextToEncoding = textToEncoding;
        }
    }
}