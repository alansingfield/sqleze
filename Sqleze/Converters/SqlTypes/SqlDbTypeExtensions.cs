using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Linq;
using System.Collections.ObjectModel;

namespace Sqleze.Converters.SqlTypes
{
    public static class SqlDbTypeExtensions
    {
        public static bool IsAnsiType(this SqlDbType sqlDbType) => sqlDbType switch
        {
            SqlDbType.VarChar   => true,
            SqlDbType.Char      => true,
            SqlDbType.Text      => true,
            _ => false,
        };

        public static bool IsBinType(this SqlDbType sqlDbType) => sqlDbType switch
        {
            SqlDbType.VarBinary => true,
            SqlDbType.Binary    => true,
            SqlDbType.Image     => true,
            SqlDbType.Timestamp => true,
            SqlDbType.Udt       => true,
            _ => false,
        };

        public static bool IsNCharType(this SqlDbType sqlDbType) => sqlDbType switch
        {
            SqlDbType.NVarChar  => true,
            SqlDbType.NChar     => true,
            SqlDbType.NText     => true,
            SqlDbType.Xml       => true,
            _ => false,
        };

        public static bool IsSizeInCharacters(this SqlDbType sqlDbType) => sqlDbType switch
        {
            SqlDbType.NVarChar  => true,
            SqlDbType.NChar     => true,
            SqlDbType.NText     => true,
            SqlDbType.Xml       => true,
            _ => false,
        };

        public static bool HasSize(this SqlDbType sqlDbType) => sqlDbType switch
        {
            SqlDbType.NVarChar  => true,
            SqlDbType.VarChar   => true,
            SqlDbType.VarBinary => true,
            SqlDbType.NChar     => true,
            SqlDbType.Char      => true,
            SqlDbType.Binary    => true,
            _ => false,
        };

        public static bool HasScale(this SqlDbType sqlDbType) => sqlDbType switch
        {
            SqlDbType.Decimal        => true,
            SqlDbType.DateTimeOffset => true,
            SqlDbType.DateTime2      => true,
            SqlDbType.Time           => true,
            _ => false,
        };
        public static bool HasPrecision(this SqlDbType sqlDbType) => sqlDbType switch
        {
            SqlDbType.Decimal => true,
            _ => false,
        };

        public static int MaxFixedSize(this SqlDbType sqlDbType) => sqlDbType switch
        {
            SqlDbType.NVarChar =>   4000,
            SqlDbType.NChar =>      4000,
            SqlDbType.Binary =>     8000,
            SqlDbType.VarChar =>    8000,
            SqlDbType.Char =>       8000,
            SqlDbType.VarBinary =>  8000,
            _ => 0,
        };

        public static bool HasVarMaxSize(this SqlDbType sqlDbType) => sqlDbType switch
        {
            SqlDbType.NVarChar =>   true,
            SqlDbType.VarChar =>    true,
            SqlDbType.VarBinary =>  true,
            _ => false,
        };

    }
}
