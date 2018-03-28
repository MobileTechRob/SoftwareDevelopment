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
}