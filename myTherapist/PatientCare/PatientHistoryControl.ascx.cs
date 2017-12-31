﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PatientCare_PatientHistoryControl : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        System.Data.DataTable patientHistory = null;

        if (Session["PatientName"] != null)
            lblPatientName.Text = Session["PatientName"].ToString();

        MyDataGridPager pager = null;

        if (Session["PatientId"] != null)
        {        
            pager = new MyDataGridPager("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\SoftwareDevelopmentProjects\\WebProjects\\myTherapist\\myTherapist\\App_Data\\myTherapist.mdf;Integrated Security=True", "PatientAppointmentInformation");

            pager.AddColumn("ApptDate", "", MyDataTypes.DATETIME, true, 35);
            pager.AddColumn("PatientId", "", MyDataTypes.INTEGER, true, 35);
            pager.AddColumn("RLU", "", MyDataTypes.STRING, false, 0);
            pager.AddColumn("SP", "", MyDataTypes.STRING, false, 0);
            pager.AddColumn("KD1", "", MyDataTypes.STRING, false, 0);
            pager.AddColumn("LHT", "", MyDataTypes.STRING, false, 0);
            pager.AddColumn("LV", "", MyDataTypes.STRING, false, 0);
            pager.AddColumn("KD2", "", MyDataTypes.STRING, false, 0);
            pager.AddColumn("TherapyPerformed", "", MyDataTypes.STRING, false, 0);
            pager.AddColumn("OilsUsed", "", MyDataTypes.STRING, false, 0);
            pager.AddColumn("SessionGoals", "", MyDataTypes.STRING, false, 0);

            pager.AddColumn("ImageBeforeTherapy", "", MyDataTypes.STRING, false, 0);
            pager.AddColumn("ImageAfterTherapy", "", MyDataTypes.STRING, false, 0);

            pager.NumberRowsToDisplay = 0;
            pager.WhereClause = String.Format("PatientId = {0}", Session["PatientId"].ToString());
            pager.Sort = MyDataSort.DESC;

            patientHistory = pager.BuildTable();
            int rowIndex = 0;

            TableRow row = null;
            TableCell columnHeader = null;
            TableCell pulseStrength = null;
            System.Data.DataRow tableRow = null;
            int itemArrayIndex = 0;

            row = new TableRow();
            columnHeader = new TableCell();
            columnHeader.Text = "ApptDate";
            row.Cells.Add(columnHeader);

            columnHeader = new TableCell();
            columnHeader.Text = "RLU";
            row.Cells.Add(columnHeader);

            columnHeader = new TableCell();
            columnHeader.Text = "SP";
            row.Cells.Add(columnHeader);

            columnHeader = new TableCell();
            columnHeader.Text = "KD";
            row.Cells.Add(columnHeader);

            columnHeader = new TableCell();
            columnHeader.Text = "LHT";
            row.Cells.Add(columnHeader);

            columnHeader = new TableCell();
            columnHeader.Text = "LV";
            row.Cells.Add(columnHeader);

            columnHeader = new TableCell();
            columnHeader.Text = "KD";
            row.Cells.Add(columnHeader);
            
            patientHistoryGrid.Rows.Add(row);
            row = new TableRow();
            row.Height = 20;

            // blank row
            patientHistoryGrid.Rows.Add(row);
            

            while (rowIndex < patientHistory.Rows.Count)
            {
                tableRow = patientHistory.Rows[rowIndex];                
                row = new TableRow();
                itemArrayIndex = 0;

                while (itemArrayIndex < 7)
                {
                    pulseStrength = new TableCell();
                    pulseStrength.Text = tableRow.ItemArray[itemArrayIndex].ToString();
                    row.Cells.Add(pulseStrength);

                    if (itemArrayIndex == 0)
                        itemArrayIndex += 2;
                    else
                        itemArrayIndex += 1;

                    itemArrayIndex = itemArrayIndex + 1;
                }
                
                patientHistoryGrid.Rows.Add(row);
                row = new TableRow();

                // blank row
                patientHistoryGrid.Rows.Add(row);

                rowIndex++;
            }                       
        }
    }
}





