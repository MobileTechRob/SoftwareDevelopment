using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Text;

public enum MyDataTypes
{
    INTEGER,
    STRING
}

public class MyDataGridColumn
{
    public string DataBaseTableColumnName { get; set; }
    public string DataGridColumnName { get; set; }
    public bool OrderByColumn { get; set; }
    public MyDataTypes DataType { get; set; }

    public MyDataGridColumn()
    {

    }
}

/// <summary>
/// Summary description for DataGridPager
/// </summary>
public class MyDataGridPager
{
    public int NumberRowsToDisplay { get; set; }
    public int PageNumber { get; set; }
    public string WhereClause {get;set;}

    private DataTable dataGridTable;
    private string databaseTableName;
    private SortedDictionary<int, MyDataGridColumn> columnDictionary;
    private System.Data.SqlClient.SqlConnection databaseConnection;
    private System.Data.SqlClient.SqlCommand databaseCommand;
    private System.Data.SqlClient.SqlDataReader databaseReader;

    public MyDataGridPager(string connnectionString, string tableName)
    {
        //
        // TODO: Add constructor logic here
        //

        dataGridTable = new DataTable();
        columnDictionary = new SortedDictionary<int, MyDataGridColumn>();
        databaseConnection = new System.Data.SqlClient.SqlConnection(connnectionString);
        databaseTableName = tableName;
    }

    public void AddColumn(string databaseColumnName, string dataGridColumnName, MyDataTypes dataType, bool orderByColumn)
    {
        MyDataGridColumn column = new MyDataGridColumn();
        column.DataBaseTableColumnName = databaseColumnName;
        column.DataGridColumnName = dataGridColumnName;
        column.OrderByColumn = orderByColumn;
        column.DataType = dataType;

        columnDictionary.Add(columnDictionary.Count + 1, column);
    }

    public DataTable BuildTable()
    {
        StringBuilder sqlCommandString = null;
        StringBuilder sqlSelectClause = null;
        string orderByColumnName = string.Empty;
        char[] parms = {','};
        List<string> tlist = null;

        databaseConnection.Open();

        if (databaseConnection.State == ConnectionState.Open)
        {
            databaseCommand = new System.Data.SqlClient.SqlCommand();
            sqlSelectClause = new StringBuilder();

            sqlSelectClause.Append("SELECT ");

            foreach (MyDataGridColumn column in columnDictionary.Values)
            {
                sqlSelectClause.Append(column.DataBaseTableColumnName);
                sqlSelectClause.Append(",");

                if (column.OrderByColumn)
                    orderByColumnName = column.DataBaseTableColumnName;
            }

            sqlCommandString = new StringBuilder();
            sqlCommandString.Append(sqlSelectClause.ToString().TrimEnd(parms));
            sqlCommandString.Append(" FROM ");
            sqlCommandString.Append(databaseTableName);
            sqlCommandString.Append(" ORDER BY ");
            sqlCommandString.Append(orderByColumnName);
            sqlCommandString.Append(" OFFSET ");
            sqlCommandString.Append(((PageNumber - 1) * NumberRowsToDisplay).ToString());
            sqlCommandString.Append(" ROWS ");
            sqlCommandString.Append(" FETCH NEXT ");
            sqlCommandString.Append(NumberRowsToDisplay.ToString());
            sqlCommandString.Append(" ROWS ONLY ");

            //= "SELECT * FROM TableName ORDER BY id OFFSET 10 ROWS FETCH NEXT 10 ROWS ONLY";=
            databaseCommand.CommandText = sqlCommandString.ToString();
            databaseCommand.Connection = databaseConnection;
            databaseReader = databaseCommand.ExecuteReader();
            string fieldData = string.Empty;
            tlist = new List<string>();

            if (databaseReader.HasRows)
            {
                while (databaseReader.Read())
                {
                    fieldData = databaseReader.GetString(1);
                    tlist.Add(fieldData);
                }
            }            
        }
                   
        return dataGridTable;
    }


}