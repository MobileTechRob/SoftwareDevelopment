<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PatientCare.aspx.cs" Inherits="PatientCare_PatientCare" EnableEventValidation="false"  %>
<%@ Register src="PatientListing.ascx" tagname="PatientListing" tagprefix="uc1" %>

<!DOCTYPE html>

<link rel="stylesheet" runat="server" href="sitestyles.css"/> 
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
        </div>

        <uc1:PatientListing ID="PatientListing1" runat="server" />
        
        <div class="menubuttontable">
            <div class="tr">
                <div class="td">
                    <asp:Button runat="server" ID="btnPreviousPage" Text="<" />
                    <asp:Button runat="server" ID="btnNextPAge" Text=">" />
                </div>
            </div>
        </div>        
        <br />
        <div class="menubuttontable">
            <div class="tr">
                <div class="td">
                    <asp:Button runat="server" ID="btnCreatePatient" Text="Create Patient" />                   
                    <asp:Button runat="server" ID="btnStartAppt" Text="Start Appointment" />
                    <asp:Button runat="server" ID="btnPatientHistory" Text="Patient History" />
                </div>
            </div>
        </div>
    </form>
</body>
</html>
