using Sqleze.Dynamics;
using Sqleze.Metadata;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze.TableValuedParameters
{

    public record SqlDataRecordAdapterCreateOptions<T>
    (
        IEnumerable<T> Items,
        MSS.SqlMetaData[] SqlMetaDatas,
        Action<T, MSS.SqlDataRecord> WriteAction
    );

    public interface ISqlDataRecordAdapter<T> : IEnumerable<MSS.SqlDataRecord>
    {
    }

    public class SqlDataRecordAdapter<T> : ISqlDataRecordAdapter<T>
    {
        private readonly MSS.SqlDataRecord sqlDataRecord;

        private readonly IEnumerable<T> items;
        private readonly Action<T, MSS.SqlDataRecord> writeAction;

        public SqlDataRecordAdapter(
            SqlDataRecordAdapterCreateOptions<T> arg,
            ISqlDataRecordFactory sqlDataRecordFactory)
        {
            this.sqlDataRecord = sqlDataRecordFactory.New(arg.SqlMetaDatas);
            this.writeAction = arg.WriteAction;
            this.items = arg.Items;
        }

        public IEnumerator<MSS.SqlDataRecord> GetEnumerator()
        {
            // TODO - populate default values here

            foreach(var item in items)
            {
                // Write into SqlDataRecord
                writeAction(item, sqlDataRecord);

                // Pass the populated SqlDataRecord to SQL Server. Note that we re-use
                // the same SqlDataRecord object for each row in the enumeration.
                // This is for performance reasons.
                yield return sqlDataRecord;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            // No need for this, SQL always uses the generic version.
            throw new NotImplementedException();
        }

        // TODO - cope with mismatched types more gracefully
        //        catch(Exception e)
        //{
        //    // This exception will typically occur on a type mismatch between the
        //    // column type in SQL and the .NET type.
        //    e.Data["Field name"] = sqlDataRecord.GetName(i);
        //    throw;
        //}
    }
}
