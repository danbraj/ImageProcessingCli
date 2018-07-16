using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using ImageProcessingCli.Models.ArgumentsParser;
using ImageProcessingCli.Models.Enums;
using ImageProcessingCli.Models.ImageProcessing;
using L = ImageProcessingCli.Models.Logger.Logger;

namespace ImageProcessingCli
{
    class Program
    {
        static void Main(string[] args)
        {
            var result = Execute(args);
            L.WriteResult(result);
        }

        static readonly Command codingCommands = Command.Encode | Command.Decode;
        static readonly Command processingCommands = Command.Negative | Command.Grayscale | Command.Sepia;

        static Error Execute(string[] args)
        {
            var argsResult = ArgumentsParser.ParseArguments(args);

            if (argsResult != null)
            {
                string bitmapFileName = argsResult.BitmapFile;
                bool isFileExists = File.Exists(bitmapFileName);

                if (isFileExists)
                {
                    var currentCommand = argsResult.Command;

                    Bitmap bitmap = currentCommand == Command.Encode ? new Bitmap(Image.FromFile(bitmapFileName)) : new Bitmap(bitmapFileName);

                    if ((currentCommand & processingCommands) == currentCommand)
                    {
                        ImageProcessing imageProcessing;

                        switch (currentCommand)
                        {
                            case Command.Negative:
                                imageProcessing = new NegativeImageConverter(bitmap);
                                break;
                            case Command.Grayscale:
                                imageProcessing = new GrayscaleImageConverter(bitmap);
                                break;
                            case Command.Sepia:
                                imageProcessing = new SepiaImageConverter(bitmap);
                                break;
                            default:
                                return Error.UnknownCommand;
                        }

                        imageProcessing.Convert().Save(argsResult.OutcomeBitmapFile);
                    }
                    else if ((currentCommand & codingCommands) == currentCommand)
                    {
                        var textCoding = new TextCoding(bitmap);

                        if (currentCommand == Command.Encode)
                        {
                            L.WriteLine($"Długość tekstu do zakodowania: {argsResult.TextToEncoding.Length}", ConsoleColor.Cyan);
                            textCoding.Encode(argsResult.TextToEncoding);
                            textCoding.GetBitmap().Save(argsResult.OutcomeBitmapFile, ImageFormat.Png);
                            if (textCoding.IsOutRange)
                            {
                                L.WriteLine($"Ostrzeżenie: Zabrakło wolnych bitów do zakodowania!", ConsoleColor.Yellow);
                            }
                        }
                        else if (currentCommand == Command.Decode)
                        {
                            string message = textCoding.Decode();
                            L.WriteLine($"Odkodowana wiadomość:\n\n{message}\n", ConsoleColor.White);
                        }
                    }
                    else
                    {
                        return Error.UnknownCommand;
                    }
                    return Error.Ok;
                }
                else
                {
                    return Error.NoFileExists;
                }
            }
            else
            {
                return Error.ArgsParse;
            }
        }
    }
}
