namespace Sqleze.Metadata
{
    public record TableTypeColumnDefinition
    (
        string ColumnName,
        int ColumnOrdinal,
        string Datatype,
        int Length,
        int Precision,
        int Scale,
        bool IsNullable
    );
}
