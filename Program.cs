using System;
using System.Drawing;
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
                    var bitmap = new Bitmap(bitmapFileName);

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
                        default:
                            break;
                    }
                    Console.WriteLine("Gotowe!");
                }
                else
                {
                    Console.WriteLine("Nie ma takiego pliku!");
                }
            }
            else
            {
                ShowApplicationUsage();
            }
        }

        public static void ShowApplicationUsage()
        {
            Console.WriteLine(
$@"Użycie:  app [command] [bitmap-file]

command:
    -n|--negative   Obraca kolory w obrazie
    -g|--grayscale  Zamienia obraz na czarno-biały
    -s|--sepia      Stosuje filtr sepia do obrazu
    
bitmap-file:
    Ścieżka do pliku graficznego (*.bmp, *.png, *.jpg)"
            );
        }
    }
}
