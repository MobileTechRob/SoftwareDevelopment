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
        PatientListing1.Visible = false;
        AddEditPatientControl1.Visible = false;
        PatientApptControl1.Visible = true;
        PatientHistoryControl1.Visible = false;

        btnStartAppt.Visible = false;
        btnSaveAppt.Visible = true;
        btnCancelAppt.Visible = true;
        btnDeletePatient.Visible = false;
        btnCreatePatient.Visible = false;        
        btnPatientListing.Visible = true;
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
            btnStartAppt.Visible = true;
            btnPatientListing.Visible = true;
            btnDeletePatient.Visible = true;
            btnPatientHistory.Visible = false;
        }
           
    }

    protected void btnSaveAppt_Click(object sender, EventArgs e)
    {
        RadioButtonList rbl = null;
        FileUpload fupl = null;

        TextBox txtBox = null;

        rbl = (RadioButtonList)PatientApptControl1.FindControl("RLU");
        string rlu = rbl.Text;

        rbl = (RadioButtonList)PatientApptControl1.FindControl("SP");
        string sp = rbl.Text;

        rbl = (RadioButtonList)PatientApptControl1.FindControl("KD1");
        string kd1 = rbl.Text;

        rbl = (RadioButtonList)PatientApptControl1.FindControl("LHT");
        string lht = rbl.Text;

        rbl = (RadioButtonList)PatientApptControl1.FindControl("LV");
        string lv = rbl.Text;

        rbl = (RadioButtonList)PatientApptControl1.FindControl("KD2");
        string kd2 = rbl.Text;

        fupl = (FileUpload)PatientApptControl1.FindControl("uploadImageBefore");
        string filePathImageBefore = string.Empty;
        filePathImageBefore = fupl.FileName;

        fupl = (FileUpload)PatientApptControl1.FindControl("uploadImageAfter");
        string filePathImageAfter = string.Empty;
        filePathImageAfter = fupl.FileName;

        txtBox = (TextBox)PatientApptControl1.FindControl("txtboxTherapyPerformed");
        string therapy = txtBox.Text;

        txtBox = (TextBox)PatientApptControl1.FindControl("txtboxOilsUsed");
        string oilsUsed = txtBox.Text;

        txtBox = (TextBox)PatientApptControl1.FindControl("txtBoxSessionGoals");
        string sessionGoals = txtBox.Text;

        PatientAppointmentInfomationDataContext patientApptDataContext = new PatientAppointmentInfomationDataContext("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\SoftwareDevelopmentProjects\\WebProjects\\myTherapist\\myTherapist\\App_Data\\myTherapist.mdf;Integrated Security=True");
        PatientAppointmentInformation patientAppointmentInformation = new PatientAppointmentInformation();
        
        string apptDateStr = DateTime.Now.ToShortDateString() + " " + DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString() + "." + DateTime.Now.Millisecond.ToString();
        DateTime apptDate = DateTime.Parse(apptDateStr);

        patientAppointmentInformation.ApptDate = apptDate;
        patientAppointmentInformation.ImageBeforeTherapy = filePathImageBefore;
        patientAppointmentInformation.ImageAfterTherapy = filePathImageAfter;

        patientAppointmentInformation.RLU = rlu;        
        patientAppointmentInformation.SP = sp;
        patientAppointmentInformation.KD1 = kd1;

        patientAppointmentInformation.LHT = lht;
        patientAppointmentInformation.LV = lv;
        patientAppointmentInformation.KD2 = kd2;

        patientAppointmentInformation.TherapyPerformed = therapy;
        patientAppointmentInformation.OilsUsed = oilsUsed;
        patientAppointmentInformation.SessionGoals = sessionGoals;

        patientApptDataContext.PatientAppointmentInformations.InsertOnSubmit(patientAppointmentInformation);
        patientApptDataContext.SubmitChanges();

        string[] p = {",", " ", "\r\n" };

        string[] listOfOilsUsed = oilsUsed.Split(p, StringSplitOptions.RemoveEmptyEntries);

        if (listOfOilsUsed.Length > 0)
        {
            OilsUsedDataContext oilDataContext = new OilsUsedDataContext("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\SoftwareDevelopmentProjects\\WebProjects\\myTherapist\\myTherapist\\App_Data\\myTherapist.mdf;Integrated Security=True");

            foreach (string oil in listOfOilsUsed)
            {
                OilsUsed oils = new OilsUsed();
                oils.ApptDate = apptDate;
                oils.OilsUsed1 = oil;
                oilDataContext.OilsUseds.InsertOnSubmit(oils);
                oilDataContext.SubmitChanges();
            }               
        }

        PatientApptControl1.Visible = false;
        PatientListing1.Visible = true;
        btnCancelAppt.Visible = false;
        btnSaveAppt.Visible = false;
        btnCreatePatient.Visible = true;
        btnStartAppt.Visible = true;
    }

    protected void btnPatientListing_Click(object sender, EventArgs e)
    {
        PatientListing1.Visible = true;
        PatientApptControl1.Visible = false;
        PatientHistoryControl1.Visible = false;

        btnCreatePatient.Visible = true;
        btnCancelAppt.Visible = false;
        btnSaveAppt.Visible = false;
        btnPatientHistory.Visible = true;
        btnPatientListing.Visible = false;
        btnDeletePatient.Visible = true;        
    }

    protected void btnDeletePatient_Click(object sender, EventArgs e)
    {
        PatientDataContext patientDC = new PatientDataContext("Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename = C:\\SoftwareDevelopmentProjects\\WebProjects\\myTherapist\\myTherapist\\App_Data\\myTherapist.mdf; Integrated Security = True");
        PatientInformation patientToDelete = new PatientInformation();
        Int32 patientId = 0;

        if (Int32.TryParse(Session["PatientID"].ToString(), out patientId))
        {
            patientToDelete = patientDC.PatientInformations.Single(patient => patient.Id == patientId);

            patientDC.PatientInformations.DeleteOnSubmit(patientToDelete);
            patientDC.SubmitChanges();
            PatientListing1.LoadGrid();
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
        btnDeletePatient.Visible = true;
        btnPatientHistory.Visible = true;
    }
}