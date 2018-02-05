using System;

namespace GProject.Models.Enums
{
    [Flags]
    enum Command
    {
        None = 0,
        Negative = 1,
        Grayscale = 2,
        Sepia = 4,
        Encode = 8,
        Decode = 16
    }
}