using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DatabaseObjects
{

    public class PatientInformation
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        public PatientInformation()
        {
        }
    }

    /// <summary>
    /// Summary description for PatientInformationTableManager
    /// </summary>
    public class PatientInformationTableManager
    {
        public PatientInformationTableManager()
        {
            //
            // TODO: Add constructor logic here
            //
        }
    }
}

