<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FrmEditTipoContrato.aspx.cs" Inherits="Modules.Admin.Catalogos.FrmEditTipoContrato" %>
<%@ Register TagPrefix="ajaxToolkit" Namespace="AjaxControlToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

<asp:ValidationSummary ID="vsGeneral" runat="server" DisplayMode="BulletList" ShowMessageBox="true" CssClass="validator" ShowSummary="False" ValidationGroup="vgGeneral"/>
 <div style="padding:3px; text-align:right;">
    <asp:button id="btnReturn" runat="server" OnClick="BtnBackClick" text="Regresar" causesvalidation="False"></asp:button>
	<asp:button id="btnSave" runat="server" OnClick="BtnSaveClick" text="Guardar"></asp:button>
    <asp:button id="btnAct" runat="server" OnClick="BtnActClick" text="Guardar"></asp:button>
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
                            <th style="width:1px">*</th>
						    <th style="text-align:left; vertical-align:top">Id Tipo Contrato</th>
						    <td align="left" class="Line">
                            <asp:textbox id="txtIdTipoContrato" MaxLength="10" runat="server"></asp:textbox>
                            
                             <asp:requiredfieldvalidator id="rfvIdTipoContrato" 
						        runat="server" 
						        errormessage="El campo [Id Tipo Contrato] es requerido!!." 
						        cssclass="validator"
								display="Dynamic" 
								enableclientscript="true" 
								controltovalidate="txtIdTipoContrato">
							</asp:requiredfieldvalidator>
                            </td>
					    </tr>
					    <tr>
                            <th style="width:1px">*</th>
						    <th style="text-align:left; vertical-align:top">Descripción</th>
						    <td align="left" class="Line">
						        <asp:textbox id="txtDescripción" runat="server" width="400px">
						        </asp:textbox>
						        <asp:requiredfieldvalidator id="rfvtxtDescripción" 
						        runat="server" 
						        errormessage="El campo [Descripción] es requerido!!." 
						        cssclass="validator"
								display="Dynamic" 
								enableclientscript="true" 
								controltovalidate="txtDescripción">
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
</asp:Content>
