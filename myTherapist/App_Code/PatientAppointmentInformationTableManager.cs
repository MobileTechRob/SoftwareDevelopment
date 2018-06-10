using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using MyTherapistEncryption;

namespace DatabaseObjects
{
    public class PatientAppointment
    {
        public int PatientId { get; set; }
        public Guid ApptId { get; set; }
        public DateTime AppointmentDate { get; set; }

        public string PulseKD1 { get; set; }
        public string PulseRLU { get; set; }
        public string PulseSP { get; set; }
        public string PulseKD2 { get; set; }
        public string PulseLHT { get; set; }
        public string PulseLV { get; set; }
        
        public string ImageBeforeTherapy { get; set; }
        public string ImageAfterTherapy { get; set; }

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
        MyTherapistEncryption.SecurityController dataEncryptionAlgorithm = null;
               
        public PatientAppointmentInformationTableManager()
        {
            //
            // TODO: Add constructor logic here
            //
            patientApptDataContext = new PatientAppointmentInfomationDataContext(WebConfigurationManager.ConnectionStrings["MyTherapistDatabaseConnectionString"].ConnectionString);
            patientAppointmentInformation = new PatientAppointmentInformation();
            dataEncryptionAlgorithm = new MyTherapistEncryption.SecurityController();
        }

        public void Delete(PatientAppointment patientApptInfo)
        {
            var patientApptsToDelete = patientApptDataContext.PatientAppointmentInformations.Select(patientAppt => patientAppt.ApptId == patientApptInfo.ApptId);

            if (patientApptsToDelete.Count() > 0)
            {
                IEnumerable<PatientAppointmentInformation> patientApptDelete = (from patientAppts in patientApptDataContext.PatientAppointmentInformations where patientAppts.PatientId == patientApptInfo.PatientId select patientAppts);
                patientApptDataContext.PatientAppointmentInformations.DeleteAllOnSubmit(patientApptDelete);
                patientApptDataContext.SubmitChanges();
            }
        }

        public PatientAppointment FindPatientAppointment(PatientAppointment patientAppt)
        {
            PatientAppointmentInformation patientApptRecord = null; ;
            PatientAppointment patientAppt1 = new PatientAppointment();

            var appointmentquery = from patients in patientApptDataContext.PatientAppointmentInformations where patients.ApptId == patientAppt.ApptId select patients;

            patientApptRecord = appointmentquery.First();

            patientAppt1.AppointmentDate = patientApptRecord.ApptDate;
            patientAppt1.ApptId = patientApptRecord.ApptId;
            patientAppt1.ImageAfterTherapy = dataEncryptionAlgorithm.DecryptData(patientApptRecord.ImageAfterTherapy);
            patientAppt1.ImageBeforeTherapy = dataEncryptionAlgorithm.DecryptData(patientApptRecord.ImageBeforeTherapy);
            patientAppt1.OilsAndTherapy = dataEncryptionAlgorithm.DecryptData(patientApptRecord.TherapyPerformed);
            patientAppt1.SessionGoals = dataEncryptionAlgorithm.DecryptData(patientApptRecord.SessionGoals);
            
            patientAppt1.PulseKD1 = dataEncryptionAlgorithm.DecryptData(patientApptRecord.KD1);
            patientAppt1.PulseKD2 = dataEncryptionAlgorithm.DecryptData(patientApptRecord.KD2);
            patientAppt1.PulseLHT = dataEncryptionAlgorithm.DecryptData(patientApptRecord.LHT);
            patientAppt1.PulseLV = dataEncryptionAlgorithm.DecryptData(patientApptRecord.LV);
            patientAppt1.PulseRLU = dataEncryptionAlgorithm.DecryptData(patientApptRecord.RLU);
            patientAppt1.PulseSP = dataEncryptionAlgorithm.DecryptData(patientApptRecord.SP);
            
            return patientAppt1;
        }

        public void Update(PatientAppointment patientAppt)
        {
            PatientAppointmentInformation patientRecord1 = null;
            bool insert = false;

            try
            {
                var appointmentquery = from patients in patientApptDataContext.PatientAppointmentInformations where patients.ApptId == patientAppt.ApptId select patients;
                patientRecord1 = appointmentquery.First();

                patientRecord1.LV = dataEncryptionAlgorithm.EncryptData(patientAppt.PulseLV);
                patientRecord1.RLU = dataEncryptionAlgorithm.EncryptData(patientAppt.PulseRLU);
                patientRecord1.KD1 = dataEncryptionAlgorithm.EncryptData(patientAppt.PulseKD1);
                patientRecord1.SP = dataEncryptionAlgorithm.EncryptData(patientAppt.PulseSP);
                patientRecord1.LHT = dataEncryptionAlgorithm.EncryptData(patientAppt.PulseLHT);
                patientRecord1.KD2 = dataEncryptionAlgorithm.EncryptData(patientAppt.PulseKD2);

                patientRecord1.ImageBeforeTherapy = dataEncryptionAlgorithm.EncryptData(patientAppt.ImageBeforeTherapy);
                patientRecord1.ImageAfterTherapy = dataEncryptionAlgorithm.EncryptData(patientAppt.ImageAfterTherapy);

                patientRecord1.TherapyPerformed = dataEncryptionAlgorithm.EncryptData(patientAppt.OilsAndTherapy);
                patientRecord1.SessionGoals = dataEncryptionAlgorithm.EncryptData(patientAppt.SessionGoals);           
            }
            catch (Exception ex)
            {
                ex.GetHashCode();
                insert = true;
            }

            if (insert)
            {
                patientRecord1 = new PatientAppointmentInformation();
                patientRecord1.PatientId = patientAppt.PatientId;
                patientRecord1.ApptId = Guid.NewGuid();
                patientRecord1.ApptDate = patientAppt.AppointmentDate;

                patientRecord1.LV = dataEncryptionAlgorithm.EncryptData(patientAppt.PulseLV);
                patientRecord1.RLU = dataEncryptionAlgorithm.EncryptData(patientAppt.PulseRLU);
                patientRecord1.KD1 = dataEncryptionAlgorithm.EncryptData(patientAppt.PulseKD1);
                patientRecord1.SP = dataEncryptionAlgorithm.EncryptData(patientAppt.PulseSP);
                patientRecord1.LHT = dataEncryptionAlgorithm.EncryptData(patientAppt.PulseLHT);
                patientRecord1.KD2 = dataEncryptionAlgorithm.EncryptData(patientAppt.PulseKD2);

                patientRecord1.ImageBeforeTherapy = dataEncryptionAlgorithm.EncryptData(patientAppt.ImageBeforeTherapy);
                patientRecord1.ImageAfterTherapy = dataEncryptionAlgorithm.EncryptData(patientAppt.ImageAfterTherapy);
                
                patientRecord1.TherapyPerformed = dataEncryptionAlgorithm.EncryptData(patientAppt.OilsAndTherapy);
                patientRecord1.SessionGoals = dataEncryptionAlgorithm.EncryptData(patientAppt.SessionGoals);

                patientApptDataContext.PatientAppointmentInformations.InsertOnSubmit(patientRecord1);
            }

            patientApptDataContext.SubmitChanges();
        }
    }
}

