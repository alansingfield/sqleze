using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sqleze;
using Sqleze.Composition;
using Sqleze.Util;
using Sqleze.Dynamics;
using System.Linq.Expressions;

namespace Sqleze
{
    public static partial class CoreExtensions
    {
        /// <summary>
        /// Open a SqlezeConnection; this must be disposed when finished with.
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static ISqlezeConnection Connect(this ISqlezeBuilder builder)
            => builder.Build().Connect();

        public static ISqlezeRowset<T> OpenRowsetNullable<T>(this ISqlezeRowsetBuilder builder)
            => builder.Build().OpenRowsetNullable<T>();

        public static ISqlezeRowset<T> OpenRowsetNullable<T>(this ISqlezeRowsetFactory factory)
            => factory.OpenRowsetNullable<T>();

        public static ISqlezeRowset<T> OpenRowset<T>(this ISqlezeRowsetBuilder sqlezeRowsetBuilder)
            where T : notnull
        {
            return sqlezeRowsetBuilder
                .WithScalarReaderFallbackPolicy(useDefaultInsteadOfNull: true)
                .OpenRowsetNullable<T>();
        }

        public static ISqlezeRowset<T> OpenRowset<T>(this ISqlezeReader sqlezeReader)
            where T : notnull
        {
            return sqlezeReader
                .WithScalarReaderFallbackPolicy(useDefaultInsteadOfNull: true)
                .Build()
                .OpenRowsetNullable<T>();
        }

        public static void Use<T>(this ISqlezeScope sqlezeScope, T? instance)
            => sqlezeScope.Use(typeof(T), instance);

        public static ISqlezeCommand Sql(this ISqlezeConnection sqlezeConnection, string sql)
            => sqlezeConnection.OpenCommand(sql, isStoredProc: false);

        public static ISqlezeCommand StoredProc(this ISqlezeConnection sqlezeConnection, string storedProcName)
            => sqlezeConnection.OpenCommand(storedProcName, isStoredProc: true);

        public static ISqlezeCommand Sql(this ISqlezeCommandBuilder sqlezeCommandBuilder, string sql)
            => sqlezeCommandBuilder.OpenCommand(sql, isStoredProc: false);

        public static ISqlezeCommand StoredProc(this ISqlezeCommandBuilder sqlezeCommandBuilder, string storedProcName)
            => sqlezeCommandBuilder.OpenCommand(storedProcName, isStoredProc: true);


        public static Task<ISqlezeReader> ExecuteReaderAsync(this ISqlezeCommand sqlezeCommand, CancellationToken cancellationToken)
            => sqlezeCommand.ExecuteReaderAsync(null, cancellationToken);

        public static Task<int> ExecuteNonQueryAsync(this ISqlezeCommand sqlezeCommand, CancellationToken cancellationToken)
            => sqlezeCommand.ExecuteNonQueryAsync(null, cancellationToken);
    }
}
