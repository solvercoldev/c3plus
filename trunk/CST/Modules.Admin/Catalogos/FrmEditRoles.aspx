<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FrmEditRoles.aspx.cs" Inherits="Modules.Admin.Catalogos.FrmEditRoles" %>
<%@ Register TagPrefix="ajaxToolkit" Namespace="AjaxControlToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

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
						    <th style="text-align:left; vertical-align:top">ID Rol</th>
						    <td align="left" class="Line">
                            <asp:textbox id="txtIdRol" runat="server"></asp:textbox></td>
					    </tr>
					    <tr>
                            <th style="width:1px">*</th>
						    <th style="text-align:left; vertical-align:top">Nombre Rol</th>
						    <td align="left" class="Line">
                            <asp:textbox id="txtNombreRol" runat="server"></asp:textbox>
                            
                             <asp:requiredfieldvalidator id="rfvNombreRol" 
						        runat="server" 
						        errormessage="El campo [Nombre Rol] es requerido!!." 
						        cssclass="validator"
								display="Dynamic" 
								enableclientscript="true" 
								controltovalidate="txtNombreRol">
							</asp:requiredfieldvalidator>
                            </td>
					    </tr>
					    <tr>
                            <th style="width:1px"></th>
						    <th style="text-align:left; vertical-align:top">Activa</th>
						    <td align="left" class="Line"><asp:checkbox id="chkActive" runat="server" Checked="true"></asp:checkbox></td>
					    </tr>
                        <tr>
                            <th style="width:1px"></th>
						    <th style="text-align:left; vertical-align:top">Es Grupo</th>
						    <td align="left" class="Line"><asp:checkbox id="chkGrupo" runat="server" Checked="true"></asp:checkbox></td>                                
                        </tr>
                        <tr>
						    <td>&nbsp;</td>
						    <td>&nbsp;</td>
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
