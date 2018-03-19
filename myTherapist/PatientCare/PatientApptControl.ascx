<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PatientApptControl.ascx.cs" Inherits="PatientCare_PatientApptControl" %>
<link rel="stylesheet" runat="server" href="../stylesheets/sitestyles.css" id="Link1" /> 
<h4 title="myTherapist">Patient Appointment</h4>
<asp:Label ID="lblPatientHeader" runat="server" Text="Patient Name:"></asp:Label>
<asp:Label ID="patientHeader" runat="server"></asp:Label>
<br />
<br />
<br />

<div class="tablenoborder">
   <div class="trpatientappt">
       <div class="tdpulsesection">
            <div class="trpulseinfoheader">
                <div class="tdpulseinfoheader">          
                    <asp:Label runat="server" Text="Pulse Information" ID="test"/>      
                    <asp:Label runat="server" Text="  " Height="15"/>      
                </div>
            </div>
            <div class="trpulseinfocolumn">
                <div class="tdpulseinfocolumn">                
                    <asp:Label runat="server" Text="  " Height="15"/>      
                </div>
            </div>
            <div class="trpulseinfocolumn">   
                <div class="tdpulseinfocolumn">          
                    <asp:Label runat="server" Text="RLU"/>
                        <asp:RadioButtonList runat="server" AutoPostBack="false" ID="RLU">
                            <asp:ListItem Text="No" Value="No"></asp:ListItem>
                            <asp:ListItem Text="Weak" Value="Weak"></asp:ListItem>
                            <asp:ListItem Text="Normal" Value="Normal"></asp:ListItem>
                            <asp:ListItem Text="Strong" Value="Strong"></asp:ListItem>
                        </asp:RadioButtonList>
                </div>
                <div class="tdpulseinfocolumn">
                    <asp:Label runat="server" Text="SP"/>
                    <asp:RadioButtonList runat="server" AutoPostBack="false" ID="SP">
                         <asp:ListItem Text="No" Value="No"></asp:ListItem>
                        <asp:ListItem Text="Weak" Value="Weak"></asp:ListItem>
                        <asp:ListItem Text="Normal" Value="Normal"></asp:ListItem>
                        <asp:ListItem Text="Strong" Value="Strong"></asp:ListItem>
                    </asp:RadioButtonList>
                </div>
                <div class="tdpulseinfocolumn">
                    <asp:Label runat="server" Text="KD"/>
                        <asp:RadioButtonList runat="server" AutoPostBack="false" ID="KD1">
                            <asp:ListItem Text="No" Value="No"></asp:ListItem>
                            <asp:ListItem Text="Weak" Value="Weak"></asp:ListItem>
                            <asp:ListItem Text="Normal" Value="Normal"></asp:ListItem>
                            <asp:ListItem Text="Strong" Value="Strong"></asp:ListItem>
                        </asp:RadioButtonList>
                </div>
            </div>
            <div class="trpulseinfocolumn">
                <div class="tdpulseinfocolumn">                
                    <asp:Label runat="server" Text="  " Height="25" />     
                </div>
            </div>
            <div class="trpulseinfocolumn">   
                <div class="tdpulseinfocolumn">          
                    <asp:Label runat="server" Text="LHT"/>
                    <asp:RadioButtonList runat="server" AutoPostBack="false" ID="LHT">
                         <asp:ListItem Text="No" Value="No"></asp:ListItem>
                        <asp:ListItem Text="Weak" Value="Weak"></asp:ListItem>
                        <asp:ListItem Text="Normal" Value="Normal"></asp:ListItem>
                        <asp:ListItem Text="Strong" Value="Strong"></asp:ListItem>
                    </asp:RadioButtonList>
                </div>
                <div class="tdpulseinfocolumn">
                    <asp:Label runat="server" Text="LV"/>
                    <asp:RadioButtonList runat="server" AutoPostBack="false" ID="LV">
                         <asp:ListItem Text="No" Value="No"></asp:ListItem>
                         <asp:ListItem Text="Weak" Value="Weak"></asp:ListItem>
                        <asp:ListItem Text="Normal" Value="Normal"></asp:ListItem>
                        <asp:ListItem Text="Strong" Value="Strong"></asp:ListItem>
                    </asp:RadioButtonList>
                </div>
                <div class="tdpulseinfocolumn">
                    <asp:Label runat="server" Text="KD"/>
                    <asp:RadioButtonList runat="server" AutoPostBack="false" ID="KD2">
                        <asp:ListItem Text="No" Value="No"></asp:ListItem>
                        <asp:ListItem Text="Weak" Value="Weak"></asp:ListItem>
                        <asp:ListItem Text="Normal" Value="Normal"></asp:ListItem>
                        <asp:ListItem Text="Strong" Value="Strong"></asp:ListItem>
                    </asp:RadioButtonList>
                </div>
            </div>
         </div>

         <div class="tdimagesectionone">
            <div class="trimageinfoheader">                
                <asp:Label runat="server" Text="Image Before"></asp:Label><asp:FileUpload runat="server" ID="uploadImageBefore" />
                
                <div class="tdimagecolumn">     
                   <asp:Image runat="server" Width="250" Height="250" ImageAlign="Middle" ImageUrl="~/Images/photoGoesHere.png" ID="ImageBefore" />                   
                </div>                 
            </div>
         </div>

         <div class="tdcolumnspacer">
             <div class="trcolumnspacer">                 
             </div>
         </div>

         <div class="tdimagesectiontwo">
            <div class="trimageinfoheader">
                <asp:Label runat="server" Text="Image After"></asp:Label><asp:FileUpload runat="server" ID="uploadImageAfter" />
                
                <div class="tdimagecolumn">                
                    <asp:Image runat="server" Width="250" Height="250" ImageAlign="Middle" ImageUrl="~/Images/photoGoesHere.png" ID="ImageAfter" />                    
                </div>
            </div>
         </div>
    </div>
    <div class="tr">
       <div class="tdcolumnspacer">             
           <asp:Label runat="server" ID="Label1" Text="" />             
       </div>
       <div class="tdcolumnspacer">             
           <asp:Label runat="server" ID="Label2" Text="" />             
       </div>
       <div class="tdcolumnspacer">             
           <asp:Button runat="server" ID="btnPreviewA" Text="Preview" />             
       </div>
    </div>
</div> 

<div class="tablenoborder">    
    <div class="tdpatientnotes">    
        <div class="trpatientnotes">                        
            <asp:Label runat="server" ID="lblTherapya" Text="Therapy/Oils Used"/>
        </div>
        <div class="trpatientnotes">                        
            <div class="tdpatientnotes">                             
                <asp:TextBox runat="server" Width="2000px" Height="150" TextMode="MultiLine" ID="txtBoxTherapyPerformed"/>
            </div>
        </div>
    </div>

    <div class="tdpatientnotes">    
        <div class="trpatientnotes">        
            <asp:Label runat="server" ID="lblSessionGoalsa" Text="Session Goals"></asp:Label>
        </div>
        <div class="trpatientnotes">
            <div class="tdpatientnotes">           
                <asp:TextBox runat="server" Width="2000px" Height="150" TextMode="MultiLine" ID="txtBoxSessionGoals"/>
            </div>
        </div>
    </div>
</div> 
