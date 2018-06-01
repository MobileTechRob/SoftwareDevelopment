using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataGridObjects;
using System.Web.Configuration;
using System.Data.SqlClient;

public partial class PatientCare_PatientListing : System.Web.UI.UserControl
{
    MyDataGridPager pager = null;
    DatabaseRowObject databaseRowObject = null;
    DataGridObject dataGridObject = null;
   
    protected void Page_Load(object sender, EventArgs e)
    {         
        if (IsPostBack == false)
            btnPreviousPage.Enabled = false;

        LoadGrid();
    }

    public void LoadGrid()
    {
        DatabaseRowObject.DatabaseColumnObject databaseColumnObj = null;

        dataGridObject = new DataGridObject(WebConfigurationManager.ConnectionStrings["MyTherapistDatabaseConnectionString"].ConnectionString, "PatientInformation");        
        databaseRowObject = new DatabaseRowObject();
        databaseColumnObj = new DatabaseRowObject.DatabaseColumnObject();

        databaseColumnObj.DataBaseTableColumnName = "Id";
        databaseColumnObj.DataGridColumnName = "Id";
        databaseColumnObj.DataType = MyDataTypes.INTEGER;        
        databaseColumnObj.IncludeInDataGrid = true;
        databaseColumnObj.OrderByColumn = true;
        databaseColumnObj.Encrypted = false;
        databaseRowObject.AddColumn(databaseColumnObj);

        databaseColumnObj = new DatabaseRowObject.DatabaseColumnObject();
        databaseColumnObj.DataBaseTableColumnName = "FirstName";
        databaseColumnObj.DataGridColumnName = "First Name";
        databaseColumnObj.DataType = MyDataTypes.STRING;        
        databaseColumnObj.IncludeInDataGrid = true;
        databaseColumnObj.OrderByColumn = false;
        databaseColumnObj.Encrypted = false;
        databaseRowObject.AddColumn(databaseColumnObj);

        databaseColumnObj = new DatabaseRowObject.DatabaseColumnObject();
        databaseColumnObj.DataBaseTableColumnName = "LastName";
        databaseColumnObj.DataGridColumnName = "Last Name";
        databaseColumnObj.DataType = MyDataTypes.STRING;        
        databaseColumnObj.IncludeInDataGrid = true;
        databaseColumnObj.OrderByColumn = false;
        databaseColumnObj.Encrypted = false;
        databaseRowObject.AddColumn(databaseColumnObj);

        databaseColumnObj = new DatabaseRowObject.DatabaseColumnObject();
        databaseColumnObj.DataBaseTableColumnName = "BirthDate";
        databaseColumnObj.DataGridColumnName = "BirthDate";
        databaseColumnObj.DataType = MyDataTypes.STRING;
        databaseColumnObj.IncludeInDataGrid = true;
        databaseColumnObj.OrderByColumn = false;
        databaseColumnObj.Encrypted = true;
        databaseRowObject.AddColumn(databaseColumnObj);

        databaseColumnObj = new DatabaseRowObject.DatabaseColumnObject();
        databaseColumnObj.DataBaseTableColumnName = "EmailAddress";
        databaseColumnObj.DataGridColumnName = "Email Address";
        databaseColumnObj.DataType = MyDataTypes.STRING;        
        databaseColumnObj.IncludeInDataGrid = true;
        databaseColumnObj.OrderByColumn = false;
        databaseColumnObj.Encrypted = true;
        databaseRowObject.AddColumn(databaseColumnObj);

        databaseColumnObj = new DatabaseRowObject.DatabaseColumnObject();
        databaseColumnObj.DataBaseTableColumnName = "TelephoneNumber";
        databaseColumnObj.DataGridColumnName = "Telephone Number";
        databaseColumnObj.DataType = MyDataTypes.STRING;        
        databaseColumnObj.IncludeInDataGrid = true;
        databaseColumnObj.OrderByColumn = false;
        databaseColumnObj.Encrypted = true;
        databaseRowObject.AddColumn(databaseColumnObj);

        dataGridObject.DatabaseRowObject = databaseRowObject;
        dataGridObject.FillToCompletePage = true;
        dataGridObject.PageNumber = 1;
        dataGridObject.NumberRowsToDisplay = 15;

        pager = new MyDataGridPager();

        pager.AddColumn("Id", 15);
        pager.AddColumn("First Name", 25);
        pager.AddColumn("Last Name", 250);
        pager.AddColumn("Email Address", 250);
        pager.AddColumn("Telephone Number", 100);       
       
        if (txtBoxPatientFirstName.Text.Length > 0)
            dataGridObject.AddWhereClauseArgument("FirstName", txtBoxPatientFirstName.Text);

        if (txtBoxPatientLastName.Text.Length > 0)
            dataGridObject.AddWhereClauseArgument("LastName", txtBoxPatientLastName.Text);

        if (IsPostBack == false)
        {
            dataGridObject.PageNumber = 1;
            Session["PatientList_PageNumber"] = "1";
        }
        else
            dataGridObject.PageNumber = Int32.Parse(Session["PatientList_PageNumber"].ToString());

        patientlistgridview.DataSource = dataGridObject.BuildTable();
        patientlistgridview.RowDataBound += Patientlistgridview_RowDataBound;        
        patientlistgridview.SelectedIndexChanged += Patientlistgridview_SelectedIndexChanged;
        patientlistgridview.DataBind();

        if (dataGridObject.NumberOfCompletedPages == 0)
          btnNextPAge.Enabled = false;     
        else
          btnNextPAge.Enabled = true;
    }

    private void Patientlistgridview_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridView gridView = (GridView)sender;
        int selectedIndex = gridView.SelectedIndex;
        int result = 0;

        if (Int32.TryParse(patientlistgridview.Rows[selectedIndex].Cells[0].Text, out result))
        {
            int patientID = Int32.Parse(patientlistgridview.Rows[selectedIndex].Cells[0].Text);
            string patientName = patientlistgridview.Rows[selectedIndex].Cells[1].Text;

            for (int index = 0; index < patientlistgridview.Rows.Count; index++)
                patientlistgridview.Rows[index].BackColor = System.Drawing.ColorTranslator.FromHtml("#F4A460");

            patientlistgridview.Rows[selectedIndex].BackColor = System.Drawing.ColorTranslator.FromHtml("#F9cda8");

            Session["PatientID"] = patientlistgridview.Rows[selectedIndex].Cells[0].Text;
            Session["PatientFirstName"] = patientlistgridview.Rows[selectedIndex].Cells[1].Text;
            Session["PatientLastName"] = patientlistgridview.Rows[selectedIndex].Cells[2].Text;
            Session["PatientBirthDate"] = patientlistgridview.Rows[selectedIndex].Cells[3].Text;
        }
    }

    private void Patientlistgridview_RowDataBound(object sender, GridViewRowEventArgs e)
    {        
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(patientlistgridview,"Select$" + e.Row.RowIndex);            
            e.Row.Cells[0].Width = pager.ColumnWidth(1);
            e.Row.Cells[1].Width = pager.ColumnWidth(2);
            e.Row.Cells[2].Width = pager.ColumnWidth(3);
            e.Row.Cells[3].Width = pager.ColumnWidth(4);
        }
    }

    protected void btnPreviousPage_Click(object sender, EventArgs e)
    {        
        string page = Session["PatientList_PageNumber"].ToString();
        int pageNumber = Int32.Parse(page);
        pageNumber -= 1;

        Session["PatientList_PageNumber"] = pageNumber.ToString();

        if (pageNumber == 1)
            btnPreviousPage.Enabled = false;
        else
            btnPreviousPage.Enabled = true;

        btnNextPAge.Enabled = true;

        if (pager.NumberOfCompletePages < pageNumber)
            btnNextPAge.Enabled = false;

        LoadGrid();      
    }

    protected void btnNextPAge_Click(object sender, EventArgs e)
    {
        string page = Session["PatientList_PageNumber"].ToString();
        int pageNumber = Int32.Parse(page);
        pageNumber += 1;

        Session["PatientList_PageNumber"] = pageNumber.ToString();

        LoadGrid();
        btnPreviousPage.Enabled = true;
        btnNextPAge.Enabled = true;

        if (pager.NumberOfCompletePages < pageNumber)
            btnNextPAge.Enabled = false;
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {

    }

}


