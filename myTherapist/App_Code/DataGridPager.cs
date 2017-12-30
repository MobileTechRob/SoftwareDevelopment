using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Text;

public enum MyDataTypes
{
    INTEGER,
    STRING,
    DATETIME
}

public enum MyDataSort
{
    ASC,
    DESC    
}

public class MyDataGridColumn
{
    public string DataBaseTableColumnName { get; set; }
    public string DataGridColumnName { get; set; }
    public bool OrderByColumn { get; set; }
    public MyDataTypes DataType { get; set; }
    public int HeaderWidth { get; set; }

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
    public MyDataSort Sort { get; set; }
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
        Sort = MyDataSort.ASC;
    }

    public void AddColumn(string databaseColumnName, string dataGridColumnName, MyDataTypes dataType, bool orderByColumn, int gridWidth)
    {
        MyDataGridColumn column = new MyDataGridColumn();
        column.DataBaseTableColumnName = databaseColumnName;
        column.DataGridColumnName = dataGridColumnName;
        column.OrderByColumn = orderByColumn;
        column.DataType = dataType;
        column.HeaderWidth = gridWidth;

        columnDictionary.Add(columnDictionary.Count + 1, column);
    }

    public int ColumnWidth(int column)
    {
        if (columnDictionary.ContainsKey(column))
            return columnDictionary[column].HeaderWidth;
        else
            return 10;
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
                DataColumn dataColumn = new DataColumn();
                dataColumn.ColumnName = column.DataGridColumnName;

                if (column.DataType == MyDataTypes.INTEGER)
                    dataColumn.DataType = System.Type.GetType("System.Int32");

                if (column.DataType == MyDataTypes.STRING)
                    dataColumn.DataType = System.Type.GetType("System.String");

                if (column.DataType == MyDataTypes.DATETIME)
                    dataColumn.DataType = System.Type.GetType("System.DateTime");

                dataGridTable.Columns.Add(column.DataGridColumnName, dataColumn.DataType);

                sqlSelectClause.Append(column.DataBaseTableColumnName);
                sqlSelectClause.Append(",");

                if (column.OrderByColumn)
                    orderByColumnName = column.DataBaseTableColumnName;
            }

            sqlCommandString = new StringBuilder();
            sqlCommandString.Append(sqlSelectClause.ToString().TrimEnd(parms));
            sqlCommandString.Append(" FROM ");
            sqlCommandString.Append(databaseTableName);

            if (String.IsNullOrEmpty(orderByColumnName) == false)
            {
                sqlCommandString.Append(" ORDER BY ");
                sqlCommandString.Append(orderByColumnName);
            }

            if (Sort == MyDataSort.ASC)
                sqlCommandString.Append(" ASC ");
            else
                sqlCommandString.Append(" DESC ");

            if (NumberRowsToDisplay > 0)
            { 
                sqlCommandString.Append(" OFFSET ");
                sqlCommandString.Append(((PageNumber - 1) * NumberRowsToDisplay).ToString());
                sqlCommandString.Append(" ROWS ");
                sqlCommandString.Append(" FETCH NEXT ");
                sqlCommandString.Append(NumberRowsToDisplay.ToString());
                sqlCommandString.Append(" ROWS ONLY ");
            }


            //= "SELECT * FROM TableName ORDER BY id OFFSET 10 ROWS FETCH NEXT 10 ROWS ONLY";=
            databaseCommand.CommandText = sqlCommandString.ToString();
            databaseCommand.Connection = databaseConnection;
            databaseReader = databaseCommand.ExecuteReader();
            string fieldData = string.Empty;
            tlist = new List<string>();

            DataRow row=null;
            object[] itemArray = null;
            int itemCount = 0;
            
            if (databaseReader.HasRows)
            {                           
                while (databaseReader.Read())
                {
                    row = dataGridTable.NewRow();
                    itemCount = 0;
                 
                    itemArray = new object[columnDictionary.Count];

                    foreach (MyDataGridColumn column in columnDictionary.Values)
                    {
                        switch (column.DataType)
                        {
                            case MyDataTypes.INTEGER:
                                itemArray[itemCount] = Utilities.ParseInt32(databaseReader, itemCount);
                                break;

                            case MyDataTypes.STRING:
                                itemArray[itemCount] = Utilities.ParseStr(databaseReader, itemCount);
                                break;

                            case MyDataTypes.DATETIME:
                                itemArray[itemCount] = Utilities.ParseDateTime(databaseReader, itemCount);
                                break;
                        }

                        itemCount++;
                    }

                    row.ItemArray = itemArray;
                    dataGridTable.Rows.Add(row);
                }

                databaseReader.Close();
                databaseConnection.Close();
            }            
        }
                   
        return dataGridTable;
    }


}