using Sqleze;
using Sqleze.Options;
using Sqleze.Params;
using Sqleze.SqlClient;

namespace Sqleze;

public class SqlezeCommand : ISqlezeCommand
{
    private readonly IResolverContext commandScope;
    private readonly CommandCreateOptions commandCreateOptions;
    private readonly ISqlezeReaderFactory sqlezeReaderFactory;
    private readonly IAdo ado;
    private readonly IParameterPreparation parameterPreparation;

    #if DRYIOC_DLL
    public
    #else
    internal
    #endif
    SqlezeCommand(
        IResolverContext commandScope,
        CommandCreateOptions commandCreateOptions,
        ISqlezeParameterCollection sqlezeParameterCollection,
        ISqlezeReaderFactory sqlezeReaderFactory,
        IAdo ado,
        IParameterPreparation parameterPreparation)
    {
        this.commandScope = commandScope;
        this.commandCreateOptions = commandCreateOptions;
        this.Parameters = sqlezeParameterCollection;
        this.sqlezeReaderFactory = sqlezeReaderFactory;
        this.ado = ado;
        this.parameterPreparation = parameterPreparation;
    }

    public ISqlezeParameterCollection Parameters { get; }

    public int ExecuteNonQuery(ISqlezeReaderFactory? sqlezeReaderFactory = null)
    {
        closeExisting();

        var factory = sqlezeReaderFactory ?? this.sqlezeReaderFactory;
        var execScope = factory.OpenScope();

        setupCommand(execScope);

        int result;

        try
        {
            result = ado.ExecuteNonQuery();

            ado.ReadOutputParams();
        }
        finally
        {
            ado.EndCommand();
        }

        return result;
    }

    public async Task<int> ExecuteNonQueryAsync(
        ISqlezeReaderFactory? sqlezeReaderFactory = null, 
        CancellationToken cancellationToken = default)
    {
        await closeExistingAsync(cancellationToken).ConfigureAwait(false);

        var factory = sqlezeReaderFactory ?? this.sqlezeReaderFactory;
        var execScope = factory.OpenScope();

        await setupCommandAsync(execScope, cancellationToken).ConfigureAwait(false);

        int result;

        try
        {
            result = await ado.ExecuteNonQueryAsync(cancellationToken).ConfigureAwait(false);

            ado.ReadOutputParams();
        }
        finally
        {
            ado.EndCommand();
        }

        return result;
    }

    //public async Task<ISqlezeReader> ExecuteReaderAsync(CancellationToken cancellationToken = default)
    //    => await this.ExecuteReaderAsync(this.sqlezeReaderFactory, cancellationToken).ConfigureAwait(false);

    public ISqlezeReader ExecuteReader(ISqlezeReaderFactory? sqlezeReaderFactory = null)
    {
        closeExisting();

        var factory = sqlezeReaderFactory ?? this.sqlezeReaderFactory;
        var executeScope = factory.OpenScope();

        setupCommand(executeScope);

        ado.ExecuteReader();

        return executeScope.OpenReader();
    }

    public async Task<ISqlezeReader> ExecuteReaderAsync(
        ISqlezeReaderFactory? sqlezeReaderFactory = null,
        CancellationToken cancellationToken = default)
    {
        await closeExistingAsync(cancellationToken).ConfigureAwait(false);

        var factory = sqlezeReaderFactory ?? this.sqlezeReaderFactory;
        var execScope = factory.OpenScope();

        await setupCommandAsync(execScope, cancellationToken).ConfigureAwait(false);

        await ado.ExecuteReaderAsync(cancellationToken).ConfigureAwait(false);

        return execScope.OpenReader();
    }

    public ISqlezeReaderBuilder With<T>(Action<T, ISqlezeScope> configure)
    {
        var scopedFactoryFunc = commandScope.Resolve<Func<IScopedSqlezeReaderBuilder<T>>>();

        // Create a new factory which is configured to open a new scope
        var scopedFactory = scopedFactoryFunc();

        // Configure factory scope and create the SqlezeReaderFactory
        return scopedFactory.Create(configure);
    }


    private void setupCommand(IScopedSqlezeReaderFactory execScope)
    {
        var adoParameters = parameterPreparation.Prepare().ToList();

        ado.StartCommand(commandCreateOptions, execScope, adoParameters);
    }

    private async Task setupCommandAsync(IScopedSqlezeReaderFactory execScope, CancellationToken cancellationToken = default)
    {
        var adoParameters = (await parameterPreparation
                .PrepareAsync(cancellationToken)
                .ConfigureAwait(false) 
            ).ToList();

        ado.StartCommand(commandCreateOptions, execScope, adoParameters);
    }


    private void closeExisting()
    {
        if(ado.SqlDataReader != null)
        {
            ado.ThrowIfPendingOutputCaptures();
            ado.CompleteReader();
        }

        if(ado.SqlCommand != null)
            ado.EndCommand();
    }

    private async Task closeExistingAsync(CancellationToken cancellationToken = default)
    {
        if(ado.SqlDataReader != null)
        {
            ado.ThrowIfPendingOutputCaptures();
            await ado.CompleteReaderAsync(cancellationToken).ConfigureAwait(false);
        }

        if(ado.SqlCommand != null)
            ado.EndCommand();
    }
}