using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using DataGridObjects;

/// <summary>
/// Summary description for DatabaseRowParser
/// </summary>
public class DatabaseRowParser
{
    public System.Data.SqlClient.SqlDataReader sqlReader { get; set; }
    private DataGridObjects.DataGridObject dataGridObj;
    
    public DatabaseRowParser(DataGridObjects.DataGridObject dataGridObj)
    {
        //
        // TODO: Add constructor logic here
        //
        this.sqlReader = sqlReader;
        this.dataGridObj = dataGridObj;
    }
    
    public object GetValue(string fieldName)
    {
        //var column = from adataColumn in dataGridObj.GetColumnObjectCollection().Where<   select adataColumn.DataBaseTableColumnName == fieldName;

        IEnumerator<DataGridObject.MyDataGridColumn> iter = dataGridObj.GetEnumerator();
        DataGridObject.MyDataGridColumn col = null;

        while (iter.MoveNext())
        {              
            if (iter.Current.DataBaseTableColumnName == fieldName) 
                break;
        }

        col = iter.Current;
        object columnValue = null;

        switch (col.DataType)
        {
            case MyDataTypes.INTEGER:                
                columnValue = Utilities.ParseInt32(sqlReader, col.Column);
                break;

            case MyDataTypes.STRING:                
                columnValue = Utilities.ParseStr(sqlReader, col.Column);
                break;

            case MyDataTypes.DATETIME:
                columnValue = Utilities.ParseDateTime(sqlReader, col.Column);                 
                break;

            case MyDataTypes.GUID:
                columnValue = Utilities.ParseGuid(sqlReader, col.Column);
                break;
        }

        return columnValue;
    }    
}