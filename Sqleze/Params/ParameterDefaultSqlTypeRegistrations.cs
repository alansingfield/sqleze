using System.Data.SqlTypes;
using System.Collections;
using Sqleze;
using Sqleze.DryIoc;

namespace Sqleze.Params
{
    public static class ParameterDefaultSqlTypeRegistrations
    {
        public static void RegisterParameterDefaultSqlTypes(this IRegistrator registrator)
        {
            registrator.RegisterParameterStandardTypes();
            registrator.RegisterEnumerableAsTableType();
        }

        public static void RegisterParameterStandardTypes(this IRegistrator registrator)
        {
            registrator.RegisterInstance<IParameterDefaultSqlTypeOptions<object>>(
                new ParameterDefaultSqlTypeOptions<object>()
                { SqlTypeName = "nvarchar" });

            registrator.RegisterInstance<IParameterDefaultSqlTypeOptions<string>>(
                new ParameterDefaultSqlTypeOptions<string>() 
                { SqlTypeName = "nvarchar" });

            registrator.RegisterInstance<IParameterDefaultSqlTypeOptions<int>>(
                new ParameterDefaultSqlTypeOptions<int>()
                { SqlTypeName = "int" });

            registrator.RegisterInstance<IParameterDefaultSqlTypeOptions<int?>>(
                new ParameterDefaultSqlTypeOptions<int?>()
                { SqlTypeName = "int" });

            registrator.RegisterInstance<IParameterDefaultSqlTypeOptions<byte[]>>(
                new ParameterDefaultSqlTypeOptions<byte[]>()
                { SqlTypeName = "varbinary" });
            
            registrator.RegisterInstance<IParameterDefaultSqlTypeOptions<byte?[]>>(
                new ParameterDefaultSqlTypeOptions<byte?[]>()
                { SqlTypeName = "varbinary" });

            registrator.RegisterInstance<IParameterDefaultSqlTypeOptions<bool>>(
                new ParameterDefaultSqlTypeOptions<bool>()
                { SqlTypeName = "bit" });

            registrator.RegisterInstance<IParameterDefaultSqlTypeOptions<bool?>>(
                new ParameterDefaultSqlTypeOptions<bool?>()
                { SqlTypeName = "bit" });

            registrator.RegisterInstance<IParameterDefaultSqlTypeOptions<byte>>(
                new ParameterDefaultSqlTypeOptions<byte>()
                { SqlTypeName = "tinyint" });

            registrator.RegisterInstance<IParameterDefaultSqlTypeOptions<byte?>>(
                new ParameterDefaultSqlTypeOptions<byte?>()
                { SqlTypeName = "tinyint" });

            registrator.RegisterInstance<IParameterDefaultSqlTypeOptions<short>>(
                new ParameterDefaultSqlTypeOptions<short>()
                { SqlTypeName = "smallint" });

            registrator.RegisterInstance<IParameterDefaultSqlTypeOptions<short?>>(
                new ParameterDefaultSqlTypeOptions<short?>()
                { SqlTypeName = "smallint" });

            registrator.RegisterInstance<IParameterDefaultSqlTypeOptions<long>>(
                new ParameterDefaultSqlTypeOptions<long>()
                { SqlTypeName = "bigint" });

            registrator.RegisterInstance<IParameterDefaultSqlTypeOptions<long?>>(
                new ParameterDefaultSqlTypeOptions<long?>()
                { SqlTypeName = "bigint" });

            registrator.RegisterInstance<IParameterDefaultSqlTypeOptions<float>>(
                new ParameterDefaultSqlTypeOptions<float>()
                { SqlTypeName = "real" });

            registrator.RegisterInstance<IParameterDefaultSqlTypeOptions<float?>>(
                new ParameterDefaultSqlTypeOptions<float?>()
                { SqlTypeName = "real" });

            registrator.RegisterInstance<IParameterDefaultSqlTypeOptions<double>>(
                new ParameterDefaultSqlTypeOptions<double>()
                { SqlTypeName = "float" });

            registrator.RegisterInstance<IParameterDefaultSqlTypeOptions<double?>>(
                new ParameterDefaultSqlTypeOptions<double?>()
                { SqlTypeName = "float" });

            registrator.RegisterInstance<IParameterDefaultSqlTypeOptions<decimal>>(
                new ParameterDefaultSqlTypeOptions<decimal>()
                { SqlTypeName = "decimal" });

            registrator.RegisterInstance<IParameterDefaultSqlTypeOptions<decimal?>>(
                new ParameterDefaultSqlTypeOptions<decimal?>()
                { SqlTypeName = "decimal" });

            registrator.RegisterInstance<IParameterDefaultSqlTypeOptions<DateTime>>(
                new ParameterDefaultSqlTypeOptions<DateTime>()
                { SqlTypeName = "datetime2", Scale = 7 });

            registrator.RegisterInstance<IParameterDefaultSqlTypeOptions<DateTime?>>(
                new ParameterDefaultSqlTypeOptions<DateTime?>()
                { SqlTypeName = "datetime2", Scale = 7 });

            registrator.RegisterInstance<IParameterDefaultSqlTypeOptions<DateTimeOffset>>(
                new ParameterDefaultSqlTypeOptions<DateTimeOffset>()
                { SqlTypeName = "datetimeoffset", Scale = 7 });

            registrator.RegisterInstance<IParameterDefaultSqlTypeOptions<DateTimeOffset?>>(
                new ParameterDefaultSqlTypeOptions<DateTimeOffset?>()
                { SqlTypeName = "datetimeoffset", Scale = 7 });

            registrator.RegisterInstance<IParameterDefaultSqlTypeOptions<TimeSpan>>(
                new ParameterDefaultSqlTypeOptions<TimeSpan>()
                { SqlTypeName = "time", Scale = 7 });

            registrator.RegisterInstance<IParameterDefaultSqlTypeOptions<TimeSpan?>>(
                new ParameterDefaultSqlTypeOptions<TimeSpan?>()
                { SqlTypeName = "time", Scale = 7 });

            registrator.RegisterInstance<IParameterDefaultSqlTypeOptions<TimeOnly>>(
                new ParameterDefaultSqlTypeOptions<TimeOnly>()
                { SqlTypeName = "time", Scale = 7 });

            registrator.RegisterInstance<IParameterDefaultSqlTypeOptions<TimeOnly?>>(
                new ParameterDefaultSqlTypeOptions<TimeOnly?>()
                { SqlTypeName = "time", Scale = 7 });

            registrator.RegisterInstance<IParameterDefaultSqlTypeOptions<DateOnly>>(
                new ParameterDefaultSqlTypeOptions<DateOnly>()
                { SqlTypeName = "date" });

            registrator.RegisterInstance<IParameterDefaultSqlTypeOptions<DateOnly?>>(
                new ParameterDefaultSqlTypeOptions<DateOnly?>()
                { SqlTypeName = "date" });

            registrator.RegisterInstance<IParameterDefaultSqlTypeOptions<Guid>>(
                new ParameterDefaultSqlTypeOptions<Guid>()
                { SqlTypeName = "uniqueidentifier" });

            registrator.RegisterInstance<IParameterDefaultSqlTypeOptions<Guid?>>(
                new ParameterDefaultSqlTypeOptions<Guid?>()
                { SqlTypeName = "uniqueidentifier" });

            registrator.RegisterInstance<IParameterDefaultSqlTypeOptions<SqlBinary>>(
                new ParameterDefaultSqlTypeOptions<SqlBinary>()
                { SqlTypeName = "varbinary" });

            registrator.RegisterInstance<IParameterDefaultSqlTypeOptions<SqlBinary?>>(
                new ParameterDefaultSqlTypeOptions<SqlBinary?>()
                { SqlTypeName = "varbinary" });

            registrator.RegisterInstance<IParameterDefaultSqlTypeOptions<SqlBoolean>>(
                new ParameterDefaultSqlTypeOptions<SqlBoolean>()
                { SqlTypeName = "bit" });

            registrator.RegisterInstance<IParameterDefaultSqlTypeOptions<SqlBoolean?>>(
                new ParameterDefaultSqlTypeOptions<SqlBoolean?>()
                { SqlTypeName = "bit" });

            registrator.RegisterInstance<IParameterDefaultSqlTypeOptions<SqlByte>>(
                new ParameterDefaultSqlTypeOptions<SqlByte>()
                { SqlTypeName = "tinyint" });

            registrator.RegisterInstance<IParameterDefaultSqlTypeOptions<SqlByte?>>(
                new ParameterDefaultSqlTypeOptions<SqlByte?>()
                { SqlTypeName = "tinyint" });

            registrator.RegisterInstance<IParameterDefaultSqlTypeOptions<SqlBytes>>(
                new ParameterDefaultSqlTypeOptions<SqlBytes>()
                { SqlTypeName = "varbinary" });

            // SqlBytes is a class so no separate nullable version
            //registrator.RegisterInstance<IParameterDefaultSqlTypeOptions<SqlBytes?>>(
            //    new ParameterDefaultSqlTypeOptions<SqlBytes?>()
            //    { SqlTypeName = "varbinary" });

            //registrator.RegisterInstance<IParameterDefaultSqlTypeOptions<SqlChars>>(
            //    new ParameterDefaultSqlTypeOptions<SqlChars>()
            //    { SqlTypeName = "xxxx" });

            //registrator.RegisterInstance<IParameterDefaultSqlTypeOptions<SqlChars?>>(
            //    new ParameterDefaultSqlTypeOptions<SqlChars?>()
            //    { SqlTypeName = "xxxx" });

            registrator.RegisterInstance<IParameterDefaultSqlTypeOptions<SqlDateTime>>(
                new ParameterDefaultSqlTypeOptions<SqlDateTime>()
                { SqlTypeName = "datetime" });

            registrator.RegisterInstance<IParameterDefaultSqlTypeOptions<SqlDateTime?>>(
                new ParameterDefaultSqlTypeOptions<SqlDateTime?>()
                { SqlTypeName = "datetime" });

            registrator.RegisterInstance<IParameterDefaultSqlTypeOptions<SqlDouble>>(
                new ParameterDefaultSqlTypeOptions<SqlDouble>()
                { SqlTypeName = "float" });

            registrator.RegisterInstance<IParameterDefaultSqlTypeOptions<SqlDouble?>>(
                new ParameterDefaultSqlTypeOptions<SqlDouble?>()
                { SqlTypeName = "float" });

            registrator.RegisterInstance<IParameterDefaultSqlTypeOptions<SqlGuid>>(
                new ParameterDefaultSqlTypeOptions<SqlGuid>()
                { SqlTypeName = "uniqueidentifier" });

            registrator.RegisterInstance<IParameterDefaultSqlTypeOptions<SqlGuid?>>(
                new ParameterDefaultSqlTypeOptions<SqlGuid?>()
                { SqlTypeName = "uniqueidentifier" });

            registrator.RegisterInstance<IParameterDefaultSqlTypeOptions<SqlInt16>>(
                new ParameterDefaultSqlTypeOptions<SqlInt16>()
                { SqlTypeName = "smallint" });

            registrator.RegisterInstance<IParameterDefaultSqlTypeOptions<SqlInt16?>>(
                new ParameterDefaultSqlTypeOptions<SqlInt16?>()
                { SqlTypeName = "smallint" });

            registrator.RegisterInstance<IParameterDefaultSqlTypeOptions<SqlInt32>>(
                new ParameterDefaultSqlTypeOptions<SqlInt32>()
                { SqlTypeName = "int" });

            registrator.RegisterInstance<IParameterDefaultSqlTypeOptions<SqlInt32?>>(
                new ParameterDefaultSqlTypeOptions<SqlInt32?>()
                { SqlTypeName = "int" });

            registrator.RegisterInstance<IParameterDefaultSqlTypeOptions<SqlInt64>>(
                new ParameterDefaultSqlTypeOptions<SqlInt64>()
                { SqlTypeName = "bigint" });

            registrator.RegisterInstance<IParameterDefaultSqlTypeOptions<SqlInt64?>>(
                new ParameterDefaultSqlTypeOptions<SqlInt64?>()
                { SqlTypeName = "bigint" });

            registrator.RegisterInstance<IParameterDefaultSqlTypeOptions<SqlMoney>>(
                new ParameterDefaultSqlTypeOptions<SqlMoney>()
                { SqlTypeName = "money" });

            registrator.RegisterInstance<IParameterDefaultSqlTypeOptions<SqlMoney?>>(
                new ParameterDefaultSqlTypeOptions<SqlMoney?>()
                { SqlTypeName = "money" });

            registrator.RegisterInstance<IParameterDefaultSqlTypeOptions<SqlDecimal>>(
                new ParameterDefaultSqlTypeOptions<SqlDecimal>()
                { SqlTypeName = "decimal" });

            registrator.RegisterInstance<IParameterDefaultSqlTypeOptions<SqlDecimal?>>(
                new ParameterDefaultSqlTypeOptions<SqlDecimal?>()
                { SqlTypeName = "decimal" });

            registrator.RegisterInstance<IParameterDefaultSqlTypeOptions<SqlSingle>>(
                new ParameterDefaultSqlTypeOptions<SqlSingle>()
                { SqlTypeName = "real" });

            registrator.RegisterInstance<IParameterDefaultSqlTypeOptions<SqlSingle?>>(
                new ParameterDefaultSqlTypeOptions<SqlSingle?>()
                { SqlTypeName = "real" });

            registrator.RegisterInstance<IParameterDefaultSqlTypeOptions<SqlString>>(
                new ParameterDefaultSqlTypeOptions<SqlString>()
                { SqlTypeName = "nvarchar" });

            registrator.RegisterInstance<IParameterDefaultSqlTypeOptions<SqlString?>>(
                new ParameterDefaultSqlTypeOptions<SqlString?>()
                { SqlTypeName = "nvarchar" });

            registrator.RegisterInstance<IParameterDefaultSqlTypeOptions<SqlXml>>(
                new ParameterDefaultSqlTypeOptions<SqlXml>()
                { SqlTypeName = "xml" });

            // SqlXml is a class so no separate nullable version
            //registrator.RegisterInstance<IParameterDefaultSqlTypeOptions<SqlXml?>>(
            //    new ParameterDefaultSqlTypeOptions<SqlXml?>()
            //    { SqlTypeName = "xml" });
        }

        public static void RegisterEnumerableAsTableType(this IRegistrator registrator)
        {
            // If an IEnumerable is passed in and we don't have any other match, it must be a table type
            // parameter.
            registrator.Register(typeof(IParameterDefaultSqlTypeOptions<>),
                typeof(ParameterDefaultSqlTypeOptionsTableType<>),
                setup: Setup.With(condition: 
                    Condition.GenericArgOf<IEnumerable>()));
        }
    }
    
    

//    public class ParamDefaultSqlTypeNVarChar : IParamDefaultSqlType
//    {
//        public ParameterDefaultSqlTypeOptions GetDefaultSqlTypeOptions() => new() { SqlTypeName = "nvarchar" };
//    }

//    public class ParamDefaultSqlTypeChar : IParamDefaultSqlType
//    {
//        public ParameterDefaultSqlTypeOptions GetDefaultSqlTypeOptions() => new() { Length = 1 };
//    }

//    public class ParamDefaultSqlTypeByteArray : IParamDefaultSqlType<byte[]>
//    {
//        public ParameterDefaultSqlTypeOptions GetDefaultSqlTypeOptions() => new() { SqlTypeName = "varbinary", Length = -1 };
//    }

//    public class ParamDefaultSqlTypeGuid : IParamDefaultSqlType
//    {
//        public ParameterDefaultSqlTypeOptions GetDefaultSqlTypeOptions() => new() { SqlTypeName = "uniqueidentifier" };
//    }

//    public class ValueTypeToSqlTypeConverter<T>
//    {
//        public string Convert()
//        {
//            // Specfics for IEnumerable<T> excluding byte[] etc.
//            // Add registrations for IEnumerable<string> to tt_int_vals.

//            switch(typeof(T))
//            {
//                case typeof(char):
//                case typeof(char[]):
//                    return "nvarchar";
//                default:
//                    switch(Type.GetTypeCode(typeof(T)))
//                    {
//                        case TypeCode.Empty: throw ADP.InvalidDataType(TypeCode.Empty);
//                        case TypeCode.Object:
//                            if(dataType == typeof(System.Byte[]))
//                            {
//                                // mdac 90455 must not default to image if inferLen is false ...
//                                //
//                                if(!inferLen || ((byte[])value).Length <= TdsEnums.TYPE_SIZE_LIMIT)
//                                {
//                                    return MetaVarBinary;
//                                }
//                                else
//                                {
//                                    return MetaImage;
//                                }
//                            }
//                            else if(dataType == typeof(System.Guid))
//                            {
//                                return "uniqueidentifier";
//                            }
//                            else if(dataType == typeof(System.Object))
//                            {
//                                return MetaVariant;
//                            } // check sql types now
//                            else if(dataType == typeof(SqlBinary))
//                                return "varbinary";
//                            else if(dataType == typeof(SqlBoolean))
//                                return "bit";
//                            else if(dataType == typeof(SqlByte))
//                                return "tinyint";
//                            else if(dataType == typeof(SqlBytes))
//                                return "varbinary";
//                            else if(dataType == typeof(SqlChars))
//                                return "nvarchar";
//                            else if(dataType == typeof(SqlDateTime))
//                                return "datetime";  // NOT a datetime2 since SqlDateTime corresponds to datetime
//                            else if(dataType == typeof(SqlDouble))
//                                return "float";
//                            else if(dataType == typeof(SqlGuid))
//                                return "uniqueidentifier";
//                            else if(dataType == typeof(SqlInt16))
//                                return "smallint";
//                            else if(dataType == typeof(SqlInt32))
//                                return "int";
//                            else if(dataType == typeof(SqlInt64))
//                                return "bigint";
//                            else if(dataType == typeof(SqlMoney))
//                                return "money";
//                            else if(dataType == typeof(SqlDecimal))
//                                return "decimal";
//                            else if(dataType == typeof(SqlSingle))
//                                return "real";
//                            else if(dataType == typeof(SqlXml))
//                                return "xml";
//                            else if(dataType == typeof(SqlString))
//                            {
//                                return "nvarchar";
//                            }
//                            else if(dataType == typeof(IEnumerable<DbDataRecord>) || dataType == typeof(DataTable))
//                            {
//                                return MetaTable;
//                            }
//                            else if(dataType == typeof(TimeSpan))
//                            {
//                                return "time";
//                            }
//                            else if(dataType == typeof(DateTimeOffset))
//                            {
//                                return "datetimeoffset";
//                            }
//                            else
//                            {
//                                // UDT ?
//                                SqlUdtInfo attribs = SqlUdtInfo.TryGetFromType(dataType);
//                                if(attribs != null)
//                                {
//                                    return MetaUdt;
//                                }
//                                if(streamAllowed)
//                                {
//                                    // Derived from Stream ?
//                                    if(typeof(Stream).IsAssignableFrom(dataType))
//                                    {
//                                        return MetaVarBinary;
//                                    }
//                                    // Derived from TextReader ?
//                                    if(typeof(TextReader).IsAssignableFrom(dataType))
//                                    {
//                                        return MetaNVarChar;
//                                    }
//                                    // Derived from XmlReader ? 
//                                    if(typeof(System.Xml.XmlReader).IsAssignableFrom(dataType))
//                                    {
//                                        return MetaXml;
//                                    }
//                                }
//                            }
//                            throw ADP.UnknownDataType(dataType);

//                        case TypeCode.DBNull: throw ADP.InvalidDataType(TypeCode.DBNull);
//                        case TypeCode.Boolean: return "bit";
////                        case TypeCode.Char: throw ADP.InvalidDataType(TypeCode.Char);
////                        case TypeCode.SByte: throw ADP.InvalidDataType(TypeCode.SByte);
//                        case TypeCode.Byte: return "tinyint";
//                        case TypeCode.Int16: return "smallint";
////                        case TypeCode.UInt16: throw ADP.InvalidDataType(TypeCode.UInt16);
//                        case TypeCode.Int32: return "int";
////                        case TypeCode.UInt32: throw ADP.InvalidDataType(TypeCode.UInt32);
//                        case TypeCode.Int64: return "bigint";
////                        case TypeCode.UInt64: throw ADP.InvalidDataType(TypeCode.UInt64);
//                        case TypeCode.Single: return "real";
//                        case TypeCode.Double: return "double";
//                        case TypeCode.Decimal: return "decimal";
//                        case TypeCode.DateTime: return "datetime2";     // MS uses datetime not datetime2 
//                        case TypeCode.String: return "nvarchar";        // MS uses varchar or nvarchar depending on length
//                        default: throw ADP.UnknownDataTypeCode(dataType, Type.GetTypeCode(dataType));
//                    }
//            }
//        }
    //}
}
