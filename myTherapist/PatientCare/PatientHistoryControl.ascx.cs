using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PatientCare_PatientHistoryControl : System.Web.UI.UserControl
{
    //System.Data.DataTable patientHistory = null;
    MyDataGridPager pager = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["PatientName"] != null)
            lblPatientName.Text = Session["PatientName"].ToString();

        if (Session["PatientId"] != null)
        {
            BuildGrid();
        }
        else
            Session["PatientHistorySortOrder"] = "DESC";
    }

    private void BuildGrid()
    {
        pager = new MyDataGridPager("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\SoftwareDevelopmentProjects\\WebProjects\\myTherapist\\myTherapist\\App_Data\\myTherapist.mdf;Integrated Security=True", "PatientAppointmentInformation");
        pager.NumberRowsToDisplay = 10;
        pager.AddColumn("ApptDate", "Appointment Date", MyDataTypes.DATETIME, true, 35);
        pager.AddColumn("PatientId", "", MyDataTypes.INTEGER, false, 35);
        pager.AddColumn("GUID", "", MyDataTypes.GUID, false, 0);
        pager.AddColumn("RLU", "RLU", MyDataTypes.STRING, false, 0);
        pager.AddColumn("SP", "SP", MyDataTypes.STRING, false, 0);
        pager.AddColumn("KD1", "KD", MyDataTypes.STRING, false, 0);
        pager.AddColumn("LHT", "LHT", MyDataTypes.STRING, false, 0);
        pager.AddColumn("LV", "LV", MyDataTypes.STRING, false, 0);
        pager.AddColumn("KD2", "KD ", MyDataTypes.STRING, false, 0);

        if (IsPostBack == false)
        {
            Session["PatientHistorySortOrder"] = "DESC";
            pager.Sort = MyDataSort.DESC;
        }
        else
        {
            if (Session["PatientHistorySortOrder"] != null)
            {
                if (Session["PatientHistorySortOrder"].ToString() == "DESC")
                    pager.Sort = MyDataSort.DESC;
                else
                    pager.Sort = MyDataSort.ASC;
            }
        }

        pager.AddColumn("ImageBeforeTherapy", "Image Before Therapy", MyDataTypes.STRING, false, 0);
        pager.AddColumn("ImageAfterTherapy", "Image After Therapy", MyDataTypes.STRING, false, 0);

        pager.WhereClause = String.Format("PatientId = {0}", Session["PatientId"].ToString());

        patienthistorygridview.DataSource = pager.BuildTable();
        patienthistorygridview.RowDataBound += Patienthistorygridview_RowDataBound;
        patienthistorygridview.SelectedIndexChanged += Patienthistorygridview_SelectedIndexChanged;
        patienthistorygridview.DataBind();
    }


    private void Patienthistorygridview_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridView gridView = (GridView)sender;
        int selectedIndex = gridView.SelectedIndex;

        if (selectedIndex == -1)
        {
            if (Session["PatientHistorySortOrder"] != null)
            {
                if (Session["PatientHistorySortOrder"].ToString() == "DESC")             
                    Session["PatientHistorySortOrder"] = "ASC";                                   
                else                
                    Session["PatientHistorySortOrder"] = "DESC";

                BuildGrid();
            }
        }
        else if (selectedIndex != -1)
        {
            for (int index = 0; index < patienthistorygridview.Rows.Count; index++)
                patienthistorygridview.Rows[index].BackColor = System.Drawing.ColorTranslator.FromHtml("#F4A460");

            patienthistorygridview.Rows[selectedIndex].BackColor = System.Drawing.ColorTranslator.FromHtml("#F9cda8");

            string date = patienthistorygridview.Rows[selectedIndex].Cells[0].Text;
            string id = patienthistorygridview.Rows[selectedIndex].Cells[1].Text;

            Session["PatientHistoryAppointmentDate"] = patienthistorygridview.Rows[selectedIndex].Cells[0].Text;
            Session["PatientHistoryPatientId"] = patienthistorygridview.Rows[selectedIndex].Cells[1].Text;
            Session["PatientHistoryGuid"] = patienthistorygridview.Rows[selectedIndex].Cells[2].Text;
        }
    }

    private void Patienthistorygridview_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(patienthistorygridview, "Select$-1");            
            e.Row.Cells[0].Style["text-decoration"]="underline";
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[2].Visible = false;
        }
        else if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(patienthistorygridview, "Select$" + e.Row.RowIndex);
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[2].Visible = false;
        }
    }

    public bool ShowIndividualAppt()
    {
        if ((Session["PatientHistoryAppointmentDate"] == null) && (Session["PatientHistoryPatientId"] == null))
        {
            lblNotice.Text = "Select an appointment";
            return false;
        }

        return true;
    }

}



