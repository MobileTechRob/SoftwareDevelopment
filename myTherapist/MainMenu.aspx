 <%@ Page Language="C#" AutoEventWireup="true" CodeFile="MainMenu.aspx.cs" Inherits="MainMenu" %>

<!DOCTYPE html>
<link rel="stylesheet" runat="server" href="stylesheets/sitestyles.css"/> 
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">    
    <title>myTherapist</title>
</head>

<body class="bodystyle">
    <form id="form1" runat="server">
        <div class="menutable">
            <div class="tr">
                <div class="td">
                    <asp:Image runat="server" ImageUrl="~/Images/PamCare.PNG" ID="PamCareImage" ImageAlign="Middle" />
                </div>
            </div>
            <div class="tr">
                <div class="td">
                    <asp:TextBox runat="server" ID="txtBoxUserName"  />                    
                </div>
            </div>
            <div class="tr">
                 <div class="td">                
                </div>
            </div>        
            <div class="tr">
                 <div class="td">                    
                    <asp:LinkButton runat="server" ID="lnkBtnPatientCare" Text="Patient Care" Font-Size="XX-Large" OnClick="btnPatientCare_Click"></asp:LinkButton>
                    <asp:Label runat="server" ID="Label2" Text="    " Font-Size="Large" />                               
                </div>
            </div>
        </div>
    </form>
</body>

</html>
