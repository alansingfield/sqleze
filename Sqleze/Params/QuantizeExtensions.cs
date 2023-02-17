using Sqleze.Converters.SqlTypes;
using Sqleze;
using Sqleze.Params;
using Sqleze.ValueGetters;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace Sqleze;


public static class PreferredSizeExtensions
{
    public static ISqlezeBuilder WithPreferredNVarCharSizes(
        this ISqlezeBuilder sqlezeConnectionBuilder,
        params int[] sizes)
    {
        return sqlezeConnectionBuilder.With<NVarCharSizeQuantizeRoot>(
            (root, scope) =>
            {
                useNVarCharSizeQuantizeOptions(scope, sizes);
            });
    }

    public static ISqlezeParameterBuilder WithPreferredNVarCharSizes(
        this ISqlezeParameterCollection sqlezeParameters,
        params int[] sizes)
    {
        return sqlezeParameters.With<NVarCharSizeQuantizeRoot>(
            (root, scope) =>
            {
                useNVarCharSizeQuantizeOptions(scope, sizes);
            });
    }

    public static ISqlezeParameterBuilder WithPreferredNVarCharSizes(
        this ISqlezeParameterBuilder sqlezeParameterBuilder,
        params int[] sizes)
    {
        return sqlezeParameterBuilder.With<NVarCharSizeQuantizeRoot>(
            (root, scope) =>
            {
                useNVarCharSizeQuantizeOptions(scope, sizes);
            });
    }

    private static void useNVarCharSizeQuantizeOptions(ISqlezeScope scope, params int[] sizes)
    {
        scope.Use(new NVarCharSizeQuantizeOptions(ImmutableArray.CreateRange(sizes.OrderBy(x => x))));
    }


    public static ISqlezeBuilder WithPreferredVarCharSizes(
    this ISqlezeBuilder sqlezeConnectionBuilder,
    params int[] sizes)
    {
        return sqlezeConnectionBuilder.With<VarCharSizeQuantizeRoot>(
            (root, scope) =>
            {
                useVarCharSizeQuantizeOptions(scope, sizes);
            });
    }

    public static ISqlezeParameterBuilder WithPreferredVarCharSizes(
        this ISqlezeParameterCollection sqlezeParameters,
        params int[] sizes)
    {
        return sqlezeParameters.With<VarCharSizeQuantizeRoot>(
            (root, scope) =>
            {
                useVarCharSizeQuantizeOptions(scope, sizes);
            });
    }
    public static ISqlezeParameterBuilder WithPreferredVarCharSizes(
        this ISqlezeParameterBuilder sqlezeParameterBuilder,
        params int[] sizes)
    {
        return sqlezeParameterBuilder.With<VarCharSizeQuantizeRoot>(
            (root, scope) =>
            {
                useVarCharSizeQuantizeOptions(scope, sizes);
            });
    }

    private static void useVarCharSizeQuantizeOptions(ISqlezeScope scope, params int[] sizes)
    {
        scope.Use(new VarCharSizeQuantizeOptions(ImmutableArray.CreateRange(sizes.OrderBy(x => x))));
    }


    public static ISqlezeBuilder WithPreferredVarBinarySizes(
    this ISqlezeBuilder sqlezeConnectionBuilder,
    params int[] sizes)
    {
        return sqlezeConnectionBuilder.With<VarBinarySizeQuantizeRoot>(
            (root, scope) =>
            {
                useVarBinarySizeQuantizeOptions(scope, sizes);
            });
    }

    public static ISqlezeParameterBuilder WithPreferredVarBinarySizes(
        this ISqlezeParameterCollection sqlezeParameters,
        params int[] sizes)
    {
        return sqlezeParameters.With<VarBinarySizeQuantizeRoot>(
            (root, scope) =>
            {
                useVarBinarySizeQuantizeOptions(scope, sizes);
            });
    }
    public static ISqlezeParameterBuilder WithPreferredVarBinarySizes(
        this ISqlezeParameterBuilder sqlezeParameterBuilder,
        params int[] sizes)
    {
        return sqlezeParameterBuilder.With<VarBinarySizeQuantizeRoot>(
            (root, scope) =>
            {
                useVarBinarySizeQuantizeOptions(scope, sizes);
            });
    }

    private static void useVarBinarySizeQuantizeOptions(ISqlezeScope scope, params int[] sizes)
    {
        scope.Use(new VarBinarySizeQuantizeOptions(ImmutableArray.CreateRange(sizes.OrderBy(x => x))));
    }


    public static ISqlezeBuilder WithPreferredNumericPrecisionScales(
        this ISqlezeBuilder sqlezeConnectionBuilder,
        IEnumerable<(int precision, int scale)> precisionScales)
    {
        return sqlezeConnectionBuilder.With<PrecisionScaleQuantizeRoot>(
            (root, scope) =>
            {
                useNumericPrecisionScaleQuantizeOptions(scope, precisionScales);
            });
    }

    public static ISqlezeParameterBuilder WithPreferredNumericPrecisionScales(
        this ISqlezeParameterCollection sqlezeParameters,
        IEnumerable<(int precision, int scale)> precisionScales)
    {
        return sqlezeParameters.With<PrecisionScaleQuantizeRoot>(
            (root, scope) =>
            {
                useNumericPrecisionScaleQuantizeOptions(scope, precisionScales);
            });
    }
    public static ISqlezeParameterBuilder WithPreferredNumericPrecisionScales(
        this ISqlezeParameterBuilder sqlezeParameterBuilder,
        IEnumerable<(int precision, int scale)> precisionScales)
    {
        return sqlezeParameterBuilder.With<PrecisionScaleQuantizeRoot>(
            (root, scope) =>
            {
                useNumericPrecisionScaleQuantizeOptions(scope, precisionScales);
            });
    }

    private static void useNumericPrecisionScaleQuantizeOptions(ISqlezeScope scope, IEnumerable<(int precision, int scale)> precisionScales)
    {
        validatePrecisionScales(precisionScales);

        scope.Use(new PrecisionScaleQuantizeOptions(
            ImmutableArray.CreateRange(
                precisionScales
                    .Select(x => (precision: (byte)x.precision, scale: (byte)x.scale))
                    .OrderBy(x => x.precision)
                    .ThenBy(x => x.scale))));
    }

    private static void validatePrecisionScales(IEnumerable<(int precision, int scale)> precisionScales)
    {
        var badItems = precisionScales
            .Where(x => x.precision < 1
                     || x.precision > 38
                     || x.scale < 0
                     || x.scale > x.precision)
            .Take(1)
            .ToArray();

        if(badItems.Length > 0)
            throw new Exception($"A precision / scale of ({badItems[0].precision},{badItems[0].scale}) is not valid");
    }




    public static ISqlezeBuilder WithPreferDateTime(
        this ISqlezeBuilder sqlezeConnectionBuilder)
    {
        return sqlezeConnectionBuilder.With<DateTimeChoiceRoot>(
            (root, scope) =>
            {
                prefer(scope, "datetime");
            });
    }

    public static ISqlezeParameterBuilder WithPreferDateTime(
        this ISqlezeParameterCollection sqlezeParameters)
    {
        return sqlezeParameters.With<DateTimeChoiceRoot>(
            (root, scope) =>
            {
                prefer(scope, "datetime");
            });
    }

    public static ISqlezeParameterBuilder WithPreferDateTime(
        this ISqlezeParameterBuilder sqlezeParameterBuilder)
    {
        return sqlezeParameterBuilder.With<DateTimeChoiceRoot>(
            (root, scope) =>
            {
                prefer(scope, "datetime");
            });
    }

    public static ISqlezeBuilder WithPreferDateTime2(
        this ISqlezeBuilder sqlezeConnectionBuilder,
        int? scale = null)
    {
        return sqlezeConnectionBuilder.With<DateTimeChoiceRoot>(
            (root, scope) =>
            {
                prefer(scope, "datetime2", scale);
            });
    }

    public static ISqlezeParameterBuilder WithPreferDateTime2(
        this ISqlezeParameterCollection sqlezeParameters,
        int? scale = null)
    {
        return sqlezeParameters.With<DateTimeChoiceRoot>(
            (root, scope) =>
            {
                prefer(scope, "datetime2", scale);
            });
    }

    public static ISqlezeParameterBuilder WithPreferDateTime2(
        this ISqlezeParameterBuilder sqlezeParameterBuilder,
        int? scale = null)
    {
        return sqlezeParameterBuilder.With<DateTimeChoiceRoot>(
            (root, scope) =>
            {
                prefer(scope, "datetime2", scale);
            });
    }

    private static void prefer(ISqlezeScope scope, string sqlTypeName, int? scale = null)
    {
        var sqlDbType = SqlDbTypeConverter.ToSqlDbType(sqlTypeName);

        byte byteScale = (byte)(sqlDbType switch
        {
            SqlDbType.DateTime => 0,
            SqlDbType.DateTime2 => scale ?? 7,
            _ => throw new Exception("Expected either datetime or datetime2")
        });

        scope.Use<IParameterDefaultSqlTypeOptions<DateTime>>(
            new ParameterDefaultSqlTypeOptions<DateTime>()
            { SqlTypeName = sqlTypeName, Scale = byteScale });

        scope.Use<IParameterDefaultSqlTypeOptions<DateTime?>>(
            new ParameterDefaultSqlTypeOptions<DateTime?>()
            { SqlTypeName = sqlTypeName, Scale = byteScale });

    }

}
