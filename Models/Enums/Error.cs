using System;

namespace ImageProcessingCli.Core.Enums
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