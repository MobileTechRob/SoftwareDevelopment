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

    public string[] months = { "Jan", "Feb", "Mar", "Apr", "May", "June", "July", "Aug", "Sept", "Oct", "Nov", "Dec",""};

    protected void Page_Load(object sender, EventArgs e)
    {
        int count = 0;

        if (datepickerMonth.Items.Count == 0)
        {
            while (String.IsNullOrEmpty(months[count]) == false)
                datepickerMonth.Items.Add(new ListItem(months[count++]));
        }

        if (datepickerDay.Items.Count == 0)
        {
            for (count = 1; count <= 31; count++)
              datepickerDay.Items.Add(new ListItem(count.ToString()));
        }

        if (datepickerYear.Items.Count == 0)
        {
            for (count = 1900; count <= 2050; count++)
                datepickerYear.Items.Add(new ListItem(count.ToString()));
        }

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

        datepickerMonth.Text = months[DateTime.Parse(pi.BirthDate.ToString()).Month];
        datepickerDay.Text = DateTime.Parse(pi.BirthDate.ToString()).Day.ToString();
        datepickerYear.Text = DateTime.Parse(pi.BirthDate.ToString()).Year.ToString();

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
            pi.BirthDate = DateTime.Parse(String.Format("{0}/{1}/{2} 00:00:00", datepickerMonth.SelectedValue, datepickerDay.SelectedValue, datepickerYear.SelectedValue));
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
            pi.BirthDate = DateTime.Parse(String.Format("{0}/{1}/{2} 00:00:00", datepickerMonth.SelectedValue, datepickerDay.SelectedValue, datepickerYear.SelectedValue));
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