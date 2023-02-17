using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze.Util
{
    public static class DecimalExtensions
    {
        private static decimal[] _powersOfTen;
        private static decimal[] _negativePowersOfTen;

        static DecimalExtensions()
        {
            decimal power = 1;

            _powersOfTen = new decimal[28];
            _negativePowersOfTen = new decimal[28];

            for(int i = 0; i <= 27; i++)
            {
                _powersOfTen[i] = power;
                _negativePowersOfTen[i] = 1m / power;

                power *= 10;
            }
        }

        /// <summary>
        /// Scale of number as a power of ten
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        public static byte Scale(this decimal arg)
        {
            int[] parts = Decimal.GetBits(arg);

            return (byte)((parts[3] >> 16) & 0x7F);
        }

        private static decimal[] _nines = new[]
        {
            -1m,
            9m,
            99m,
            999m,
            9999m,
            99999m,
            999999m,
            9999999m,
            99999999m,
            999999999m,
            9999999999m,
            99999999999m,
            999999999999m,
            9999999999999m,
            99999999999999m,
            999999999999999m,
            9999999999999999m,
            99999999999999999m,
            999999999999999999m,
            9999999999999999999m,
            99999999999999999999m,
            999999999999999999999m,
            9999999999999999999999m,
            99999999999999999999999m,
            999999999999999999999999m,
            9999999999999999999999999m,
            99999999999999999999999999m,
            999999999999999999999999999m,
            9999999999999999999999999999m,
            decimal.MaxValue,
        };


        public static byte SigFigs(this decimal arg)
        {
            int pos = Array.BinarySearch<decimal>(_nines, arg.Mantissa());
            if(pos < 0) pos = ~pos;

            return (byte)pos;
        }

        public static byte Precision(this decimal arg)
        {
            return (arg < 1m) ? (byte)(arg.Scale() + (byte)1) : arg.SigFigs();
        }

        /// <summary>
        /// True for negative
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        public static bool IsNegative(this decimal arg)
        {
            int[] parts = Decimal.GetBits(arg);
            return (parts[3] & 0x80000000) != 0;
        }

        /// <summary>
        /// Integral part of the number
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        public static decimal Mantissa(this decimal arg)
        {
            int[] parts = Decimal.GetBits(arg);

            return new decimal(
                parts[0],
                parts[1],
                parts[2],
                ((uint)parts[3] & (uint)0x80000000) != (uint)0,
                (byte)0);
        }

        /// <summary>
        /// Fix the scale of the decimal argument to a specific number of decimal places.
        /// The number will be truncated if it does not fit the precision, and you will
        /// get an OverflowException if the number is too large for the scale chosen.
        /// </summary>
        /// <param name="arg"></param>
        /// <param name="scale"></param>
        /// <returns></returns>
        public static decimal Fix(this decimal arg, int scale = 0)
        {
            if(scale < 0 || scale > 28)
                throw new ArgumentOutOfRangeException(nameof(scale));

            decimal mantissa = arg.Mantissa();
            bool isNegative = arg.IsNegative();
            int oldScale = arg.Scale();

            if(scale == oldScale)
                return arg;

            int change = scale - oldScale;
            if(change < 0)
                change = -change;

            decimal factor;
            bool final10 = false;   // Deal with very large numbers where we can't
                                    // represent the multiplication factor directly.

            if(change == 28)
            {
                factor = _powersOfTen[27];
                final10 = true;
            }
            else
            {
                factor = _powersOfTen[change];
            }

            if(scale > oldScale)
            {
                mantissa *= factor;

                if(final10)
                    mantissa *= 10;
            }
            else if(scale < oldScale)
            {
                mantissa = Decimal.Truncate(mantissa / factor);

                if(final10)
                    mantissa = Decimal.Truncate(mantissa / 10);
            }

            var parts = Decimal.GetBits(mantissa);

            return new decimal(parts[0], parts[1], parts[2], isNegative, (byte)scale);
        }

        public static decimal? Fix(this decimal? arg, int scale = 0)
        {
            if(arg == null)
                return null;

            return Fix(arg.Value, scale);
        }


        public static decimal Round(this decimal arg, int scale = 0)
        {
            arg = Decimal.Round(arg, scale, MidpointRounding.AwayFromZero);
            return Fix(arg, scale);
        }

        public static decimal? Round(this decimal? arg, int scale = 0)
        {
            if(arg == null)
                return null;

            return Round(arg.Value, scale);
        }

        public static decimal Bound(this decimal arg, int precision, int scale)
        {
            if(scale > precision)
                throw new ArgumentOutOfRangeException(nameof(precision), "Precision cannot be greater than scale");

            if(scale < 0 || scale > 28)
                throw new ArgumentOutOfRangeException(nameof(scale));

            if(precision < 0 || precision > 28)
                throw new ArgumentOutOfRangeException(nameof(precision));

            // E.g. precision 5, scale = 2 gives us 999.99
            // calculated as:
            //     10^(5-2) - (1 / 10^2)
            // =   10^3     - (1 / 100)
            // =   1000 - 0.01
            // =   999.99
            decimal limit = _powersOfTen[precision - scale]
                - _negativePowersOfTen[scale];

            if(arg.IsNegative())
            {
                if(arg < -limit)
                    return -limit;
            }
            else
            {
                if(arg > limit)
                    return limit;
            }

            return Round(arg, scale);
        }

        public static decimal? Bound(this decimal? arg, int precision, int scale)
        {
            if(arg == null)
                return null;

            return Bound(arg.Value, precision, scale);
        }

        /// <summary>
        /// Converts zero arg to null. Useful for divisors to avoid divide by zero.
        /// </summary>
        /// <param name="arg"></param>
        /// <param name="match"></param>
        /// <returns></returns>
        public static decimal? NullIfZero(this decimal? arg) => arg == 0m ? null : arg;
        /// <summary>
        /// Converts zero arg to null. Useful for divisors to avoid divide by zero.
        /// </summary>
        /// <param name="arg"></param>
        /// <param name="match"></param>
        /// <returns></returns>
        public static decimal? NullIfZero(this decimal arg) => arg == 0m ? (decimal?)null : arg;

        public static decimal? Abs(this decimal? arg) => arg == null ? (decimal?)null : Math.Abs(arg.Value);

        public static decimal Limit(this decimal arg, decimal lbound = Decimal.MinValue, decimal ubound = Decimal.MaxValue)
        {
            return arg > ubound ? ubound
                : arg < lbound ? lbound
                : arg;
        }

        public static decimal? Limit(this decimal? arg, decimal lbound = Decimal.MinValue, decimal ubound = Decimal.MaxValue)
        {
            return arg?.Limit(lbound, ubound);
        }

        public static byte? Scale(this decimal? arg) => arg == null ? null : arg.Value.Scale();
        public static byte? Precision(this decimal? arg) => arg == null ? null : arg.Value.Precision();
        public static byte? SigFigs(this decimal? arg) => arg == null ? null : arg.Value.SigFigs();
    }

}
