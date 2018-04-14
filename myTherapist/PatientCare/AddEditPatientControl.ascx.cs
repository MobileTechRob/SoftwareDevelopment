using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using System.Data.SqlClient;

public partial class PatientCare_AddEditPatientControl : System.Web.UI.UserControl
{

    public event EventHandler patientCareCanceled;
    public event EventHandler patientCareSaved;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack == false)
        {
            txtboxFirstName.Text = "";
            txtboxLastName.Text = "";
            txtboxPhone.Text = "";
            txtboxEmailAddress.Text = "";
        }
    }

    public void SetEditMode()
    {
        Session["EditPatientInformation"] = "true";

        PatientInformation pi = new PatientInformation();
        PatientDataContext patientDC = new PatientDataContext(WebConfigurationManager.ConnectionStrings["MyTherapistDatabaseConnectionString"].ConnectionString);

        int patientID = Int32.Parse(Session["PatientID"].ToString());

        IQueryable<PatientInformation> apatient = from patients in patientDC.PatientInformations where (patients.Id == patientID) select patients;
        pi = apatient.Single<PatientInformation>();

        txtboxFirstName.Text = pi.FirstName;
        txtboxLastName.Text = pi.LastName;
        
        DateTime dt = pi.BirthDate.Value;

        //datePicker.Text = new DateTime(2012, 4, 7).ToString();
        
        txtboxPhone.Text= pi.TelephoneNumber;
        txtboxEmailAddress.Text = pi.EmailAddress;        
    }

    protected void btnSavePatient_Click(object sender, EventArgs e)
    {
        PatientInformation pi = new PatientInformation();
        PatientDataContext patientDC = new PatientDataContext(WebConfigurationManager.ConnectionStrings["MyTherapistDatabaseConnectionString"].ConnectionString);

        if (Session["EditPatientInformation"] == null)
        {
            pi.FirstName = txtboxFirstName.Text;
            pi.LastName = txtboxLastName.Text;
            pi.BirthDate = DateTime.Parse(datePicker.Text);
            pi.TelephoneNumber = txtboxPhone.Text;
            pi.EmailAddress = txtboxEmailAddress.Text;

            try
            {
                patientDC.PatientInformations.InsertOnSubmit(pi);
                patientDC.SubmitChanges();

                if (patientCareSaved != null)
                    patientCareSaved(this, new EventArgs());

            }
            catch (Exception ex)
            {
            }
        }
        else
        {
            int patientID = Int32.Parse(Session["PatientID"].ToString());

            IQueryable<PatientInformation> apatient = from patients in patientDC.PatientInformations where (patients.Id == patientID) select patients;
            pi = apatient.Single<PatientInformation>();
            pi.FirstName = txtboxFirstName.Text;
            pi.LastName = txtboxLastName.Text;
            pi.BirthDate = DateTime.Parse(datePicker.Text);
            pi.TelephoneNumber = txtboxPhone.Text;
            pi.EmailAddress = txtboxEmailAddress.Text;

            try
            {
                patientDC.SubmitChanges();

                Session["EditPatientInformation"] = null;

                if (patientCareSaved != null)
                    patientCareSaved(this, new EventArgs());
            }
            catch (Exception ex)
            {
            }            
        }

    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        if (patientCareCanceled != null)
            patientCareCanceled(this, new EventArgs());
    }
}