using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using MyTherapistEncryption;

namespace DatabaseObjects
{
    public class Patient
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Birthday { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        public Patient()
        {
        }
    }

    /// <summary>
    /// Summary description for PatientInformationTableManager
    /// </summary>
    public class PatientInformationTableManager
    {
        private PatientInformation pi = null;
        private PatientDataContext patientDC = null;
        private MyTherapistEncryption.SecurityController dataEncryptionAlgorithm = null;

        public PatientInformationTableManager()
        {
            //
            // TODO: Add constructor logic here
            //
            pi = new PatientInformation();
            patientDC = new PatientDataContext(WebConfigurationManager.ConnectionStrings["MyTherapistDatabaseConnectionString"].ConnectionString);
            dataEncryptionAlgorithm = new MyTherapistEncryption.SecurityController();
        }

        public void Update(Patient patient)
        {
            IQueryable<PatientInformation> apatient = from patients in patientDC.PatientInformations where (patients.Id == patient.Id) select patients;
            bool insert = false;

            try
            {
                pi = apatient.Single<PatientInformation>();
            }
            catch (Exception ex)
            {
                insert = true;
                ex.GetHashCode();
            }

            pi.FirstName = patient.FirstName;
            pi.LastName = patient.LastName;

            pi.BirthDate = dataEncryptionAlgorithm.EncryptData(patient.Birthday);
            pi.TelephoneNumber = dataEncryptionAlgorithm.EncryptData(patient.PhoneNumber);
            pi.EmailAddress = dataEncryptionAlgorithm.EncryptData(patient.Email);

            if (insert)
            {
                patientDC.PatientInformations.InsertOnSubmit(pi);
                patientDC.SubmitChanges();
            }
            else
            {
                patientDC.SubmitChanges();
            }
        }

        public void Delete(Patient patient)
        {
            PatientDataContext patientDC = new PatientDataContext(WebConfigurationManager.ConnectionStrings["MyTherapistDatabaseConnectionString"].ConnectionString);
            PatientInformation patientToDelete = new PatientInformation();


            patientToDelete = patientDC.PatientInformations.Single(patientRecord => patientRecord.Id == patient.Id);
            patientDC.PatientInformations.DeleteOnSubmit(patientToDelete);
            patientDC.SubmitChanges();           
        }

        public Patient FindPatient(Patient patient)
        {
            IQueryable<PatientInformation> apatient = from patients in patientDC.PatientInformations where (patients.Id == patient.Id) select patients;

            try
            {
                pi = apatient.Single<PatientInformation>();
                patient.FirstName = pi.FirstName;
                patient.LastName = pi.LastName;
                patient.Birthday = dataEncryptionAlgorithm.DecryptData(pi.BirthDate);
                patient.PhoneNumber = dataEncryptionAlgorithm.DecryptData(pi.TelephoneNumber);
                patient.Email = dataEncryptionAlgorithm.DecryptData(pi.EmailAddress);
            }
            catch (Exception ex)
            {
                ex.GetHashCode();
            }

            return patient;
        }

    }
}

