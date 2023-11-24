using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze;
public class Startup
{
    private readonly Container container;

    public ISqlezeRoot Root { get; init; }

    public Startup(Action<IStartupRegistration>? startupRegistration = null)
    {
        this.container = new Container();
        container.RegisterSqleze();

        if(startupRegistration != null)
        {
            startupRegistration(new StartupRegistration(container));
        }

        this.Root = container.Resolve<ISqlezeRoot>();
    }
}

public interface IStartupRegistration
{
    public IRegistrator Registrator { get; }
}

public record StartupRegistration
(
    IRegistrator Registrator
)
    : IStartupRegistration;

