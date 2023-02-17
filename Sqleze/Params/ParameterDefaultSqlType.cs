using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Common;
using System.Data.SqlTypes;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using Sqleze;
using Sqleze.DryIoc;

namespace Sqleze.Params
{
    public interface IParameterDefaultSqlTypeOptions
    {
        int Length { get; }
        SqlezeParameterMode Mode { get; }
        byte Precision { get; }
        byte Scale { get; }
        string SqlTypeName { get; }
    }
    public interface IParameterDefaultSqlTypeOptions<T> : IParameterDefaultSqlTypeOptions { }

    public record ParameterDefaultSqlTypeOptions : IParameterDefaultSqlTypeOptions
    {
        public string SqlTypeName { get; set; } = "";
        public int Length { get; set; }
        public byte Precision { get; set; }
        public byte Scale { get; set; }

        public SqlezeParameterMode Mode { get; set; }
    }

    public record ParameterDefaultSqlTypeOptions<T> : ParameterDefaultSqlTypeOptions, IParameterDefaultSqlTypeOptions<T>
    {
    }
    public record ParameterDefaultSqlTypeOptionsTableType<T> : ParameterDefaultSqlTypeOptions, IParameterDefaultSqlTypeOptions<T>
    {
        public ParameterDefaultSqlTypeOptionsTableType()
        {
            this.Mode = SqlezeParameterMode.TableType;
        }
    }
}
