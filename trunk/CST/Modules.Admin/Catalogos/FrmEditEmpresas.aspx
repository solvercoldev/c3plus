<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FrmEditEmpresas.aspx.cs" Inherits="Modules.Admin.Catalogos.FrmEditEmpresas" %>
<%@ Register TagPrefix="ajaxToolkit" Namespace="AjaxControlToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

<asp:ValidationSummary ID="vsGeneral" runat="server" DisplayMode="BulletList" ShowMessageBox="true" CssClass="validator" ShowSummary="False" ValidationGroup="vgGeneral"/>
 <div style="padding:3px; text-align:right;">
    <asp:button id="btnReturn" runat="server" OnClick="BtnBackClick" text="Regresar" causesvalidation="False"></asp:button>
	<asp:button id="btnSave" runat="server" OnClick="BtnSaveClick" text="Guardar"></asp:button>
    <asp:button id="btnAct" runat="server" OnClick="BtnActClick" text="Guardar"></asp:button>
	<asp:button causesvalidation="False" id="btnEliminar" OnClick="BtnDeleteClick" OnClientClick="return confirm('¿Esta seguro?');" runat="server" text="Borrar" style="display: none"></asp:button>
</div>

<table width="100%" class="tblSecciones">
        <tr>
            <td>     
				    <table id="userdetails" width="100%">
					    
					    <tr>
						    <td>&nbsp;</td>
						    <td>&nbsp;</td>
					    </tr>
					    <tr>
                            <th style="width:1px">*</th>
						    <th style="text-align:left; vertical-align:top">Nit</th>
						    <td align="left" class="Line">
                            <asp:textbox id="txtNit" runat="server"></asp:textbox>
                            
                             <asp:requiredfieldvalidator id="rfvNit" 
						        runat="server" 
						        errormessage="El campo [Nit] es requerido!!." 
						        cssclass="validator"
								display="Dynamic" 
								enableclientscript="true" 
								controltovalidate="txtNit">
							</asp:requiredfieldvalidator>
                            </td>
					    </tr>
                        <tr>
                            <th style="width:1px">*</th>
						    <th style="text-align:left; vertical-align:top">Razón Social</th>
						    <td align="left" class="Line">
                            <asp:textbox id="txtRazonSocial" runat="server" width="400px"></asp:textbox>
                            
                             <asp:requiredfieldvalidator id="rfvRazonSocial" 
						        runat="server" 
						        errormessage="El campo [Razón Social] es requerido!!." 
						        cssclass="validator"
								display="Dynamic" 
								enableclientscript="true" 
								controltovalidate="txtRazonSocial">
							</asp:requiredfieldvalidator>
                            </td>
					    </tr>
					    <tr>
                            <th style="width:1px">*</th>
						    <th style="text-align:left; vertical-align:top">Dirección</th>
						    <td align="left" class="Line">
						        <asp:textbox id="txtDireccion" runat="server" width="400px">
						        </asp:textbox>
						        <asp:requiredfieldvalidator id="rfvDireccion" 
						        runat="server" 
						        errormessage="El campo [Direccion] es requerido!!." 
						        cssclass="validator"
								display="Dynamic" 
								enableclientscript="true" 
								controltovalidate="txtDireccion">
								</asp:requiredfieldvalidator>
						    </td>
					    </tr>
					    <tr>
                            <th style="width:1px">*</th>
						    <th style="text-align:left; vertical-align:top">Telefono 1</th>
						    <td align="left" class="Line">
						        <asp:textbox id="txtTelefono1" runat="server">
						        </asp:textbox>
						        <asp:requiredfieldvalidator id="rfvTelefono1" 
						        runat="server" 
						        errormessage="El campo [Telefono 1] es requerido!!." 
						        cssclass="validator"
								display="Dynamic" 
								enableclientscript="true" 
								controltovalidate="txtTelefono1">
								</asp:requiredfieldvalidator>
						    </td>
					    </tr>
                        <tr>
                            <th style="width:1px">*</th>
						    <th style="text-align:left; vertical-align:top">Telefono 2</th>
						    <td align="left" class="Line">
						        <asp:textbox id="txtTelefono2" runat="server">
						        </asp:textbox>
						        <asp:requiredfieldvalidator id="rfvTelefono2" 
						        runat="server" 
						        errormessage="El campo [Telefono 2] es requerido!!." 
						        cssclass="validator"
								display="Dynamic" 
								enableclientscript="true" 
								controltovalidate="txtTelefono2">
								</asp:requiredfieldvalidator>
						    </td>
					    </tr>
                        <tr>
                            <th style="width:1px">*</th>
						    <th style="text-align:left; vertical-align:top">Logo</th>
						    <td align="left" class="Line">
						        <asp:textbox id="txtLogo" runat="server" width="400px">
						        </asp:textbox>
						        <asp:requiredfieldvalidator id="rfvLogo" 
						        runat="server" 
						        errormessage="El campo [Logo] es requerido!!." 
						        cssclass="validator"
								display="Dynamic" 
								enableclientscript="true" 
								controltovalidate="txtLogo">
								</asp:requiredfieldvalidator>
						    </td>
					    </tr>
					    <tr>
                            <th style="width:1px"></th>
						    <th style="text-align:left; vertical-align:top">Activa</th>
						    <td align="left" class="Line"><asp:checkbox id="chkActive" runat="server" Checked="true"></asp:checkbox></td>
					    </tr>					
				    </table>
    			
		
            </td>
       
        
        </tr>
    
    </table>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Footer" runat="server">
<div class="StyleFooter">Created by 
    <asp:Literal ID="LitCreatedBy" runat="server"></asp:Literal>
    On 
    <asp:Literal ID="LitCreatedOn" runat="server"></asp:Literal>
    -- Last Modified By
    <asp:Literal ID="LiModifiedBy" runat="server"></asp:Literal>
    On
    <asp:Literal ID="LiModifiedOn" runat="server"></asp:Literal>
</div>
</asp:Content>
