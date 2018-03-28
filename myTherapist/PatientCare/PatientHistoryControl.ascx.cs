using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataGridObjects;

public partial class PatientCare_PatientHistoryControl : System.Web.UI.UserControl
{    
    MyDataGridPager pager = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.Visible == false)
        {
            Session["PatientHistorySortOrder"] = "DESC";
            return;
        }

        if (Session["PatientName"] != null)
            lblPatientName.Text = Session["PatientName"].ToString();

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

        DatabaseRowObject.DatabaseColumnObject col = new DatabaseRowObject.DatabaseColumnObject();
        
        dbRowObject.AddColumn("ApptDate", "Appointment Date", MyDataTypes.DATETIME, true, 35);
        dbRowObject.AddColumn("PatientId", "", MyDataTypes.INTEGER, false, 35);
        dbRowObject.AddColumn("GUID", "", MyDataTypes.GUID, false, 0);
        dbRowObject.AddColumn("RLU", "RLU", MyDataTypes.STRING, false, 0);
        dbRowObject.AddColumn("SP", "SP", MyDataTypes.STRING, false, 0);
        dbRowObject.AddColumn("KD1", "KD", MyDataTypes.STRING, false, 0);
        dbRowObject.AddColumn("LHT", "LHT", MyDataTypes.STRING, false, 0);
        dbRowObject.AddColumn("LV", "LV", MyDataTypes.STRING, false, 0);
        dbRowObject.AddColumn("KD2", "KD ", MyDataTypes.STRING, false, 0);
        dbRowObject.AddColumn("ImageBeforeTherapy", "", MyDataTypes.STRING, false, 0);
        dbRowObject.AddColumn("ImageAfterTherapy", "", MyDataTypes.STRING, false, 0);

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

        pager.WhereClause = String.Format("PatientId = {0}", Session["PatientId"].ToString());

        
        DataGridObjects.DataCellDisplay cellOfDataToDisplay = new DataCellDisplay();
        DataGridObjects.DataRowDisplay rowOfDataToDisplay = new DataGridObjects.DataRowDisplay();
        DataGridObjects.GridRowObject dataFormat = new DataGridObjects.GridRowObject();

        cellOfDataToDisplay = new DataCellDisplay();
        cellOfDataToDisplay.DatabaseColumnName = "ApptDate";
        cellOfDataToDisplay.MyDataType = MyDataTypes.DATETIME;

        rowOfDataToDisplay.Add(cellOfDataToDisplay);

        cellOfDataToDisplay = new DataCellDisplay();
        cellOfDataToDisplay.DatabaseColumnName = "RLU";
        cellOfDataToDisplay.MyDataType = MyDataTypes.STRING;

        rowOfDataToDisplay.Add(cellOfDataToDisplay);
        
        cellOfDataToDisplay = new DataCellDisplay();
        cellOfDataToDisplay.DatabaseColumnName = "SP";
        cellOfDataToDisplay.MyDataType = MyDataTypes.STRING;

        rowOfDataToDisplay.Add(cellOfDataToDisplay);

        cellOfDataToDisplay = new DataCellDisplay();
        cellOfDataToDisplay.DatabaseColumnName = "KD1";
        cellOfDataToDisplay.MyDataType = MyDataTypes.STRING; 
        
        rowOfDataToDisplay.Add(cellOfDataToDisplay);

        cellOfDataToDisplay = new DataCellDisplay();
        cellOfDataToDisplay.DatabaseColumnName = "LHT";
        cellOfDataToDisplay.MyDataType = MyDataTypes.STRING;

        rowOfDataToDisplay.Add(cellOfDataToDisplay);

        cellOfDataToDisplay = new DataCellDisplay();
        cellOfDataToDisplay.DatabaseColumnName = "LV";
        cellOfDataToDisplay.MyDataType = MyDataTypes.STRING;

        rowOfDataToDisplay.Add(cellOfDataToDisplay);

        cellOfDataToDisplay = new DataCellDisplay();
        cellOfDataToDisplay.DatabaseColumnName = "KD2";
        cellOfDataToDisplay.MyDataType = MyDataTypes.STRING;

        rowOfDataToDisplay.Add(cellOfDataToDisplay);        
        dataFormat.Add(rowOfDataToDisplay);

        rowOfDataToDisplay = new DataGridObjects.DataRowDisplay();
        cellOfDataToDisplay = new DataCellDisplay();
        cellOfDataToDisplay.DatabaseColumnName = "ImageBeforeTherapy";
        cellOfDataToDisplay.MyDataType = MyDataTypes.STRING;
        rowOfDataToDisplay.Add(cellOfDataToDisplay);

        cellOfDataToDisplay = new DataCellDisplay();
        cellOfDataToDisplay.DatabaseColumnName = "ImageAfterTherapy";
        cellOfDataToDisplay.MyDataType = MyDataTypes.STRING;

        rowOfDataToDisplay.Add(cellOfDataToDisplay);

        dataFormat.Add(rowOfDataToDisplay);

        patienthistorygridview.DataSource = dataGridObj.BuildTable(dataFormat);
        patienthistorygridview.RowDataBound += Patienthistorygridview_RowDataBound;
        patienthistorygridview.SelectedIndexChanged += Patienthistorygridview_SelectedIndexChanged;
        patienthistorygridview.DataBind();        
    }


    private void Patienthistorygridview_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridView gridView = (GridView)sender;
        int selectedIndex = gridView.SelectedIndex;

        if (selectedIndex == -1)
        {
            if (Session["PatientHistorySortOrder"] != null)
            {
                if (Session["PatientHistorySortOrder"].ToString() == "DESC")             
                    Session["PatientHistorySortOrder"] = "ASC";                                   
                else                
                    Session["PatientHistorySortOrder"] = "DESC";

                BuildGrid();
            }
        }
        else if (selectedIndex != -1)
        {
            for (int index = 0; index < patienthistorygridview.Rows.Count; index++)
                patienthistorygridview.Rows[index].BackColor = System.Drawing.ColorTranslator.FromHtml("#F4A460");

            patienthistorygridview.Rows[selectedIndex].BackColor = System.Drawing.ColorTranslator.FromHtml("#F9cda8");

            string date = patienthistorygridview.Rows[selectedIndex].Cells[0].Text;
            string id = patienthistorygridview.Rows[selectedIndex].Cells[1].Text;

            Session["PatientHistoryAppointmentDate"] = patienthistorygridview.Rows[selectedIndex].Cells[0].Text;
            Session["PatientHistoryPatientId"] = patienthistorygridview.Rows[selectedIndex].Cells[1].Text;
            Session["PatientHistoryGuid"] = patienthistorygridview.Rows[selectedIndex].Cells[2].Text;
        }
    }

    private void Patienthistorygridview_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        TableCell cell = null;
        Image img = null;
        const int IMAGE_HEIGHT = 200;
        const int IMAGE_WIDTH = IMAGE_HEIGHT;

        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(patienthistorygridview, "Select$-1");            
            e.Row.Cells[0].Style["text-decoration"]="underline";            
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[2].Visible = false;

            int indexBeforeTherapy = pager.GridIndexByColumnName("ImageBeforeTherapy");
            int indexAfterTherapy = pager.GridIndexByColumnName("ImageAfterTherapy");

            e.Row.Cells[indexBeforeTherapy].Visible = false;
            e.Row.Cells[indexAfterTherapy].Visible = false;
            
            if (e.Row.FindControl("Image Before Therapy") == null)
            {
                cell = new TableCell();
                cell.ID = "Image Before Therapy";
                cell.Text = "Image Before Therapy";
                cell.Font.Bold = true;
                e.Row.Cells.Add(cell);

                cell = new TableCell();
                cell.Text = "Image After Therapy";
                cell.Font.Bold = true;
                e.Row.Cells.Add(cell);
            }

        }
        else if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(patienthistorygridview, "Select$" + e.Row.RowIndex);
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[2].Visible = false;

            int indexBeforeTherapy = pager.GridIndexByColumnName("ImageBeforeTherapy");
            int indexAfterTherapy = pager.GridIndexByColumnName("ImageAfterTherapy");

            e.Row.Cells[indexBeforeTherapy].Visible = false;
            e.Row.Cells[indexAfterTherapy].Visible = false;
            
            // Tongue Image wont be there so have to create it
            if (e.Row.FindControl("TongueImage") == null)
            {

                cell = new TableCell();
                cell.ID = "TongueImage";               
                img = new Image();
                img.ImageUrl = e.Row.Cells[indexBeforeTherapy].Text;
                img.ImageAlign = ImageAlign.Middle;
                img.Width = IMAGE_WIDTH;
                img.Height = IMAGE_HEIGHT;
                cell.HorizontalAlign = HorizontalAlign.Left;
                cell.Controls.Add(img);
                e.Row.Cells.Add(cell);

                cell = new TableCell();
                img = new Image();
                img.ImageUrl = e.Row.Cells[indexAfterTherapy].Text;
                img.ImageAlign = ImageAlign.Middle;
                img.Width = IMAGE_WIDTH;
                img.Height = IMAGE_HEIGHT;
                cell.HorizontalAlign = HorizontalAlign.Left;
                cell.Controls.Add(img);
                e.Row.Cells.Add(cell);
            }
        }
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



