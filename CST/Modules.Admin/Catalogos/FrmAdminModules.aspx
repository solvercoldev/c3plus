<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FrmAdminModules.aspx.cs" Inherits="Modules.Admin.Catalogos.FrmAdminModules" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="csc" Namespace="ServerControls" Assembly="ServerControls" %>



<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

 <table class="tbl" width="100%">
                    
            <asp:repeater id="rptModules" runat="server" OnItemDataBound="RptModulesItemDataBound" OnItemCommand="RptModulesItemCommand" >
			<headertemplate>

                    <tr>
                        <th>
                            Nombre Modulo</th>
                        <th>
                            Ensamblado</th>
                        <th>
                            Cargar al Inicio</th>
                            <th>
                            Estado Activacion
                            </th>
                        <th>
                            Estado Instalación
                        </th>
                        <th>
                            Acciones</th>
                    </tr>
			</headertemplate>
			<itemtemplate>
			    <tr>
                        <td>
                            <%# DataBinder.Eval(Container.DataItem, "Nombre")%>
                        </td>
                        <td>
                            <%# DataBinder.Eval(Container.DataItem, "NombreEnsamblado")%>
                        </td>
                        <td align="center">
                            <asp:CheckBox ID="chkBoxActivation" runat="server" AutoPostBack="true" OnCheckedChanged="ChkBoxActivationCheckedChanged" /></td>
                       <td align="center">
                          <asp:Literal ID="litActivationStatus" runat="server" />
                       </td>
                        <td>
                            <asp:Literal ID="litStatus" runat="server"></asp:Literal></td>
                        <td>
                            <asp:LinkButton ID="lbtInstall" runat="server" Visible="False" CommandName="Install"
                                CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Nombre") + ":" + DataBinder.Eval(Container.DataItem, "NombreEnsamblado") %>'>Install</asp:LinkButton>&nbsp;
                            <asp:LinkButton ID="lbtUpgrade" runat="server" Visible="False" CommandName="Upgrade"
                                CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Nombre") + ":" + DataBinder.Eval(Container.DataItem, "NombreEnsamblado") %>'>Upgrade</asp:LinkButton>&nbsp;
                            <asp:LinkButton ID="lbtUninstall" runat="server" Visible="False" CommandName="Uninstall"
                                CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Nombre") + ":" + DataBinder.Eval(Container.DataItem, "NombreEnsamblado") %>'>Uninstall</asp:LinkButton>
                        </td>
                    </tr>	
	        </itemtemplate>
			</asp:repeater>
                    
    </table>   
<div class="pager" style="width:100%">
		<csc:pagerlinq
        id="pgrListado" 
        runat="server"
        OnPageChanged="PgrListadoPageChanged"  
        pagesize="20"></csc:pagerlinq>
	</div>	
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Footer" runat="server">
</asp:Content>
