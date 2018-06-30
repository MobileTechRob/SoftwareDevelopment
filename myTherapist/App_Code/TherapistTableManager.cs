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

        public MassageTherapists(string name, string password)
        {
            Name = name;
            Password = password;
        }

        public MassageTherapists(string id)
        {
            Id = Guid.Parse(id);
        }

        public MassageTherapists(Guid id)
        {
            Id = id;
        }

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
            bool newrecord = false;
            Therapist therapistRecord = null;

            try
            {
                var therapistQuery = from therapist in therapistDatabaseContext.Therapists where therapist.Id == person.Id select therapist;
                therapistRecord = therapistQuery.Single<Therapist>();                
                therapistRecord.Name = dataEncryptionAlgo.EncryptData(person.Name);
                therapistRecord.Password = dataEncryptionAlgo.EncryptData(person.Password);                
            }
            catch (Exception ex)
            {
                newrecord = true;
            }

            if (newrecord)
            {
                therapistRecord = new Therapist();

                therapistRecord.Id = Guid.NewGuid();
                therapistRecord.Name = dataEncryptionAlgo.EncryptData(person.Name);
                therapistRecord.Password = dataEncryptionAlgo.EncryptData(person.Password);

                therapistDatabaseContext.Therapists.InsertOnSubmit(therapistRecord);                
            }

            therapistDatabaseContext.SubmitChanges();
        }

        public MassageTherapists FindTherapist(MassageTherapists person)
        {
            MassageTherapists massageTherapist= null;
            Therapist therapistRecord = null;

            if (person.Id != Guid.Empty)
            {
                try
                {
                    var query = from therapist in therapistDatabaseContext.Therapists where therapist.Id == person.Id select therapist;
                    therapistRecord = query.First<Therapist>();

                    massageTherapist = new MassageTherapists(therapistRecord.Id);

                    MyTherapistEncryption.SecurityController dataEncryptionAlgo = new MyTherapistEncryption.SecurityController();

                    massageTherapist.Name = dataEncryptionAlgo.DecryptData(therapistRecord.Name);
                    massageTherapist.Password = dataEncryptionAlgo.DecryptData(therapistRecord.Password);
                }
                catch (Exception ex)
                {
                    ex.GetHashCode();
                }
            }
            else
            {
                try
                {
                    MyTherapistEncryption.SecurityController dataEnryptionAlgo = new SecurityController();

                    string encryptedName = dataEnryptionAlgo.EncryptData(person.Name);
                    string encryptedPassword = dataEnryptionAlgo.EncryptData(person.Password);
                    
                    var query = from therapist in therapistDatabaseContext.Therapists where (therapist.Name == encryptedName && therapist.Password == encryptedPassword) select therapist;
                    therapistRecord = query.First<Therapist>();

                    massageTherapist = new MassageTherapists(therapistRecord.Id);

                    MyTherapistEncryption.SecurityController dataEncryptionAlgo = new MyTherapistEncryption.SecurityController();

                    massageTherapist.Name = dataEncryptionAlgo.DecryptData(therapistRecord.Name);
                    massageTherapist.Password = dataEncryptionAlgo.DecryptData(therapistRecord.Password);
                }
                catch (Exception ex)
                {
                }
            }

            return massageTherapist;
        }
        
        public void DeleteTherapist(MassageTherapists person)
        {
            var therapistRecord = from therapist in therapistDatabaseContext.Therapists where therapist.Id == person.Id select therapist;
           
            therapistDatabaseContext.Therapists.DeleteOnSubmit(therapistRecord.Single<Therapist>());
                       
            therapistDatabaseContext.SubmitChanges();
        }
    }
}

