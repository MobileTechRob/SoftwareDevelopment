<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PatientCare.aspx.cs" Inherits="PatientCare_PatientCare" EnableEventValidation="false"  %>
<%@ Register src="PatientListing.ascx" tagname="PatientListing" tagprefix="uc1" %>

<%@ Register src="AddEditPatientControl.ascx" tagname="AddEditPatientControl" tagprefix="uc2" %>

<!DOCTYPE html>

<link rel="stylesheet" runat="server" href="../stylesheets/sitestyles.css"/> 
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>

<body class="bodystyle" >
    <form id="form1" runat="server">
        
        <uc1:PatientListing ID="PatientListing1" runat="server" />                
        <uc2:AddEditPatientControl ID="AddEditPatientControl1" runat="server" />
        <br />

        <asp:Panel runat="server" ID="menuPanel">
        <div id="navigationMenu" class="menubuttontable">
            <div class="tr">
                <div class="td">
                    <asp:Button runat="server" ID="btnCreatePatient" Text="Create Patient" OnClick="btnCreatePatient_Click" />                   
                    <asp:Button runat="server" ID="btnStartAppt" Text="Start Appointment" OnClick="btnStartAppt_Click" />
                    <asp:Button runat="server" ID="btnPatientHistory" Text="Patient History" OnClick="btnPatientHistory_Click" />
                </div>
            </div>
        </div>
        </asp:Panel>

    </form>
</body>
</html>
