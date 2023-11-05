using Microsoft.SqlServer.Types;
using Sqleze.SpatialTypes.ValueGetters;
using Sqleze.ValueGetters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sqleze.Params;
using Sqleze;
using System.Data.SqlTypes;
using Sqleze.Readers;

namespace Sqleze.Registration;

public static class SpatialTypeRegistrationExtensions
{

    public static void RegisterSpatialTypes(this IRegistrator registrator)
    {
        registrator.RegisterSpatialSqlDbTypeResolver();
        registrator.RegisterSpatialTypeValueGetters();
        registrator.RegisterSpatialTypeParameterDefaults();
        registrator.RegisterSpatialReaders();
    }

    public static void RegisterSpatialSqlDbTypeResolver(this IRegistrator registrator)
    {
        registrator.Register<IKnownSqlDbTypeResolver, SpatialSqlDbTypeResolver>(
            Reuse.Singleton,
            ifAlreadyRegistered: IfAlreadyRegistered.AppendNotKeyed);
    }

    public static void RegisterSpatialTypeValueGetters(this IRegistrator registrator)
    {
        registrator.Register<IReaderGetValue<SqlGeometry>, ReaderGetValueSqlGeometry>(Reuse.Singleton);
        registrator.Register<IReaderGetValue<SqlGeography>, ReaderGetValueSqlGeography>(Reuse.Singleton);
        registrator.Register<IReaderGetValue<SqlHierarchyId>, ReaderGetValueSqlHierarchyId>(Reuse.Singleton);
        registrator.Register<IReaderGetValue<SqlHierarchyId?>, ReaderGetValueSqlHierarchyIdNullable>(Reuse.Singleton);


        registrator.Register<IReaderGetValue<byte[]?, IKnownSqlDbTypeGeography>, ReaderGetValueSqlGeographyToByteArrayNullable>(Reuse.Singleton);
        registrator.Register<IReaderGetValue<byte[], IKnownSqlDbTypeGeography>, ReaderGetValueSqlGeographyToByteArray>(Reuse.Singleton);
        registrator.Register<IReaderGetValue<string?, IKnownSqlDbTypeGeography>, ReaderGetValueSqlGeographyToStringNullable>(Reuse.Singleton);
        registrator.Register<IReaderGetValue<string, IKnownSqlDbTypeGeography>, ReaderGetValueSqlGeographyToString>(Reuse.Singleton);
        registrator.Register<IReaderGetValue<object?, IKnownSqlDbTypeGeography>, ReaderGetValueSqlGeographyToObjectNullable>(Reuse.Singleton);
        registrator.Register<IReaderGetValue<object, IKnownSqlDbTypeGeography>, ReaderGetValueSqlGeographyToObject>(Reuse.Singleton);

        registrator.Register<IReaderGetValue<byte[]?, IKnownSqlDbTypeGeometry>, ReaderGetValueSqlGeometryToByteArrayNullable>(Reuse.Singleton);
        registrator.Register<IReaderGetValue<byte[], IKnownSqlDbTypeGeometry>, ReaderGetValueSqlGeometryToByteArray>(Reuse.Singleton);
        registrator.Register<IReaderGetValue<string?, IKnownSqlDbTypeGeometry>, ReaderGetValueSqlGeometryToStringNullable>(Reuse.Singleton);
        registrator.Register<IReaderGetValue<string, IKnownSqlDbTypeGeometry>, ReaderGetValueSqlGeometryToString>(Reuse.Singleton);
        registrator.Register<IReaderGetValue<object?, IKnownSqlDbTypeGeometry>, ReaderGetValueSqlGeometryToObjectNullable>(Reuse.Singleton);
        registrator.Register<IReaderGetValue<object, IKnownSqlDbTypeGeometry>, ReaderGetValueSqlGeometryToObject>(Reuse.Singleton);

        registrator.Register<IReaderGetValue<byte[]?, IKnownSqlDbTypeHierarchyId>, ReaderGetValueSqlHierarchyIdToByteArrayNullable>(Reuse.Singleton);
        registrator.Register<IReaderGetValue<byte[], IKnownSqlDbTypeHierarchyId>, ReaderGetValueSqlHierarchyIdToByteArray>(Reuse.Singleton);
        registrator.Register<IReaderGetValue<string?, IKnownSqlDbTypeHierarchyId>, ReaderGetValueSqlHierarchyIdToStringNullable>(Reuse.Singleton);
        registrator.Register<IReaderGetValue<string, IKnownSqlDbTypeHierarchyId>, ReaderGetValueSqlHierarchyIdToString>(Reuse.Singleton);
        registrator.Register<IReaderGetValue<object?, IKnownSqlDbTypeHierarchyId>, ReaderGetValueSqlHierarchyIdToObjectNullable>(Reuse.Singleton);
        registrator.Register<IReaderGetValue<object, IKnownSqlDbTypeHierarchyId>, ReaderGetValueSqlHierarchyIdToObject>(Reuse.Singleton);
    }

    public static void RegisterSpatialTypeParameterDefaults(this IRegistrator registrator)
    {
        registrator.RegisterInstance<IParameterDefaultSqlTypeOptions<SqlGeometry>>(
            new ParameterDefaultSqlTypeOptions<SqlGeometry>()
            { SqlTypeName = "geometry", Mode = SqlezeParameterMode.AssemblyType });

        registrator.RegisterInstance<IParameterDefaultSqlTypeOptions<SqlGeography>>(
            new ParameterDefaultSqlTypeOptions<SqlGeography>()
            { SqlTypeName = "geography", Mode = SqlezeParameterMode.AssemblyType });

        registrator.RegisterInstance<IParameterDefaultSqlTypeOptions<SqlHierarchyId>>(
            new ParameterDefaultSqlTypeOptions<SqlHierarchyId>()
            { SqlTypeName = "hierarchyid", Mode = SqlezeParameterMode.AssemblyType });

        registrator.RegisterInstance<IParameterDefaultSqlTypeOptions<SqlHierarchyId?>>(
            new ParameterDefaultSqlTypeOptions<SqlHierarchyId?>()
            { SqlTypeName = "hierarchyid", Mode = SqlezeParameterMode.AssemblyType });
    }

    public static void RegisterSpatialReaders(this IRegistrator registrator)
    {
        registrator.Register<IReader<SqlGeography?>, ScalarReader<SqlGeography?>>();
        registrator.Register<IReader<SqlGeometry?>, ScalarReader<SqlGeometry?>>();
        registrator.Register<IReader<SqlHierarchyId>, ScalarReader<SqlHierarchyId>>();
        registrator.Register<IReader<SqlHierarchyId?>, ScalarReader<SqlHierarchyId?>>();

        //registrator.Register<IReader<SqlGeography>, ScalarReader<SqlGeography>>();
        //registrator.Register<IReader<SqlGeometry>, ScalarReader<SqlGeometry>>();
    }
}
