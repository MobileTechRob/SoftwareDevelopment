using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MainMenu : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        MyDataGridPager pager = new MyDataGridPager("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\SoftwareDevelopmentProjects\\WebProjects\\myTherapist\\myTherapist\\App_Data\\myTherapist.mdf;Integrated Security=True","PatientInformation");
        pager.AddColumn("Id", "Id", MyDataTypes.INTEGER ,true);
        pager.AddColumn("Name", "Name", MyDataTypes.STRING ,false);
        pager.NumberRowsToDisplay = 3;
        pager.PageNumber = 2;
        pager.BuildTable();
    }

    protected void btnPatientCare_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/PatientCare/PatientCare.aspx");
    }

    protected void btnSystemConfiguration_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/PatientCare/PatientCare.aspx");
    }
}