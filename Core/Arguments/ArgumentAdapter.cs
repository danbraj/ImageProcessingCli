using System.Text.RegularExpressions;
using ImageProcessingCli.Core.Enums;

namespace ImageProcessingCli.Core.Arguments
{
  class ArgumentAdapter
  {
    private string[] args;

    public ArgumentAdapter(string[] args)
    {
      this.args = args;
    }

    public ArgumentPayload Parse()
    {
      if (this.args.Length >= 2)
      {
        Processing processing;
        if (this.args[0] == "-n" || this.args[0] == "--negative")
        {
          processing = Processing.Negative;
        }
        else if (this.args[0] == "-g" || this.args[0] == "--grayscale")
        {
          processing = Processing.Grayscale;
        }
        else if (this.args[0] == "-s" || this.args[0] == "--sepia")
        {
          processing = Processing.Sepia;
        }
        else if (this.args[0] == "-b" || this.args[0] == "--bluish")
        {
          processing = Processing.Bluish;
        }
        else if (this.args[0] == "-e" || this.args[0] == "--encode")
        {
          processing = Processing.Encode;
        }
        else if (this.args[0] == "-d" || this.args[0] == "--decode")
        {
          processing = Processing.Decode;
        }
        else
        {
          return null;
        }

        Match result = Regex.Match(this.args[1], @"(.*)\.(jpg|bmp|png)$");
        if (result.Success)
        {
          var data = result.Groups;
          if (data.Count == 3)
          {
            return new ArgumentPayload(processing, data[1].Value, data[2].Value, this.args.Length > 2 ? this.args[2] : "");
          }
        }
      }
      return null;
    }
  }
}