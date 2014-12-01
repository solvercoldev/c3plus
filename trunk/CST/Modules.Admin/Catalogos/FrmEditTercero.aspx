﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FrmEditTercero.aspx.cs" Inherits="Modules.Admin.Catalogos.FrmEditTercero" %>
<%@ Register TagPrefix="ajaxToolkit" Namespace="AjaxControlToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

<asp:ValidationSummary ID="vsGeneral" runat="server" DisplayMode="BulletList" ShowMessageBox="true" CssClass="validator" ShowSummary="False" ValidationGroup="vgGeneral"/>
 <div style="padding:3px; text-align:right;">
    <asp:button id="btnReturn" runat="server" OnClick="BtnBackClick" text="Regresar" causesvalidation="False"></asp:button>
	<asp:button id="btnSave" runat="server" OnClick="BtnSaveClick" text="Guardar"></asp:button>
    <asp:button id="btnAct" runat="server" OnClick="BtnActClick" text="Actualizar"></asp:button>
	<asp:button id="btnEliminar" OnClientClick="return confirm('¿Esta seguro?');" runat="server" OnClick="BtnDeleteClick" causesvalidation="False" text="Borrar"></asp:button>
	
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
						    <th align="left">Id Tercero</th>
						    <td align="left">
                            <asp:textbox id="txtIdTercero" MaxLength="10" runat="server"></asp:textbox>
                            
                             <asp:requiredfieldvalidator id="rfvIdTercero" 
						        runat="server" 
						        errormessage="El campo [Id Tercero] es requerido!!." 
						        cssclass="validator"
								display="Dynamic" 
								enableclientscript="true" 
								controltovalidate="txtIdTercero">
							</asp:requiredfieldvalidator>
                            </td>
					    </tr>
					    <tr>
						    <th align="left">Nombre</th>
						    <td align="left">
						        <asp:textbox id="txtNombre" runat="server" width="400px">
						        </asp:textbox>
						        <asp:requiredfieldvalidator id="rfvtxtNombre" 
						        runat="server" 
						        errormessage="El campo [Nombre] es requerido!!." 
						        cssclass="validator"
								display="Dynamic" 
								enableclientscript="true" 
								controltovalidate="txtNombre">
								</asp:requiredfieldvalidator>
						</td>
					    </tr>				
				    </table>
    			
		
            </td>
       
        
        </tr>
    
    </table>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Footer" runat="server">
</asp:Content>
