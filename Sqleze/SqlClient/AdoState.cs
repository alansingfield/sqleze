using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze.SqlClient;

public enum AdoState
{
    /// <summary>
    /// ADO connection is not open
    /// </summary>
    Disconnected = 0,
    /// <summary>
    /// ADO connection is opened
    /// </summary>
    Connected = 1,
    /// <summary>
    /// Within a transaction
    /// </summary>
    TransactionStarted = 2,
    /// <summary>
    /// Command is open
    /// </summary>
    CommandOpen = 3,
    /// <summary>
    /// SqlDataReader is open.
    /// </summary>
    ReaderActive = 4
}
