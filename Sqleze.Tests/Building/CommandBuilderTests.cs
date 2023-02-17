using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using Sqleze.ConnectionStrings;
using Sqleze;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze.Tests.Building
{
    [TestClass]
    public class CommandBuilderTests
    {
        [TestMethod]
        public void CommandBuild1()
        {
            var container = new Container().WithNSubstituteFallback();

            container.RegisterSqleze();

            container.Register<CommandInterceptor>(
                Reuse.ScopedToService<IScopedSqlezeCommandBuilder<CommandInterceptor>>());


            var factory = container.Resolve<ISqlezeBuilder>();

            var connection = factory
                .WithConnectionString("Server=XYZ")
                .Build()
                .Connect();

            int testIdx = 0;
            var command = connection
                .With<CommandInterceptor>((interceptor, scope) =>
                {
                    interceptor.ConnectionStringProvider.GetConnectionString().ShouldBe("Server=XYZ");
                    interceptor.ConnectionStringProvider.ShouldBeOfType<VerbatimConnectionStringProvider>();

                    interceptor.SqlezeConnection.ShouldBe(connection);

                    testIdx++;
                })
                .OpenCommand("SELECT 0", false);

            command.ShouldNotBeNull();

            testIdx.ShouldBe(1);
        }


        [TestMethod]
        public void CommandBuild2()
        {
            var container = new Container().WithNSubstituteFallback();

            container.RegisterSqleze();

            container.Register<CommandInterceptor>(
                Reuse.ScopedToService<IScopedSqlezeCommandBuilder<CommandInterceptor>>());


            var connectionBuilder = container.Resolve<ISqlezeBuilder>();

            var connection = connectionBuilder
                .WithConnectionString("Server=XYZ")
                .Build()
                .Connect();

            int testIdx = 0;
            var commandBuilder = connection
                .With<CommandInterceptor>((interceptor, scope) =>
                {
                    interceptor.ConnectionStringProvider.GetConnectionString().ShouldBe("Server=XYZ");
                    interceptor.ConnectionStringProvider.ShouldBeOfType<VerbatimConnectionStringProvider>();

                    interceptor.SqlezeConnection.ShouldBe(connection);

                    testIdx++;
                })
                .WithCommandText("SELECT 0")
                .Build();

            testIdx.ShouldBe(1);

            var command1 = commandBuilder.OpenCommand();
            command1.ShouldNotBeNull();

            var command2 = commandBuilder.OpenCommand();
            command2.ShouldNotBeNull();

            testIdx.ShouldBe(1);

            command2.ShouldNotBe(command1);
        }



        public class CommandInterceptor
        {
            public CommandInterceptor(IConnectionStringProvider connectionStringProvider,
                ISqlezeConnection sqlezeConnection)
            {
                ConnectionStringProvider = connectionStringProvider;
                SqlezeConnection = sqlezeConnection;
            }

            public IConnectionStringProvider ConnectionStringProvider { get; }
            public ISqlezeConnection SqlezeConnection { get; }
        }
        public class ConnectionInterceptor
        {
            public ConnectionInterceptor(IConnectionStringProvider connectionStringProvider,
                ISqlezeConnection sqlezeConnection)
            {
                ConnectionStringProvider = connectionStringProvider;
                SqlezeConnection = sqlezeConnection;
            }

            public IConnectionStringProvider ConnectionStringProvider { get; }
            public ISqlezeConnection SqlezeConnection { get; }
        }
    }
}
