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
        if (Session["PatientName"] != null)
            lblPatientName.Text = Session["PatientName"].ToString();

        MyDataGridPager pager = null;

        if (Session["PatientId"] != null)
        {        
            pager = new MyDataGridPager("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\SoftwareDevelopmentProjects\\WebProjects\\myTherapist\\myTherapist\\App_Data\\myTherapist.mdf;Integrated Security=True", "PatientAppointmentInformation");

            pager.AddColumn("ApptDate", "", MyDataTypes.DATETIME, true, 35);
            pager.AddColumn("PatientId", "", MyDataTypes.INTEGER, true, 35);

            pager.AddColumn("RLU", "", MyDataTypes.STRING, false, 0);
            pager.AddColumn("SP", "", MyDataTypes.STRING, false, 0);
            pager.AddColumn("KD1", "", MyDataTypes.STRING, false, 0);
            pager.AddColumn("LHT", "", MyDataTypes.STRING, false, 0);
            pager.AddColumn("LV", "", MyDataTypes.STRING, false, 0);
            pager.AddColumn("KD2", "", MyDataTypes.STRING, false, 0);

            pager.AddColumn("TherapyPerformed", "", MyDataTypes.STRING, false, 0);
            pager.AddColumn("OilsUsed", "", MyDataTypes.STRING, false, 0);
            pager.AddColumn("SessionGoals", "", MyDataTypes.STRING, false, 0);

            pager.AddColumn("ImageBeforeTherapy", "", MyDataTypes.STRING, false, 0);
            pager.AddColumn("ImageAfterTherapy", "", MyDataTypes.STRING, false, 0);

            pager.NumberRowsToDisplay = 0;
            pager.WhereClause = String.Format("PatientId = {0}", Session["PatientId"].ToString());
            pager.Sort = MyDataSort.DESC;
            pager.BuildTable();
        }
    }
}





