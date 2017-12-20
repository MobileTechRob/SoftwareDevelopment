<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MainMenu.aspx.cs" Inherits="MainMenu" %>

<!DOCTYPE html>

<link rel="stylesheet" type="text/css" href="~/Content/sitestyles.css"> 
<html xmlns="http://www.w3.org/1999/xhtml">



<head runat="server">    
    <style>
        .menutable {display: table;
    align-content: center;        
    position: absolute;
    margin-top: 20%;
    margin-left: 40%;}

        .tr {display: table-row;
    align-content: center;}

.td {display: table-cell;
    align-content: center;
    vertical-align: middle;}

    </style>
    <title>hello</title>
</head>


<body>
    <form id="form1" runat="server">
        <div class="menutable">
            <div class="tr">
                <div class="td">
                    <asp:LinkButton runat="server"  ID="btnPatientCare" Text="Patient Care" OnClick="btnPatientCare_Click"></asp:LinkButton>
                </div>
                <div class="td">
                    <asp:LinkButton runat="server"  ID="btnPatientReports" Text="Patient Reports" OnClick="btnPatientReports_Click"></asp:LinkButton>
                </div>
            </div>
        </div>
    </form>
</body>

</html>
