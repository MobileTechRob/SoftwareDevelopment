using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PatientCare_PatientCare : System.Web.UI.Page
{    
    protected void Page_Load(object sender, EventArgs e)
    {

        AddEditPatientControl1.patientCareCanceled += AddEditPatientControl1_patientCareCanceled;
        AddEditPatientControl1.patientCareSaved += AddEditPatientControl1_patientCareSaved;

        if (IsPostBack == false)
        {
            PatientListing1.Visible = true;
            AddEditPatientControl1.Visible = false;
            PatientApptControl1.Visible = false;

            btnSaveAppt.Visible = false;
            btnPatientListing.Visible = false;

        }                  
    }

    private void AddEditPatientControl1_patientCareSaved(object sender, EventArgs e)
    {
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
        PatientListing1.Visible = false;
        AddEditPatientControl1.Visible = false;
        PatientApptControl1.Visible = true;

        btnStartAppt.Visible = false;
        btnSaveAppt.Visible = true;
        btnCreatePatient.Visible = false;        
        btnPatientListing.Visible = true;
    }

    protected void btnPatientHistory_Click(object sender, EventArgs e)
    {
        
    }

    protected void btnSaveAppt_Click(object sender, EventArgs e)
    {
        RadioButtonList rbl = null;
        TextBox txtBox = null;
        
        rbl = (RadioButtonList)PatientApptControl1.FindControl("LHT");
        string lht = rbl.Text;

        rbl = (RadioButtonList)PatientApptControl1.FindControl("SP");
        string sp = rbl.Text;

        rbl = (RadioButtonList)PatientApptControl1.FindControl("KD1");
        string kd1 = rbl.Text;

        rbl = (RadioButtonList)PatientApptControl1.FindControl("RHT");
        string rht = rbl.Text;

        rbl = (RadioButtonList)PatientApptControl1.FindControl("SP2");
        string sp1 = rbl.Text;

        rbl = (RadioButtonList)PatientApptControl1.FindControl("KD2");
        string kd2 = rbl.Text;

        txtBox = (TextBox)PatientApptControl1.FindControl("txtboxTherapyPerformed");
        string therapy = txtBox.Text;

        txtBox = (TextBox)PatientApptControl1.FindControl("txtboxOilsUsed");
        string oilsUsed = txtBox.Text;

        txtBox = (TextBox)PatientApptControl1.FindControl("txtBoxSessionGoals");
        string sessionGoals = txtBox.Text;

        PatientAppointmentInfomationDataContext patientApptDataContext = new PatientAppointmentInfomationDataContext("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\SoftwareDevelopmentProjects\\WebProjects\\myTherapist\\myTherapist\\App_Data\\myTherapist.mdf;Integrated Security=True");
        PatientAppointmentInformation patientAppointmentInformation = new PatientAppointmentInformation();


        patientAppointmentInformation.ApptDate = DateTime.Now;
        patientAppointmentInformation.ImageBeforeTherapy = "";
        patientAppointmentInformation.ImageAfterTherapy = "";

        patientAppointmentInformation.LHT = "";
        patientAppointmentInformation.SP = "";
        patientAppointmentInformation.KD1 = "";

        patientAppointmentInformation.RLU = "";
        patientAppointmentInformation.LV = "";
        patientAppointmentInformation.KD2 = "";

        patientAppointmentInformation.TherapyPerformed = "";
        patientAppointmentInformation.OilsUsed = "";
        patientAppointmentInformation.SessionGoals = "";

        patientApptDataContext.PatientAppointmentInformations.InsertOnSubmit(patientAppointmentInformation);
        patientApptDataContext.SubmitChanges();        
    }

    protected void btnPatientListing_Click(object sender, EventArgs e)
    {

    }
}