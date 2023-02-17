namespace Sqleze;

public interface ISqlezeParameter
{
    string Name { get; }
    string AdoName { get; }
    object? Value { get; set; }
    bool OmitInput { get; set; }
    Action<object?>? OutputAction { get; set; }
    ISqlezeCommand Command { get; }
    Type ValueType { get; }
    string SqlTypeName { get; set; }
    int Length { get; set; }
    byte Precision { get; set; }
    byte Scale { get; set; }
    SqlezeParameterMode Mode { get; set; }
}

public enum SqlezeParameterMode
{
    Scalar = 0,
    ReturnValue,
    TableType,
    AssemblyType
}

public interface ISqlezeParameter<T> : ISqlezeParameter
{
    new T? Value { get; set; }
    new Action<T?>? OutputAction { get; set; }

}
