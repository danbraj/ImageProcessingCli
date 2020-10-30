using System;

namespace ImageProcessingCli.Core.Enums
{
  [Flags]
  enum Error
  {
    Ok = 0,
    ArgsParse = 1,
    UnknownCommand = 2,
    NoFileExists = 4,
    ProcessingException = 8
  }
}