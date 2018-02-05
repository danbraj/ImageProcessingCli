using System.Text.RegularExpressions;
using GProject.Models.Enums;

namespace GProject.Models.ArgumentsParser
{
    static class ArgumentsParser
    {
        public static ArgumentsParsingResult ParseArguments(string[] args)
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
                } else if (args[0] == "-e" || args[0] == "--encode") {
                    command = Command.Encode;
                } else if (args[0] == "-d" || args[0] == "--decode") {
                    command = Command.Decode;
                } else {
                    return null;
                }

                Match result = Regex.Match(args[1], @"(.*)\.(jpg|bmp|png)$");
                if (result.Success)
                {
                    var data = result.Groups;
                    if (data.Count == 3)
                    {
                        return new ArgumentsParsingResult(command, data[1].Value, data[2].Value, args.Length > 2 ? args[2] : "");
                    }
                }
            }
            return null;
        }
    }
}