<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PatientCare.aspx.cs" Inherits="PatientCare_PatientCare" EnableEventValidation="false"  %>
<%@ Register src="PatientListing.ascx" tagname="PatientListing" tagprefix="uc1" %>

<%@ Register src="AddEditPatientControl.ascx" tagname="AddEditPatientControl" tagprefix="uc2" %>

<%@ Register src="PatientApptControl.ascx" tagname="PatientApptControl" tagprefix="uc3" %>

<%@ Register src="PatientHistoryControl.ascx" tagname="PatientHistoryControl" tagprefix="uc4" %>

<!DOCTYPE html>

<link rel="stylesheet" runat="server" href="../stylesheets/sitestyles.css"/> 
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>myTherapist</title>
</head>

<body class="bodystyle" >
    <form id="form1" runat="server">
        
        <uc1:PatientListing ID="PatientListing1" runat="server" />                
        <uc2:AddEditPatientControl ID="AddEditPatientControl1" runat="server" />
        <uc3:PatientApptControl ID="PatientApptControl1" runat="server" />
        <uc4:PatientHistoryControl ID="PatientHistoryControl1" runat="server" />
        <br />
        <asp:Label runat="server" ID="lblWarningText" Text=""/>
        <br />
        <asp:Panel runat="server" ID="menuPanel">
        <div id="navigationMenu" class="menubuttontable">
            <div class="tr">
                <div class="td">
                    <asp:Button runat="server" ID="btnCreatePatient" Text="Create Patient" OnClick="btnCreatePatient_Click" />  
                    <asp:Button runat="server" ID="btnUpdatePatient" Text="Update Patient" OnClick="btnUpdatePatient_Click" />  
                    <asp:Button runat="server" ID="btnSaveAppt" Text="Save Appointment" OnClick="btnSaveAppt_Click" />
                    <asp:Button runat="server" ID="btnCancelAppt" Text="Cancel Appointment" OnClick="btnCancelAppt_Click" />
                    <asp:Button runat="server" ID="btnStartAppt" Text="Start New Appointment" OnClick="btnStartAppt_Click" />                    
                    <asp:Button runat="server" ID="btnEditAppt" Text="Edit Appointment" OnClick="btnEditAppt_Click" />                    
                    <asp:Button runat="server" ID="btnSaveChanges" Text="Save Changes" OnClick="btnSaveChanges_Click" />                    
                    <asp:Button runat="server" ID="btnCancelChanges" Text="Cancel Changes" OnClick="btnCancelChanges_Click"/>  
                    <asp:Button runat="server" ID="btnPatientListing" Text="Patient Listing" OnClick="btnPatientListing_Click" />
                    <asp:Button runat="server" ID="btnPatientHistory" Text="Patient History" OnClick="btnPatientHistory_Click" />
                    <asp:Button runat="server" ID="btnDeletePatient" Text="Delete Patient" OnClick="btnDeletePatient_Click" />
                </div>
            </div>
        </div>
        </asp:Panel>
        <br />
        <br />
        <br />
    </form>
</body>
</html>
