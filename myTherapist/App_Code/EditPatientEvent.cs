using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for EditPatientEvent
/// </summary>
public class EditPatientEvent : EventArgs
{
    public int PatientID { get; set; }

    public EditPatientEvent(int patientId)
    {
        //
        // TODO: Add constructor logic here
        //
        PatientID = patientId;
    }
}