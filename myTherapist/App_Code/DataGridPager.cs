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
    //private SortedDictionary<int, DataGridObject.MyDataGridColumn> columnDictionary;
    private SortedDictionary<string, int> gridIndexByColumnName;
    private bool fillToCompletePage = false;
    private DataGridObject dataGridObject;

    private System.Data.SqlClient.SqlConnection databaseConnection;
    private System.Data.SqlClient.SqlCommand databaseCommand;
    private System.Data.SqlClient.SqlDataReader databaseReader;
    StringBuilder whereClause = null;


    public MyDataGridPager(string connnectionString, string tableName, bool fillToCompletePage, int rowsToDisplay = 15)
    {
        //
        // TODO: Add constructor logic here
        //

        dataGridTable = new DataTable();
        dataGridObject = new DataGridObject();

        //columnDictionary = new SortedDictionary<int, DataGridObject.MyDataGridColumn>();
        gridIndexByColumnName = new SortedDictionary<string, int>();
        databaseConnection = new System.Data.SqlClient.SqlConnection(connnectionString);
        databaseTableName = tableName;
        Sort = MyDataSort.ASC;
        PageNumber = 1;
        NumberRowsToDisplay = rowsToDisplay;
        whereClause = new StringBuilder();
        this.fillToCompletePage = fillToCompletePage;
    }

    public void AddColumn(string databaseColumnName, string dataGridColumnName, MyDataTypes dataType, bool orderByColumn, int gridWidth, bool includeInGrid = true)
    {
        DataGridObject.MyDataGridColumn column = new DataGridObject.MyDataGridColumn();
        column.DataBaseTableColumnName = databaseColumnName;
        column.DataGridColumnName = dataGridColumnName;
        column.OrderByColumn = orderByColumn;
        column.DataType = dataType;
        column.HeaderWidth = gridWidth;
        column.IncludeInDataGrid = includeInGrid;

        dataGridObject.AddColumn(column);
        
        gridIndexByColumnName.Add(databaseColumnName, gridIndexByColumnName.Count);
    }

    public void AddWhereClauseArgument(string fieldName, string fieldValue)
    {        
        whereClause.Append(fieldName);
        whereClause.Append(" LIKE '");
        whereClause.Append("%");
        whereClause.Append(fieldValue);
        whereClause.Append("%");
        whereClause.Append("' AND ");
    }

    public int GridIndexByColumnName(string columnName)
    {
        int index;

        gridIndexByColumnName.TryGetValue(columnName, out index);

        return index;
    }

    public int ColumnWidth(int column)
    {
        if (dataGridObject.ContainColumn(column))
            return dataGridObject.GetDataGridColumn(column).HeaderWidth;
        else
            return 10;
    }

    public DataTable BuildTable(DataGridObjects.GridRowObject gridInfo = null)
    {
        StringBuilder sqlCommandString = null;
        StringBuilder sqlSelectClause = null;
        string orderByColumnName = string.Empty;
        char[] parms = {','};
        List<string> tlist = null;
        string wherePhrase = string.Empty;

        databaseConnection.Open();

        if (databaseConnection.State == ConnectionState.Open)
        {
            databaseCommand = new System.Data.SqlClient.SqlCommand();
            sqlSelectClause = new StringBuilder();

            sqlSelectClause.Append("SELECT Count(*) FROM ");
            sqlSelectClause.Append(databaseTableName);

            databaseCommand.CommandText = sqlSelectClause.ToString();
            databaseCommand.Connection = databaseConnection;
            _totalNumberOfTableRows = (int)databaseCommand.ExecuteScalar();
            sqlSelectClause.Clear();

            _numberOfCompletePages = _totalNumberOfTableRows / NumberRowsToDisplay;

            sqlSelectClause.Append("SELECT ");

            IEnumerator<DataGridObject.MyDataGridColumn> iter = dataGridObject.GetEnumerator();

            //foreach (DataGridObject.MyDataGridColumn column in columnDictionary.Values)

            while (iter.MoveNext())
            {
                DataColumn dataColumn = new DataColumn();
                DataGridObjects.DataGridObject.MyDataGridColumn column;
                column = iter.Current;

                dataColumn.ColumnName = column.DataGridColumnName;

                if (column.DataType == MyDataTypes.INTEGER)
                    dataColumn.DataType = System.Type.GetType("System.Int32");

                if (column.DataType == MyDataTypes.STRING)
                    dataColumn.DataType = System.Type.GetType("System.String");

                if (column.DataType == MyDataTypes.DATETIME)
                    dataColumn.DataType = System.Type.GetType("System.DateTime");

                if (column.DataType == MyDataTypes.GUID)
                    dataColumn.DataType = System.Type.GetType("System.Guid");
                                                
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

            if (whereClause.Length > 0)
            {
                wherePhrase = whereClause.ToString().TrimEnd(new char[] { ' ', 'A', 'N', 'D' });
                sqlCommandString.Append(" WHERE ");
                sqlCommandString.Append(wherePhrase);
            }

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
            int numberOfColumnsInDataRow = 0;
            int numberDataRows = 0;

            if (databaseReader.HasRows)
            {                           
                while (databaseReader.Read())
                {
                    // if there is a gridInfo object then 

                    if (gridInfo != null)
                    {
                        IEnumerator<DataRowDisplay> rowIter = gridInfo.GetEnumerator();

                        while (rowIter.MoveNext())
                        {
                            DataRowDisplay rowToDisplay = rowIter.Current;

                            IEnumerator<DataCellDisplay> columnIter = rowToDisplay.GetEnumerator();

                            while (columnIter.MoveNext())
                            {
                                DataCellDisplay cellToDisplay = columnIter.Current;                                
                            }

                        }
                    }
                    else
                    {
                        // for each row in the grid info object                        
                        // iterate through the columns and find that column in the the row from the database
                        // place that value of the column in the row for the data grid object 

                        // else a row from the database is a row in the datagridview
                        row = dataGridTable.NewRow();
                        itemCount = 0;

                        itemArray = new object[dataGridObject.NumberColumns];

                        iter = dataGridObject.GetEnumerator();

                        //foreach (DataGridObject.MyDataGridColumn column in columnDictionary.Values)
                        //   foreach (DataGridObject.MyDataGridColumn column in columnDictionary.Values)

                        DataGridObject.MyDataGridColumn column = null;

                        while (iter.MoveNext())
                        {
                            column = iter.Current;

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

                                case MyDataTypes.GUID:
                                    itemArray[itemCount] = Utilities.ParseGuid(databaseReader, itemCount);
                                    break;
                            }

                            itemCount++;
                        }

                        row.ItemArray = itemArray;
                        dataGridTable.Rows.Add(row);
                    }
                }

                databaseReader.Close();
                databaseConnection.Close();
            }

            if (fillToCompletePage)
            {
                if (dataGridTable.Rows.Count < NumberRowsToDisplay)
                {
                    while (NumberRowsToDisplay != dataGridTable.Rows.Count)
                    {
                        itemArray = new object[dataGridObject.NumberColumns];
                        row = dataGridTable.NewRow();
                        row.ItemArray = itemArray;
                        dataGridTable.Rows.Add(row);
                    }
                }
            }
        }

        return dataGridTable;
    }
}