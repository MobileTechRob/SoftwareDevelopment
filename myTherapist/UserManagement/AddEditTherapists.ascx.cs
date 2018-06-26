using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DatabaseObjects;
using MyTherapistEncryption;

public partial class UserManagement_AddEditTherapists : System.Web.UI.UserControl
{
    public event EventHandler TherapistUpdated;
    public event EventHandler TherapistFound;

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnSaveEditTherapist_Click(object sender, EventArgs e)
    {
        TherapistTableManager therapistTableManager = new TherapistTableManager();
        DatabaseObjects.MassageTherapists therapist = new DatabaseObjects.MassageTherapists();

        if (Session["TherapistId"] == null)
            therapist.Id = Guid.Empty;
        else
            therapist.Id = Guid.Parse(Session["TherapistId"].ToString());

        therapist.Name = txtBoxTherapistName.Text;
        therapist.Password = txtBoxTherapistPassword.Text;

        therapistTableManager.UpdateTherapist(therapist);

        Session["TherapistId"] = null;

        if (TherapistUpdated != null)
            TherapistUpdated(this, e); 
    }

    public void FindTherapist(MassageTherapists person)
    {
        TherapistTableManager therapistTableManager = new TherapistTableManager();
        MassageTherapists therapist = null;
        MyTherapistEncryption.SecurityController dataEncryption = new SecurityController();

        therapist =  therapistTableManager.FindTherapist(person);

        if (therapist != null)
        {            
            txtBoxTherapistName.Text = therapist.Name;
            txtBoxTherapistPassword.Text = therapist.Name; 
        }

        if (TherapistFound != null)
            TherapistFound(this, null);
    }
}


