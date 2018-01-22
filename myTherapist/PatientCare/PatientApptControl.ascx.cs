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
    }

    public void LoadPatientAppt()
    {

        DateTime dt = DateTime.MinValue;
        long id = 0;

        if ((Session["EditPatient"] != null) && (Session["EditPatient"].ToString() == "true"))
        {
            PatientAppointmentInfomationDataContext piDC = new PatientAppointmentInfomationDataContext("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\SoftwareDevelopmentProjects\\WebProjects\\myTherapist\\myTherapist\\App_Data\\myTherapist.mdf;Integrated Security=True");
           
            //PatientAppointmentInformation patientRecord = null;// new PatientAppointmentInformation();
            PatientAppointmentInformation patientRecord1 = null;

            if (Session["PatientHistoryAppointmentDate"] != null)
            {
                string datestring = Session["PatientHistoryAppointmentDate"].ToString();               
                dt = DateTime.Parse(datestring);
            }

            if (Session["PatientHistoryPatientId"] != null)
            {
                string idstring = Session["PatientHistoryPatientId"].ToString();
                id = long.Parse(idstring);
            }

            var appointmentquery = from patients in piDC.PatientAppointmentInformations where patients.ApptDate == dt select patients;



            //patientRecord1 = piDC.PatientAppointmentInformations.Single(patientRecord => (patientRecord.ApptDate == dt && patientRecord.PatientId == id));
            //patientRecord1 = piDC.PatientAppointmentInformations.Single(appointmentquery.First);
            patientRecord1 = appointmentquery.First();



            string RLU = patientRecord1.RLU;
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
        
        string therapy = txtboxTherapyPerformed.Text;        
        string oilsUsed = txtboxOilsUsed.Text;        
        string sessionGoals = txtBoxSessionGoals.Text;

        PatientAppointmentInfomationDataContext patientApptDataContext = new PatientAppointmentInfomationDataContext("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\SoftwareDevelopmentProjects\\WebProjects\\myTherapist\\myTherapist\\App_Data\\myTherapist.mdf;Integrated Security=True");
        PatientAppointmentInformation patientAppointmentInformation = new PatientAppointmentInformation();

        string apptDateStr = DateTime.Now.ToShortDateString() + " " + DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString();
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

        string[] p = { ",", " ", "\r\n" };

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
    }

}