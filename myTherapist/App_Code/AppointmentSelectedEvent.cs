using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for AppointmentSelectedEvent
/// </summary>
public class AppointmentSelectedEvent : EventArgs
{
    public Guid appointmentGuid { get; set; }

    public AppointmentSelectedEvent(Guid apptGuid)
    {
        //
        // TODO: Add constructor logic here
        //
        appointmentGuid = apptGuid;
    }
}