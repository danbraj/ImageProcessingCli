using System;

namespace ImageProcessingCli.Models.Enums
{
    [Flags]
    enum Error
    {
        Ok = 0,
        ArgsParse = 1,
        NoFileExists = 2,
        UnknownCommand = 4
    }
}