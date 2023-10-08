
namespace Sqleze.DryIoc;

#if DRYIOC_DLL
public
#else
internal
#endif
static class Condition
{
    /// <summary>
    /// For use with Setup.With(condition: Condition.ScopedToService&lt;TScopeService&gt;)
    /// Returns true if the request is scoped to the supplied service, or if any parent
    /// scope is scoped to the supplied service. Used to choose an alternate implementation
    /// of a service when a particular scope is reached.
    /// </summary>
    /// <typeparam name="TScopeService"></typeparam>
    /// <returns></returns>

    public static Func<Request, bool> ScopedToService<TScopeService>() => 
    request =>
    {
        var scope = request.CurrentScope;

        while(scope != null)
        {
            var resolutionScopeName = scope.Name as ResolutionScopeName;
            var scopeType = resolutionScopeName?.ServiceType;

            if(scopeType == typeof(TScopeService))
            {
                return true;
            }

            scope = scope.Parent;
        }

        return false;
    };


    public static Func<Request, bool> ScopedToGenericArg<TSpecific, TCategory>(int argPosition = 0)
    where TSpecific : TCategory => 
    request =>
    {
        var scope = request.CurrentScope;

        while(scope != null)
        {
            var resolutionScopeName = scope.Name as ResolutionScopeName;
            var scopeType = resolutionScopeName?.ServiceType;

            // Here we want to accept, reject or look to parent scope based on the first generic argument.
            // e.g.
            // Scope is: ISqlezeConnection<NeutralNamingConventionRoot>
            // NeutralNamingConventionRoot inherits from NamingConventionCategory
            // CamelNamingConventionRoot also inherits from NamingConventionCategory
            //
            // If scope is ISomethingElse - loop round and look at parent scope.
            // If scope is ISqlezeConnection<NeutralNamingConventionRoot> - return true,
            //      (condition matches so use this implementation)
            // If scope is ISqlezeConnection<CamelNamingConventionRoot> - return false
            //      (condition matches the category, but it is not us, so don't use this implementation)
            if(scopeType != null && scopeType.IsClosedGeneric())
            {
                // Get the generic argument at our desired position (usually 0)
                // e.g. IMyScope<MyCategory>
                var genArg = scopeType.GetGenericArguments().Skip(argPosition).FirstOrDefault();

                if(genArg.IsAssignableTo<TCategory>())
                {
                    // Return true if we've matched the right scope.
                    // Return false (don't look at parents) if we are in the right category
                    // but it's the wrong one.
                    return genArg.IsAssignableTo<TSpecific>();
                }
            }

            scope = scope.Parent;
        }

        return false;
    };

    public static Func<Request, bool> ScopedToGenericArg<TSpecific>(int argPosition = 0)
        => ScopedToGenericArg<TSpecific, TSpecific>(argPosition);

    public static Func<Request, bool> GenericArgOf<T>(int argPosition = 0) =>
        GenericArgOf(typeof(T), argPosition);
    
    public static Func<Request, bool> GenericArgOf(Type type, int argPosition = 0) =>
    request =>
    {
        var serviceType = request.ServiceType;

        if(!serviceType.IsClosedGeneric())
            return false;

        var genArg = serviceType.GetGenericArguments().Skip(argPosition).FirstOrDefault();

        return genArg?.IsAssignableTo(type) ?? false;
    };

    public static Func<Request, bool> GenericArgIs(Func<Type, bool> argCondition, int argPosition = 0) =>
        request =>
        {
            var serviceType = request.ServiceType;

            if(!serviceType.IsClosedGeneric())
                return false;

            var genArg = serviceType.GetGenericArguments().Skip(argPosition).FirstOrDefault();

            if(genArg == null)
                return false;

            return argCondition(genArg);
        };
}