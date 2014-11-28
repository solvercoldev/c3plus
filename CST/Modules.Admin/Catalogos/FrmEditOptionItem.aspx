<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FrmEditOptionItem.aspx.cs" Inherits="Modules.Admin.Catalogos.FrmEditOptionItem" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<asp:ValidationSummary ID="vsGeneral" runat="server" DisplayMode="BulletList" ShowMessageBox="true" CssClass="validator" ShowSummary="true" ValidationGroup="vgGeneral"/>
 <div style="padding:3px; text-align:right;">
    <asp:button id="btnReturn" runat="server" OnClick="BtnBackClick" text="Regresar" causesvalidation="False"></asp:button>
	<asp:button id="btnSave" runat="server" OnClick="BtnSaveClick" text="Guardar"></asp:button>
	<asp:button id="btnEliminar" OnClientClick="return confirm('¿Esta seguro?');" Visible="false" runat="server" OnClick="BtnDeleteClick" causesvalidation="False" text="Eliminar"></asp:button>
</div>
<table width="100%" class="tblSecciones" >
        <tr>
          
            <td>
				    <table id="userdetails" width="100%">
					    
					    <tr>
						    <td>&nbsp;</td>
						    <td>&nbsp;</td>
					    </tr>
                        <tr>
						    <th style="text-align:left;vertical-align:top">Module Id:</th>
						    <td align="left">
                                <asp:Literal runat="server" ID="txtModule"></asp:Literal>
                            </td>
					    </tr>
                        <tr>
						    <th style="text-align:left;vertical-align:top">Key:</th>
						    <td align="left">
                            <asp:Literal runat="server" ID="txtKey"></asp:Literal>
						    </td>
					    </tr>
					    <tr>
						    <th style="text-align:left;vertical-align:top">Value:</th>
						    <td align="left">
						        <asp:textbox id="txtValue" runat="server" width="400px" MaxLength="512">
						        </asp:textbox>
						        <asp:requiredfieldvalidator id="rfvNombre" 
						        runat="server" 
						        errormessage="El campo [Value] es requerido!!." 
						        cssclass="validator"
								display="Dynamic" 
								enableclientscript="true" 
								controltovalidate="txtValue">
								</asp:requiredfieldvalidator>
                            </td>
					    </tr>
					    <tr>
						    <th style="text-align:left;vertical-align:top">Descripción:</th>
						    <td align="left">
						        <asp:textbox id="txtDescripcion" runat="server" width="400px" MaxLength="512">
						        </asp:textbox>
						        <asp:requiredfieldvalidator id="rfvDescripcion" 
						        runat="server" 
						        errormessage="El campo [Descripcion] es requerido!!." 
						        cssclass="validator"
								display="Dynamic" 
								enableclientscript="true" 
								controltovalidate="txtDescripcion">
								</asp:requiredfieldvalidator>
                            </td>
					    </tr>
					    <tr>
						    <th style="text-align:left;vertical-align:top">Activo:</th>
						    <td align="left"><asp:checkbox id="chkActive" runat="server" Checked="true"></asp:checkbox></td>
					    </tr>	
                        <tr>
						    <th style="text-align:left;vertical-align:top">Creado por:</th>
						    <td align="left"><asp:Label ID="lblCreateBy" runat="server"></asp:Label></td>
					    </tr>
                        <tr>
						    <th style="text-align:left;vertical-align:top">Fecha creación:</th>
						    <td align="left"><asp:Label ID="lblCreateOn" runat="server"></asp:Label></td>
					    </tr>
                        <tr>
						    <th style="text-align:left;vertical-align:top">Modificado por:</th>
						    <td align="left"><asp:Label ID="lblModifiedBy" runat="server"></asp:Label></td>
					    </tr>
                        <tr>
						    <th style="text-align:left;vertical-align:top">Fecha modificación:</th>
						    <td align="left"><asp:Label ID="lblModifiedOn" runat="server"></asp:Label></td>
					    </tr>
					    <tr>
						    <td align="left"></td>
						    <td align="left"></td>
					    </tr>
				    </table>
            </td>
        </tr>
    
    </table>
</asp:Content>

