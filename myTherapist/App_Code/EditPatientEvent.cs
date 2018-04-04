using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for EditPatientEvent
/// </summary>
public class EditPatientEvent : EventArgs
{
    public Guid PatientID { get; set; }

    public EditPatientEvent(Guid patientId)
    {
        //
        // TODO: Add constructor logic here
        //
        PatientID = patientId;
    }
}