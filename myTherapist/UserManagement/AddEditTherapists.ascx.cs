using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DatabaseObjects;


public partial class UserManagement_AddEditTherapists : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnSaveEditTherapist_Click(object sender, EventArgs e)
    {
        TherapistTableManager therapistTableManager = new TherapistTableManager();
        DatabaseObjects.MassageTherapists therapist = new DatabaseObjects.MassageTherapists();
     
        therapist.Id = Guid.Empty;
        therapist.Name = txtBoxTherapistName.Text;
        therapist.Password = txtBoxTherapistPassword.Text;

        therapistTableManager.UpdateTherapist(therapist);
    }
}


