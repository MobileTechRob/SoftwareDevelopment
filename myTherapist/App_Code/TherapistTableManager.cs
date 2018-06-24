using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using CommonDefinitions;
using MyTherapistEncryption;

namespace DatabaseObjects
{
    public class MassageTherapists
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
        TherapistsDataContext therapistDatabaseContext;

        public TherapistTableManager()
        {
            //
            // TODO: Add constructor logic here
            //            
            therapistDatabaseContext = new TherapistsDataContext(WebConfigurationManager.ConnectionStrings[CommonDefinitions.CommonDefinitions.MYTHERAPIST_DATABASE_CONNECTION_STRING].ConnectionString);          
        }

        public void UpdateTherapist(MassageTherapists person)
        {
            MyTherapistEncryption.SecurityController dataEncryptionAlgo = new SecurityController();

            if (person.Id.Equals(Guid.Empty))
            {
                person.Id = Guid.NewGuid();
                
                Therapist therapistRecord = new Therapist();

                therapistRecord.Id = person.Id;
                therapistRecord.Name = dataEncryptionAlgo.EncryptData(person.Name);
                therapistRecord.Password = dataEncryptionAlgo.EncryptData(person.Password);

                therapistDatabaseContext.Therapists.InsertOnSubmit(therapistRecord);
                therapistDatabaseContext.SubmitChanges();
            }
            
            //var therapistQuery = from therapist in therapistDatabaseContext.Therapists where therapist.Id == person.Id select therapist;
                       
        }

        public MassageTherapists FindTherapist(MassageTherapists person)
        {
            MassageTherapists massageTherapist= null;
            Therapist therapistRecord = null;

            try
            {
                var query = from therapist in therapistDatabaseContext.Therapists where therapist.Id == person.Id select therapist;
                therapistRecord = query.First<Therapist>();

                massageTherapist = new MassageTherapists();

                MyTherapistEncryption.SecurityController dataEncryptionAlgo = new MyTherapistEncryption.SecurityController();

                massageTherapist.Id = therapistRecord.Id;
                massageTherapist.Name = dataEncryptionAlgo.DecryptData(therapistRecord.Name);
                massageTherapist.Password = dataEncryptionAlgo.DecryptData(therapistRecord.Password);
            }
            catch (Exception ex)
            {
                ex.GetHashCode();
            }

            return massageTherapist;
        }
        
        public void DeleteTherapist(MassageTherapists person)
        {

        }
    }
}

