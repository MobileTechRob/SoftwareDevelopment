using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using DataGridObjects;
using MyTherapistEncryption;

/// <summary>
/// Summary description for DatabaseRowParser
/// </summary>
public class DatabaseRowParser
{
    public System.Data.SqlClient.SqlDataReader sqlReader { get; set; }
    private DataGridObjects.DatabaseRowObject databaseRowObj;
    private MyTherapistEncryption.SecurityController dataEncryptionAlgorithm;

    public DatabaseRowParser(DataGridObjects.DatabaseRowObject databaseRowObj, MyTherapistEncryption.SecurityController dataEncryptionAlgorithm)
    {
        //
        // TODO: Add constructor logic here
        //
        this.sqlReader = sqlReader;
        this.databaseRowObj = databaseRowObj;
        this.dataEncryptionAlgorithm = dataEncryptionAlgorithm;
    }
    
    public object GetValue(string fieldName)
    {
        IEnumerator<DatabaseRowObject.DatabaseColumnObject> iter = databaseRowObj.GetEnumerator();
        DatabaseRowObject.DatabaseColumnObject col = null;

        while (iter.MoveNext())
        {              
            if (iter.Current.DataBaseTableColumnName == fieldName) 
                break;
        }

        col = iter.Current;
        object retValue = null;
        object columnValue = null;

        switch (col.DataType)
        {
            case MyDataTypes.INTEGER:                
                columnValue = Utilities.ParseInt32(sqlReader, col.Column);
                break;

            case MyDataTypes.STRING:                
                columnValue = Utilities.ParseStr(sqlReader, col.Column);
                break;

            case MyDataTypes.DATETIME:
                columnValue = Utilities.ParseDateTime(sqlReader, col.Column);
                break;
                
            case MyDataTypes.GUID:
                columnValue = Utilities.ParseGuid(sqlReader, col.Column);
                break;
        }
        
        if (col.Encrypted)
        {
            retValue = dataEncryptionAlgorithm.DecryptObj(columnValue);

            if (col.DisplayType == MyDisplayTypes.DATE)
            {
                DateTime dt = DateTime.Parse(retValue.ToString());
                retValue = (object)String.Format("{0}/{1}/{2}", dt.Month.ToString(), dt.Day.ToString(), dt.Year.ToString()); ;
            }

            return retValue;
        }            
        else
            return columnValue;
    }    
}

