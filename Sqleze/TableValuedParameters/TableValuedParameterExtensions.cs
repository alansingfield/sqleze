using Sqleze;
using Sqleze.Params;
using Sqleze.TableValuedParameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze
{
    public static class TableValuedParameterExtensions
    {
        public static ISqlezeBuilder WithTableTypeFor<T>(
            this ISqlezeBuilder sqlezeConnectionBuilder,
            string tableTypeName)
        {
            return sqlezeConnectionBuilder.With<TableTypeRoot>(
                (root, scope) =>
                {
                    scope.Use<IParameterDefaultSqlTypeOptions<IEnumerable<T>>>(
                        new ParameterDefaultSqlTypeOptions<IEnumerable<T>>()
                        { SqlTypeName = tableTypeName, Mode = SqlezeParameterMode.TableType });

                    scope.Use<IParameterDefaultSqlTypeOptions<IList<T>>>(
                        new ParameterDefaultSqlTypeOptions<IList<T>>()
                        { SqlTypeName = tableTypeName, Mode = SqlezeParameterMode.TableType });

                    scope.Use<IParameterDefaultSqlTypeOptions<List<T>>>(
                        new ParameterDefaultSqlTypeOptions<List<T>>()
                        { SqlTypeName = tableTypeName, Mode = SqlezeParameterMode.TableType });

                    scope.Use<IParameterDefaultSqlTypeOptions<T[]>>(
                        new ParameterDefaultSqlTypeOptions<T[]>()
                        { SqlTypeName = tableTypeName, Mode = SqlezeParameterMode.TableType });
                });

        }
    }
}

namespace Sqleze.TableValuedParameters
{
    public class TableTypeRoot { }

    public class TableTypeRoot<T> : TableTypeRoot { } 

    public static class ValuedParameterExtensionsRegistration
    {
        public static void RegisterTableValuedParameterExtensions(this IRegistrator registrator)
        {
            registrator.Register(typeof(TableTypeRoot<>));
            registrator.Register<TableTypeRoot>();
        }
    }
}

