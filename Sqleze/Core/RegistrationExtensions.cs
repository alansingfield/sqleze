using Sqleze.Collations;
using Sqleze.Composition;
using Sqleze.Converters.Metadata;
using Sqleze.DryIoc;
using Sqleze.Dynamics;
using Sqleze.InfoMessage;
using Sqleze;
using Sqleze.Metadata;
using Sqleze.NamingConventions;
using Sqleze.Options;
using Sqleze.OutputParamReaders;
using Sqleze.Params;
using Sqleze.Readers;
using Sqleze.RowsetMetadata;
using Sqleze.SqlClient;
using Sqleze.TableValuedParameters;
using Sqleze.ValueGetters;
using System.Collections;
using System.ComponentModel;
using System.Xml.Linq;
using Sqleze.Security;
using Sqleze.Timeout;
using Sqleze.Configuration;
using Sqleze.DataSets;
using Sqleze.Registration;

namespace Sqleze.Registration;

public static class CoreRegistrationExtensions
{
    public static void RegisterSqlezeCore(this IRegistrator registrator)
    {
        registrator.Register(typeof(IGenericResolver<>), typeof(GenericResolver<>));
        registrator.Register(typeof(IMultiResolver<,>), typeof(MultiResolver<,>));

        registrator.Register<ISqlezeBuilder, SqlezeConnectionBuilder>(
            Reuse.ScopedOrSingleton,
            setup: Setup.With(asResolutionCall: true));

        registrator.Register<ISqleze, SqlezeConnectionFactory>(
            Reuse.ScopedOrSingleton);

        registrator.Register<IScopedSqlezeConnectionFactory, ScopedSqlezeConnectionFactory>(
            Reuse.Scoped,
            setup: Setup.With(openResolutionScope: true));

        registrator.Register(typeof(IScopedSqlezeConnectionBuilder<>), typeof(ScopedSqlezeConnectionBuilder<>),
            Reuse.Scoped,
            setup: Setup.With(openResolutionScope: true));

        registrator.Register<ISqlezeConnectionProvider, SqlezeConnectionProvider>(
            Reuse.Scoped,
            setup: Setup.With(openResolutionScope: true));

        registrator.Register<ISqlezeConnection, SqlezeConnection>(
            Reuse.ScopedToService<ISqlezeConnectionProvider>(),
            setup: Setup.With(asResolutionCall: true));

        registrator.Register<ConfigurationRoot>();

        registrator.Register<ISqlezeCommandBuilder, SqlezeCommandBuilder>(
            Reuse.ScopedOrSingleton,
            setup: Setup.With(asResolutionCall: true));

        registrator.Register<ISqlezeCommandFactory, SqlezeCommandFactory>(
            Reuse.Scoped);

        registrator.Register<IScopedSqlezeCommandFactory, ScopedSqlezeCommandFactory>(
            Reuse.Scoped,
            setup: Setup.With(openResolutionScope: true));

        registrator.Register(typeof(IScopedSqlezeCommandBuilder<>), typeof(ScopedSqlezeCommandBuilder<>),
            Reuse.Scoped,
            setup: Setup.With(openResolutionScope: true));

        registrator.Register<ISqlezeCommandProvider, SqlezeCommandProvider>(
            Reuse.Scoped,
            setup: Setup.With(openResolutionScope: true));

        registrator.Register<ISqlezeCommand, SqlezeCommand>(
            Reuse.ScopedToService<ISqlezeCommandProvider>(),
            setup: Setup.With(asResolutionCall: true));

        registrator.Register<CommandTextRoot>();
        registrator.Register<CommandCreateOptions>(Reuse.ScopedTo<ISqlezeCommandBuilder>());


        registrator.Register<ISqlezeReaderBuilder, SqlezeReaderBuilder>(
            Reuse.ScopedOrSingleton,
            setup: Setup.With(asResolutionCall: true));

        registrator.Register<ISqlezeReaderFactory, SqlezeReaderFactory>(
            Reuse.Scoped);

        registrator.Register<IScopedSqlezeReaderFactory, ScopedSqlezeReaderFactory>(
            Reuse.Scoped,
            setup: Setup.With(openResolutionScope: true));

        registrator.Register(typeof(IScopedSqlezeReaderBuilder<>), typeof(ScopedSqlezeReaderBuilder<>),
            Reuse.Scoped,
            setup: Setup.With(openResolutionScope: true));

        registrator.Register<ISqlezeReaderProvider, SqlezeReaderProvider>(
            Reuse.Scoped,
            setup: Setup.With(openResolutionScope: true));

        registrator.Register<ISqlezeReader, SqlezeReader>(
            Reuse.ScopedToService<ISqlezeReaderProvider>(),
            setup: Setup.With(asResolutionCall: true));



        registrator.Register<ISqlezeRowsetBuilder, SqlezeRowsetBuilder>(
            Reuse.ScopedOrSingleton,
            setup: Setup.With(asResolutionCall: true));

        registrator.Register<ISqlezeRowsetFactory, SqlezeRowsetFactory>(
            Reuse.Scoped);

        registrator.Register<IScopedSqlezeRowsetFactory, ScopedSqlezeRowsetFactory>(
            Reuse.Scoped,
            setup: Setup.With(openResolutionScope: true));

        registrator.Register(typeof(IScopedSqlezeRowsetBuilder<>), typeof(ScopedSqlezeRowsetBuilder<>),
            Reuse.Scoped,
            setup: Setup.With(openResolutionScope: true));

        registrator.RegisterGenericPromotion<ISqlezeRowsetProvider>(typeof(ISqlezeRowsetProvider<>));

        registrator.Register(typeof(ISqlezeRowsetProvider<>), typeof(SqlezeRowsetProvider<>),
            Reuse.Scoped,
            setup: Setup.With(openResolutionScope: true));

        registrator.RegisterGenericPromotion<ISqlezeRowset>(typeof(ISqlezeRowset<>));

        registrator.Register(typeof(ISqlezeRowset<>), typeof(SqlezeRowset<>),
            Reuse.ScopedToService<ISqlezeRowsetProvider>(),
            setup: Setup.With(asResolutionCall: true));





        registrator.Register<ISqlezeScope, SqlezeScope>(Reuse.Scoped);



        registrator.Register<ISqlezeParameterBuilder, SqlezeParameterBuilder>(
            Reuse.ScopedOrSingleton,
            setup: Setup.With(asResolutionCall: true));

        registrator.Register<ISqlezeParameterFactory, SqlezeParameterFactory>(
            Reuse.Scoped);

        registrator.Register<IScopedSqlezeParameterFactory, ScopedSqlezeParameterFactory>(
            Reuse.Scoped,
            setup: Setup.With(openResolutionScope: true));

        registrator.Register(typeof(IScopedSqlezeParameterBuilder<>), typeof(ScopedSqlezeParameterBuilder<>),
            Reuse.Scoped,
            setup: Setup.With(openResolutionScope: true));

        registrator.RegisterGenericPromotion<ISqlezeParameterProvider>(typeof(ISqlezeParameterProvider<>));

        registrator.Register(typeof(ISqlezeParameterProvider<>), typeof(SqlezeParameterProvider<>),
            Reuse.Scoped,
            setup: Setup.With(openResolutionScope: true));

        registrator.RegisterGenericPromotion<ISqlezeParameter>(typeof(ISqlezeParameter<>));

        registrator.Register(typeof(ISqlezeParameter<>), typeof(SqlezeParameter<>),
            Reuse.ScopedToService<ISqlezeParameterProvider>(),
            setup: Setup.With(asResolutionCall: true));


        registrator.RegisterPlaceholder<ParameterCreateOptions>();


        // Parameters collection in the same scope as the command
        registrator.Register<ISqlezeParameterCollection, SqlezeParameterCollection>(
            Reuse.ScopedToService<ISqlezeCommandProvider>());

        //registrator.Register(typeof(ISqlezeParameter<>), typeof(SqlezeParameter<>), Reuse.Transient);


        //registrator.Register(typeof(IScopedSqlezeParameterBuilder<>), typeof(ScopedSqlezeParameterBuilder<>),
        //    Reuse.Scoped,
        //    setup: Setup.With(openResolutionScope: true));


        registrator.Register<IAdo, Ado>(Reuse.ScopedToService<ISqlezeConnectionProvider>());

        registrator.Register<IAdoConnection, AdoConnection>(Reuse.ScopedToService<ISqlezeConnectionProvider>());
        registrator.Register<IAdoDataReader, AdoDataReader>(Reuse.ScopedToService<ISqlezeRowsetProvider>());

        registrator.Register<IConnectionPreOpen, ConnectionPreOpenConnectionString>(Reuse.Scoped);
        registrator.RegisterInfoMessage();
        registrator.RegisterSqlCredential();
        registrator.RegisterAccessToken();
        registrator.RegisterCommandTimeout();

        //registrator.Register<CommandCreateOptions>(Reuse.ScopedToService<ISqlezeCommandProvider>());
        //registrator.RegisterPlaceholder<ICommandCreateOptions>();

        registrator.Register<IDataReaderFieldNames, DataReaderFieldNames>(Reuse.Transient);

        registrator.RegisterSqlezeReaders();
        registrator.RegisterValueGetters();
        registrator.RegisterSqlezeNamingConventions();
        registrator.RegisterMultiParameterSetter();

//        registrator.Register<INamingConvention, NeutralNamingConvention>(Reuse.Scoped);
        registrator.Register<ICollation, Collation>(Reuse.Scoped);
        registrator.Register<CollationOptions>(Reuse.ScopedOrSingleton);


        registrator.Register(typeof(IDynamicPropertyCaller<>), typeof(DynamicPropertyCaller<>), Reuse.Singleton);
        registrator.Register(typeof(IConstructorCache<>), typeof(ConstructorCache<>), Reuse.Singleton);
        registrator.RegisterDefaultValueCache();
        registrator.Register<IDefaultFallbackExpressionBuilder, DefaultFallbackExpressionBuilder>(Reuse.Singleton);

        registrator.Register(typeof(IConstructorLambdaCache<>), typeof(ConstructorLambdaCache<>), Reuse.Singleton);
        registrator.Register(typeof(IConstructorLambdaBuilder<>), typeof(ConstructorLambdaBuilder<>), Reuse.Singleton);
        registrator.Register(typeof(IBestMatchConstructor<>), typeof(BestMatchConstructor<>), Reuse.Singleton);

        registrator.RegisterParameterDefaultSqlTypes();
        registrator.RegisterParameterPreparation();

        registrator.Register<ITableTypeMetadataQuery, TableTypeMetadataQuery>(Reuse.Transient);
        registrator.Register<ITableTypeMetadataCache, TableTypeMetadataCache>(Reuse.ScopedToService<IScopedSqlezeConnectionFactory>());
        registrator.RegisterRecordValueSetters();
        registrator.Register(typeof(ISqlDataRecordAdapter<>), typeof(SqlDataRecordAdapter<>), Reuse.Transient);

        //registrator.Register(typeof(ISqlDataRecordMapper<>), typeof(SqlDataRecordPropertyMapper<>), Reuse.ScopedOrSingleton);
        registrator.RegisterSqlDataRecordMappers();

        registrator.RegisterTableTypeUnmappedColumnsPolicy();
        registrator.RegisterTableTypeDuplicateColumnsPolicy();
        registrator.RegisterTableTypeUnmappedPropertiesPolicy();
        registrator.RegisterTableValuedParameterExtensions();

        registrator.Register<ITableTypeColumnToSqlMetaDataConverter, TableTypeColumnToSqlMetaDataConverter>(Reuse.Singleton);

        registrator.RegisterGenericPromotion(typeof(ISqlDataRecordWriterFromProperty<>), typeof(ISqlDataRecordWriterFromProperty<,>));
        registrator.Register(typeof(ISqlDataRecordWriterFromProperty<,>), typeof(SqlDataRecordWriterFromProperty<,>));

        registrator.RegisterOutputParamReaders();

        registrator.Register<IStoredProcMetadataQuery, StoredProcMetadataQuery>(Reuse.Transient);
        registrator.Register<IStoredProcMetadataCache, StoredProcMetadataCache>(Reuse.ScopedToService<IScopedSqlezeConnectionFactory>());
        registrator.Register(typeof(IStoredProcParamDefinitionProvider<>), typeof(StoredProcParamDefinitionProvider<>),
            Reuse.ScopedToService<IScopedSqlezeParameterFactory>(),
            setup: Setup.With(asResolutionCall: true));

        registrator.Register<ISchemaDataRowToSqlValueMetadataConverter, SchemaDataRowToSqlValueMetadataConverter>(Reuse.Singleton);
        registrator.RegisterRowsetMetadata();

        registrator.RegisterFillDataTable();
    }
}
