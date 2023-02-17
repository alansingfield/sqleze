namespace Sqleze;

public interface ISqlezeConnectionProvider
{
    ISqlezeConnection SqlezeConnection { get; }
}

