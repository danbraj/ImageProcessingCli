using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using GProject.Models.ArgumentsParser;
using GProject.Models.Enums;
using GProject.Models.ImageProcessing;

namespace GProject
{
    class Program
    {
        static void Main(string[] args)
        {
            var argsResult = ArgumentsParser.ParseArguments(args);
            if (argsResult != null)
            {
                string bitmapFileName = argsResult.BitmapFile;
                bool isFileExists = File.Exists(bitmapFileName);
                if (isFileExists)
                {
                    var currentCommand = argsResult.Command;

                    var processingCommands = Command.Negative | Command.Grayscale | Command.Sepia;
                    var codingCommands = Command.Encode | Command.Decode;

                    Bitmap bitmap = currentCommand == Command.Decode ? new Bitmap(bitmapFileName) : new Bitmap(Image.FromFile(bitmapFileName));

                    if ((processingCommands & currentCommand) == currentCommand)
                    {
                        ImageProcessing imageProcessing;

                        if (currentCommand == Command.Negative)
                        {
                            imageProcessing = new NegativeImageConverter(bitmap);
                        }
                        else if (currentCommand == Command.Grayscale)
                        {
                            imageProcessing = new GrayscaleImageConverter(bitmap);
                        }
                        else
                        {
                            imageProcessing = new SepiaImageConverter(bitmap);
                        }

                        imageProcessing.Convert().Save(argsResult.OutcomeBitmapFile);
                    }
                    else if ((codingCommands & currentCommand) == currentCommand)
                    {
                        var textCoding = new TextCoding(bitmap);

                        if (currentCommand == Command.Encode)
                        {
                            WriteLine($"Długość tekstu do zakodowania: {argsResult.TextToEncoding.Length}", ConsoleColor.Cyan);
                            textCoding.Encode(argsResult.TextToEncoding);
                            textCoding.GetBitmap().Save(argsResult.OutcomeBitmapFile, ImageFormat.Png);
                            if (textCoding.IsOutRange)
                            { 
                                WriteLine($"Ostrzeżenie: Zabrakło wolnych bitów do zakodowania!", ConsoleColor.Yellow);
                            }
                        }
                        else if (currentCommand == Command.Decode)
                        {
                            string message = textCoding.Decode();
                            Console.WriteLine($"Odkodowana wiadomość:\n\n{message}\n");
                        }
                    }
                    WriteLine("Gotowe!", ConsoleColor.Green);
                }
                else
                {
                    WriteLine("Nie ma takiego pliku!", ConsoleColor.Yellow);
                }
            }
            else
            {
                ShowApplicationUsage();
            }
        }

        private static void ShowApplicationUsage()
        {
            Console.WriteLine(
$@"Użycie:  app [command] [bitmap-file]
         app --encode [bitmap-file] ""[text]

command:
    -n|--negative       Obraca kolory w obrazie
    -g|--grayscale      Zamienia obraz na czarno-biały
    -s|--sepia          Stosuje filtr sepia do obrazu
    -e|--encode         Ukrywa tekst w obrazie
    -d|--decode         Wyświetla ukryty tekst w obrazie
    
bitmap-file:
    Ścieżka do pliku graficznego (*.bmp, *.png, *.jpg)

text:
    Tekst do zakodowania"
            );
        }

        private static void WriteLine(string content, ConsoleColor cc)
        {
            Console.ForegroundColor = cc;
            Console.WriteLine(content);
            Console.ResetColor();
        }
    }
}
