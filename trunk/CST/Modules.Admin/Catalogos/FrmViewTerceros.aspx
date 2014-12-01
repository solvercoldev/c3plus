<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FrmViewTerceros.aspx.cs" Inherits="Modules.Admin.Catalogos.FrmViewTerceros" %>
<%@ Register TagPrefix="csc" Namespace="ServerControls" Assembly="ServerControls" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

<div style="padding:3px; text-align:right;">
  <asp:button id="btnNew" OnClick="BtnNewClick" runat="server" text="Nuevo Tercero" Visible="true"></asp:button>
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
                                <th>Tercero</th>
		                        <th>Nombre</th>
			                    <th style="width:50px;"></th>
			                    <th style="width:30px;"></th>
		                    </tr>
	                    </headertemplate>
	                    <itemtemplate>
		                    <tr>
			                    <td align="left"><%# DataBinder.Eval(Container.DataItem, "IdTercero")%></td>
                                <td align="left"><%# DataBinder.Eval(Container.DataItem, "Nombre")%></td>
			                    <td align="center">
				                    <asp:LinkButton ID="CmdEditar" CausesValidation="false" Text="Editar" runat="server" />
			                    </td>
		                    </tr>
	                    </itemtemplate>
                        <AlternatingItemTemplate>
                            <tr class="AlternateGridStyle">
		                        <td align="left"><%# DataBinder.Eval(Container.DataItem, "IdTercero")%></td>
                                <td align="left"><%# DataBinder.Eval(Container.DataItem, "Nombre")%></td>
			                    <td align="center">
				                    <asp:LinkButton ID="CmdEditar" CausesValidation="false" Text="Editar" runat="server" />
			                    </td>
		                    </tr>
                        </AlternatingItemTemplate>
                    </asp:repeater>
                    </table>

                    <div class="pager">
				            <csc:PagerLinq ID="pgrListado" runat="server" PageSize="15" OnPageChanged="PgrChanged"/>
	                </div>		
                </ContentTemplate>
                </asp:UpdatePanel>
              
                        
        </td>
        
       
    
    </tr>

</table>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Footer" runat="server">
</asp:Content>
