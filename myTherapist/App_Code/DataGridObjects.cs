using System;
using System.Collections.Generic;
using System.Linq;
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
    }
    
    // represents what one row in the grid looks like.
    public class DataRowDisplay
    {
        List<DataCellDisplay> cells = new List<DataCellDisplay>();

        public void Add(DataCellDisplay cell)
        {
            cells.Add(cell);
        }
    }

    public class GridRowObject
    {
        private List<DataRowDisplay> rows = new List<DataRowDisplay>();

        public int NumberDataRows {get { return rows.Count; } }
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
    }

    /// <summary>
    /// Summary description for DataGridObjects
    /// </summary>
    public class DataGridObject
    {
        public DataGridObject()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public class MyDataGridColumn
        {
            public string DataBaseTableColumnName { get; set; }
            public bool IncludeInDataGrid { get; set; }
            public string DataGridColumnName { get; set; }
            public bool OrderByColumn { get; set; }
            public MyDataTypes DataType { get; set; }
            public int HeaderWidth { get; set; }

            public MyDataGridColumn()
            {

            }
        }
    }
}

