using Sqleze.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze.Readers;

/// <summary>
/// Wrapper around SqlDataReader.Read() / ReadAsync() which calls IAdo.ReaderFault if an
/// exception occurs.
/// </summary>
public interface ICheckedAdoDataReader
{
    bool Read();
    Task<bool> ReadAsync(CancellationToken cancellationToken = default);
}

public class CheckedAdoDataReader : ICheckedAdoDataReader
{
    private readonly IAdo ado;
    private readonly IAdoDataReader adoDataReader;
    private readonly CheckedAdoDataReaderOptions dataReaderOptions;

    public CheckedAdoDataReader(IAdo ado, IAdoDataReader adoDataReader,
        CheckedAdoDataReaderOptions dataReaderOptions
        )
    {
        this.ado = ado;
        this.adoDataReader = adoDataReader;
        this.dataReaderOptions = dataReaderOptions;
    }

    public bool Read()
    {
        bool result;

        try
        {
            result = this.adoDataReader.SqlDataReader.Read();
        }
        catch(Exception)
        {
            ado.ReaderFault();
            throw;
        }

        return result;
    }

    public async Task<bool> ReadAsync(CancellationToken cancellationToken = default)
    {
        // https://github.com/dotnet/SqlClient/issues/593
        // A bug in Microsoft.Data.SqlClient means that reading large string/binary
        // fields with ReadAsync() is extremely slow. This allows us to switch over
        // to the synchronous version until they fix it.
        if(dataReaderOptions.UseSynchronousReadWhenAsync)
            return Read();

        bool result;

        try
        {
            result = await this.adoDataReader.SqlDataReader
                .ReadAsync(cancellationToken)
                .ConfigureAwait(false);
        }
        catch(Exception)
        {
            await ado.ReaderFaultAsync().ConfigureAwait(false);
            throw;
        }

        return result;
    }
}
