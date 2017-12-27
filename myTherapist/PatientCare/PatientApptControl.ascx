<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PatientApptControl.ascx.cs" Inherits="PatientCare_PatientApptControl" %>
<link rel="stylesheet" runat="server" href="./stylesheets/sitestyles.css" id="Link1" /> 

<h4 title="myTherapist">Patient Appointment</h4>

<asp:Label ID="lblPatientHeader" runat="server" Text="Patient Name:"></asp:Label>
<asp:Label ID="patientHeader" runat="server"></asp:Label>

<asp:Table runat="server" ID="PatientApptControl_Table1">
    <asp:TableRow runat="server">
        <asp:TableCell runat="server">
            <asp:Table runat="server">
                <asp:TableRow>            
                    <asp:TableCell runat="server" Text="LHT">    
                        <asp:RadioButtonList runat="server" AutoPostBack="false" ID="LHT">
                            <asp:ListItem Text="None" Value="None"></asp:ListItem>
                            <asp:ListItem Text="Weak" Value="Weak"></asp:ListItem>
                            <asp:ListItem Text="Strong" Value="Strong"></asp:ListItem>
                        </asp:RadioButtonList>
                    </asp:TableCell>
                    <asp:TableCell runat="server" Text="SP">    
                            <asp:RadioButtonList runat="server" AutoPostBack="false" ID="SP">
                                <asp:ListItem Text="None" Value="None"></asp:ListItem>
                                <asp:ListItem Text="Weak" Value="Weak"></asp:ListItem>
                                <asp:ListItem Text="Strong" Value="Strong"></asp:ListItem>
                            </asp:RadioButtonList>
                    </asp:TableCell>
                    <asp:TableCell runat="server" Text="KD">    
                            <asp:RadioButtonList runat="server" AutoPostBack="false" ID="KD1">
                                <asp:ListItem Text="None" Value="None"></asp:ListItem>
                                <asp:ListItem Text="Weak" Value="Weak"></asp:ListItem>
                                <asp:ListItem Text="Strong" Value="Strong"></asp:ListItem>
                            </asp:RadioButtonList>
                    </asp:TableCell>        
                    <asp:TableCell runat="server">    
                            <asp:Label runat="server" Width ="50" Text=""></asp:Label>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>            
                    <asp:TableCell runat="server" Text="RHT">    
                        <asp:RadioButtonList runat="server" AutoPostBack="false" ID="RHT">
                                <asp:ListItem Text="None" Value="None"></asp:ListItem>
                                <asp:ListItem Text="Weak" Value="Weak"></asp:ListItem>
                                <asp:ListItem Text="Strong" Value="Strong"></asp:ListItem>

                        </asp:RadioButtonList>
                    </asp:TableCell>
                    <asp:TableCell runat="server" Text="SP">    
                            <asp:RadioButtonList runat="server" AutoPostBack="false" ID="SP2">
                                <asp:ListItem Text="None" Value="None"></asp:ListItem>
                                <asp:ListItem Text="Weak" Value="Weak"></asp:ListItem>
                                <asp:ListItem Text="Strong" Value="Strong"></asp:ListItem>

                            </asp:RadioButtonList>
                    </asp:TableCell>
                    <asp:TableCell runat="server" Text="KD">    
                            <asp:RadioButtonList runat="server" AutoPostBack="false" ID="KD2">
                                <asp:ListItem Text="None" Value="None"></asp:ListItem>
                                <asp:ListItem Text="Weak" Value="Weak"></asp:ListItem>
                                <asp:ListItem Text="Strong" Value="Strong"></asp:ListItem>
                            </asp:RadioButtonList>
                    </asp:TableCell>        
                    <asp:TableCell runat="server">    
                            <asp:Label runat="server" Width ="50" Text=""></asp:Label>
                    </asp:TableCell>
                </asp:TableRow>                
            </asp:Table>
        </asp:TableCell>
        <asp:TableCell runat="server">              
            <asp:Table runat="server">
                <asp:TableRow runat="server">
                    <asp:TableCell><asp:Label runat="server" Text="Image Before"></asp:Label></asp:TableCell><asp:TableCell><asp:FileUpload runat="server" ID="uploadImageBefore" /></asp:TableCell>
                </asp:TableRow>
                <asp:TableRow runat="server">
                    <asp:TableCell Width="50" Height="50" ColumnSpan="2"><asp:Image runat="server" ImageUrl="~/Images/photoGoesHere.png" ID="ImageFiller1" /></asp:TableCell>
                </asp:TableRow>           
            </asp:Table>
        </asp:TableCell>

        <asp:TableCell runat="server">    
            <asp:Table runat="server">
                <asp:TableRow runat="server">
                    <asp:TableCell><asp:Label runat="server" Text="Image After"></asp:Label></asp:TableCell><asp:TableCell><asp:FileUpload runat="server" ID="uploadImageAfter" /></asp:TableCell>
                </asp:TableRow>
                <asp:TableRow runat="server">
                    <asp:TableCell Width="50" Height="50" ColumnSpan="2"><asp:Image runat="server" ImageUrl="~/Images/photoGoesHere.png" ID="Image1" /></asp:TableCell>
                </asp:TableRow>           
            </asp:Table>            
        </asp:TableCell>
        
    </asp:TableRow>

    <asp:TableRow>
        <asp:TableCell ColumnSpan="7">
            <asp:Label runat="server" ID="lblTherapy" Text="Therapy"></asp:Label>
        </asp:TableCell>
    </asp:TableRow>
    <asp:TableRow>
        <asp:TableCell ColumnSpan="7">            
            <asp:TextBox runat="server" Width="1400px" Height="150" TextMode="MultiLine" ID="txtboxTherapyPerformed"/>            
        </asp:TableCell>
    </asp:TableRow>
    <asp:TableRow>
        <asp:TableCell ColumnSpan="7">
            <asp:Label runat="server" ID="lblOilsUsed" Text="Oils Used"></asp:Label>
        </asp:TableCell>
    </asp:TableRow>
    <asp:TableRow>
        <asp:TableCell ColumnSpan="7">
            <asp:TextBox runat="server" Width="1400px" Height="150" TextMode="MultiLine" ID="txtboxOilsUsed"/>            
        </asp:TableCell>
    </asp:TableRow>
    <asp:TableRow>
        <asp:TableCell ColumnSpan="7">
            <asp:Label runat="server" ID="lblSessionGoals" Text="Session Goals"></asp:Label>            
        </asp:TableCell>
    </asp:TableRow>
    <asp:TableRow>
        <asp:TableCell ColumnSpan="7">
            <asp:TextBox runat="server" Width="1400px" Height="150" TextMode="MultiLine" ID="txtBoxSessionGoals"/>            
        </asp:TableCell>
    </asp:TableRow>    
</asp:Table>

