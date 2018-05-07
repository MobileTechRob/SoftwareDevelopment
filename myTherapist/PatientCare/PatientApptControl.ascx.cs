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
            patientAppointmentInformation.ApptId = Guid.Parse(Session[CommonDefinitions.CommonDefinitions.PATIENT_HISTORY_GUID].ToString());
            patientAppointmentInformation = patApptMgr.FindPatientAppointment(patientAppointmentInformation);

            if (!string.IsNullOrEmpty(patientAppointmentInformation.PulseRLU.Trim()))
                RLU.SelectedValue = patientAppointmentInformation.PulseRLU.Trim();

            if (!string.IsNullOrEmpty(patientAppointmentInformation.PulseSP.Trim()))
                SP.SelectedValue = patientAppointmentInformation.PulseSP.Trim();

            if (!string.IsNullOrEmpty(patientAppointmentInformation.PulseKD1.Trim()))
                KD1.SelectedValue = patientAppointmentInformation.PulseKD1.Trim();

            if (!string.IsNullOrEmpty(patientAppointmentInformation.PulseLHT.Trim()))
                LHT.SelectedValue = patientAppointmentInformation.PulseLHT.Trim();

            if (!string.IsNullOrEmpty(patientAppointmentInformation.PulseLV.Trim()))
                LV.SelectedValue = patientAppointmentInformation.PulseLV.Trim();

            if (!string.IsNullOrEmpty(patientAppointmentInformation.PulseKD2.Trim()))
                KD2.SelectedValue = patientAppointmentInformation.PulseKD2.Trim();

            ImageBefore.ImageUrl = patientAppointmentInformation.ImageBeforeTherapy;
            ImageAfter.ImageUrl = patientAppointmentInformation.ImageAfterTherapy;

            txtBoxTherapyPerformed.Text = patientAppointmentInformation.OilsAndTherapy;
            txtBoxSessionGoals.Text = patientAppointmentInformation.SessionGoals;            
        }
    }

    public void SaveAppt()
    {
        PatientAppointment patientAppointmentInformation = new PatientAppointment();
        PatientAppointmentInformationTableManager patApptMgr = new PatientAppointmentInformationTableManager();

        patientAppointmentInformation.ApptId = Guid.Empty;

        if (Session[CommonDefinitions.CommonDefinitions.PATIENT_HISTORY_GUID] != null)
            patientAppointmentInformation.ApptId = Guid.Parse(Session[CommonDefinitions.CommonDefinitions.PATIENT_HISTORY_GUID].ToString());
       
        patientAppointmentInformation.PatientId = Int32.Parse(Session[CommonDefinitions.CommonDefinitions.PATIENT_ID].ToString());
        patientAppointmentInformation.AppointmentDate = DateTime.Parse(Session[CommonDefinitions.CommonDefinitions.PATIENT_APPT_START_DATE_TIME].ToString());
        patientAppointmentInformation.ImageBeforeTherapy = uploadImageBefore.FileName;
        patientAppointmentInformation.ImageAfterTherapy = uploadImageAfter.FileName;        
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