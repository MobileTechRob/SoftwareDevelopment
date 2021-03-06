﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using System.Data.SqlClient;


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

    public enum MyDisplayTypes
    {
        INTEGER,
        STRING,
        DATE,
        DATETIME
    }

    public enum MyDataSort
    {
        ASC,
        DESC
    }

    public class DataCellDisplay
    {
        public string DatabaseColumnName { get; set; }
        //public int ColumnOrder { get; set; }
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

    // represents what one "row" in the grid looks like.
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

    public class DataGridObject
    {
        private System.Data.SqlClient.SqlConnection databaseConnection;
        private System.Data.SqlClient.SqlCommand databaseCommand;
        private System.Data.SqlClient.SqlDataReader databaseReader;
        private MyTherapistEncryption.SecurityController dataEncryptionAlgorithm = null;

        private string databaseTableName;
        private DataTable dataGridTable = null;
        private StringBuilder whereClause = null;

        public DatabaseRowObject DatabaseRowObject { get;set;}  
        public int PageNumber { get; set; }        
        public MyDataSort Sort { get; set; }
        public int NumberRowsToDisplay { get; set; }
        public bool FillToCompletePage { get; set; }        
        public string WhereClause { get; set; }
        public int NumberOfCompletedPages { get; set; }

        public DataGridObject(string connnectionString, string tableName)
        {
            databaseConnection = new SqlConnection(connnectionString);
            databaseTableName = tableName;
            dataGridTable = new DataTable();
            whereClause = new StringBuilder();
            
            dataEncryptionAlgorithm = new MyTherapistEncryption.SecurityController();
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

        public DataTable BuildTable(DataGridObjects.GridRowObject gridInfo = null)
        {
            int _totalNumberOfTableRows = 0;
            StringBuilder sqlCommandString = null;
            StringBuilder sqlSelectClause = null;
            string orderByColumnName = string.Empty;
            char[] parms = { ',' };
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

                NumberOfCompletedPages = _totalNumberOfTableRows / Math.Max(NumberRowsToDisplay, 10);

                sqlSelectClause.Append("SELECT ");

                IEnumerator<DatabaseRowObject.DatabaseColumnObject> iter = DatabaseRowObject.GetEnumerator();

                DataColumn dataColumn = null;
                DataGridObjects.DatabaseRowObject.DatabaseColumnObject column = null;

                while (iter.MoveNext())
                {
                    dataColumn = new DataColumn();                        
                    column = iter.Current;

                    dataColumn.ColumnName = column.DataGridColumnName;

                    if (column.DataType == MyDataTypes.INTEGER)
                        dataColumn.DataType = System.Type.GetType("System.Int32");

                    if (column.DataType == MyDataTypes.STRING)
                        dataColumn.DataType = System.Type.GetType("System.String");

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

                ///////// now check for joins
                iter.Reset();

                while (iter.MoveNext())
                {
                    if (string.IsNullOrEmpty(iter.Current.JoinTable) == false)
                    {
                        sqlCommandString.Append(" ");
                        sqlCommandString.Append(" INNER JOIN  ");
                        sqlCommandString.Append(iter.Current.JoinTable);
                        sqlCommandString.Append(" ON ");
                        sqlCommandString.Append(iter.Current.JoinColumnMainTable);
                        sqlCommandString.Append(" = ");
                        sqlCommandString.Append(iter.Current.JoinColumnJoinTable);

                    }
                }
                
                ////////

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

                databaseCommand.CommandText = sqlCommandString.ToString();
                databaseCommand.Connection = databaseConnection;
                databaseReader = databaseCommand.ExecuteReader();
                string fieldData = string.Empty;
                tlist = new List<string>();

                DataRow row = null;
                object[] itemArray = null;
                int itemCount = 0;
                DatabaseRowParser rowParser = null;
                rowParser = new DatabaseRowParser(DatabaseRowObject, dataEncryptionAlgorithm);
                DataRowDisplay rowToDisplay = null;
                
                if (databaseReader.HasRows)
                {
                    while (databaseReader.Read())
                    {
                        
                        // for each row in the grid info object                        
                        // iterate through the columns and find that column in the the row from the database
                        // place that value of the column in the row for the data grid object 

                        // else a row from the database is a row in the datagridview
                        row = dataGridTable.NewRow();
                        itemCount = 0;

                        itemArray = new object[DatabaseRowObject.NumberColumns];
                        iter = DatabaseRowObject.GetEnumerator();

                        column = null;
                        rowParser = new DatabaseRowParser(DatabaseRowObject, dataEncryptionAlgorithm);
                        rowParser.sqlReader = databaseReader;


                        while (iter.MoveNext())
                        {
                           column = iter.Current;
                           itemArray[itemCount] = rowParser.GetValue(column.DataBaseTableColumnName);
                           itemCount++;
                        }

                        row.ItemArray = itemArray;
                        dataGridTable.Rows.Add(row);                        
                    }

                    databaseReader.Close();
                    databaseConnection.Close();
                }

                if (FillToCompletePage)
                {
                    if (dataGridTable.Rows.Count < NumberRowsToDisplay)
                    {
                        while (NumberRowsToDisplay != dataGridTable.Rows.Count)
                        {
                            itemArray = new object[DatabaseRowObject.NumberColumns];
                            row = dataGridTable.NewRow();
                            row.ItemArray = itemArray;
                            dataGridTable.Rows.Add(row);
                        }
                    }
                }
            }

            return dataGridTable;
        }

        //public void SetColumnWidth(string columnName, int gridwidth)
        //{
        //    List<DatabaseRowObject.DatabaseColumnObject>.Enumerator iter;
           
        //    iter = DatabaseRowObject.GetColumnObjectCollection().GetEnumerator();

        //    while (iter.MoveNext())
        //    {
        //        if (iter.Current.DataBaseTableColumnName == columnName)
        //            iter.Current.ColumnWidth = gridwidth;
        //    }
        //}

        public int ColumnWidth(string columnName)
        {
            int columnWidth = 0;           
            SortedDictionary<int, DatabaseRowObject.DatabaseColumnObject>.ValueCollection.Enumerator iter;

            iter = this.DatabaseRowObject.GetColumnObjectCollection().GetEnumerator();
                       
            while (iter.MoveNext())
            {
                if (iter.Current.DataBaseTableColumnName == columnName)
                    columnWidth = iter.Current.ColumnWidth;
            }

            return columnWidth;
        }
    }
    
    /// <summary>
    /// Summary description for DataGridObjects
    /// </summary>
    public class DatabaseRowObject : IEnumerable<DatabaseRowObject.DatabaseColumnObject>
    {
        public class DatabaseColumnObject
        {
            public string DataBaseTableColumnName { get; set; }
            public bool IncludeInDataGrid { get; set; }
            public string DataGridColumnName { get; set; }
            public string JoinTable { get; set; }
            public string JoinColumnMainTable { get; set; }
            public string JoinColumnJoinTable { get; set; }
            public bool OrderByColumn { get; set; }
            public int Column { get; set; }
            public int ColumnWidth { get; set; }

            private MyDataTypes _dataType;
            public MyDataTypes DataType
            {
                get { return _dataType; }   
                set
                {
                    _dataType = value;

                    if (value == MyDataTypes.INTEGER)
                    {
                        DisplayType = MyDisplayTypes.INTEGER;
                    }
                    else if (value == MyDataTypes.STRING)
                    {
                        DisplayType = MyDisplayTypes.STRING;
                    }
                }
            }

            public MyDisplayTypes DisplayType { get; set; }
            public bool Encrypted { get; set; }
            
            public DatabaseColumnObject()
            {

            }
        }

        private SortedDictionary<int, DatabaseRowObject.DatabaseColumnObject> columnDictionary;
        private List<DatabaseRowObject.DatabaseColumnObject> columnList;

   
        public int NumberColumns { get { return columnDictionary.Count; } }

        public DatabaseRowObject()
        {
            //
            // TODO: Add constructor logic here
            //
            columnDictionary = new SortedDictionary<int, DatabaseRowObject.DatabaseColumnObject>();
            columnList = new List<DatabaseColumnObject>();         
        }

        public SortedDictionary<int, DatabaseRowObject.DatabaseColumnObject>.ValueCollection GetColumnObjectCollection()
        {
            return columnDictionary.Values;
        }

        public void AddColumn(string databaseColumnName, string dataGridColumnName, MyDataTypes dataType, bool orderByColumn, int columnInGridWidth, bool encrypted, bool includeInGrid = true)
        {
            DatabaseColumnObject colObj = new DatabaseColumnObject();
            colObj.DataBaseTableColumnName = databaseColumnName;
            colObj.DataGridColumnName = dataGridColumnName;
            colObj.DataType = dataType;            
            colObj.IncludeInDataGrid = includeInGrid;
            colObj.OrderByColumn = orderByColumn;
            colObj.Column = columnDictionary.Count;
            colObj.Encrypted = encrypted;
            colObj.ColumnWidth = columnInGridWidth;

            columnDictionary.Add(columnDictionary.Count + 1, colObj);
            columnList.Add(colObj);
        }

        public void AddColumn(string databaseColumnName, string dataGridColumnName, MyDataTypes dataType, bool orderByColumn, int columnInGridWidth, bool encrypted, string joinTable, string joinColumnMainTable, string joinColumnJoinTable, bool includeInGrid = true)
        {
            DatabaseColumnObject colObj = new DatabaseColumnObject();
            colObj.DataBaseTableColumnName = databaseColumnName;
            colObj.DataGridColumnName = dataGridColumnName;
            colObj.DataType = dataType;
            colObj.IncludeInDataGrid = includeInGrid;
            colObj.OrderByColumn = orderByColumn;
            colObj.Column = columnDictionary.Count;
            colObj.Encrypted = encrypted;
            colObj.ColumnWidth = columnInGridWidth;
            colObj.JoinTable = joinTable;
            colObj.JoinColumnMainTable = joinColumnMainTable;
            colObj.JoinColumnJoinTable = joinColumnJoinTable;

            columnDictionary.Add(columnDictionary.Count + 1, colObj);
            columnList.Add(colObj);
        }

        public void AddColumn(DatabaseColumnObject dataColumn)
        {
            dataColumn.Column = columnDictionary.Count;
            columnDictionary.Add(columnDictionary.Count + 1, dataColumn);
            columnList.Add(dataColumn);
        }
        
        public int ColumnIndex(string columnName)
        {
            List<DatabaseColumnObject>.Enumerator iter;

            iter = columnList.GetEnumerator();

            while (iter.MoveNext())
            {
                if (iter.Current.DataBaseTableColumnName == columnName)
                    return iter.Current.Column;                                    
            }

            return -1;
        }


        public DatabaseColumnObject GetDataGridColumn(int column)
        {
            DatabaseColumnObject col = null;

            columnDictionary.TryGetValue(column, out col);

            return col;
        }

        public bool ContainColumn(int column)
        {
            return columnDictionary.ContainsKey(column);
        }

        public IEnumerator<DatabaseColumnObject> GetEnumerator()
        {            
            return columnList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return columnList.GetEnumerator();
        }
    }
}



