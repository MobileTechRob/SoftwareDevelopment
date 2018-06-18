using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserManagement_Therapists : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        AddEditTherapists1.Visible = false;
        TherapistList1.Visible = true;
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {

    }
}