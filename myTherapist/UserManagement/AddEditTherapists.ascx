<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AddEditTherapists.ascx.cs" Inherits="UserManagement_AddEditTherapists" %>
<asp:Panel runat="server" ID="panel" GroupingText="Therapist Information">
<br />
<asp:Label ID="lblTherapistName" Text="Therapist&nbsp&nbsp" runat="server" ></asp:Label><asp:TextBox ID="txtBoxTherapistName" Width="250" runat="server"></asp:TextBox>
<br />
<br />
<asp:Button ID="btnSaveEdit" runat="server" Height="26px" Text="Save" Width="36px" OnClick="btnAddEdit_Click" />
</asp:Panel>
