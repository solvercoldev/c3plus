<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="wucUserLogin.ascx.cs" Inherits="ASP.NETCLIENTE.Pages.UserControls.wucUserLogin" %>



 <table cellpadding="0" cellspacing="0">
   
    <tr>
         <td>
           <asp:Image ID="imgRol" runat="server" ImageUrl="~/Resources/Images/PersonalInformation.png" />
        </td>
        <td style="width:10px;"></td>
        <td class="textlogin">
            <asp:Literal ID="litUser" runat="server"></asp:Literal>
        </td>
        <td style="width:10px;"></td>
        <td align="right">
            <asp:LoginStatus 
            ID="LoginStatus1" 
            runat="server" 
            Font-Bold="true"
            LogoutText="Salir"
            CssClass="LinkLogOut"
            LogoutAction="Redirect" 
            LogoutPageUrl="~/Login.aspx" 
            OnLoggedOut ="LoginStatusOnLoggedOut"/>
        </td>
        <td style="width:10px;"></td>
    </tr>
   
</table>   