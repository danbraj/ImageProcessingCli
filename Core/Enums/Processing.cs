using System;

namespace ImageProcessingCli.Core.Enums
{
  [Flags]
  enum Processing
  {
    None = 0,
    Negative = 1,
    Grayscale = 2,
    Sepia = 4,
    Encode = 8,
    Decode = 16,
    Bluish = 32
  }
}