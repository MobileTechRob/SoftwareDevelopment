using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PatientCare_PatientHistoryControl : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        System.Data.DataTable patientHistory = null;

        if (Session["PatientName"] != null)
            lblPatientName.Text = Session["PatientName"].ToString();

        MyDataGridPager pager = null;

        if (Session["PatientId"] != null)
        {
            pager = new MyDataGridPager("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\SoftwareDevelopmentProjects\\WebProjects\\myTherapist\\myTherapist\\App_Data\\myTherapist.mdf;Integrated Security=True", "PatientAppointmentInformation");
            pager.NumberRowsToDisplay = 5;
            pager.AddColumn("ApptDate", "Appointment Date", MyDataTypes.DATETIME, true, 35);
            pager.AddColumn("RLU", "RLU", MyDataTypes.STRING, false, 0);
            pager.AddColumn("SP", "SP", MyDataTypes.STRING, false, 0);
            pager.AddColumn("KD1", "KD", MyDataTypes.STRING, false, 0);
            pager.AddColumn("LHT", "LHT", MyDataTypes.STRING, false, 0);
            pager.AddColumn("LV", "LV", MyDataTypes.STRING, false, 0);
            pager.AddColumn("KD2", "KD ", MyDataTypes.STRING, false, 0);

            pager.AddColumn("ImageBeforeTherapy", "Image Before Therapy", MyDataTypes.STRING, false, 0);
            pager.AddColumn("ImageAfterTherapy", "Image After Therapy", MyDataTypes.STRING, false, 0);

            pager.NumberRowsToDisplay = 0;
            pager.WhereClause = String.Format("PatientId = {0}", Session["PatientId"].ToString());
            pager.Sort = MyDataSort.DESC;

            patienthistorygridview.DataSource = pager.BuildTable();
            patienthistorygridview.RowDataBound += Patienthistorygridview_RowDataBound;
            patienthistorygridview.SelectedIndexChanged += Patienthistorygridview_SelectedIndexChanged;
            patienthistorygridview.DataBind();
        }
    }

    private void Patienthistorygridview_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    private void Patienthistorygridview_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(patienthistorygridview, "Select$" + e.Row.RowIndex);
        }
    }
}



