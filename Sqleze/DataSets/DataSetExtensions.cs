using Sqleze;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze;
public static class DataSetExtensions
{
    public static T ExecuteDataSet<T>(this ISqlezeReader sqlezeReader, params string[] tableNames) where T: DataSet, new()
    {
        var dataSet = new T();

        int tableCount = dataSet.Tables.Count;

        if(tableNames.Length == 0)
        {
            if(dataSet.Tables.Count > 1)
                throw new Exception("Specify tableNames parameter if more than one DataTable in DataSet");

            if(dataSet.Tables.Count == 0)
                dataSet.Tables.Add();

            tableNames = new[] { dataSet.Tables[0].TableName };
        }
        else
        {
            if(dataSet.Tables.Count == 0)
            {
                foreach(var tableName in tableNames)
                {
                    dataSet.Tables.Add(tableName);
                }
            }
        }

        foreach(var tableName in tableNames)
        {
            var dataTable = dataSet.Tables[tableName];
            if(dataTable == null)
                throw new Exception($"Unknown table name {tableName}");

            sqlezeReader.FillDataTable(dataTable);
        }

        return dataSet;
    }

    public static T ExecuteDataSet<T>(this ISqlezeCommand sqlezeCommand, params string[] tableNames) where T : DataSet, new()
    {
        return sqlezeCommand.ExecuteReader().ExecuteDataSet<T>(tableNames);
    }

    public static T ExecuteDataSet<T>(this ISqlezeParameter sqlezeParameter, params string[] tableNames) where T : DataSet, new()
    {
        return sqlezeParameter.Command.ExecuteReader().ExecuteDataSet<T>(tableNames);
    }

    public static T ExecuteDataSet<T>(this ISqlezeParameterCollection sqlezeParameterCollection, params string[] tableNames) where T : DataSet, new()
    {
        return sqlezeParameterCollection.Command.ExecuteReader().ExecuteDataSet<T>(tableNames);
    }


    public static DataSet ExecuteDataSet(this ISqlezeReader sqlezeReader, params string[] tableNames)
        => sqlezeReader.ExecuteDataSet<DataSet>(tableNames);

    public static DataSet ExecuteDataSet(this ISqlezeCommand sqlezeCommand, params string[] tableNames)
        => sqlezeCommand.ExecuteDataSet<DataSet>(tableNames);

    public static DataSet ExecuteDataSet(this ISqlezeParameter sqlezeParameter, params string[] tableNames)
        => sqlezeParameter.ExecuteDataSet<DataSet>(tableNames);

    public static DataSet ExecuteDataSet(this ISqlezeParameterCollection sqlezeParameterCollection, params string[] tableNames)
        => sqlezeParameterCollection.ExecuteDataSet<DataSet>(tableNames);
}
