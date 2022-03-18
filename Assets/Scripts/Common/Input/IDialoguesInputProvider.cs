namespace Sheldier.Common
{
    public interface IDialoguesInputProvider : ICursorProvider
    {
        InputButton LowerChoice { get; }
        InputButton LeftChoice { get; }
        InputButton UpperChoice { get; }
        InputButton RightChoice { get; }
    }
}