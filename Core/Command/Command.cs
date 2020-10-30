namespace ImageProcessingCli.Core.Command
{
  abstract class Command
  {
    protected string bitmapPath;

    protected Command(string bitmapPath)
    {
      this.bitmapPath = bitmapPath;
    }

    public abstract void Execute();
  }
}