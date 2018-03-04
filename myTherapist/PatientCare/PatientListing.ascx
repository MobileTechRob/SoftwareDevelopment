<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PatientListing.ascx.cs" Inherits="PatientCare_PatientListing" %>
<link rel="stylesheet" runat="server" href="sitestyles.css" id="stylesheetlink" /> 

<h2>Patient Listing</h2>

<asp:Label runat="server" Text="First Name "></asp:Label><asp:TextBox runat="server" ID="txtBoxPatientFirstName"></asp:TextBox>&nbsp&nbsp&nbsp<asp:Label runat="server" Text="Last Name "></asp:Label><asp:TextBox runat="server" ID="txtBoxPatientLastName"></asp:TextBox>&nbsp&nbsp&nbsp<asp:Button runat="server" ID="btnSearch" Text="Search" OnClick="btnSearch_Click" />
<br />
<br />
<asp:GridView runat="server" ID="patientlistgridview" RowStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"  RowStyle-Width="50" ></asp:GridView>

<div class="menubuttontable">
  <div class="tr">
     <div class="td">
        <asp:Button runat="server" ID="btnPreviousPage"  UseSubmitBehavior="false" Text="<" OnClick="btnPreviousPage_Click" />
        <asp:Button runat="server" ID="btnNextPAge" Text=">" UseSubmitBehavior="false" OnClick="btnNextPAge_Click" />
     </div>
  </div>
</div>        

