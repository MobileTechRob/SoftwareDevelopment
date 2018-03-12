using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PatientCare_AddEditPatientControl : System.Web.UI.UserControl
{

    public event EventHandler patientCareCanceled;
    public event EventHandler patientCareSaved;

    protected void Page_Load(object sender, EventArgs e)
    {
        //string s = "";

    }

    public void SetEditMode()
    {
        Session["EditPatientInformation"] = "true";
    }

    protected void btnSavePatient_Click(object sender, EventArgs e)
    {
        PatientInformation pi = new PatientInformation();
        PatientDataContext patientDC = new PatientDataContext("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\SoftwareDevelopmentProjects\\WebProjects\\myTherapist\\myTherapist\\App_Data\\myTherapist.mdf;Integrated Security=True");

        if (Session["EditPatientInformation"] == null)
        {
            pi.FirstName = txtboxFirstName.Text;
            pi.LastName = txtboxLastName.Text;
            pi.TelephoneNumber = txtboxPhone.Text;
            pi.EmailAddress = txtboxEmailAddress.Text;
            patientDC.PatientInformations.InsertOnSubmit(pi);
            patientDC.SubmitChanges();

            if (patientCareSaved != null)
                patientCareSaved(this, new EventArgs());
        }
        else
        {



        }
        
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        if (patientCareCanceled != null)
            patientCareCanceled(this, new EventArgs());
    }
}