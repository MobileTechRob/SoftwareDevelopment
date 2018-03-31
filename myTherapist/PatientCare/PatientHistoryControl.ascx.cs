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
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.Visible == false)
        {
            Session["PatientHistorySortOrder"] = "DESC";
            return;
        }

        lblPatientName.Text = "";

        if ((Session["PatientFirstName"] != null) && (Session["PatientLastName"] != null))
            lblPatientName.Text = String.Format("{0} {1}", Session["PatientFirstName"].ToString() , Session["PatientLastName"].ToString());

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

        const int IMAGE_HEIGHT = 200;
        const int IMAGE_WIDTH = IMAGE_HEIGHT;

        TableRow row = new TableRow();
        TableCell cell = new TableCell();
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
                LinkButton linkButton = new LinkButton();
                linkButton.Text = dataRow.ItemArray[0].ToString();
                linkButton.Command += LinkButton_Command;
                linkButton.CommandArgument = dataRow.ItemArray[1].ToString();

                cell.Controls.Add(linkButton);
                row.Cells.Add(cell);

                cell = new TableCell();
                cell.Text = dataRow.ItemArray[2].ToString();
                row.Cells.Add(cell);

                cell = new TableCell();
                cell.Text = dataRow.ItemArray[3].ToString();
                row.Cells.Add(cell);

                cell = new TableCell();
                cell.Text = dataRow.ItemArray[4].ToString();
                row.Cells.Add(cell);

                cell = new TableCell();
                cell.Text = dataRow.ItemArray[5].ToString();
                row.Cells.Add(cell);

                cell = new TableCell();
                cell.Text = dataRow.ItemArray[6].ToString();
                row.Cells.Add(cell);

                cell = new TableCell();
                cell.Text = dataRow.ItemArray[7].ToString();
                row.Cells.Add(cell);

                PatientApptData.Rows.Add(row);

                row = new TableRow();
                cell = new TableCell();
                row.Cells.Add(cell);

                cell = new TableCell();
                cell.ColumnSpan = 3;
                img = new Image();
                img.ImageUrl = dataRow.ItemArray[8].ToString();
                img.ImageAlign = ImageAlign.Middle;
                img.Width = IMAGE_WIDTH;
                img.Height = IMAGE_HEIGHT;
                cell.HorizontalAlign = HorizontalAlign.Left;
                cell.Controls.Add(img);
                row.Cells.Add(cell);

                cell = new TableCell();
                cell.ColumnSpan = 3;
                img = new Image();
                img.ImageUrl = dataRow.ItemArray[9].ToString();
                img.ImageAlign = ImageAlign.Middle;
                img.Width = IMAGE_WIDTH;
                img.Height = IMAGE_HEIGHT;
                cell.HorizontalAlign = HorizontalAlign.Left;
                cell.Controls.Add(img);
                row.Cells.Add(cell);
                PatientApptData.Rows.Add(row);

                row = new TableRow();
                cell = new TableCell();
                cell.Text = "  ";
                cell.ColumnSpan = 7;
                row.Cells.Add(cell);
                PatientApptData.Rows.Add(row);
            }
        }
    }

    private void LinkButton_Command(object sender, CommandEventArgs e)
    {        
        LinkButton lnk = (LinkButton)sender;
        AppointmentSelectedEvent apptEvent = new AppointmentSelectedEvent(Guid.Parse(lnk.CommandArgument));

        // set this so when at the patient information screen , the program comes back to patitent appoinment screen
        Session["EditPatientAppointment"] = "true";

        if (AppointmentSelected != null)
            AppointmentSelected(this, apptEvent);
    }

    private void LinkButton_Click(object sender, EventArgs e)
    {
        string s = "ast";
        LinkButton lnk = (LinkButton)sender;
    }

    //private void Patienthistorygridview_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    GridView gridView = (GridView)sender;
    //    int selectedIndex = gridView.SelectedIndex;

    //    if (selectedIndex == -1)
    //    {
    //        if (Session["PatientHistorySortOrder"] != null)
    //        {
    //            if (Session["PatientHistorySortOrder"].ToString() == "DESC")             
    //                Session["PatientHistorySortOrder"] = "ASC";                                   
    //            else                
    //                Session["PatientHistorySortOrder"] = "DESC";

    //            BuildGrid();
    //        }
    //    }
    //    //else if (selectedIndex != -1)
    //    //{
    //    //    for (int index = 0; index < patienthistorygridview.Rows.Count; index++)
    //    //        patienthistorygridview.Rows[index].BackColor = System.Drawing.ColorTranslator.FromHtml("#F4A460");
    //    //    patienthistorygridview.Rows[selectedIndex].BackColor = System.Drawing.ColorTranslator.FromHtml("#F9cda8");
    //    //    string date = patienthistorygridview.Rows[selectedIndex].Cells[0].Text;
    //    //    string id = patienthistorygridview.Rows[selectedIndex].Cells[1].Text;
    //    //    Session["PatientHistoryAppointmentDate"] = patienthistorygridview.Rows[selectedIndex].Cells[0].Text;
    //    //    Session["PatientHistoryPatientId"] = patienthistorygridview.Rows[selectedIndex].Cells[1].Text;
    //    //    Session["PatientHistoryGuid"] = patienthistorygridview.Rows[selectedIndex].Cells[2].Text;
    //    //}
    //}

    //private void Patienthistorygridview_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    TableCell cell = null;
    //    Image img = null;
    //    const int IMAGE_HEIGHT = 200;
    //    const int IMAGE_WIDTH = IMAGE_HEIGHT;

    //    if (e.Row.RowType == DataControlRowType.Header)
    //    {
    //        e.Row.Cells[0].Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(patienthistorygridview, "Select$-1");            
    //        e.Row.Cells[0].Style["text-decoration"]="underline";            
    //        e.Row.Cells[1].Visible = false;
    //        e.Row.Cells[2].Visible = false;

    //        int indexBeforeTherapy = pager.GridIndexByColumnName("ImageBeforeTherapy");
    //        int indexAfterTherapy = pager.GridIndexByColumnName("ImageAfterTherapy");

    //        e.Row.Cells[indexBeforeTherapy].Visible = false;
    //        e.Row.Cells[indexAfterTherapy].Visible = false;
            
    //        if (e.Row.FindControl("Image Before Therapy") == null)
    //        {
    //            cell = new TableCell();
    //            cell.ID = "Image Before Therapy";
    //            cell.Text = "Image Before Therapy";
    //            cell.Font.Bold = true;
    //            e.Row.Cells.Add(cell);

    //            cell = new TableCell();
    //            cell.Text = "Image After Therapy";
    //            cell.Font.Bold = true;
    //            e.Row.Cells.Add(cell);
    //        }

    //    }
    //    else if (e.Row.RowType == DataControlRowType.DataRow)
    //    {
    //        e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(patienthistorygridview, "Select$" + e.Row.RowIndex);
    //        e.Row.Cells[1].Visible = false;
    //        e.Row.Cells[2].Visible = false;

    //        int indexBeforeTherapy = pager.GridIndexByColumnName("ImageBeforeTherapy");
    //        int indexAfterTherapy = pager.GridIndexByColumnName("ImageAfterTherapy");

    //        e.Row.Cells[indexBeforeTherapy].Visible = false;
    //        e.Row.Cells[indexAfterTherapy].Visible = false;
            
    //        // Tongue Image wont be there so have to create it
    //        if (e.Row.FindControl("TongueImage") == null)
    //        {

    //            cell = new TableCell();
    //            cell.ID = "TongueImage";               
    //            img = new Image();
    //            img.ImageUrl = e.Row.Cells[indexBeforeTherapy].Text;
    //            img.ImageAlign = ImageAlign.Middle;
    //            img.Width = IMAGE_WIDTH;
    //            img.Height = IMAGE_HEIGHT;
    //            cell.HorizontalAlign = HorizontalAlign.Left;
    //            cell.Controls.Add(img);
    //            e.Row.Cells.Add(cell);

    //            cell = new TableCell();
    //            img = new Image();
    //            img.ImageUrl = e.Row.Cells[indexAfterTherapy].Text;
    //            img.ImageAlign = ImageAlign.Middle;
    //            img.Width = IMAGE_WIDTH;
    //            img.Height = IMAGE_HEIGHT;
    //            cell.HorizontalAlign = HorizontalAlign.Left;
    //            cell.Controls.Add(img);
    //            e.Row.Cells.Add(cell);
    //        }
    //    }
    //}

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



