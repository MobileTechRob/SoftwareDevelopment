<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Therapists.aspx.cs" Inherits="UserManagement_Therapists" EnableEventValidation="false" %>

<%@ Register src="AddEditTherapists.ascx" tagname="AddEditTherapists" tagprefix="uc1" %>
<%@ Register src="TherapistList.ascx" tagname="TherapistList" tagprefix="uc2" %>
<link rel="stylesheet" runat="server" href="../stylesheets/sitestyles.css"/> 

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <title>MyTherapist</title>
</head>

<h3>Therapists</h3>
<uc1:AddEditTherapists ID="AddEditTherapists1" runat="server" />
<body class="bodystyle">
    <form id="form1" runat="server">
        <div>
        </div>
        <uc2:TherapistList ID="TherapistList1" runat="server" />
        <br />
        <asp:Label ID="UserAlert" runat="server" Text=""></asp:Label>
        <br />
        <asp:Panel ID="Panel" runat="server">
            <asp:Button ID="btnAdd" runat="server" OnClick="btnAdd_Click" Text="Add" /> 
            <asp:Button ID="btnEdit" runat="server" OnClick="btnEdit_Click" Text="Edit" /> 
        </asp:Panel>
    </form>
</body>
</html>
