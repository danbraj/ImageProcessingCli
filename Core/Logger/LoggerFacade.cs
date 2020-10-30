using System;

namespace ImageProcessingCli.Core.Logger
{
  static class LoggerFacade
  {
    public static void Info(string content)
    {
      Write(content, ConsoleColor.White);
    }

    public static void Highlight(string content)
    {
      Write(content, ConsoleColor.Cyan);
    }

    public static void Success(string content)
    {
      Write(content, ConsoleColor.Green);
    }

    public static void Warn(string content)
    {
      Write(content, ConsoleColor.Yellow);
    }

    public static void Error(string content)
    {
      Write(content, ConsoleColor.Red);
    }

    private static void Write(string content, ConsoleColor cc)
    {
      Console.ForegroundColor = cc;
      Console.WriteLine(content);
      Console.ResetColor();
    }
  }
}