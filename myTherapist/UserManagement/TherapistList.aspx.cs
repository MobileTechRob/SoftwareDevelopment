using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataGridObjects;
using System.Web.Configuration;

public partial class UserManagement_AddEditTherapist : System.Web.UI.Control
{
    protected void Page_Load(object sender, EventArgs e)
    {
            
    }

    public void Refresh()
    {
        DataGridObjects.DatabaseRowObject databaseRowObject = new DatabaseRowObject();

        databaseRowObject.AddColumn("TherapistId", "TherapistId", MyDataTypes.GUID, true, 300, false);
        databaseRowObject.AddColumn("TherapistName", "TherapistName", MyDataTypes.STRING, true, 300, false);

        DataGridObjects.DataGridObject gridObject = new DataGridObject(WebConfigurationManager.ConnectionStrings["MyTherapistDatabaseConnectionString"].ConnectionString, "Therapists");

        gridObject.DatabaseRowObject = databaseRowObject;

        therapistlist.DataSource = gridObject.BuildTable();

    }


}

