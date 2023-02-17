using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze.OutputParamReaders;

public static class OutputParamReaderRegistration
{
    public static void RegisterOutputParamReaders(this IRegistrator registrator)
    {
        registrator.Register(typeof(IOutputParamReader<>), typeof(OutputParamReader<>), Reuse.Singleton);

        registrator.Register<IOutputParamReader<SqlBinary>, OutputParamReaderSqlType<SqlBinary>>(Reuse.Singleton);
        registrator.Register<IOutputParamReader<SqlBinary?>, OutputParamReaderSqlTypeNullable<SqlBinary?>>(Reuse.Singleton);

        registrator.Register<IOutputParamReader<SqlBoolean>, OutputParamReaderSqlType<SqlBoolean>>(Reuse.Singleton);
        registrator.Register<IOutputParamReader<SqlBoolean?>, OutputParamReaderSqlTypeNullable<SqlBoolean?>>(Reuse.Singleton);

        registrator.Register<IOutputParamReader<SqlByte>, OutputParamReaderSqlType<SqlByte>>(Reuse.Singleton);
        registrator.Register<IOutputParamReader<SqlByte?>, OutputParamReaderSqlTypeNullable<SqlByte?>>(Reuse.Singleton);

        registrator.Register<IOutputParamReader<SqlBytes>, OutputParamReaderSqlType<SqlBytes>>(Reuse.Singleton);
//        registrator.Register<IOutputParamReader<SqlBytes?>, OutputParamReaderSqlTypeNullable<SqlBytes?>>(Reuse.Singleton);

        registrator.Register<IOutputParamReader<SqlChars>, OutputParamReaderSqlType<SqlChars>>(Reuse.Singleton);
//        registrator.Register<IOutputParamReader<SqlChars?>, OutputParamReaderSqlTypeNullable<SqlChars?>>(Reuse.Singleton);

        registrator.Register<IOutputParamReader<SqlDateTime>, OutputParamReaderSqlType<SqlDateTime>>(Reuse.Singleton);
        registrator.Register<IOutputParamReader<SqlDateTime?>, OutputParamReaderSqlTypeNullable<SqlDateTime?>>(Reuse.Singleton);

        registrator.Register<IOutputParamReader<SqlDouble>, OutputParamReaderSqlType<SqlDouble>>(Reuse.Singleton);
        registrator.Register<IOutputParamReader<SqlDouble?>, OutputParamReaderSqlTypeNullable<SqlDouble?>>(Reuse.Singleton);

        registrator.Register<IOutputParamReader<SqlGuid>, OutputParamReaderSqlType<SqlGuid>>(Reuse.Singleton);
        registrator.Register<IOutputParamReader<SqlGuid?>, OutputParamReaderSqlTypeNullable<SqlGuid?>>(Reuse.Singleton);

        registrator.Register<IOutputParamReader<SqlInt16>, OutputParamReaderSqlType<SqlInt16>>(Reuse.Singleton);
        registrator.Register<IOutputParamReader<SqlInt16?>, OutputParamReaderSqlTypeNullable<SqlInt16?>>(Reuse.Singleton);

        registrator.Register<IOutputParamReader<SqlInt32>, OutputParamReaderSqlType<SqlInt32>>(Reuse.Singleton);
        registrator.Register<IOutputParamReader<SqlInt32?>, OutputParamReaderSqlTypeNullable<SqlInt32?>>(Reuse.Singleton);

        registrator.Register<IOutputParamReader<SqlInt64>, OutputParamReaderSqlType<SqlInt64>>(Reuse.Singleton);
        registrator.Register<IOutputParamReader<SqlInt64?>, OutputParamReaderSqlTypeNullable<SqlInt64?>>(Reuse.Singleton);

        registrator.Register<IOutputParamReader<SqlMoney>, OutputParamReaderSqlType<SqlMoney>>(Reuse.Singleton);
        registrator.Register<IOutputParamReader<SqlMoney?>, OutputParamReaderSqlTypeNullable<SqlMoney?>>(Reuse.Singleton);

        registrator.Register<IOutputParamReader<SqlDecimal>, OutputParamReaderSqlType<SqlDecimal>>(Reuse.Singleton);
        registrator.Register<IOutputParamReader<SqlDecimal?>, OutputParamReaderSqlTypeNullable<SqlDecimal?>>(Reuse.Singleton);

        registrator.Register<IOutputParamReader<SqlSingle>, OutputParamReaderSqlType<SqlSingle>>(Reuse.Singleton);
        registrator.Register<IOutputParamReader<SqlSingle?>, OutputParamReaderSqlTypeNullable<SqlSingle?>>(Reuse.Singleton);

        registrator.Register<IOutputParamReader<SqlString>, OutputParamReaderSqlType<SqlString>>(Reuse.Singleton);
        registrator.Register<IOutputParamReader<SqlString?>, OutputParamReaderSqlTypeNullable<SqlString?>>(Reuse.Singleton);

        registrator.Register<IOutputParamReader<SqlXml>, OutputParamReaderSqlType<SqlXml>>(Reuse.Singleton);
        //registrator.Register<IOutputParamReader<SqlXml?>, OutputParamReaderSqlTypeNullable<SqlXml?>>(Reuse.Singleton);
    }
}
