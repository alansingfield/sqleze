namespace Sqleze;

// Was originally ISqlezeConnectionFactory
public interface ISqleze
{
    ISqlezeConnection Connect();
}
