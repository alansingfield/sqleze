
using Sqleze.DryIoc;

namespace Sqleze.Registration;

#if DRYIOC_DLL
public
#else
internal
#endif
static class GenericPromoterExtensions
{
    /// <summary>
    /// Configures the container so that a smaller service interface can be implemented
    /// by a class of closed generic type with a final type specified at runtime.
    /// </summary>
    /// <param name="registrator">DryIoc container</param>
    /// <param name="smallerType">typeof(IMyService&lt;&gt;)</param>
    /// <param name="biggerOpenGeneric">typeof(IMyService&lt;,&gt;)</param>
    /// <param name="argPosition">Which position to put the extra type into.</param>
    /// <param name="trialType">Where there is a constraint, need to specify an extra type to try on registration</param>
    public static void RegisterGenericPromotion(
        this IRegistrator registrator,
        Type smallerType,
        Type biggerOpenGeneric,
        int? argPosition = null,
        Type? trialType = null)
    {
        var promoter = GenericPromoter.For(biggerOpenGeneric, argPosition);

        // Verify promotion will work.
        promoter.Promote(smallerType, trialType ?? typeof(object));

        registrator.RegisterInstance(promoter, serviceKey: smallerType);
    }

    public static void RegisterGenericPromotion<TSmallerType>(
        this IRegistrator registrator,
        Type biggerOpenGeneric) => registrator.RegisterGenericPromotion(typeof(TSmallerType), biggerOpenGeneric);

}