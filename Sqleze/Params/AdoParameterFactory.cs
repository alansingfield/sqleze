using Sqleze.Converters.SqlTypes;
using Sqleze;
using Sqleze.Metadata;
using Sqleze.OutputParamReaders;
using Sqleze.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze.Params;

public class AdoParameterFactory<T> : IAdoParameterFactory<T>
{
    private readonly ISqlezeParameter<T> sqlezeParameter;
    private readonly Func<MS.SqlParameter> newMsSqlParameter;
    private readonly IOutputParamReader<T> outputParamReader;
    private readonly IStoredProcParamDefinitionProvider<T> storedProcParamDefinitionProvider;
    private readonly IParameterSpecResolver<T> parameterSpecResolver;

    public AdoParameterFactory(ISqlezeParameter<T> sqlezeParameter,
        Func<MS.SqlParameter> newMsSqlParameter,
        IOutputParamReader<T> outputParamReader,
        IStoredProcParamDefinitionProvider<T> storedProcParamDefinitionProvider,
        IParameterSpecResolver<T> parameterSpecResolver)
    {
        this.sqlezeParameter = sqlezeParameter;
        this.newMsSqlParameter = newMsSqlParameter;
        this.outputParamReader = outputParamReader;
        this.storedProcParamDefinitionProvider = storedProcParamDefinitionProvider;
        this.parameterSpecResolver = parameterSpecResolver;
    }

    public IAdoParameter Create()
    {
        var scalarParameterSpec = parameterSpecResolver.Resolve();

        return createInternal(scalarParameterSpec);
    }

    public async Task<IAdoParameter> CreateAsync(CancellationToken cancellationToken = default)
    {
        var scalarParameterSpec = await parameterSpecResolver
            .ResolveAsync(cancellationToken)
            .ConfigureAwait(false);

        return createInternal(scalarParameterSpec);
    }

    private IAdoParameter createInternal(ScalarParameterSpec scalarParameterSpec)
    {
        verifyParameterMode();

        var mssqlParameter = newMsSqlParameter();

        mssqlParameter.ParameterName = sqlezeParameter.AdoName;

        // Add in the lambda to capture the output
        var outputAction = prepareOutput(mssqlParameter);

        // Set up the parameter direction (Input / InputOutput / Output / ReturnValue)
        mssqlParameter.Direction = computeDirection(hasOutput: outputAction != null);

        // Write the value if it is Input / InputOutput direction.
        setValue(mssqlParameter);

        // Set db type, length, precision, scale AFTER setting the value because ADO
        // makes a guess at the DB type when you set the value.
        setSpecificSqlType(scalarParameterSpec, mssqlParameter);

        // TODO - make constructor of AdoParameter into an options
        // so we can construct from the container.
        return new AdoParameter(mssqlParameter, outputAction);

    }

    private void verifyParameterMode()
    {
        if(!(sqlezeParameter.Mode is SqlezeParameterMode.Scalar or SqlezeParameterMode.ReturnValue))
            throw new Exception($"Incorrect {nameof(IAdoParameterFactory)} for parameter with mode {sqlezeParameter.Mode}");
    }

    private void setSpecificSqlType(ScalarParameterSpec scalarParameterSpec, MS.SqlParameter mssqlParameter)
    {
        mssqlParameter.SqlDbType = scalarParameterSpec.SqlDbType;

        if(scalarParameterSpec.Size != null)
            mssqlParameter.Size = scalarParameterSpec.Size.Value;

        if(scalarParameterSpec.Scale != null)
            mssqlParameter.Scale = scalarParameterSpec.Scale.Value;

        if(scalarParameterSpec.Precision != null)
            mssqlParameter.Precision = scalarParameterSpec.Precision.Value;
    }

    private void setValue(MS.SqlParameter mssqlParameter)
    {
        if(sqlezeParameter.OmitInput || sqlezeParameter.Mode != SqlezeParameterMode.Scalar)
            return;

        mssqlParameter.Value = ((object?)sqlezeParameter.Value) ?? System.DBNull.Value;
    }

    private Action? prepareOutput(MS.SqlParameter mssqlParameter)
    {
        if(sqlezeParameter.OutputAction == null)
            return null;

        // Set up the lambda to retrieve the parameter's output value after running the SQL.
        return outputParamReader.PrepareOutputAction(mssqlParameter, sqlezeParameter.OutputAction);
    }

    private ParameterDirection computeDirection(bool hasOutput)
    {
        return
        (
            sqlezeParameter.Mode, 
            hasOutput, 
            sqlezeParameter.OmitInput
        ) 
        switch 
        {
            // Mode                             Output? Omit input?
            (SqlezeParameterMode.Scalar,        true,   false)  => ParameterDirection.InputOutput,
            (SqlezeParameterMode.Scalar,        true,   true)   => ParameterDirection.Output,
            (SqlezeParameterMode.ReturnValue,   _,      _)      => ParameterDirection.ReturnValue,

            // These cases shouldn't happen since we check earlier.
            (SqlezeParameterMode.TableType,     _,      _)      => throw new Exception("SqlezeParameterMode is incorrect"),
            (SqlezeParameterMode.AssemblyType,  _,      _)      => throw new Exception("SqlezeParameterMode is incorrect"),

            _ => ParameterDirection.Input
        };
    }


}
