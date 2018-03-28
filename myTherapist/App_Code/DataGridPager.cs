using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataGridObjects;

/// <summary>
/// Summary description for DataGridPager
/// </summary>
public class MyDataGridPager
{
    private int _totalNumberOfTableRows = 0;
    private int _numberOfCompletePages = 0;
    
    public int NumberRowsToDisplay { get; set; }
    public int NumberOfTableRows { get { return _totalNumberOfTableRows; } }
    public int NumberOfCompletePages { get { return _numberOfCompletePages; } }
    public int PageNumber { get; set; }
    public string WhereClause {get;set;}
    public MyDataSort Sort { get; set; }
    private DataTable dataGridTable;
    private string databaseTableName;
    private SortedDictionary<int, Column> columnDictionary;
    private SortedDictionary<string, int> gridIndexByColumnName;
    private bool fillToCompletePage = false;
    //private DatabaseRowObject databaseRowObject;

    private System.Data.SqlClient.SqlConnection databaseConnection;
    private System.Data.SqlClient.SqlCommand databaseCommand;
    private System.Data.SqlClient.SqlDataReader databaseReader;
    StringBuilder whereClause = null;

    private class Column
    {
        public string Name { get; set; }
        public int Width { get; set; }
    }

    public MyDataGridPager()
    {
        //
        // TODO: Add constructor logic here
        //
        columnDictionary = new SortedDictionary<int, Column>();
        gridIndexByColumnName = new SortedDictionary<string, int>();
        Sort = MyDataSort.ASC;
        PageNumber = 1;                
    }

    public void AddColumn(string dataGridColumnName, int columnWidth)
    {
        Column column = new Column();
        column.Name = dataGridColumnName;
        column.Width = columnWidth;

        columnDictionary.Add(columnDictionary.Count, column);        
    }

    public int GridIndexByColumnName(string columnName)
    {
        int index;

        gridIndexByColumnName.TryGetValue(columnName, out index);

        return index;
    }

    public int ColumnWidth(int column)
    {
        if (columnDictionary.ContainsKey(column))
            return columnDictionary[column].Width;
        else
            return 10;
    }

    //public DataTable BuildTable(DataGridObjects.GridRowObject gridInfo = null)
    //{
    //    StringBuilder sqlCommandString = null;
    //    StringBuilder sqlSelectClause = null;
    //    string orderByColumnName = string.Empty;
    //    char[] parms = {','};
    //    List<string> tlist = null;
    //    string wherePhrase = string.Empty;

    //    databaseConnection.Open();

    //    if (databaseConnection.State == ConnectionState.Open)
    //    {
    //        databaseCommand = new System.Data.SqlClient.SqlCommand();
    //        sqlSelectClause = new StringBuilder();

    //        sqlSelectClause.Append("SELECT Count(*) FROM ");
    //        sqlSelectClause.Append(databaseTableName);

    //        databaseCommand.CommandText = sqlSelectClause.ToString();
    //        databaseCommand.Connection = databaseConnection;
    //        _totalNumberOfTableRows = (int)databaseCommand.ExecuteScalar();
    //        sqlSelectClause.Clear();

    //        _numberOfCompletePages = _totalNumberOfTableRows / NumberRowsToDisplay;

    //        sqlSelectClause.Append("SELECT ");

    //        IEnumerator<DatabaseRowObject.DatabaseColumnObject> iter = databaseRowObject.GetEnumerator();

    //        //foreach (DataGridObject.MyDataGridColumn column in columnDictionary.Values)

    //        while (iter.MoveNext())
    //        {
    //            DataColumn dataColumn = new DataColumn();
    //            DataGridObjects.DatabaseRowObject.DatabaseColumnObject column;
    //            column = iter.Current;

    //            dataColumn.ColumnName = column.DataGridColumnName;

    //            if (column.DataType == MyDataTypes.INTEGER)
    //                dataColumn.DataType = System.Type.GetType("System.Int32");

    //            if (column.DataType == MyDataTypes.STRING)
    //                dataColumn.DataType = System.Type.GetType("System.String");

    //            if (column.DataType == MyDataTypes.DATETIME)
    //                dataColumn.DataType = System.Type.GetType("System.DateTime");

    //            if (column.DataType == MyDataTypes.GUID)
    //                dataColumn.DataType = System.Type.GetType("System.Guid");
                                                
    //            dataGridTable.Columns.Add(column.DataGridColumnName, dataColumn.DataType);

    //            sqlSelectClause.Append(column.DataBaseTableColumnName);
    //            sqlSelectClause.Append(",");

    //            if (column.OrderByColumn)
    //                orderByColumnName = column.DataBaseTableColumnName;
    //        }

    //        sqlCommandString = new StringBuilder();
    //        sqlCommandString.Append(sqlSelectClause.ToString().TrimEnd(parms));
    //        sqlCommandString.Append(" FROM ");
    //        sqlCommandString.Append(databaseTableName);

    //        if (whereClause.Length > 0)
    //        {
    //            wherePhrase = whereClause.ToString().TrimEnd(new char[] { ' ', 'A', 'N', 'D' });
    //            sqlCommandString.Append(" WHERE ");
    //            sqlCommandString.Append(wherePhrase);
    //        }

    //        if (String.IsNullOrEmpty(orderByColumnName) == false)
    //        {
    //            sqlCommandString.Append(" ORDER BY ");
    //            sqlCommandString.Append(orderByColumnName);
    //        }

    //        if (Sort == MyDataSort.ASC)
    //            sqlCommandString.Append(" ASC ");
    //        else
    //            sqlCommandString.Append(" DESC ");

    //        if (NumberRowsToDisplay > 0)
    //        { 
    //            sqlCommandString.Append(" OFFSET ");
    //            sqlCommandString.Append(((PageNumber - 1) * NumberRowsToDisplay).ToString());
    //            sqlCommandString.Append(" ROWS ");
    //            sqlCommandString.Append(" FETCH NEXT ");
    //            sqlCommandString.Append(NumberRowsToDisplay.ToString());
    //            sqlCommandString.Append(" ROWS ONLY ");
    //        }

    //        databaseCommand.CommandText = sqlCommandString.ToString();
    //        databaseCommand.Connection = databaseConnection;
    //        databaseReader = databaseCommand.ExecuteReader();
    //        string fieldData = string.Empty;
    //        tlist = new List<string>();

    //        DataRow row=null;
    //        object[] itemArray = null;
    //        int itemCount = 0;
    //        DatabaseRowParser rowParser = null;
    //        rowParser = new DatabaseRowParser(databaseRowObject);
    //        DataRowDisplay rowToDisplay = null;


    //        if (databaseReader.HasRows)
    //        {                           
    //            while (databaseReader.Read())
    //            {
    //                // if there is a gridInfo object then 
    //                if (gridInfo != null)
    //                {
    //                    IEnumerator<DataRowDisplay> rowIter = gridInfo.GetEnumerator();
    //                    rowParser.sqlReader = databaseReader;

    //                    while (rowIter.MoveNext())
    //                    {
    //                        itemCount = 0;
    //                        rowToDisplay = rowIter.Current;

    //                        row = dataGridTable.NewRow();                            
    //                        itemArray = new object[rowToDisplay.NumberItems];
                         
    //                        IEnumerator<DataCellDisplay> columnIter = rowToDisplay.GetEnumerator();

    //                        while (columnIter.MoveNext())
    //                        {
    //                            DataCellDisplay cellToDisplay = columnIter.Current;
                            
    //                            if (cellToDisplay.MyDataType == MyDataTypes.DATETIME)
    //                                itemArray[itemCount] = DateTime.Parse(rowParser.GetValue(cellToDisplay.DatabaseColumnName).ToString());
    //                            else
    //                                itemArray[itemCount] = rowParser.GetValue(cellToDisplay.DatabaseColumnName);

    //                            itemCount++;

    //                        }

    //                        row.ItemArray = itemArray;
    //                        dataGridTable.Rows.Add(row);
    //                    }
    //                }
    //                else
    //                {
    //                    // for each row in the grid info object                        
    //                    // iterate through the columns and find that column in the the row from the database
    //                    // place that value of the column in the row for the data grid object 

    //                    // else a row from the database is a row in the datagridview
    //                    row = dataGridTable.NewRow();
    //                    itemCount = 0;

    //                    itemArray = new object[dataGridObject.NumberColumns];
    //                    iter = dataGridObject.GetEnumerator();

    //                    DataGridObject.MyDataGridColumn column = null;

    //                    rowParser = new DatabaseRowParser(dataGridObject);
    //                    rowParser.sqlReader = databaseReader;


    //                    while (iter.MoveNext())
    //                    {
    //                        column = iter.Current;

    //                        itemArray[itemCount] = rowParser.GetValue(column.DataBaseTableColumnName);

    //                        //switch (column.DataType)
    //                        //{
    //                        //    case MyDataTypes.INTEGER:
    //                        //        itemArray[itemCount] = Utilities.ParseInt32(databaseReader, itemCount);
    //                        //        break;

    //                        //    case MyDataTypes.STRING:
    //                        //        itemArray[itemCount] = Utilities.ParseStr(databaseReader, itemCount);
    //                        //        break;

    //                        //    case MyDataTypes.DATETIME:
    //                        //        itemArray[itemCount] = Utilities.ParseDateTime(databaseReader, itemCount);
    //                        //        break;

    //                        //    case MyDataTypes.GUID:
    //                        //        itemArray[itemCount] = Utilities.ParseGuid(databaseReader, itemCount);
    //                        //        break;
    //                        //}

    //                        itemCount++;
    //                    }

    //                    row.ItemArray = itemArray;
    //                    dataGridTable.Rows.Add(row);
    //                }
    //            }

    //            databaseReader.Close();
    //            databaseConnection.Close();
    //        }

    //        if (fillToCompletePage)
    //        {
    //            if (dataGridTable.Rows.Count < NumberRowsToDisplay)
    //            {
    //                while (NumberRowsToDisplay != dataGridTable.Rows.Count)
    //                {
    //                    itemArray = new object[dataGridObject.NumberColumns];
    //                    row = dataGridTable.NewRow();
    //                    row.ItemArray = itemArray;
    //                    dataGridTable.Rows.Add(row);
    //                }
    //            }
    //        }
    //    }

    //    return dataGridTable;
    //}



}