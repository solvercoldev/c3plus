﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FrmViewRoles.aspx.cs" Inherits="Modules.Admin.Catalogos.FrmViewRoles" %>
<%@ Register TagPrefix="csc" Namespace="ServerControls" Assembly="ServerControls" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

<div style="padding:3px; text-align:right;">
  <asp:button id="btnNew" OnClick="BtnNewClick" runat="server" text="Nuevo Rol" Visible="true"></asp:button>
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
                                <th>Id Rol</th>
		                        <th>Nombre Rol</th>
			                    <th>Create On</th>
			                    <th style="width:50px;">Active</th>
                                <th style="width:50px;">Is Group</th>
			                    <th style="width:30px;"></th>
		                    </tr>
	                    </headertemplate>
	                    <itemtemplate>
		                    <tr>
			                    <td align="left"><%# DataBinder.Eval(Container.DataItem, "IdRol")%></td>
                                <td align="left"><%# DataBinder.Eval(Container.DataItem, "NombreRol")%></td>
			                    <td align="center"><%# DataBinder.Eval(Container.DataItem, "CreateOn", "{0:d}")%></td>
			                    <td align="center"><asp:CheckBox id="chkActivo" runat="server" Enabled="false"></asp:CheckBox></td>
                                <td align="center"><asp:CheckBox id="chkGroup" runat="server" Enabled="false"></asp:CheckBox></td>
			                    <td align="center">
				                    <asp:LinkButton ID="CmdEditar" CausesValidation="false" Text="Editar" runat="server" />
			                    </td>
		                    </tr>
	                    </itemtemplate>
                        <AlternatingItemTemplate>
                            <tr class="AlternateGridStyle">
			                    <td align="left"><%# DataBinder.Eval(Container.DataItem, "IdRol")%></td>
                                <td align="left"><%# DataBinder.Eval(Container.DataItem, "NombreRol")%></td>
			                    <td align="center"><%# DataBinder.Eval(Container.DataItem, "CreateOn", "{0:d}")%></td>
			                    <td align="center"><asp:CheckBox id="chkActivo" runat="server" Enabled="false"></asp:CheckBox></td>
                                <td align="center"><asp:CheckBox id="chkGroup" runat="server" Enabled="false"></asp:CheckBox></td>
			                    <td align="center">
				                    <asp:LinkButton ID="CmdEditar" CausesValidation="false" Text="Editar" runat="server" />
			                    </td>
		                    </tr>
                        </AlternatingItemTemplate>
                    </asp:repeater>
                    </table>

                    <div class="pager">
				            <csc:PagerLinq ID="pgrListado" OnPageChanged="PgrChanged" PageSize="15" runat="server" />
	                </div>		
                </ContentTemplate>
                </asp:UpdatePanel>
        </td>
    </tr>
</table>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Footer" runat="server">
</asp:Content>
