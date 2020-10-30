using System;
using ImageProcessingCli.Core.Arguments;
using ImageProcessingCli.Core.Command;
using ImageProcessingCli.Core.Enums;
using ImageProcessingCli.Core.Logger;

namespace ImageProcessingCli
{
  class Program
  {
    static int Main(string[] args)
    {
      ArgumentAdapter argumentAdapter = new ArgumentAdapter(args);
      ArgumentPayload ap = argumentAdapter.Parse();
      if (ap == null) 
      {
        ApplicationUsage();
        return 1;
      }

      Command command;
      switch (ap.ProcessingCommand)
      {
        case Processing.Negative:
          command = new NegativeCommand(ap.BitmapPath, ap.OutcomeBitmapPath);
          break;
        case Processing.Grayscale:
          command = new GrayscaleCommand(ap.BitmapPath, ap.OutcomeBitmapPath);
          break;
        case Processing.Sepia:
          command = new SepiaCommand(ap.BitmapPath, ap.OutcomeBitmapPath);
          break;
        case Processing.Encode:
          command = new TextCodingCommand(ap.BitmapPath, ap.OutcomeBitmapPath, ap.TextToEncoding);
          break;
        case Processing.Decode:
          command = new TextDecodingCommand(ap.BitmapPath);
          break;
        case Processing.Bluish:
          command = new BluishCommand(ap.BitmapPath, ap.OutcomeBitmapPath);
          break;
        default:
          LoggerFacade.Warn("Niepoprawna akcja!");
          return 2;
      }
      command.Execute();
      LoggerFacade.Success("Gotowe!");
      return 0;
    }

    private static void ApplicationUsage()
    {
      Console.WriteLine(
$@"Użycie:  app [command] [bitmap-file]
         app --encode [bitmap-file] ""[text]
command:
    -n|--negative       Obraca kolory w obrazie
    -g|--grayscale      Zamienia obraz na czarno-biały
    -s|--sepia          Stosuje filtr sepia do obrazu
    -b|--bluish         Stosuje filtr bluish do obrazu
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