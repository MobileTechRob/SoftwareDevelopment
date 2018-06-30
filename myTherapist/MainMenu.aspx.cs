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
            Response.Redirect("~/UserManagement/Therapists.aspx");
        }
        else
        {
            TherapistTableManager therapistTableManager = new TherapistTableManager();
            MassageTherapists therapist = new MassageTherapists(txtBoxUserName.Text, txtBoxPassword.Text);
            MassageTherapists massagePerson = therapistTableManager.FindTherapist(therapist);

            if (massagePerson != null)
            {
                Session[CommonDefinitions.CommonDefinitions.THERAPIST_NAME] = massagePerson.Name;
                Session[txtBoxUserName.Text.ToUpper()] = massagePerson.Id;
                Response.Redirect("~/PatientCare/PatientCare.aspx");
            }
            else
            {
                lblInformationText.Text = "Invalid Logon";
                lblInformationText.ForeColor = System.Drawing.Color.Red;
            }
        }
    }

    protected void btnSystemConfiguration_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/PatientCare/PatientCare.aspx");
    }
}