using Sqleze;
using Sqleze.Options;
using Sqleze.Params;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze
{
    public class SqlezeParameter<T> : ISqlezeParameter<T>
    {
        private T? value;
        private Action<T?>? outputAction;

        private readonly IParameterDefaultSqlTypeOptions<T> parameterDefaultSqlTypeOptions;

        public SqlezeParameter(ParameterCreateOptions parameterCreateOptions,
            IParameterDefaultSqlTypeOptions<T> parameterDefaultSqlTypeOptions,
            ISqlezeCommand sqlezeCommand)
        {
            this.parameterDefaultSqlTypeOptions = parameterDefaultSqlTypeOptions;
            this.Command = sqlezeCommand;

            this.AdoName = parameterCreateOptions.AdoName;
            this.Name = parameterCreateOptions.Name;

            setDefaultsByType(parameterDefaultSqlTypeOptions);
        }

        [MemberNotNull(nameof(SqlTypeName))]
        private void setDefaultsByType(IParameterDefaultSqlTypeOptions parameterDefaultSqlTypeOptions)
        {
            this.SqlTypeName = parameterDefaultSqlTypeOptions.SqlTypeName;
            this.Mode = parameterDefaultSqlTypeOptions.Mode;
            this.Scale = parameterDefaultSqlTypeOptions.Scale;
            this.Precision = parameterDefaultSqlTypeOptions.Precision;
            this.Length = parameterDefaultSqlTypeOptions.Length;
        }

        public T? Value { 
            get => value;
            set 
            {
                this.value = value;
            }
        }

        public string Name { get; private set; }

        public string AdoName { get; private set; }

        public bool OmitInput { get; set; }


        public Action<T?>? OutputAction
        {
            get => outputAction;
            set
            {
                outputAction = value;
            }
        }

        public ISqlezeCommand Command { get; init; }

        public Type ValueType => typeof(T);

        public SqlezeParameterMode Mode { get; set; }
        public string SqlTypeName { get; set; }
        public int Length { get; set; }
        public byte Precision { get; set; }
        public byte Scale { get; set; }

        Action<object?>? ISqlezeParameter.OutputAction
        {
            get
            {
                return (this.OutputAction == null)
                    ? null
                    : x => this.OutputAction((T?)x);
            }

            set
            {
                this.OutputAction = (value == null)
                    ? (Action<T?>?)null
                    : (x => value((object?)x));
            }
        }

        object? ISqlezeParameter.Value 
        {
            get => this.Value; 
            set => this.Value = (T?)value; 
        }
    }
}
