using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DatabaseObjects;
using CommonDefinitions;

public partial class MainMenu : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
      
    }

    protected void btnPatientCare_Click(object sender, EventArgs e)
    {
        DateTime result = DateTime.MinValue;

        if (txtBoxUserName.Text.Equals("MyTherapist"))
        {
            Session[CommonDefinitions.CommonDefinitions.SHOW_THERAPIST_LIST] = "true";
            Response.Redirect("~/UserManagement/Therapists.aspx");
        }
        else if (txtBoxUserName.Text.Equals("Pam", StringComparison.CurrentCultureIgnoreCase))
            Response.Redirect("~/PatientCare/PatientCare.aspx");
    }

    protected void btnSystemConfiguration_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/PatientCare/PatientCare.aspx");
    }
}