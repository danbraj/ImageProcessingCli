# ImageProcessingCli

> An application that allows the image to process and encode a text in it

## Installation

0. Install .NET Core 
1. Build project\
`dotnet build`
2. Run application\
`dotnet run [...args]`
    * Use as framework-dependent deployment\
    `dotnet publish -c Release -o ./ImageProcessingCli && cd ImageProcessingCli`\
    `dotnet ImageProcessingCli.dll [...args]`

## Usage

```bash
# Invert colors
dotnet run [-n|--negative] [<FILENAME>]

# Convert image to grayscale
dotnet run [-g|--grayscale] [<FILENAME>]

# Convert image to sepia
dotnet run [-s|--sepia] [<FILENAME>]

# Encode given text in image
dotnet run [-e|--encode] [<FILENAME>] "string"

# Decode a text from image and display it
dotnet run [-d|--decode] [<FILENAME>]
```

### Example

`dotnet run --negative cat.jpg`\
`dotnet run -e cool-photo.jpg "secret"`\
`dotnet run -d cool-photo-with-secret-text.jpg`