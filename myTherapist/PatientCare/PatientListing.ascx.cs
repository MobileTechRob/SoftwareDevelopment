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

        patientlisting.DataSource = pager.BuildTable();
        patientlisting.DataBind();        
    }
}


