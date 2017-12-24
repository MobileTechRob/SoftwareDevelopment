<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AddEditPatientControl.ascx.cs" Inherits="PatientCare_AddEditPatientControl" %>
<link rel="stylesheet" runat="server" href="../stylesheets/sitestyles.css" id="stylesheetlink" /> 


<link rel="stylesheet" runat="server" href="sitestyles.css" id="Link1" /> 

<h2>New Patient </h2>


<div class="menubuttontable">
  <div class="tr">
      <div class="td">
          <asp:Label runat="server" Text="Name" Width="50" ID="lblName"/>
          <asp:TextBox runat="server" ID="txtboxName" Text=""/>
      </div>
  </div>
  <div class="tr">
      <div class="td">
          <asp:Label runat="server" Text="Phone" Width="50" ID="lblPatientPhone"/>
          <asp:TextBox runat="server" ID="txtboxPhone" Text=""/>
      </div>
  </div>
  <div class="tr">
      <div class="td">
          <asp:Label runat="server" Text="Email" Width="50" ID="lblEmailAddress"/>
          <asp:TextBox runat="server" ID="txtboxEmailAddress" Text=""/>
      </div>
  </div>

     <div class="td">
        <asp:Button runat="server" ID="btnSavePatient" Text="Save" OnClick="btnSavePatient_Click" />
        <asp:Button runat="server" ID="btnCancel" Text="Cancel" OnClick="btnCancel_Click" />
     </div>
  </div>
</div>        

