using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PatientCare_PatientCare : System.Web.UI.Page
{
    PatientCare_PatientListing patientListing;

    protected void Page_Load(object sender, EventArgs e)
    {
        patientListing = PatientListing1;

        AddEditPatientControl1.patientCareCanceled += AddEditPatientControl1_patientCareCanceled;
        AddEditPatientControl1.patientCareSaved += AddEditPatientControl1_patientCareSaved;

        if (IsPostBack == false)
        {
            PatientListing1.Visible = true;
            AddEditPatientControl1.Visible = false;
            PatientApptControl1.Visible = false;
            PatientHistoryControl1.Visible = false;

            btnSaveAppt.Visible = false;
            btnPatientListing.Visible = false;
            btnCancelAppt.Visible = false;
            btnSaveChanges.Visible = false;
            btnEditAppt.Visible = false;
            btnCancelChanges.Visible = false;
            
        }                        
    }

    private void AddEditPatientControl1_patientCareSaved(object sender, EventArgs e)
    {
        PatientListing1.LoadGrid();
        PatientListing1.Visible = true;
        AddEditPatientControl1.Visible = false;

        menuPanel.Visible = true;
    }

    private void AddEditPatientControl1_patientCareCanceled(object sender, EventArgs e)
    {
        PatientListing1.Visible = true;
        AddEditPatientControl1.Visible = false;

        menuPanel.Visible = true;
    }

    protected void btnCreatePatient_Click(object sender, EventArgs e)
    {
        PatientListing1.Visible = false;
        AddEditPatientControl1.Visible = true;

        menuPanel.Visible = false;
        Response.Clear();
    }

    protected void btnStartAppt_Click(object sender, EventArgs e)
    {
        if (Session["PatientID"] != null)
        {
            PatientListing1.Visible = false;
            AddEditPatientControl1.Visible = false;
            PatientApptControl1.Visible = true;
            PatientHistoryControl1.Visible = false;

            btnStartAppt.Visible = false;
            btnEditAppt.Visible = false;
            btnSaveAppt.Visible = true;
            btnCancelAppt.Visible = true;
            btnDeletePatient.Visible = false;
            btnCreatePatient.Visible = false;
            btnPatientListing.Visible = true;
        }
        else
        {
            lblWarningText.Text = "Select a Patient";
            lblWarningText.ForeColor = System.Drawing.Color.Red;
        }
    }

    protected void btnPatientHistory_Click(object sender, EventArgs e)
    {
        if (Session["PatientID"] == null)
        {
            lblWarningText.Text = "Select a Patient";
            lblWarningText.ForeColor = System.Drawing.Color.Red;

            PatientListing1.Visible = true;
            AddEditPatientControl1.Visible = false;
            PatientApptControl1.Visible = false;
            PatientHistoryControl1.Visible = false;
        }
        else
        {
            lblWarningText.Visible = false;
            lblWarningText.Text = "";

            PatientListing1.Visible = false;
            AddEditPatientControl1.Visible = false;
            PatientApptControl1.Visible = false;
            PatientHistoryControl1.Visible = true;

            btnCreatePatient.Visible = false;
            btnCancelAppt.Visible = false;
            btnSaveAppt.Visible = false;
            btnEditAppt.Visible = true;
            btnStartAppt.Visible = true;
            btnPatientListing.Visible = true;
            btnDeletePatient.Visible = false;
            btnPatientHistory.Visible = false;
        }
           
    }

    protected void btnSaveAppt_Click(object sender, EventArgs e)
    {
        PatientApptControl1.SaveAppt();
        PatientApptControl1.Visible = false;
        PatientListing1.Visible = true;
        btnCancelAppt.Visible = false;
        btnSaveAppt.Visible = false;
        btnCreatePatient.Visible = true;
        btnStartAppt.Visible = true;
        btnEditAppt.Visible = false;
        btnDeletePatient.Visible = false;
    }

    protected void btnPatientListing_Click(object sender, EventArgs e)
    {
        PatientListing1.Visible = true;
        PatientApptControl1.Visible = false;
        PatientHistoryControl1.Visible = false;

        btnCreatePatient.Visible = true;
        btnCancelAppt.Visible = false;
        btnSaveAppt.Visible = false;
        btnEditAppt.Visible = false;
        btnPatientHistory.Visible = true;
        btnPatientListing.Visible = false;
        btnDeletePatient.Visible = true;
        btnSaveChanges.Visible = false;
    }

    protected void btnDeletePatient_Click(object sender, EventArgs e)
    {
        PatientDataContext patientDC = new PatientDataContext("Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename = C:\\SoftwareDevelopmentProjects\\WebProjects\\myTherapist\\myTherapist\\App_Data\\myTherapist.mdf; Integrated Security = True");
        PatientInformation patientToDelete = new PatientInformation();

        PatientAppointmentInfomationDataContext patientAppointmentInformationDC = new PatientAppointmentInfomationDataContext("Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename = C:\\SoftwareDevelopmentProjects\\WebProjects\\myTherapist\\myTherapist\\App_Data\\myTherapist.mdf; Integrated Security = True");
        PatientAppointmentInformation patientAppointmentInformationToDelete = new PatientAppointmentInformation();
        

        Int32 patientId = 0;

        if (Session["PatientID"] != null)
        {
            if (Session["DeletePatientWarning"] == null)
            {                
                lblWarningText.Text = "Click Delete again to confirm this action.";
                lblWarningText.ForeColor = System.Drawing.Color.Red;
                lblWarningText.Visible = true;
                Session["DeletePatientWarning"] = "yes";
            }
            else
            {
                if (Int32.TryParse(Session["PatientID"].ToString(), out patientId))
                {
                    Session["DeletePatientWarning"] = null;
                    patientToDelete = patientDC.PatientInformations.Single(patient => patient.Id == patientId);
                    patientDC.PatientInformations.DeleteOnSubmit(patientToDelete);
                    patientDC.SubmitChanges();

                    var patientApptsToDelete =patientAppointmentInformationDC.PatientAppointmentInformations.Select(patient => patient.PatientId == patientId);

                    if (patientApptsToDelete.Count() > 0)
                    {
                        IEnumerable<PatientAppointmentInformation> patientApptDelete = (from patientAppts in patientAppointmentInformationDC.PatientAppointmentInformations where patientAppts.PatientId == patientId select patientAppts);                    
                        patientAppointmentInformationDC.PatientAppointmentInformations.DeleteAllOnSubmit(patientApptDelete);
                        patientAppointmentInformationDC.SubmitChanges();
                    }

                    lblWarningText.Visible = false;

                    PatientListing1.LoadGrid();
                }
            }
        }
        else
        {
            lblWarningText.Visible = true;
            lblWarningText.Text = "Select a patient to delete.";
            lblWarningText.ForeColor = System.Drawing.Color.Red;
        }
    }

    protected void btnCancelAppt_Click(object sender, EventArgs e)
    {
        PatientListing1.Visible = true;
        PatientApptControl1.Visible = false;
        btnCancelAppt.Visible = false;
        btnCreatePatient.Visible = true;
        btnStartAppt.Visible = true;
        btnSaveAppt.Visible = false;
        btnDeletePatient.Visible = false;
        btnPatientHistory.Visible = true;
    }

    protected void btnEditAppt_Click(object sender, EventArgs e)
    {
        if (PatientHistoryControl1.ShowIndividualAppt())
        {
            Session["EditPatientAppt"] = "true";
            btnEditAppt.Visible = false;
            btnSaveChanges.Visible = true;
            btnStartAppt.Visible = false;
            btnCancelChanges.Visible = true;
            btnCreatePatient.Visible = false;
            btnSaveAppt.Visible = false;

            PatientHistoryControl1.Visible = false;
            PatientApptControl1.Visible = true;
            PatientApptControl1.LoadPatientAppt();            
        }
    }

    protected void btnSaveChanges_Click(object sender, EventArgs e)
    {
        string s = "";

        if ((Session["EditPatientAppt"] != null) && (Session["EditPatientAppt"].ToString() == "true"))
        {
            PatientApptControl1.SaveAppt();
            PatientHistoryControl1.Refresh();
            PatientApptControl1.Visible = false;
            PatientHistoryControl1.Visible = true;
            btnEditAppt.Visible = true;
            btnSaveChanges.Visible = false;
            btnCancelChanges.Visible = false;
            btnPatientListing.Visible = true;
        }        
    }

    protected void btnCancelChanges_Click(object sender, EventArgs e)
    {
        PatientHistoryControl1.Visible = true;
        PatientApptControl1.Visible = false;

        btnCancelChanges.Visible = false;
        btnEditAppt.Visible = true;
        btnPatientListing.Visible = true;
        btnStartAppt.Visible = true;
    }

    protected void btnUpdatePatient_Click(object sender, EventArgs e)
    {
        if (Session["PatientID"] != null)
        {
            PatientListing1.Visible = false;
            AddEditPatientControl1.SetEditMode();
            AddEditPatientControl1.Visible = true;
        }
        else
        {
            lblWarningText.Text = "Select a Patient";
            lblWarningText.ForeColor = System.Drawing.Color.Red;
        }
    }
}