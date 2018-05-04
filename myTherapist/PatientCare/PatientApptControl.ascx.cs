using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Web.Configuration;
using DatabaseObjects;

public partial class PatientCare_PatientApptControl : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["PatientFirstName"] != null) && (Session["PatientLastName"] != null))
            patientNameHeader.Text = String.Format("{0} {1}", Session["PatientFirstName"].ToString(), Session["PatientLastName"].ToString());

        if (IsPostBack)
        {
            Boolean fileOK = false;
            String path = Server.MapPath("~/UploadedImages/");

            if (uploadImageBefore.HasFile)
            {
                String fileExtension = System.IO.Path.GetExtension(uploadImageBefore.FileName).ToLower();
                String[] allowedExtensions = {".gif", ".png", ".jpeg", ".jpg", ".bmp"};

                for (int i = 0; i < allowedExtensions.Length; i++)
                {
                    if (fileExtension == allowedExtensions[i])                    
                        fileOK = true;                    
                }

                if (fileOK)
                {
                    try
                    {
                        uploadImageBefore.PostedFile.SaveAs(path + uploadImageBefore.FileName);
                        Session["UploadImageBefore"] = "~/UploadedImages/" + uploadImageBefore.FileName;
                        ImageBefore.ImageUrl = "~/UploadedImages/" + uploadImageBefore.FileName;
                        //Label1.Text = "File uploaded!";
                    }
                    catch (Exception ex)
                    {
                        //Label1.Text = "File could not be uploaded.";
                    }
                }
                else
                {
                    //Label1.Text = "Cannot accept files of this type.";
                }
            }


            if (uploadImageAfter.HasFile)
            {
                String fileExtension = System.IO.Path.GetExtension(uploadImageAfter.FileName).ToLower();
                String[] allowedExtensions = { ".gif", ".png", ".jpeg", ".jpg", ".bmp" };

                for (int i = 0; i < allowedExtensions.Length; i++)
                {
                    if (fileExtension == allowedExtensions[i])
                        fileOK = true;
                }

                if (fileOK)
                {
                    try
                    {
                        uploadImageAfter.PostedFile.SaveAs(path + uploadImageAfter.FileName);
                        Session["UploadImageAfter"] = "~/UploadedImages/" + uploadImageAfter.FileName;
                        ImageAfter.ImageUrl = "~/UploadedImages/" + uploadImageAfter.FileName;
                    }
                    catch (Exception ex)
                    {                        
                    }
                }
                else
                {                    
                }
            }            
        }
    }
    
    public void LoadPatientAppt()
    {
        DateTime dt = DateTime.MinValue;
        long id = 0;
        Guid patientGuid = Guid.Empty;

        PatientAppointment patientAppointmentInformation = new PatientAppointment();
        PatientAppointmentInformationTableManager patApptMgr = new PatientAppointmentInformationTableManager();

        if ((Session["EditPatientAppt"] != null) && (Session["EditPatientAppt"].ToString() == "true"))
        {
            //PatientAppointmentInfomationDataContext piDC = new PatientAppointmentInfomationDataContext(WebConfigurationManager.ConnectionStrings["MyTherapistDatabaseConnectionString"].ConnectionString);   
            //PatientAppointmentInformation patientRecord1 = null;

            patientAppointmentInformation.ApptId = Guid.Parse(Session["PatientHistoryGuid"].ToString());

            //if (Session["PatientHistoryGuid"] != null)
            //{
              //  string guidstring = Session["PatientHistoryGuid"].ToString();
              //  patientGuid = Guid.Parse(guidstring);
            //}

            var appointmentquery = from patients in piDC.PatientAppointmentInformations where patients.GUID == patientGuid  select patients;
            patientRecord1 = appointmentquery.First();

            if (!string.IsNullOrEmpty(patientRecord1.RLU.Trim()))
                RLU.SelectedValue = patientRecord1.RLU.Trim();

            if (!string.IsNullOrEmpty(patientRecord1.SP.Trim()))
                SP.SelectedValue = patientRecord1.SP.Trim();

            if (!string.IsNullOrEmpty(patientRecord1.KD1.Trim()))
                KD1.SelectedValue = patientRecord1.KD1.Trim();

            if (!string.IsNullOrEmpty(patientRecord1.LHT.Trim()))
                LHT.SelectedValue = patientRecord1.LHT.Trim();

            if (!string.IsNullOrEmpty(patientRecord1.LV.Trim()))
                LV.SelectedValue = patientRecord1.LV.Trim();

            if (!string.IsNullOrEmpty(patientRecord1.KD2.Trim()))
                KD2.SelectedValue = patientRecord1.KD2.Trim();

            ImageBefore.ImageUrl = patientRecord1.ImageBeforeTherapy;
            ImageAfter.ImageUrl = patientRecord1.ImageAfterTherapy;

            txtBoxTherapyPerformed.Text = patientRecord1.TherapyPerformed;
            txtBoxSessionGoals.Text = patientRecord1.SessionGoals;            
        }
    }

    public void SaveAppt()
    {
        PatientAppointment patientAppointmentInformation = new PatientAppointment();
        PatientAppointmentInformationTableManager patApptMgr = new PatientAppointmentInformationTableManager();

        patientAppointmentInformation.ApptId = Guid.Empty;

        if (Session["PatientHistoryGuid"] != null)
            patientAppointmentInformation.ApptId = Guid.Parse(Session["PatientHistoryGuid"].ToString());
        
        patientAppointmentInformation.AppointmentDate = DateTime.Parse(Session["PatientApptStartDateTime"].ToString());
        patientAppointmentInformation.ImageBefore = uploadImageBefore.FileName;
        patientAppointmentInformation.ImageAfter = uploadImageAfter.FileName;        
        patientAppointmentInformation.OilsAndTherapy = txtBoxTherapyPerformed.Text; 
        patientAppointmentInformation.SessionGoals = txtBoxSessionGoals.Text;
        patientAppointmentInformation.PulseKD1 = KD1.Text;
        patientAppointmentInformation.PulseKD2 = KD2.Text;
        patientAppointmentInformation.PulseLHT = LHT.Text;
        patientAppointmentInformation.PulseLV = LV.Text;
        patientAppointmentInformation.PulseRLU= RLU.Text;
        patientAppointmentInformation.PulseSP= SP.Text;

        patApptMgr.Update(patientAppointmentInformation);
    }
}