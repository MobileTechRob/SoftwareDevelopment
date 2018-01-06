<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PatientHistoryControl.ascx.cs" Inherits="PatientCare_PatientHistoryControl" %>
<link id="stylesheetlink" runat="server" href="../stylesheets/sitestyles.css" rel="stylesheet" />
<h2>Patient History</h2>
<asp:Label runat="server" ID="lblPatientName" Text="" />
<br />
<br />
<asp:RadioButtonList runat="server" ID="rblDisplaySort"><asp:ListItem Value="Ascending"></asp:ListItem><asp:ListItem Value="Descending"></asp:ListItem></asp:RadioButtonList>
<br />
<%--<asp:Table runat="server" ID="patientHistoryGrid" HorizontalAlign="Center" />--%>
