using ImageProcessingCli.Core.Enums;

namespace ImageProcessingCli.Core.Arguments
{
  class ArgumentPayload
  {
    public Processing ProcessingCommand { get; }
    public string BitmapPath { get; }
    public string OutcomeBitmapPath { get; }
    public string TextToEncoding { get; }

    public ArgumentPayload(Processing processingCommand, string bitmapPath, string extension, string textToEncoding = "")
    {
      this.ProcessingCommand = processingCommand;
      this.BitmapPath = $"{bitmapPath}.{extension}";
      this.OutcomeBitmapPath = $"{bitmapPath}.m.{extension}";
      this.TextToEncoding = textToEncoding;
    }
  }
}