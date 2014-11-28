<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FrmAdminOptionsList.aspx.cs" Inherits="Modules.Admin.Catalogos.FrmAdminOptionsList" %>
<%@ Register TagPrefix="csc" Namespace="ServerControls" Assembly="ServerControls" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<div style="padding:3px; text-align:right;">
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
                                    
            <td style="width:20%;" valign="middle">
            <div id="divPanelShow">
                <div style="float: left; vertical-align: middle;" id="PnlFiltroHeader">
                    <asp:ImageButton 
                    ID="ShowHide" 
                    BorderStyle="None"
                    BorderWidth="0"
                    CausesValidation="false"
                    runat="server" 
                    AlternateText="Ver Filtro..." />
                </div>
                    <div style="float: left; width: 109px; vertical-align:middle; padding-left:10px;">
                    <asp:Label 
                    ID="lbFiltro" 
                    runat="server" 
                    ForeColor="#999999" 
                    Font-Names="Trebuchet MS"
                    Font-Size="0.8em">Ver Filtro...</asp:Label>
                </div>
                </div>
                <asp:button id="btnNew" runat="server" Visible="false" OnClick="BtnNewClick" text="Nuevo Option List"></asp:button>
            </td>
        </tr>
    </table>
        <asp:Panel id="PanelFiltro" CssClass="FondoSeccionFiltro" runat="server"  style="display:none;" Width="99%" >
                      
        <table class="tblSecciones" width="100%">
            <tr>
                <th class="TituloEtiqueta" style="text-align:center">
                    Buscar: 
                </th>
                <td class="TituloEtiqueta" style="text-align:left">
                    <asp:TextBox ID="txtSearch" runat="server" />
                    <asp:Button ID="btnFiltrar" runat="server" Text="Filter" CausesValidation="false" OnClick="BtnFilterReclamos_Click" OnClientClick="return ShowSplashModalLoading();" />
                </td>
                <td></td>
                <td></td>
            </tr>
        </table>

    </asp:Panel>

    <ajaxToolkit:CollapsiblePanelExtender 
        ID="cpeFiltroCategoriaReclamos" 
        runat="server" 
        TargetControlID="PanelFiltro"
        ExpandControlID="divPanelShow" 
        CollapseControlID="divPanelShow" 
        TextLabelID="lbFiltro"
        ImageControlID="ShowHide" 
        ExpandedText="Ocultar Filtro..." 
        CollapsedText="Ver Filtro..."
        ExpandedImage="~/Resources/images/Collapse.gif" 
        CollapsedImage="~/Resources/images/Expand.gif"
        SuppressPostBack="true" Collapsed="true">
    </ajaxToolkit:CollapsiblePanelExtender> 
</div>

<table style="width:100%" cellpadding="0" cellspacing="0">
    <tr>  
        <td >
                <asp:UpdatePanel ID="upPrincipal" runat="server"> 
                <ContentTemplate>
                    <table class="tbl" width="100%">
                    <asp:repeater id="rptListado" runat="server" 
                    OnItemCommand ="RptListadoItemCommand"
                    OnItemDataBound="RptListadoItemDataBound">
	                    <headertemplate>
		                    <tr>
                                <th style="width:150px;">Modulo</th>
                                <th style="width:150px;">Key</th>
			                    <th style="width:190px;">Value</th>
		                        <th style="width:190px;">Descripción</th>
			                    <th style="width:30px;"></th>
		                    </tr>
	                    </headertemplate>
	                    <itemtemplate>
		                    <tr>
                                <td align="left"><asp:Literal ID="litModulo" runat="server"></asp:Literal></td>
                                <td align="left"><asp:Literal ID="litKey" runat="server"></asp:Literal></td>
		                        <td align="left"><asp:Literal ID="litValue" runat="server"></asp:Literal></td>
                                <td align="left"><asp:Literal ID="litDescripcion" runat="server"></asp:Literal></td>
			                    <td align="center">
				                    <asp:LinkButton ID="CmdEditar" CausesValidation="false" Text="Edit" runat="server" />
			                    </td>
		                    </tr>
	                    </itemtemplate>
                        <AlternatingItemTemplate>
                            <tr class="AlternateGridStyle">
                                <td align="left"><asp:Literal ID="litModulo" runat="server"></asp:Literal></td>
                                <td align="left"><asp:Literal ID="litKey" runat="server"></asp:Literal></td>
		                        <td align="left"><asp:Literal ID="litValue" runat="server"></asp:Literal></td>
                                <td align="left"><asp:Literal ID="litDescripcion" runat="server"></asp:Literal></td>
			                    <td align="center">
				                    <asp:LinkButton ID="CmdEditar" CausesValidation="false" Text="Edit" runat="server" />
			                    </td>
		                    </tr>
                        </AlternatingItemTemplate>
                    </asp:repeater>
                    </table>

                    <div class="pager">
				            <csc:PagerLinq ID="pgrListado" runat="server" PageSize="14" OnPageChanged="PgrChanged"/>
	                </div>	
                </ContentTemplate>
                </asp:UpdatePanel>            
        </td>
    </tr>
</table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Footer" runat="server">
</asp:Content>
