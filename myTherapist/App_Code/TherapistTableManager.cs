using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using CommonDefinitions;

namespace DatabaseObjects
{
    public class Therapist
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
    }

    /// <summary>
    /// Summary description for TherapistTableManager
    /// </summary>
    public class TherapistTableManager
    {
        public TherapistTableManager()
        {
            //
            // TODO: Add constructor logic here
            //            
            TherapistsDataContext therapistDatabaseContext = new TherapistsDataContext(WebConfigurationManager.ConnectionStrings[CommonDefinitions.CommonDefinitions.MYTHERAPIST_DATABASE_CONNECTION_STRING].ConnectionString);          
        }

        public void UpdateTherapist(Therapist person)
        {

        }

        public void DeleteTherapist(Therapist person)
        {

        }
    }
}

