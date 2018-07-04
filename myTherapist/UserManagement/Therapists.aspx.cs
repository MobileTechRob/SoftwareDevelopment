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
        AddEditTherapists1.TherapistUpdateCanceled += AddEditTherapists1_TherapistUpdateCanceled;
        TherapistList1.MyTherapistUserSelected += TherapistList1_MyTherapistUserSelected;
        TherapistList1.MyTherapistUserNotSelected += TherapistList1_MyTherapistUserNotSelected;
      

        if (IsPostBack == false)
        {
            AddEditTherapists1.Visible = false;
            TherapistList1.Visible = true;
        }

        if (Session[CommonDefinitions.CommonDefinitions.MYTHERAPIST_SELECTED] != null)
            btnDelete.Enabled = false;
        else
            btnDelete.Enabled = true;
    }

    private void TherapistList1_MyTherapistUserNotSelected(object sender, EventArgs e)
    {
        btnDelete.Enabled = true;
        Session[CommonDefinitions.CommonDefinitions.MYTHERAPIST_SELECTED] = null;
    }

    private void TherapistList1_MyTherapistUserSelected(object sender, EventArgs e)
    {
        btnDelete.Enabled = false;
        Session[CommonDefinitions.CommonDefinitions.MYTHERAPIST_SELECTED] = "";
            
            ;
    }

    private void AddEditTherapists1_TherapistUpdateCanceled(object sender, EventArgs e)
    {
        AddEditTherapists1.Visible = false;
        TherapistList1.Visible = true;
        Session["TherapistId"] = null;
        MainMenuButtonsAppear();
    }

    private void AddEditTherapists1_TherapistFound(object sender, EventArgs e)
    {
        AddEditTherapists1.Visible = true;
        TherapistList1.Visible = false;
    }

    private void AddEditTherapists1_TherapistUpdated(object sender, EventArgs e)
    {
        MainMenuButtonsAppear();

        AddEditTherapists1.Visible = false;
        TherapistList1.Visible = true;

        TherapistList1.Refresh();
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        HideMainMenuButtons();
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

        UserAlert.Text = "";

        HideMainMenuButtons();
        
        MassageTherapists massageTherapist = new MassageTherapists(Session["TherapistId"].ToString());
        
        AddEditTherapists1.FindTherapist(massageTherapist);        
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (Session["TherapistId"] == null)
        {
            UserAlert.Text = "Select a Therapist";
            UserAlert.ForeColor = System.Drawing.Color.Red;
            return;
        }
        else if ((Session["TherapistId"] != null) && (Session["DeleteTherapist"] == null))
        {
            UserAlert.Text = "Click Delete again to confirm.";
            UserAlert.ForeColor = System.Drawing.Color.Red;
            Session["DeleteTherapist"] = "1";
        }
        else
        {           
            TherapistTableManager therapistTableManager = new TherapistTableManager();
            therapistTableManager.DeleteTherapist(new MassageTherapists(Session["TherapistId"].ToString()));
            TherapistList1.Refresh();
            Session["DeleteTherapist"] = null;
            UserAlert.Text = "";            
        }
   }

    public void HideMainMenuButtons()
    {
        btnAdd.Visible = false;
        btnEdit.Visible = false;
        btnDelete.Visible = false;
        btnLogOut.Visible = false;
    }

    public void MainMenuButtonsAppear()
    {
        btnAdd.Visible = true;
        btnEdit.Visible = true;
        btnDelete.Visible = true;
        btnLogOut.Visible = true;
    }
    

    protected void btnLogOut_Click(object sender, EventArgs e)
    {
        Session.Clear();
        Response.Redirect("../MainMenu.aspx");
    }
}