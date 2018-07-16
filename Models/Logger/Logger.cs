using System;
using ImageProcessingCli.Models.Enums;

namespace ImageProcessingCli.Models.Logger
{
    static class Logger
    {
        public static void WriteLine(string content, ConsoleColor cc)
        {
            Console.ForegroundColor = cc;
            Console.WriteLine(content);
            Console.ResetColor();
        }

        public static void WriteResult(Error e)
        {
            switch (e)
            {
                case Error.ArgsParse:
                    Logger.ShowApplicationUsage();
                    return;
                case Error.NoFileExists:
                    Logger.WriteLine("Nie ma takiego pliku!", ConsoleColor.Yellow);
                    return;
                case Error.UnknownCommand:
                    Logger.WriteLine("Niepoprawna akcja!", ConsoleColor.Yellow);
                    return;
                default:
                    Logger.WriteLine("Gotowe!", ConsoleColor.Green);
                    return;
            }
        }

        public static void ShowApplicationUsage()
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
    }
}