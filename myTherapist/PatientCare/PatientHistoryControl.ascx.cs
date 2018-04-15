using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataGridObjects;
using System.Data;

public partial class PatientCare_PatientHistoryControl : System.Web.UI.UserControl
{    
    MyDataGridPager pager = null;
    public event EventHandler AppointmentSelected;
    public event EventHandler EditPatientEvent;
    const int IMAGE_HEIGHT = 200;
    const int IMAGE_WIDTH = IMAGE_HEIGHT;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.Visible == false)
        {
            Session["PatientHistorySortOrder"] = "DESC";
            return;
        }
       
        if (Session["PatientId"] != null)
        {                       
            BuildGrid();
        }
        else
            Session["PatientHistorySortOrder"] = "DESC";
    }

    public void Refresh()
    {
        BuildGrid();
    }
    
    private void BuildGrid()
    {        
        pager = new MyDataGridPager();
        pager.NumberRowsToDisplay = 10;

        DataGridObject dataGridObj = new DataGridObject(WebConfigurationManager.ConnectionStrings["MyTherapistDatabaseConnectionString"].ConnectionString, "PatientAppointmentInformation");
        DatabaseRowObject dbRowObject = new DatabaseRowObject();
                
        dbRowObject.AddColumn("ApptDate", "Appointment Date", MyDataTypes.DATETIME, true, 35);
        dbRowObject.AddColumn("GUID", "", MyDataTypes.GUID, true, 35);
        dbRowObject.AddColumn("RLU", "RLU", MyDataTypes.STRING, false, 0);
        dbRowObject.AddColumn("SP", "SP", MyDataTypes.STRING, false, 0);
        dbRowObject.AddColumn("KD1", "KD", MyDataTypes.STRING, false, 0);
        dbRowObject.AddColumn("LHT", "LHT", MyDataTypes.STRING, false, 0);
        dbRowObject.AddColumn("LV", "LV", MyDataTypes.STRING, false, 0);
        dbRowObject.AddColumn("KD2", "KD ", MyDataTypes.STRING, false, 0);
        dbRowObject.AddColumn("ImageBeforeTherapy", "", MyDataTypes.STRING, false, 0);
        dbRowObject.AddColumn("ImageAfterTherapy", "", MyDataTypes.STRING, false, 0);
        dbRowObject.AddColumn("TherapyPerformed", "", MyDataTypes.STRING, false, 0);
        dbRowObject.AddColumn("SessionGoals", "", MyDataTypes.STRING, false, 0);
       
        dataGridObj.DatabaseRowObject = dbRowObject;








        if (IsPostBack == false)
        {
            Session["PatientHistorySortOrder"] = "DESC";
            pager.Sort = MyDataSort.DESC;
        }
        else
        {
            if (Session["PatientHistorySortOrder"] != null)
            {
                if (Session["PatientHistorySortOrder"].ToString() == "DESC")
                    pager.Sort = MyDataSort.DESC;
                else
                    pager.Sort = MyDataSort.ASC;
            }
        }

        dataGridObj.AddWhereClauseArgument("PatientId", Session["PatientId"].ToString());

        LinkButton linkButtonPatient = null;
        LinkButton linkButtonApptDate = null;
        TableRow row = new TableRow();
        TableCell cell = new TableCell();

        linkButtonPatient = new LinkButton();
        
        if ((Session["PatientFirstName"] != null) && (Session["PatientLastName"] != null))
            linkButtonPatient.Text = String.Format("{0} {1} {2}", Session["PatientFirstName"].ToString(), Session["PatientLastName"].ToString(), Session["PatientBirthDate"].ToString());

        linkButtonPatient.Command += linkButtonPatient_Command;
        linkButtonPatient.CommandArgument = Session["PatientId"].ToString();
        
        cell.Controls.Add(linkButtonPatient);

        row.Cells.Add(cell);       
              
        PatientApptData.Rows.Add(row);

        // spacer
        row = new TableRow();
        cell = new TableCell();
        cell.Text = "  ";
        cell.ColumnSpan = 7;
        row.Cells.Add(cell);
        PatientApptData.Rows.Add(row);

        row = new TableRow();
        cell = new TableCell();
        Image img = null;

        cell = new TableCell();
        cell.Text = " ";
        row.Cells.Add(cell);

        cell = new TableCell();
        cell.Style["font-weight"] = "bold";
        cell.Text = "RLU";
        row.Cells.Add(cell);

        cell = new TableCell();
        cell.Text = "SP";
        cell.Style["font-weight"] = "bold";
        row.Cells.Add(cell);

        cell = new TableCell();
        cell.Style["font-weight"] = "bold";
        cell.Text = "KD1";
        row.Cells.Add(cell);

        cell = new TableCell();
        cell.Style["font-weight"] = "bold";
        cell.Text = "LHT";
        row.Cells.Add(cell);

        cell = new TableCell();
        cell.Style["font-weight"] = "bold";
        cell.Text = "LV";
        row.Cells.Add(cell);

        cell = new TableCell();
        cell.Style["font-weight"] = "bold";
        cell.Text = "KD2";
        row.Cells.Add(cell);

        PatientApptData.Rows.Add(row);

        DataTable appointmentInfo = dataGridObj.BuildTable();

        System.Collections.IEnumerator iter = null;

        if (appointmentInfo.Rows.Count == 0)
        {

        }
        else
        {
            iter = appointmentInfo.Rows.GetEnumerator();

            while (iter.MoveNext())
            {
                DataRow dataRow = (DataRow)iter.Current;

                row = new TableRow();
                cell = new TableCell();
                linkButtonApptDate = new LinkButton();
                linkButtonApptDate.Text = dataRow.ItemArray[0].ToString();
                linkButtonApptDate.Command += linkButtonApptDate_Command;
                linkButtonApptDate.CommandArgument = dataRow.ItemArray[dbRowObject.ColumnIndex("ApptDate")].ToString();

                cell.Controls.Add(linkButtonApptDate);
                row.Cells.Add(cell);

                cell = new TableCell();
                cell.Text = dataRow.ItemArray[dbRowObject.ColumnIndex("RLU")].ToString();
                row.Cells.Add(cell);

                cell = new TableCell();
                cell.Text = dataRow.ItemArray[dbRowObject.ColumnIndex("SP")].ToString();
                row.Cells.Add(cell);

                cell = new TableCell();
                cell.Text = dataRow.ItemArray[dbRowObject.ColumnIndex("KD1")].ToString();
                row.Cells.Add(cell);

                cell = new TableCell();
                cell.Text = dataRow.ItemArray[dbRowObject.ColumnIndex("LHT")].ToString();
                row.Cells.Add(cell);

                cell = new TableCell();
                cell.Text = dataRow.ItemArray[dbRowObject.ColumnIndex("LV")].ToString();
                row.Cells.Add(cell);

                cell = new TableCell();
                cell.Text = dataRow.ItemArray[dbRowObject.ColumnIndex("KD2")].ToString();
                row.Cells.Add(cell);

                PatientApptData.Rows.Add(row);

                row = new TableRow();
                cell = new TableCell();
                row.Cells.Add(cell);
               
                cell = new TableCell();
                cell.ColumnSpan = 3;
                img = new Image();
                img.ImageUrl = dataRow.ItemArray[dbRowObject.ColumnIndex("ImageBeforeTherapy")].ToString();
                img.ImageAlign = ImageAlign.Middle;
                img.Width = IMAGE_WIDTH;
                img.Height = IMAGE_HEIGHT;
                cell.HorizontalAlign = HorizontalAlign.Left;
                cell.Controls.Add(img);
                row.Cells.Add(cell);

                cell = new TableCell();
                cell.ColumnSpan = 3;
                img = new Image();
                img.ImageUrl = dataRow.ItemArray[dbRowObject.ColumnIndex("ImageAfterTherapy")].ToString();
                img.ImageAlign = ImageAlign.Middle;
                img.Width = IMAGE_WIDTH;
                img.Height = IMAGE_HEIGHT;
                cell.HorizontalAlign = HorizontalAlign.Left;
                cell.Controls.Add(img);
                row.Cells.Add(cell);
                PatientApptData.Rows.Add(row);

                // spacer 
                row = new TableRow();
                cell = new TableCell();
                cell.Text = "  ";
                cell.ColumnSpan = 7;
                row.Cells.Add(cell);
                PatientApptData.Rows.Add(row);

                ///  therapy performed
                row = new TableRow();
                cell = new TableCell();
                cell.Text = dataRow.ItemArray[dbRowObject.ColumnIndex("TherapyPerformed")].ToString(); 
                cell.ColumnSpan = 7;
                row.Cells.Add(cell);                
                PatientApptData.Rows.Add(row);

                // session goals
                row = new TableRow();
                cell = new TableCell();
                cell.Text = dataRow.ItemArray[dbRowObject.ColumnIndex("SessionGoals")].ToString();
                cell.ColumnSpan = 7;
                row.Cells.Add(cell);
                PatientApptData.Rows.Add(row);                
            }
        }
    }

    private void linkButtonApptDate_Command(object sender, CommandEventArgs e)
    {        
        LinkButton lnk = (LinkButton)sender;
        AppointmentSelectedEvent apptEvent = new AppointmentSelectedEvent(Guid.Parse(lnk.CommandArgument));

        Session["EditPatientAppt"] = "true";

        if (AppointmentSelected != null)
            AppointmentSelected(this, apptEvent);
    }
    
    private void linkButtonPatient_Command(object sender, CommandEventArgs e)
    {
        // set this so when at the patient information screen , the program comes back to patitent appoinment screen
        Session["EditPatientFromHistory"] = "true";

        LinkButton lnk = (LinkButton)sender;
        EditPatientEvent patientEvent = new EditPatientEvent(Convert.ToInt32(lnk.CommandArgument));

        if (EditPatientEvent != null)
            EditPatientEvent(this, patientEvent);
    }
    
    private void LinkButton_Click(object sender, EventArgs e)
    {
        string s = "ast";
        LinkButton lnk = (LinkButton)sender;
    }

    public bool ShowIndividualAppt()
    {
        if ((Session["PatientHistoryAppointmentDate"] == null) && (Session["PatientHistoryPatientId"] == null))
        {
            lblNotice.Text = "Select an appointment";
            return false;
        }

        return true;
    }

}



