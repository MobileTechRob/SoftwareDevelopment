<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AddEditPatientControl.ascx.cs" Inherits="PatientCare_AddEditPatientControl" %>
<link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css">
<link rel="stylesheet" href="/resources/demos/style.css">
<script src="https://code.jquery.com/jquery-1.12.4.js"></script>
<script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
<script type="text/javascript" lang="javascript">

    $(
    function getMyDate() { alert("hello!"); };
    );


    //$(function () {
    //    $("#datepicker").datepicker({ changeYear: true, changeMonth: true });
    //});

    
</script>
<link rel="stylesheet" runat="server" href="../stylesheets/sitestyles.css" id="stylesheetlink" /> 

<h2 id="hdrPatientHeader">New Patient </h2>

<div class="menubuttontable">
  <div class="tr">
      <div class="tda">
          <asp:Label runat="server" Text="First Name" Width="75" ID="lblFirstName"/>
          <asp:TextBox runat="server" ID="txtboxFirstName" Width="250" Text=""/>
      </div>
      <div class="tda">
          &nbsp&nbsp<asp:Label runat="server" Text="Last Name" Width="75" ID="lblLastName"/>
          <asp:TextBox runat="server" ID="txtboxLastName" Width="250" Text=""/>
      </div>
  </div>
  <div class="tr">
      <div class="tda">
          <asp:Label runat="server" Text="Birth Date" Width="75" ID="lblBirthDate"/>          
          <input type="text" id="datepicker" />          
      </div>
  </div>
  <div class="tr">
      <div class="tda">
          <asp:Label runat="server" Text="Phone" Width="75" ID="lblPatientPhone"/>
          <asp:TextBox runat="server" ID="txtboxPhone" Text=""/>
      </div>
  </div>
  <div class="tr">
      <div class="tda">
          <asp:Label runat="server" Text="Email" Width="75" ID="lblEmailAddress"/>
          <asp:TextBox runat="server" ID="txtboxEmailAddress" Text=""/>
      </div>
  </div>
  <div class="tr">
     <div class="td">
        <asp:Button runat="server" ID="btnSavePatient" Text="Save" OnClick="btnSavePatient_Click" />
        <asp:Button runat="server" ID="btnCancel" Text="Cancel" OnClick="btnCancel_Click" />
     </div>
  </div>
</div>        

