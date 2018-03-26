using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;


namespace DataGridObjects
{
    public enum MyDataTypes
    {
        INTEGER,
        STRING,
        DATETIME,
        GUID,
        IMAGE
    }

    public enum MyDataSort
    {
        ASC,
        DESC
    }

    public class DataCellDisplay
    {
        public string DatabaseColumnName { get; set; }
        public int ColumnOrder { get; set; }
        public MyDataTypes MyDataType { get; set; }
    }

    // represents what one row of value from the database.
    public class DataRowDisplay : IEnumerable<DataCellDisplay>
    {
        private List<DataCellDisplay> cells = new List<DataCellDisplay>();
        public int NumberItems { get { return cells.Count; } }

        public void Add(DataCellDisplay cell)
        {
            cells.Add(cell);
        }

        public IEnumerator<DataCellDisplay> GetEnumerator()
        {
            return cells.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return cells.GetEnumerator();
        }        
    }

    // represents what one row in the grid looks like.
    public class GridRowObject : IEnumerable<DataRowDisplay>
    {
        private List<DataRowDisplay> rows = new List<DataRowDisplay>();


        public int NumberDataRows { get { return rows.Count; } }
        public DataRowDisplay Row(int rowNumber)
        {
            if ((rowNumber >= 1) && (rowNumber <= rows.Count))
                return rows[rowNumber - 1];

            return null;
        }

        public void Add(DataRowDisplay row)
        {
            rows.Add(row);
        }

        public IEnumerator<DataRowDisplay> GetEnumerator()
        {
            return rows.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return rows.GetEnumerator();
        }
    }

    /// <summary>
    /// Summary description for DataGridObjects
    /// </summary>
    public class DataGridObject : IEnumerable<DataGridObject.MyDataGridColumn>
    {
        public class MyDataGridColumn
        {
            public string DataBaseTableColumnName { get; set; }
            public bool IncludeInDataGrid { get; set; }
            public string DataGridColumnName { get; set; }
            public bool OrderByColumn { get; set; }
            public int Column { get; set; }
            public MyDataTypes DataType { get; set; }
            public int HeaderWidth { get; set; }

            public MyDataGridColumn()
            {

            }
        }

        private SortedDictionary<int, DataGridObject.MyDataGridColumn> columnDictionary;
        private List<DataGridObject.MyDataGridColumn> columnList;

   
        public int NumberColumns { get { return columnDictionary.Count; } }

        public DataGridObject()
        {
            //
            // TODO: Add constructor logic here
            //
            columnDictionary = new SortedDictionary<int, DataGridObject.MyDataGridColumn>();
            columnList = new List<MyDataGridColumn>();         
        }

        public SortedDictionary<int, DataGridObject.MyDataGridColumn>.ValueCollection GetColumnObjectCollection()
        {
            return columnDictionary.Values;
        }

        public void AddColumn(MyDataGridColumn dataColumn)
        {
            columnDictionary.Add(columnDictionary.Count + 1, dataColumn);
            columnList.Add(dataColumn);
        }

        public MyDataGridColumn GetDataGridColumn(int column)
        {
            MyDataGridColumn col = null;

            columnDictionary.TryGetValue(column, out col);

            return col;
        }

        public bool ContainColumn(int column)
        {
            return columnDictionary.ContainsKey(column);
        }

        public IEnumerator<MyDataGridColumn> GetEnumerator()
        {            
            return columnList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return columnList.GetEnumerator();
        }
    }
}



