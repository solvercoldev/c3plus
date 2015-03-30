<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FrmAdminMenuOption.aspx.cs" Inherits="Modules.Admin.Catalogos.FrmAdminMenuOption" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="csc" Namespace="ServerControls" Assembly="ServerControls" %>
<%@ Register Assembly="Infragistics4.WebUI.UltraWebNavigator.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
	Namespace="Infragistics.WebUI.UltraWebNavigator" TagPrefix="ignav" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<div style="padding:3px; text-align:right;">
    <asp:Button ID="btnNew" OnClick="BtnNewClick"  runat="server" Text="Nuevo Nodo"/>
    <asp:button id="btnSave" runat="server"  OnClick="BtnSaveClick" text="Guardar"></asp:button>
    <asp:button id="btnDelete" runat="server"  OnClick="BtnDeleteClick" causesvalidation="False" text="Eliminar"></asp:button>
</div>

<table width="100%" cellpadding="0" cellspacing="0" style=" padding-top:5px;">
	
	<tr>
	   
		<td style="width:30%" valign="top">
		    <fieldset>
			<asp:UpdatePanel ID="upOpciones" runat="server">
			 <ContentTemplate>
				<table width="100%">
				<tr>
					<td valign="top">
					
						<ignav:UltraWebTree 
						ID="uwtOpcionesMenu" 
						Width="95%"
						Height="350px"
						AllowDrag="true"
						
						AllowDrop="true"
						DataKeyOnClient="true"
						runat="server" DefaultImage="" 
						HoverClass="" Indentation="20" LoadOnDemand="Manual" 
						onnodeclicked="UwtOpcionesMenuNodeClicked">
						<NodePaddings Left="2px" Top="5px" />
						<SelectedNodeStyle BackColor="#02A3E9" ForeColor="White" Font-Bold="true" />
						<HoverNodeStyle Font-Bold="true" />
						</ignav:UltraWebTree>
						
					</td>
				</tr>
			   
			</table>  
			</ContentTemplate>
		  </asp:UpdatePanel>  
		</fieldset>  
		
		</td>
		<td style="width:1%">
		</td>
		<td style="width:50%" valign="top">		
		<asp:UpdatePanel ID="upDetalle" runat="server">
			<ContentTemplate>
			 <table id="userdetails" width="100%" class="tbl">					   
						<tr>
							<td align="left" style="width:30%">Descripción</td>
							<td align="left" style="width:70%">
								<asp:textbox id="txtDescripcion" ReadOnly="false" runat="server" width="200px">
								</asp:textbox>
								<%--<asp:requiredfieldvalidator id="rfvDescripcion" 
								runat="server" 
								errormessage="Descripción es requerida" 
								cssclass="validator"
								display="Dynamic" 
								enableclientscript="true" 
								controltovalidate="txtDescripcion">
								</asp:requiredfieldvalidator>--%>
						</td>
						</tr>
						<tr>
							<td align="left">Url</td>
							<td align="left">
								<asp:textbox 
								id="txtUrl" 
								runat="server" 
								ReadOnly="false"
								width="95%"></asp:textbox>
							</td>
						</tr>
						<tr>
							<td align="left">Posición</td>
							<td align="left">
								<asp:textbox 
								id="txtPosicion" 
								runat="server" 
								ReadOnly="false"
								width="50px"></asp:textbox>
								<%--<asp:requiredfieldvalidator 
								id="rfvposicion" 
								runat="server" 
								controltovalidate="txtPosicion" 
								enableclientscript="true" 
								display="Dynamic" 
								cssclass="validator" 
								errormessage="Posición es requerida">
								</asp:requiredfieldvalidator> --%>   
							</td>
						</tr>
                        <tr>
							<td align="left">Aplication ID</td>
							<td align="left">
								<asp:textbox 
								id="txtAplicationId" 
								runat="server" 
								width="50px"></asp:textbox>
								<%--<asp:requiredfieldvalidator 
								id="reqAplicationId" 
								runat="server" 
								controltovalidate="txtAplicationId" 
								enableclientscript="true" 
								display="Dynamic" 
								cssclass="validator" 
								errormessage="Aplication ID es requerida">
								</asp:requiredfieldvalidator>  --%>  
							</td>
						</tr>
						 <tr>
							<td align="left">Ver en MenuPrincipal</td>
							<td align="left">
							<asp:checkbox 
							id="chkShowInMainMenu" 
							Enabled="true"
							runat="server"></asp:checkbox></td>
						</tr>
                        <tr>
							<td align="left">Ver en Menu Secundario</td>
							<td align="left">
							<asp:checkbox 
							id="chkShowInSecondMenu" 
							Enabled="true"
							runat="server"></asp:checkbox></td>
						</tr>
						<tr>
							<td align="left">Activo</td>
							<td align="left">
							<asp:checkbox 
							id="chkActive" 
							Enabled="true"
							runat="server"></asp:checkbox></td>
						</tr>
					</table>   
		
				<table id="roles" class="tbl" width="100%">
						<asp:repeater id="rptRoles" runat="server" OnItemDataBound="RptRolesItemDataBound">
							<headertemplate>
								<tr>
									<th style="width:95%">Rol</th>
									<th style="width:5%">
									</th>
								</tr>
							</headertemplate>
							<itemtemplate>
								<tr>
									<td align="left">
                                        <asp:HiddenField ID="hddIdRol" runat="server" />
                                        <asp:Label ID="lblNombreRol" runat="server" />
                                    </td>
									<td style="text-align:center">
										<asp:checkbox id="chkRole" runat="server"></asp:checkbox>
									</td>
								</tr>
							</itemtemplate>
                            <AlternatingItemTemplate>
                                <tr class="AlternateGridStyle">
                                    <td align="left">
                                        <asp:HiddenField ID="hddIdRol" runat="server" />
                                        <asp:Label ID="lblNombreRol" runat="server" />
                                    </td>
									<td style="text-align:center">
										<asp:checkbox id="chkRole" runat="server"></asp:checkbox>
									</td>
						        </tr>
                            </AlternatingItemTemplate>
						</asp:repeater>
					</table>		
			 
				
			 </ContentTemplate>
			 <Triggers>
				<asp:AsyncPostBackTrigger ControlID="uwtOpcionesMenu" EventName="nodeclicked" />
			 </Triggers>
		</asp:UpdatePanel>
		</td>
	</tr>
   
</table>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Footer" runat="server">
</asp:Content>
