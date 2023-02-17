using Sqleze.DryIoc;
using Sqleze;
using Sqleze.ValueGetters;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sqleze.Params;

namespace Sqleze.Registration;

public static class ParamsRegistrationExtensions
{
    public static void RegisterParameterPreparation(this IRegistrator registrator)
    {
        registrator.Register<IParameterPreparation, ParameterPreparation>(
            Reuse.ScopedToService<ISqlezeCommandProvider>());

        // Standard parameter factory for normal value 
        registrator.Register(typeof(IAdoParameterFactory<>), typeof(AdoParameterFactory<>),
            Reuse.Scoped);

        // Use the TableType parameter builder for IEnumerables
        registrator.Register(typeof(IAdoParameterFactory<>), typeof(TableTypeAdoParameterFactory<,>),
            Reuse.Scoped,
            setup: Setup.With(condition: Condition.GenericArgIs(
                x => x.IsAssignableTo<IEnumerable>()
                    && x != typeof(string)
                    && x != typeof(byte[])
                    && x != typeof(byte?[])
                )));


        registrator.Register(typeof(IParameterSpecResolver<>), typeof(ParameterSpecResolver<>), Reuse.Scoped);
        registrator.Register(typeof(ITableTypeNameResolver<>), typeof(TableTypeNameResolver<>), Reuse.Scoped);

        registrator.RegisterValueSizeMeasurers();
        registrator.RegisterValuePrecisionScaleMeasurers();

        registrator.Register(typeof(ISizeQuantizer<>), typeof(SizeQuantizer<>), Reuse.Scoped);

        registrator.Register<NVarCharSizeQuantizeRoot>();
        registrator.Register<VarCharSizeQuantizeRoot>();
        registrator.Register<VarBinarySizeQuantizeRoot>();

        // Fallback empty options for size quantizer
        registrator.RegisterInstance(new NVarCharSizeQuantizeOptions(ImmutableArray.Create<int>()));
        registrator.RegisterInstance(new VarCharSizeQuantizeOptions(ImmutableArray.Create<int>()));
        registrator.RegisterInstance(new VarBinarySizeQuantizeOptions(ImmutableArray.Create<int>()));

        registrator.Register(typeof(IPrecisionScaleQuantizer<>), typeof(PrecisionScaleQuantizer<>), Reuse.Scoped);

        registrator.Register<PrecisionScaleQuantizeRoot>();
        registrator.RegisterInstance(new PrecisionScaleQuantizeOptions(ImmutableArray.Create<(byte precision, byte scale)>()));

        registrator.Register<DateTimeChoiceRoot>();
    }
}
