# ImageProcessingCli

> An application that allows the image to process and encode a text in it. Written in C# and .NET Core.

* Console app
* Implements some design patterns (e.g. adapter, command, facade).

## Installation

### Use as development project

1. Build project\
`dotnet build`
2. Run application\
`dotnet run [...args]`

## Usage

```bash

dotnet run [-n|--negative] [<FILENAME>]         # Invert colors
dotnet run [-g|--grayscale] [<FILENAME>]        # Convert image to grayscale
dotnet run [-s|--sepia] [<FILENAME>]            # Convert image to sepia
dotnet run [-b|--bluish] [<FILENAME>]           # Convert image to bluish
dotnet run [-e|--encode] [<FILENAME>] "string"  # Encode given text in image
dotnet run [-d|--decode] [<FILENAME>]           # Decode a text from image and display it


### Examples

dotnet run -s eagle.png
dotnet run --negative hawk.jpg
dotnet run -e cool-photo.jpg "secret"
dotnet run -d cool-photo-with-secret-text.jpg

```