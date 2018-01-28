using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using GProject.Enums;
using GProject.Models;

namespace GProject
{
    class Program
    {
        static void Main(string[] args)
        {
            var arguments = ArgumentsParser.ParseArguments(args);
            if (arguments != null)
            {
                string bitmapFileName = arguments.GetFullBitmapFile();
                bool isFileExists = File.Exists(bitmapFileName);
                if (isFileExists)
                {
                    Bitmap bitmap;
                    if (arguments.Command == Command.Decode)
                    {
                        bitmap = new Bitmap(bitmapFileName);
                    }
                    else
                    {
                        var image = Image.FromFile(bitmapFileName);
                        bitmap = new Bitmap(image);
                    }

                    switch (arguments.Command)
                    {
                        case Command.Negative:

                            ImageProcessing.ConvertToNegative(bitmap);
                            bitmap.Save(arguments.GetChangedBitmapFile("negative"));

                            break;
                        case Command.Grayscale:

                            ImageProcessing.ConvertToGray(bitmap);
                            bitmap.Save(arguments.GetChangedBitmapFile("grayscale"));

                            break;
                        case Command.Sepia:

                            ImageProcessing.ConvertToSepia(bitmap);
                            bitmap.Save(arguments.GetChangedBitmapFile("sepia"));

                            break;
                        case Command.Encode:

                            int maxLength = ImageProcessing.CalculateTextMaxLengthToEncode(bitmap);

                            WriteLine($"Długość tekstu do zakodowania: {arguments.TextToEncoding.Length} / {maxLength}", ConsoleColor.Cyan);
                            // TODO: gdy dł. tekstu jest większa od dł. pętli po wszystkich pikselach
                            
                            ImageProcessing.EncodeTextIn(bitmap, arguments.TextToEncoding);
                            bitmap.Save(arguments.GetChangedBitmapFile("encode"), ImageFormat.Png);

                            break;
                        case Command.Decode:

                            string message = ImageProcessing.DecodeTextFrom(bitmap);
                            Console.WriteLine($"Odkodowana wiadomość:\n\n{message}\n");

                            break;
                        default:
                            break;
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
    -n|--negative   Obraca kolory w obrazie
    -g|--grayscale  Zamienia obraz na czarno-biały
    -s|--sepia      Stosuje filtr sepia do obrazu
    -e|--encode     Ukrywa tekst w obrazie
    -d|--decode     Wyświetla ukryty tekst w obrazie
    
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
