 <%@ Page Language="C#" AutoEventWireup="true" CodeFile="MainMenu.aspx.cs" Inherits="MainMenu" %>

<!DOCTYPE html>
<link rel="stylesheet" runat="server" href="stylesheets/sitestyles.css"/> 
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">    
    <title>MyTherapist</title>
</head>

<body class="bodystyle">
    <form id="form1" runat="server">
        <div class="menutable">
            <div class="tr">
                <div class="td">
                    <asp:Image runat="server" ImageUrl="~/Images/PamCare.PNG" ID="PamCareImage" ImageAlign="Middle" />
                </div>
            </div>
        </div>
        <div class="menutabletwo">
            <br />            
            <div class="trlogin">
                <div class="tdlogin">
                    <asp:Label runat="server" ID="lblUserName" Text="Therapist"/>                    
                </div>
                <div class="tdlogin">
                    <asp:TextBox runat="server" ID="txtBoxUserName"  />                    
                </div>


            </div>
            <div class="trlogin">                
                <div class="td">
                    <asp:Label runat="server" ID="lblPassword" Text="Password"/>
                    <asp:Label runat="server" ID="lblSpacer2" Text="&nbsp&nbsp"/>
                    <asp:TextBox runat="server" ID="txtBoxPassword" />                    
                </div>
            </div>
            <br />          
                    <asp:Label runat="server" ID="lblInformationText" Text=""/>
            <br />          
            <div class="tr">
                 <div class="td">                    
                    <asp:LinkButton runat="server" ID="lnkBtnPatientCare" Text="Login" Font-Size="Large" OnClick="btnPatientCare_Click"></asp:LinkButton>
                    <asp:Label runat="server" ID="Label2" Text="    " Font-Size="Large" />                               
                </div>
            </div>
        </div>
    </form>
</body>

</html>
