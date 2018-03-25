using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

/// <summary>
/// Summary description for DatabaseRowParser
/// </summary>
public class DatabaseRowParser
{
    private System.Data.SqlClient.SqlDataReader sqlReader;
    private DataGridObjects.DataGridObject dataGridObj;

    public DatabaseRowParser(System.Data.SqlClient.SqlDataReader sqlReader, DataGridObjects.DataGridObject dataGridObj)
    {
        //
        // TODO: Add constructor logic here
        //

        this.sqlReader = sqlReader;
        this.dataGridObj = dataGridObj;
    }

    public Object GetValue(string fieldName)
    {
        return "";
    }
}