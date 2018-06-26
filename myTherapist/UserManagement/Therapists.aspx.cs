using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DatabaseObjects;

public partial class UserManagement_Therapists : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        AddEditTherapists1.TherapistUpdated += AddEditTherapists1_TherapistUpdated;
        AddEditTherapists1.TherapistFound += AddEditTherapists1_TherapistFound;

        if (IsPostBack == false)
        {
            AddEditTherapists1.Visible = false;
            TherapistList1.Visible = true;
        }
    }

    private void AddEditTherapists1_TherapistFound(object sender, EventArgs e)
    {
        AddEditTherapists1.Visible = true;
        TherapistList1.Visible = false;
    }

    private void AddEditTherapists1_TherapistUpdated(object sender, EventArgs e)
    {
        btnAdd.Visible = true;
        btnEdit.Visible = true;

        AddEditTherapists1.Visible = false;
        TherapistList1.Visible = true;

        TherapistList1.Refresh();
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        btnAdd.Visible = false;
        AddEditTherapists1.Visible = true;
        TherapistList1.Visible = false;
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        if (Session["TherapistId"] == null)
        {
            UserAlert.Text = "Select a Therapist";
            return;
        }

        btnAdd.Visible = false;
        btnEdit.Visible = false;

        MassageTherapists massageTherapist = new MassageTherapists();
        massageTherapist.Id = Guid.Parse(Session["TherapistId"].ToString());

        AddEditTherapists1.FindTherapist(massageTherapist);        
    }
}