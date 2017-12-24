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

    private void LoadGrid()
    {
        pager = new MyDataGridPager("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\SoftwareDevelopmentProjects\\WebProjects\\myTherapist\\myTherapist\\App_Data\\myTherapist.mdf;Integrated Security=True", "PatientInformation");
        pager.AddColumn("Id", "Id", MyDataTypes.INTEGER, true);
        pager.AddColumn("Name", "Name", MyDataTypes.STRING, false);
        pager.AddColumn("EmailAddress", "Email Address", MyDataTypes.STRING, false);
        pager.AddColumn("TelephoneNumber", "Telephone Number", MyDataTypes.STRING, false);
        pager.NumberRowsToDisplay = 5;

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
    }


    private void Patientlistgridview_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridView gridView = (GridView)sender;
        int selectedIndex = gridView.SelectedIndex;
    }

    private void Patientlistgridview_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(patientlistgridview,"Select$" + e.Row.RowIndex);
            e.Row.Attributes["style"] = "cursor:pointer";
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
    }
}


