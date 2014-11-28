<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="wucMenuBar.ascx.cs" Inherits="ASP.NETCLIENTE.Pages.UserControls.wucMenuBar" %>

<div style="width:100%" class="BackGroundMenuBar">
    <table>
        <tr>
            <td style="width:25px" align="center">
                <asp:ImageButton 
                ID="btnNew" 
                runat="server" 
                ToolTip="New Customer"
                OnClick="BtnNewClick"
                ImageUrl="~/Resources/Images/New.png" />
            </td>
            <td class="Separator">                
            </td>
            <td style="width:25px" align="center">
                 <asp:ImageButton 
                 ID="btnSave" 
                 runat="server" 
                 OnClick="BtnSaveClick"
                 ToolTip="Save Customer"
                 ImageUrl="~/Resources/Images/Save.png" />
            </td>
            <td class="Separator">                
            </td>
            <td style="width:25px" align="center">
                 <asp:ImageButton 
                 ID="btnDelete" 
                 runat="server" 
                 OnClick="BtnDeleteClick"
                 ToolTip="Delete Customer"
                 ImageUrl="~/Resources/Images/delete.png" />
            </td>
             <td class="Separator">                
            </td>
            <td style="width:25px" align="center">
                 <asp:ImageButton 
                 ID="btnClose"
                 runat="server" 
                 OnClick="BtnCloseClick"
                 ToolTip="Close Window"
                 ImageUrl="~/Resources/Images/close.png" />
            </td>
             <td class="Separator">                
            </td>
    </table>
</div>