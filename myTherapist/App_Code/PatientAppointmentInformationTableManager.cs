using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DatabaseObjects
{
    public class PatientAppointmentInformation
    {
        public string PulseKD1 { get; set; }
        public string PulseRLU { get; set; }
        public string PulseSP { get; set; }
        public string PulseKD2 { get; set; }
        public string PulseLHT { get; set; }
        public string PulseLV { get; set; }

        public DateTime AppointmentDate { get; set; }
        public string OilsAndTherapy { get; set; }
        public string SessionGoals { get; set; }

        public PatientAppointmentInformation()
        {

        }
    }

    /// <summary>
    /// Summary description for PatientAppointmentInformationTableManager
    /// </summary>
    public class PatientAppointmentInformationTableManager
    {
        public PatientAppointmentInformationTableManager()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public void Delete(PatientAppointmentInformation patient)
        {

        }

        public void Update(PatientAppointmentInformation patient)
        {

        }
    }
}

