<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Therapists.aspx.cs" Inherits="UserManagement_Therapists" EnableEventValidation="false" %>

<%@ Register src="AddEditTherapists.ascx" tagname="AddEditTherapists" tagprefix="uc1" %>
<%@ Register src="TherapistList.ascx" tagname="TherapistList" tagprefix="uc2" %>
<link rel="stylesheet" runat="server" href="../stylesheets/sitestyles.css"/> 

<!DOCTYPE html>

<h3>Therapists</h3>
<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <title>MyTherapist</title>
</head>

<body class="bodystyle">
    <form id="form1" runat="server">
        <uc1:AddEditTherapists ID="AddEditTherapists1" runat="server" />
        <uc2:TherapistList ID="TherapistList1" runat="server" />
        <br />
        <asp:Label ID="UserAlert" runat="server" Text=""></asp:Label>
        <br />
        <br />
        <asp:Panel ID="Panel" runat="server">
            <asp:Button ID="btnAdd" runat="server" OnClick="btnAdd_Click" Text="Add" /> 
            <asp:Button ID="btnEdit" runat="server" OnClick="btnEdit_Click" Text="Edit" /> 
            <asp:Button ID="btnDelete" runat="server" OnClick="btnDelete_Click" Text="Delete" /> 
            <asp:Button ID="btnLogOut" runat="server" OnClick="btnLogOut_Click" Text="Log Out" /> 
        </asp:Panel>
    </form>
</body>
</html>
