using Sqleze.Metadata;

namespace Sqleze.TableValuedParameters;

public interface ISqlDataRecordMapper<T>
{
    Action<T, MSS.SqlDataRecord> Map(IEnumerable<TableTypeColumnDefinition> tableTypeColumnDefinitions);
}
