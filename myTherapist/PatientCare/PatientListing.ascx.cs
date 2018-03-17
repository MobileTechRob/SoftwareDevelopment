using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class PatientCare_PatientListing : System.Web.UI.UserControl
{
    MyDataGridPager pager = null;

    protected void Page_Load(object sender, EventArgs e)
    {         
        if (IsPostBack == false)
            btnPreviousPage.Enabled = false;

        LoadGrid();
    }

    public void LoadGrid()
    {
        pager = new MyDataGridPager("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\SoftwareDevelopmentProjects\\WebProjects\\myTherapist\\myTherapist\\App_Data\\myTherapist.mdf;Integrated Security=True", "PatientInformation");
        pager.AddColumn("Id", "Id", MyDataTypes.INTEGER, true, 35);
        pager.AddColumn("FirstName", "First Name", MyDataTypes.STRING, false, 250);
        pager.AddColumn("LastName", "Last Name", MyDataTypes.STRING, false, 250);
        pager.AddColumn("EmailAddress", "Email Address", MyDataTypes.STRING, false, 250);
        pager.AddColumn("TelephoneNumber", "Telephone Number", MyDataTypes.STRING, false, 100);

        if (txtBoxPatientFirstName.Text.Length > 0)
           pager.AddWhereClauseArgument("FirstName", txtBoxPatientFirstName.Text);

        if (txtBoxPatientLastName.Text.Length > 0)
            pager.AddWhereClauseArgument("LastName", txtBoxPatientLastName.Text);

        if (IsPostBack == false)
        {
            pager.PageNumber = 1;
            Session["PatientList_PageNumber"] = "1";
        }
        else
            pager.PageNumber = Int32.Parse(Session["PatientList_PageNumber"].ToString());

        patientlistgridview.DataSource = pager.BuildTable();
        patientlistgridview.RowDataBound += Patientlistgridview_RowDataBound;        
        patientlistgridview.SelectedIndexChanged += Patientlistgridview_SelectedIndexChanged;
        patientlistgridview.DataBind();

        if (pager.NumberOfCompletePages == 0)
          btnNextPAge.Enabled = false;        
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
            Session["PatiendLastName"] = patientlistgridview.Rows[selectedIndex].Cells[2].Text;
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


