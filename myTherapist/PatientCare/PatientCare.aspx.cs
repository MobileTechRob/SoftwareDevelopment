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

    }

    protected void btnPatientListing_Click(object sender, EventArgs e)
    {

    }
}