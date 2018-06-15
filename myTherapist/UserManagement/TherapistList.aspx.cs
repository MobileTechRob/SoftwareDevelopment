using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataGridObjects;
using System.Web.Configuration;
using CommonDefinitions;

public partial class UserManagement_AddEditTherapist : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {       
        AddEditTherapists1.Visible = false;
        Refresh();
    }

    public void Refresh()
    {
        DataGridObjects.DatabaseRowObject databaseRowObject = new DatabaseRowObject();

        databaseRowObject.AddColumn("Id", "Id", MyDataTypes.GUID, true, 300, false);
        databaseRowObject.AddColumn("Name", "Name", MyDataTypes.STRING, true, 300, false);

        DataGridObjects.DataGridObject gridObject = new DataGridObject(WebConfigurationManager.ConnectionStrings[CommonDefinitions.CommonDefinitions.MYTHERAPIST_DATABASE_CONNECTION_STRING].ConnectionString, "Therapists");

        gridObject.DatabaseRowObject = databaseRowObject;
        gridObject.NumberRowsToDisplay = 5;
        gridObject.FillToCompletePage = true;

        if (Session[CommonDefinitions.CommonDefinitions.THERAPIST_LIST_PAGENUMBER] == null)
        {
            gridObject.PageNumber = 1;
            Session[CommonDefinitions.CommonDefinitions.THERAPIST_LIST_PAGENUMBER] = "1";
        }
        else
            gridObject.PageNumber = Int32.Parse(Session[CommonDefinitions.CommonDefinitions.THERAPIST_LIST_PAGENUMBER].ToString());

        therapistlist.DataSource = gridObject.BuildTable();
        therapistlist.RowCreated += Therapistlist_RowCreated;
        therapistlist.RowDataBound += Therapistlist_RowDataBound;
        therapistlist.DataBind();
    }

    private void Therapistlist_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        
    }

    private void Therapistlist_RowCreated(object sender, GridViewRowEventArgs e)
    {
        
    }
}

