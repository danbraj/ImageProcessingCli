using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using ImageProcessingCli.Core.ArgsParser;
using ImageProcessingCli.Core.Enums;
using ImageProcessingCli.Core.ImageProcessor;

namespace ImageProcessingCli
{
  static class ImageProcessing
  {
    public static Error Process(ArgsParsingResult apr)
    {
      if (apr == null) return Error.ArgsParse;

      string bitmapFileName = apr.BitmapFile;
      string outcomeBitmapFileName = apr.OutcomeBitmapFile;
      bool isFileExists = File.Exists(bitmapFileName);

      if (!isFileExists) return Error.NoFileExists;

      Command currentCommand = apr.Command;

      Bitmap bitmap = new Bitmap(Image.FromFile(bitmapFileName)); // new Bitmap(bitmapFileName);

      ImageProcessor imageProcessor;

      if (currentCommand == Command.Decode) {

      } else {
        switch (currentCommand) {
          case Command.Negative:
            imageProcessor = new GrayscaleImageProcessor(bitmap);
            break;
          case Command.Grayscale:
            imageProcessor = new GrayscaleImageProcessor(bitmap);
            break;
          case Command.Sepia:
            imageProcessor = new GrayscaleImageProcessor(bitmap);
            break;
          default:
            return Error.UnknownCommand;
        }
        var newBitmap = imageProcessor.Convert();
        newBitmap.Save(outcomeBitmapFileName);
      }
      return Error.Ok;
    }
  }
}