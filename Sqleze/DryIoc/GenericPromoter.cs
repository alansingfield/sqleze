
namespace Sqleze.DryIoc;

public interface IGenericPromoter
{
    Func<Type, Type, Type> Promote { get; }
}

public class GenericPromoter : IGenericPromoter
{
    public GenericPromoter(Func<Type, Type, Type> promote)
    {
        this.Promote = promote;
    }

    public Func<Type, Type, Type> Promote { get; }

    public static IGenericPromoter For(Func<Type, Type, Type> func)
        => new GenericPromoter(func);

    public static IGenericPromoter For(Type biggerOpenGeneric, int? argPosition = null)
        => new GenericPromoter((smallerType, extraTypeParameter) =>
        {
            var genericArgs = new List<Type>();

            if(smallerType.IsGenericType)
            {
                genericArgs.AddRange(smallerType.GetGenericArguments());
            }

            // Calculate where to insert the final missing type. Normally will be at
            // the end.
            int pos = argPosition ?? genericArgs.Count;

            genericArgs.Insert(pos, extraTypeParameter);

            var biggerClosedType = biggerOpenGeneric.MakeGenericType(genericArgs.ToArray());

            return biggerClosedType;
        });
}
