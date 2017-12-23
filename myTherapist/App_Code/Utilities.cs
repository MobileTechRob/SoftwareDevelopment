using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

/// <summary>
/// Summary description for Utilities
/// </summary>
public static class Utilities
{

    public static string ParseStr(System.Data.SqlClient.SqlDataReader rdr, int index)
    {
        string data = "";

        try
        {
            data = rdr.GetString(index);
        }
        catch (Exception ex)
        {
            data = "";
            ex.GetHashCode();
        }

        return data;
    }

    public static int ParseInt32(System.Data.SqlClient.SqlDataReader rdr, int index)
    {
        int data = 0;

        try
        {
            data = rdr.GetInt32(index);
        }
        catch (Exception ex)
        {
            data = 0;
            ex.GetHashCode();
        }

        return data;
    }

    public static DateTime ParseDateTime(System.Data.SqlClient.SqlDataReader rdr, int index)
    {
        DateTime data= DateTime.Now;

        try
        {
            data = rdr.GetDateTime(index);
        }
        catch (Exception ex)
        {
            data = DateTime.MinValue;
            ex.GetHashCode();
        }

        return data;
    }

}