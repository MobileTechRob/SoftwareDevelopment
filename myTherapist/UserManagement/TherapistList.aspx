<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TherapistList.aspx.cs" Inherits="UserManagement_AddEditTherapist" %><%@ Register src="AddEditTherapists.ascx" tagname="AddEditTherapists" tagprefix="uc1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">        
        <asp:GridView runat="server" ID="therapistlist"></asp:GridView>                     
        <uc1:AddEditTherapists ID="AddEditTherapists1" runat="server" />
        <p>
            <asp:Button ID="btnAdd" runat="server" Text="Add" />
        </p>
    </form>
</body>
</html>
