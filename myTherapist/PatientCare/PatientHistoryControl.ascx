<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PatientHistoryControl.ascx.cs" Inherits="PatientCare_PatientHistoryControl" %>
<link id="stylesheetlink" runat="server" href="../stylesheets/sitestyles.css" rel="stylesheet" />
<h2>Patient History</h2>
<asp:LinkButton runat="server" ID="lnkBtnPatientName" Text="" Command="lnkBtnPatientName_Click" />
<asp:Label runat="server" ID="lblPatientName" Text="" />
<br />
<asp:Label runat="server" ID="lblNotice" Text=""/>
<br />
<asp:Table runat="server" ID="PatientApptData" />
<br />
<asp:GridView runat="server" ID="patienthistorygridview" RowStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"  RowStyle-Width="50" ></asp:GridView>
<br />

