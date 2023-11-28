using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sqleze.Readers;

namespace Sqleze.Registration;

public static class ReaderGetValueResolverRegistrationExtensions
{
    public static void RegisterReaderGetValueResolver(this IRegistrator registrator)
    {
        registrator.Register(typeof(IReaderGetValueResolver<,>), typeof(ReaderGetValueResolver<,>));
        registrator.RegisterGenericPromotion(typeof(IReaderGetValueResolver<>), typeof(IReaderGetValueResolver<,>));
    }
}
