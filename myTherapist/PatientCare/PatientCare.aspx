<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PatientCare.aspx.cs" Inherits="PatientCare_PatientCare" %>

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

    </form>
</body>
</html>

