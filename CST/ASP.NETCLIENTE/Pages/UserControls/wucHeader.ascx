<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="wucHeader.ascx.cs" Inherits="ASP.NETCLIENTE.Pages.UserControls.wucHeader" %>

<div id="navigationbar" class="shadow1">
 <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                 <td style="width:10%" valign="top">
                    <div class="Logo"></div>
                </td>
                 <td style="width:90%">
                     <div class="cabecera" >           
                        <div style="float: right; position: static; vertical-align: top; width:100px; margin-right: 300px;margin-top: 15px; display:none;">
                        <div style="float: right; position: static;">
                        <asp:label ID="lblLoginName" runat="server" Font-Size="10px" Font-Bold="true" CssClass="LoginHeader" Visible="false"/>
                    </div>
                    <br />
                    <div style="float: right; position: static; cursor: crosshair;  ">
                        <asp:LoginStatus 
                        ID="LoginStatus1"  Visible="false"
                        runat="server" 
                        Font-Bold="true"
                        CssClass="LoginHeader"
                        LogoutAction="Redirect" 
                        LogoutPageUrl="~/Login.aspx" 
                        OnLoggedOut ="LoginStatusOnLoggedOut"/>
                    </div>
                </div>          
                     </div >
                </td>               
            </tr>
        </table>

</div>