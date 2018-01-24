using System.Text.RegularExpressions;
using GProject.Enums;

namespace GProject.Models
{
    static class ArgumentsParser
    {
        public static Arguments ParseArguments(string[] args)
        {
            if (args.Length >= 2)
            {
                Command command;
                if (args[0] == "-n" || args[0] == "--negative") {
                    command = Command.Negative;
                } else if (args[0] == "-g" || args[0] == "--grayscale") {
                    command = Command.Grayscale;
                } else if (args[0] == "-s" || args[0] == "--sepia") {
                    command = Command.Sepia;
                } else {
                    return null;
                }

                Match result = Regex.Match(args[1], @"(.*)\.(jpg|bmp|png)$");
                if (result.Success)
                {
                    var data = result.Groups;
                    if (data.Count == 3)
                    {
                        return new Arguments(command, data[1].Value, data[2].Value);
                    }
                }
            }
            return null;
        }
    }
}