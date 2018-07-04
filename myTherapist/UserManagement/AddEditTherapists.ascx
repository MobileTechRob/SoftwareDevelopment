<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AddEditTherapists.ascx.cs" Inherits="UserManagement_AddEditTherapists" %>
<asp:Panel runat="server" ID="panel" GroupingText="Therapist Information">
<br />
<asp:Table ID="TherapistInfo" GridLines="Both" runat="server">
<asp:TableRow>
<asp:TableCell><asp:Label ID="lblTherapistName" Text="Name" runat="server" ></asp:Label></asp:TableCell><asp:TableCell><asp:TextBox ID="txtBoxTherapistName" Width="250" runat="server"></asp:TextBox></asp:TableCell>
</asp:TableRow>
<asp:TableRow>
<asp:TableCell><asp:Label ID="lblTherapistPassword" Text="Password" runat="server" ></asp:Label></asp:TableCell><asp:TableCell><asp:TextBox ID="txtBoxTherapistPassword" TextMode="Password" Width="250" runat="server"></asp:TextBox></asp:TableCell>
</asp:TableRow>
</asp:Table>
<br />
</asp:Panel>
<br />
<asp:Button ID="btnSaveEditTherapist" runat="server" Text="Save" OnClick="btnSaveEditTherapist_Click" />
<asp:Button ID="btnCancelUpdateTherapist" runat="server" Text="Cancel" OnClick="btnCancelUpdateTherapist_Click" />
