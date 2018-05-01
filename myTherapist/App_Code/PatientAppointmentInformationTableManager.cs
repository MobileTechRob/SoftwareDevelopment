using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace DatabaseObjects
{
    public class PatientAppointment
    {
        public Guid ApptId { get; set; }
        public DateTime AppointmentDate { get; set; }

        public string PulseKD1 { get; set; }
        public string PulseRLU { get; set; }
        public string PulseSP { get; set; }
        public string PulseKD2 { get; set; }
        public string PulseLHT { get; set; }
        public string PulseLV { get; set; }
        
        public string ImageBefore { get; set; }
        public string ImageAfter { get; set; }

        public string OilsAndTherapy { get; set; }
        public string SessionGoals { get; set; }

        public PatientAppointment()
        {

        }
    }

    /// <summary>
    /// Summary description for PatientAppointmentInformationTableManager
    /// </summary>
    public class PatientAppointmentInformationTableManager
    {
        PatientAppointmentInfomationDataContext patientApptDataContext = null;
        PatientAppointmentInformation patientAppointmentInformation = null;

        public PatientAppointmentInformationTableManager()
        {
            //
            // TODO: Add constructor logic here
            //
            patientApptDataContext = new PatientAppointmentInfomationDataContext(WebConfigurationManager.ConnectionStrings["MyTherapistDatabaseConnectionString"].ConnectionString);
            patientAppointmentInformation = new PatientAppointmentInformation();
        }

        public void Delete(PatientAppointment patientAppt)
        {

        }

        public void Update(PatientAppointment patientAppt)
        {
            PatientAppointmentInformation patientRecord1 = null;

            var appointmentquery = from patients in patientApptDataContext.PatientAppointmentInformations where patients.GUID == patientAppt.ApptId select patients;
            patientRecord1 = appointmentquery.First();

            patientRecord1.LV = patientAppt.PulseLV;
            patientRecord1.RLU = patientAppt.PulseRLU;
            patientRecord1.KD1 = patientAppt.PulseKD1;
            patientRecord1.SP = patientAppt.PulseSP;
            patientRecord1.LHT = patientAppt.PulseLHT;
            patientRecord1.KD2 = patientAppt.PulseKD2; 

            patientRecord1.ImageBeforeTherapy = patientAppt.ImageBefore;
            patientRecord1.ImageAfterTherapy = patientAppt.ImageAfter;

            patientRecord1.TherapyPerformed = patientAppt.OilsAndTherapy;
            patientRecord1.SessionGoals = patientAppt.SessionGoals;
        }
    }
}

