using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;

public partial class PatientCare_PatientApptControl : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["PatientName"] != null)
            patientHeader.Text = Session["PatientName"].ToString();

        //if (!String.IsNullOrEmpty(uploadImageBeforea.FileName))
          //  ImageFiller1a.ImageUrl = "c:\\PatientImages\\" + uploadImageBeforea.FileName;

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

        if ((Session["EditPatient"] != null) && (Session["EditPatient"].ToString() == "true"))
        {
            PatientAppointmentInfomationDataContext piDC = new PatientAppointmentInfomationDataContext("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\SoftwareDevelopmentProjects\\WebProjects\\myTherapist\\myTherapist\\App_Data\\myTherapist.mdf;Integrated Security=True");                       
            PatientAppointmentInformation patientRecord1 = null;

            if (Session["PatientHistoryGuid"] != null)
            {
                string guidstring = Session["PatientHistoryGuid"].ToString();
                patientGuid = Guid.Parse(guidstring);
            }

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
            
        }
    }

    public void SaveAppt()
    {        
        string rlu = RLU.Text;       
        string sp = SP.Text;

        string kd1 = KD1.Text;
        
        string lht = LHT.Text;        
        string lv = LV.Text;        
        string kd2 = KD2.Text;
        
        string filePathImageBefore = string.Empty;
        filePathImageBefore = uploadImageBefore.FileName;
        
        string filePathImageAfter = string.Empty;
        filePathImageAfter = uploadImageAfter.FileName;
        
        string therapy = txtBoxTherapyPerformed.Text;        
        string oilsUsed = txtBoxOilsUsed.Text;        
        string sessionGoals = txtBoxSessionGoals.Text;

        PatientAppointmentInfomationDataContext patientApptDataContext = new PatientAppointmentInfomationDataContext("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\SoftwareDevelopmentProjects\\WebProjects\\myTherapist\\myTherapist\\App_Data\\myTherapist.mdf;Integrated Security=True");
        PatientAppointmentInformation patientAppointmentInformation = new PatientAppointmentInformation();


        if (Session["EditPatient"] == null)
        {
            string apptDateStr = DateTime.Now.ToShortDateString() + " " + DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString();
            DateTime apptDate = DateTime.Parse(apptDateStr);
            Guid patientAppt = Guid.NewGuid();

            patientAppointmentInformation.ApptDate = apptDate;
            patientAppointmentInformation.PatientId = long.Parse(Session["PatientID"].ToString());
            patientAppointmentInformation.GUID = patientAppt;

            patientAppointmentInformation.ImageBeforeTherapy = ImageBefore.ImageUrl;
            patientAppointmentInformation.ImageAfterTherapy = ImageAfter.ImageUrl;

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

            string[] p = { ",", " ", "\r\n" };

            string[] listOfOilsUsed = oilsUsed.Split(p, StringSplitOptions.RemoveEmptyEntries);

            if (listOfOilsUsed.Length > 0)
            {
                OilsUsedDataContext oilDataContext = new OilsUsedDataContext("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\SoftwareDevelopmentProjects\\WebProjects\\myTherapist\\myTherapist\\App_Data\\myTherapist.mdf;Integrated Security=True");

                foreach (string oil in listOfOilsUsed)
                {
                    OilsUsed oils = new OilsUsed();
                    oils.ApptDate = apptDate;
                    oils.Guid = patientAppt;
                    oils.OilsUsed1 = oil;
                    oilDataContext.OilsUseds.InsertOnSubmit(oils);
                    oilDataContext.SubmitChanges();
                }
            }
        }
        else
        {
            PatientAppointmentInfomationDataContext piDC = new PatientAppointmentInfomationDataContext("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\SoftwareDevelopmentProjects\\WebProjects\\myTherapist\\myTherapist\\App_Data\\myTherapist.mdf;Integrated Security=True");
            PatientAppointmentInformation patientRecord1 = null;
            Guid patientGuid = Guid.Empty;

            if (Session["PatientHistoryGuid"] != null)
            {
                string guidstring = Session["PatientHistoryGuid"].ToString();
                patientGuid = Guid.Parse(guidstring);
            }

            var appointmentquery = from patients in piDC.PatientAppointmentInformations where patients.GUID == patientGuid select patients;
            patientRecord1 = appointmentquery.First();

            patientRecord1.ImageBeforeTherapy = ImageBefore.ImageUrl;
            patientRecord1.ImageAfterTherapy = ImageAfter.ImageUrl;

            piDC.SubmitChanges();
        }
    }
}