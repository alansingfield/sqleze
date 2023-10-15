namespace Sqleze;

// Was originally ISqlezeConnectionFactory
public interface ISqleze
{
    /// <summary>
    /// Open the connection to the database. Note that the actual ADO calls
    /// are deferred until a database connection is actually needed.
    /// </summary>
    /// <returns></returns>
    ISqlezeConnection Connect();
    /// <summary>
    /// Get back to the Builder which created this Factory to enable
    /// reconfiguring with different options
    /// </summary>
    /// <returns></returns>
    ISqlezeBuilder Reconfigure();
}
