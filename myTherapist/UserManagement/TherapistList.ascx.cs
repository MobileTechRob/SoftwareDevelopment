﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataGridObjects;

public partial class UserManagement_TherapistList : System.Web.UI.UserControl
{
    DataGridObjects.DataGridObject gridObject = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        Refresh();
    }

    public void Refresh()
    {
        DataGridObjects.DatabaseRowObject databaseRowObject = new DatabaseRowObject();

        databaseRowObject.AddColumn("Id", "Id", MyDataTypes.GUID, true, 150, false, false);
        databaseRowObject.AddColumn("Name", "Name", MyDataTypes.STRING, true, 350, true);

        gridObject = new DataGridObject(WebConfigurationManager.ConnectionStrings[CommonDefinitions.CommonDefinitions.MYTHERAPIST_DATABASE_CONNECTION_STRING].ConnectionString, "Therapists");

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
        therapistlist.SelectedIndexChanged += Therapistlist_SelectedIndexChanged;
        therapistlist.DataBind();
    }

    private void Therapistlist_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridView gridView = (GridView)sender;
        int selectedIndex = gridView.SelectedIndex;
        int result = 0;

        if (Int32.TryParse(therapistlist.Rows[selectedIndex].Cells[0].Text, out result))
        {
            int patientID = Int32.Parse(therapistlist.Rows[selectedIndex].Cells[0].Text);
            string patientName = therapistlist.Rows[selectedIndex].Cells[1].Text;

            for (int index = 0; index < therapistlist.Rows.Count; index++)
                therapistlist.Rows[index].BackColor = System.Drawing.ColorTranslator.FromHtml("#F4A460");

            therapistlist.Rows[selectedIndex].BackColor = System.Drawing.ColorTranslator.FromHtml("#F9cda8");

            Session["TherapistId"] = therapistlist.Rows[selectedIndex].Cells[0].Text;
        }

    }

    private void Therapistlist_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(therapistlist, "Select$" + e.Row.RowIndex);
            e.Row.Cells[0].Visible = false;
            e.Row.Cells[1].Width = gridObject.ColumnWidth("Name");
        }
    }

    private void Therapistlist_RowCreated(object sender, GridViewRowEventArgs e)
    {
        e.Row.Cells[0].Visible = false;
    }
}