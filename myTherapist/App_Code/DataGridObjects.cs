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

