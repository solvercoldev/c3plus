<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="wucMenuPrincipal.ascx.cs" Inherits="ASP.NETCLIENTE.Pages.UserControls.WucMenuPrincipal" %>  
 
 <asp:Panel ID="pnPageMenu" runat="server" Width="100%" HorizontalAlign="Left" BackColor="#CBCBCB" BorderStyle="None" BorderWidth="0px">
  <asp:Menu ID="mnuMenuPrincipal" runat="server" Font-Bold="False"  Orientation="Horizontal" Height="20px"
            DynamicPopOutImageUrl="~/Resources/Images/hover-arrow.png" StaticPopOutImageUrl="~/Resources/Images/hover-arrow.png"
            StaticSubMenuIndent="" Font-Size="13px" BackColor="#CBCBCB" BorderStyle="Solid"
            BorderWidth="0px">
            <StaticSelectedStyle ForeColor="#E4E4E4" BackColor="#CBCBCB" BorderStyle="None" Font-Bold="false" />
            <StaticMenuItemStyle ForeColor="#585858"  BorderStyle="None" Font-Size="12px" Font-Bold="true" 
                ItemSpacing="5px" Width="100" CssClass="TextMenuStatic"
                Height="20px" />
            <DynamicHoverStyle BackColor="#C2C2C2" ForeColor="#585858" BorderStyle="None" />
            <DynamicMenuStyle BorderColor="#CBCBCB" BorderStyle="Solid" BorderWidth="0px" />
            <DynamicSelectedStyle BackColor="#C2C2C2" BorderStyle="None" Font-Bold="True" ForeColor="White"
                Height="19px" />
            <DynamicMenuItemStyle BackColor="#D8D8D8" ForeColor="#585858" Width="200px"
                ItemSpacing="0px" Height="20px" BorderStyle="None" Font-Bold="False" HorizontalPadding="10px" 
                CssClass="TextMenuDynamic" />
            <StaticHoverStyle ForeColor="White" BackColor="#CBCBCB" BorderStyle="None" Font-Bold="True" />
        </asp:Menu>
  </asp:Panel>