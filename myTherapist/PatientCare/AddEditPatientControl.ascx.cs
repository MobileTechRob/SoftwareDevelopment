using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using System.Data.SqlClient;
using DatabaseObjects;

public partial class PatientCare_AddEditPatientControl : System.Web.UI.UserControl
{
    public event EventHandler patientCareCanceled;
    public event EventHandler patientCareSaved;

    public string[] months = {"", "Jan", "Feb", "Mar", "Apr", "May", "June", "July", "Aug", "Sept", "Oct", "Nov", "Dec", null};

    protected void Page_Load(object sender, EventArgs e)
    {
        int count = 0;

        if (datepickerMonth.Items.Count == 0)
        {
            while (months[count] != null)
            {
                if (months[count].Length > 0)
                    datepickerMonth.Items.Add(new ListItem(months[count++]));
                else
                    count++;
            }
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
            ClearControl();
    }

    public void ClearControl()
    {
        txtboxFirstName.Text = "";
        txtboxLastName.Text = "";
        txtboxPhone.Text = "";
        txtboxEmailAddress.Text = "";

        datepickerMonth.SelectedIndex = 0;
        datepickerDay.SelectedIndex = 0;
        datepickerYear.SelectedIndex = 0;
    }

    public void SetEditMode()
    {
        Session["EditPatientInformation"] = "true";

        Patient patient = new Patient();
        patient.Id= Int32.Parse(Session["PatientID"].ToString());

        PatientInformationTableManager patientTableMgr = new PatientInformationTableManager();

        patient = patientTableMgr.FindPatient(patient);

        txtboxFirstName.Text = patient.FirstName;
        txtboxLastName.Text = patient.LastName;

        int m = DateTime.Parse(patient.Birthday.ToString()).Month;

        datepickerMonth.Text = months[m];
        datepickerDay.Text = DateTime.Parse(patient.Birthday.ToString()).Day.ToString();
        datepickerYear.Text = DateTime.Parse(patient.Birthday.ToString()).Year.ToString();

        txtboxPhone.Text= patient.PhoneNumber;
        txtboxEmailAddress.Text = patient.Email;        
    }

    protected void btnSavePatient_Click(object sender, EventArgs e)
    {
        int patientId = 0;
        PatientInformation pi = new PatientInformation();
        PatientDataContext patientDC = new PatientDataContext(WebConfigurationManager.ConnectionStrings["MyTherapistDatabaseConnectionString"].ConnectionString);
        
        PatientInformationTableManager patientTableMgr = new PatientInformationTableManager();
        DatabaseObjects.Patient patientInformation = new DatabaseObjects.Patient();

        patientInformation.Id = 0;

        if ((Session["PatientID"] != null) && (Int32.TryParse(Session["PatientID"].ToString(), out patientId)))
            patientInformation.Id = patientId;

        patientInformation.FirstName = txtboxFirstName.Text;
        patientInformation.LastName = txtboxLastName.Text;
        patientInformation.Birthday = String.Format("{0}/{1}/{2} 00:00:00", datepickerMonth.SelectedValue, datepickerDay.SelectedValue, datepickerYear.SelectedValue);
        patientInformation.PhoneNumber = txtboxPhone.Text;
        patientInformation.Email = txtboxEmailAddress.Text;

        patientTableMgr.Update(patientInformation);

        if (patientCareSaved != null)
            patientCareSaved(this, new EventArgs());

    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        if (patientCareCanceled != null)
            patientCareCanceled(this, new EventArgs());
    }
}