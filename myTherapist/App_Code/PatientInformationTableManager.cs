using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace DatabaseObjects
{

    public class Patient
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }
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

        public PatientInformationTableManager()
        {
            //
            // TODO: Add constructor logic here
            //
            pi = new PatientInformation();
            patientDC = new PatientDataContext(WebConfigurationManager.ConnectionStrings["MyTherapistDatabaseConnectionString"].ConnectionString);
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
            pi.BirthDate = patient.Birthday;
            pi.TelephoneNumber = patient.PhoneNumber;
            pi.EmailAddress = patient.Email;

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

        }

        public Patient FindPatient(Patient patient)
        {
            IQueryable<PatientInformation> apatient = from patients in patientDC.PatientInformations where (patients.Id == patient.Id) select patients;

            try
            {
                pi = apatient.Single<PatientInformation>();
                patient.FirstName = pi.FirstName;
                patient.LastName = pi.LastName;
                patient.Birthday = DateTime.Parse(pi.BirthDate.Value.ToShortDateString());
                patient.PhoneNumber = pi.TelephoneNumber;
                patient.Email = pi.EmailAddress;
            }
            catch (Exception ex)
            {
                ex.GetHashCode();
            }

            return patient;
        }

    }
}

