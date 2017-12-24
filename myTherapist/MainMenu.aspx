<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MainMenu.aspx.cs" Inherits="MainMenu" %>

<!DOCTYPE html>
<link rel="stylesheet" runat="server" href="stylesheets/sitestyles.css"/> 
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">    
    <title>hello</title>
</head>

<body class="bodystyle">
    <form id="form1" runat="server">
        <div class="menutable">
            <div class="tr">
                <div class="td">
                    <asp:LinkButton runat="server"  ID="btnPatientCare" Text="Patient Care" Font-Size="XX-Large" OnClick="btnPatientCare_Click"></asp:LinkButton>
                    <asp:Label runat="server" ID="space" Text=" " Font-Size="Large" />
                    <asp:LinkButton runat="server"  ID="btnSystemConfiguration" Text="System Configuration" Font-Size="XX-Large" OnClick="btnSystemConfiguration_Click"></asp:LinkButton>
                </div>
            </div>
        </div>
    </form>
</body>

</html>
