﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FrmEditMonedas.aspx.cs" Inherits="Modules.Admin.Catalogos.FrmEditMonedas" %>
<%@ Register TagPrefix="ajaxToolkit" Namespace="AjaxControlToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

<asp:ValidationSummary ID="vsGeneral" runat="server" DisplayMode="BulletList" ShowMessageBox="true" CssClass="validator" ShowSummary="False" ValidationGroup="vgGeneral"/>
 <div style="padding:3px; text-align:right;">
    <asp:button id="btnReturn" runat="server" OnClick="BtnBackClick" text="Regresar" causesvalidation="False"></asp:button>
	<asp:button id="btnSave" runat="server" OnClick="BtnSaveClick" text="Guardar"></asp:button>
    <asp:button id="btnAct" runat="server" OnClick="BtnActClick" text="Guardar"></asp:button>
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
						    <th style="text-align:left; vertical-align:top">Id Moneda</th>
						    <td align="left" class="Line">
                            <asp:textbox id="txtIdMoneda" MaxLength="10" runat="server"></asp:textbox>
                            
                             <asp:requiredfieldvalidator id="rfvIdMoneda" 
						        runat="server" 
						        errormessage="El campo [Id Moneda] es requerido!!." 
						        cssclass="validator"
								display="Dynamic" 
								enableclientscript="true" 
								controltovalidate="txtIdMoneda">
							</asp:requiredfieldvalidator>
                            </td>
					    </tr>
					    <tr>
                            <th style="width:1px">*</th>
						    <th style="text-align:left; vertical-align:top">Nombre</th>
						    <td align="left" class="Line">
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
