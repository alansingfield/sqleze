using Sqleze.DryIoc;
using Sqleze.Registration;
using Sqleze.RowsetMetadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze.Registration;
public static class RowsetMetadataRegistrationExtensions
{
    public static void RegisterRowsetMetadata(this IRegistrator registrator)
    {
        // Default impl just throws if you call it.
        registrator.Register(typeof(IRowsetMetadataProvider<>), typeof(RowsetMetadataProviderDefault<>), 
            Reuse.Singleton);

        registrator.RegisterGenericPromotion<IRowsetMetadataProvider>(typeof(IRowsetMetadataProvider<>));

        // Specific versions to get metadata as -
        // just the field names
        // field names, types and ordinals
        // full schema including column precision scale etc.
        registrator.Register<IRowsetMetadataProvider<IEnumerable<string>>, RowsetMetadataProviderFieldNames>(Reuse.ScopedOrSingleton);
        registrator.Register<IRowsetMetadataProvider<IEnumerable<FieldType>>, RowsetMetadataProviderFieldTypes>(Reuse.ScopedOrSingleton);
        registrator.Register<IRowsetMetadataProvider<IEnumerable<FieldSchema>>, RowsetMetadataProviderFieldSchemas>(Reuse.ScopedOrSingleton);
    }
}
