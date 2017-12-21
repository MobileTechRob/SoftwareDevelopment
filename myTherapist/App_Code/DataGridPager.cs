using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Text;

public class MyDataGridColumn
{
    public string DataBaseTableColumnName { get; set; }
    public string DataGridColumnName { get; set; }
    public bool OrderByColumn { get; set; }


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

    public void AddColumn(string databaseColumnName, string dataGridColumnName, bool orderByColumn)
    {
        MyDataGridColumn column = new MyDataGridColumn();
        column.DataBaseTableColumnName = databaseColumnName;
        column.DataGridColumnName = dataGridColumnName;
        column.OrderByColumn = orderByColumn;

        columnDictionary.Add(columnDictionary.Count + 1, column);
    }

    public DataTable BuildTable()
    {
        StringBuilder sqlCommandString = null;
        StringBuilder sqlSelectClause = null;
        string orderByColumnName = string.Empty;

        databaseConnection.Open();

        if (databaseConnection.State == ConnectionState.Open)
        {
            databaseCommand = new System.Data.SqlClient.SqlCommand();
            sqlSelectClause = new StringBuilder();

            sqlCommandString.Append("SELECT ");

            foreach (MyDataGridColumn column in columnDictionary.Values)
            {
                sqlSelectClause.Append(column.DataBaseTableColumnName);
                sqlSelectClause.Append(", ");

                if (column.OrderByColumn)
                    orderByColumnName = column.DataBaseTableColumnName;
            }

            sqlCommandString.Append(sqlSelectClause.ToString());
            sqlCommandString.Append(" FROM ");
            sqlCommandString.Append(databaseTableName);
            sqlCommandString.Append(" ORDER BY ");
            sqlCommandString.Append(orderByColumnName);
            sqlCommandString.Append(" OFFSET ");
            sqlCommandString.Append(((PageNumber - 1) * NumberRowsToDisplay).ToString());
            sqlCommandString.Append(" ROWS ");
            sqlCommandString.Append(" FETCH NEXT 10 ROWS ONLY ");

            //= "SELECT * FROM TableName ORDER BY id OFFSET 10 ROWS FETCH NEXT 10 ROWS ONLY";=
            databaseCommand.CommandText = sqlCommandString.ToString();
            databaseCommand.Connection = databaseConnection;
            databaseReader = databaseCommand.ExecuteReader();
        }
                   
        return dataGridTable;
    }


}