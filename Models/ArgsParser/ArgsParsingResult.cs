using System.Drawing;
using ImageProcessingCli.Core.Enums;

namespace ImageProcessingCli.Core.ArgsParser
{
  class ArgsParsingResult
  {
    public Command Command { get; }
    public string BitmapFile { get; }
    public string OutcomeBitmapFile { get; }
    public string TextToEncoding { get; }

    public ArgsParsingResult(Command command, string bitmapFile, string extension, string textToEncoding = "")
    {
      this.Command = command;
      this.BitmapFile = $"{bitmapFile}.{extension}";
      this.OutcomeBitmapFile = $"{bitmapFile}.m.{extension}";
      this.TextToEncoding = textToEncoding;
    }
  }
}