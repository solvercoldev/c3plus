<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FrmEditUsuarios.aspx.cs" Inherits="Modules.Admin.Catalogos.FrmEditUsuarios" %>
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
						    <th style="text-align:left; vertical-align:top">ID User</th>
						    <td align="left" class="Line">
                            <asp:textbox id="txtIdUser" runat="server"></asp:textbox></td>
					    </tr>
					    <tr>
                            <th style="width:1px">*</th>
						    <th style="text-align:left; vertical-align:top">Código Usuario</th>
						    <td align="left" class="Line">
                            <asp:textbox id="txtCodigoUser" runat="server"></asp:textbox>
                            
                             <asp:requiredfieldvalidator id="rfvCodigoUser" 
						        runat="server" 
						        errormessage="El campo [Código Usuario] es requerido!!." 
						        cssclass="validator"
								display="Dynamic" 
								enableclientscript="true" 
								controltovalidate="txtCodigoUser">
							</asp:requiredfieldvalidator>
                            </td>
					    </tr>
                        <tr>
                            <th style="width:1px">*</th>
						    <th style="text-align:left; vertical-align:top">Nombres </th>
						    <td align="left" class="Line">
                            <asp:textbox id="txtNombre" runat="server" width="400px"></asp:textbox>
                            
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
					    <tr>
                            <th style="width:1px">*</th>
						    <th style="text-align:left; vertical-align:top">Username</th>
						    <td align="left" class="Line">
						        <asp:textbox id="txtUserName" runat="server">
						        </asp:textbox>
						        <asp:requiredfieldvalidator id="rfvUsername" 
						        runat="server" 
						        errormessage="El campo [Username] es requerido!!." 
						        cssclass="validator"
								display="Dynamic" 
								enableclientscript="true" 
								controltovalidate="txtUserName">
								</asp:requiredfieldvalidator>
						    </td>
					    </tr>
					    <tr>
                            <th style="width:1px">*</th>
						    <th style="text-align:left; vertical-align:top">Password</th>
						    <td align="left" class="Line">
						        <asp:textbox id="txtPassword" TextMode="Password" runat="server" >
						        </asp:textbox>
						        <asp:requiredfieldvalidator id="rfvPassword" 
						        runat="server" 
						        errormessage="El campo [Password 1] es requerido!!." 
						        cssclass="validator"
								display="Dynamic" 
								enableclientscript="true" 
								controltovalidate="txtPassword">
								</asp:requiredfieldvalidator>
						    </td>
					    </tr>
                        <tr>
                            <th style="width:1px"></th>
						    <th style="text-align:left; vertical-align:top">Email</th>
						    <td align="left" class="Line">
						        <asp:textbox id="txtEmail" runat="server" width="400px">
						        </asp:textbox>
						    </td>
					    </tr>
                        <tr>
                            <th style="width:1px"></th>
						    <th style="text-align:left; vertical-align:top">Documento</th>
						    <td align="left" class="Line">
						        <asp:textbox id="txtDocumento" runat="server">
						        </asp:textbox>
						    </td>
					    </tr>
                        <tr>
                            <th style="width:1px"></th>
						    <th style="text-align:left; vertical-align:top">Telefono Fijo</th>
						    <td align="left" class="Line">
						        <asp:textbox id="txtTelefonoFijo" runat="server">
						        </asp:textbox>						        
						    </td>
					    </tr>
                        <tr>
                            <th style="width:1px"></th>
						    <th style="text-align:left; vertical-align:top">Extension</th>
						    <td align="left" class="Line">
						        <asp:textbox id="txtExtension" runat="server">
						        </asp:textbox>						        
						    </td>
					    </tr>
                        <tr>
                            <th style="width:1px"></th>
						    <th style="text-align:left; vertical-align:top">Movil</th>
						    <td align="left" class="Line">
						        <asp:textbox id="txtMovil" runat="server">
						        </asp:textbox>						        
						    </td>
					    </tr>
                        <tr>
                            <th style="width:1px">*</th>
						    <th style="text-align:left; vertical-align:top">Localización</th>
						    <td align="left" class="Line">
                                <asp:DropDownList 
                                CssClass="CombosGenericos"
						        ID="ddlLocalizacion" 
						        Width="200"
						        runat="server"></asp:DropDownList>
                                <asp:requiredfieldvalidator id="rfvLocalizacion" 
						            runat="server" 
						            errormessage="El campo [Localización] es requerido!!." 
						            cssclass="validator"
								    display="Dynamic" 
								    enableclientscript="true" 
								    controltovalidate="ddlLocalizacion">
							    </asp:requiredfieldvalidator>				        
						    </td>
					    </tr>
                        <tr>
                            <th style="width:1px"></th>
						    <th style="text-align:left; vertical-align:top">Dirección</th>
						    <td align="left" class="Line">
						        <asp:textbox id="txtDireccion" runat="server" width="400px">
						        </asp:textbox>						        
						    </td>
					    </tr>
                        <tr>
                            <th style="width:1px">*</th>
						    <th style="text-align:left; vertical-align:top">Dependencia</th>
						    <td align="left" class="Line">
                                <asp:DropDownList 
                                CssClass="CombosGenericos"
						        ID="ddlDependencia" 
						        Width="200"
						        runat="server"></asp:DropDownList>	
                                <asp:requiredfieldvalidator id="rfvDependencia" 
						            runat="server" 
						            errormessage="El campo [Dependencia] es requerido!!." 
						            cssclass="validator"
								    display="Dynamic" 
								    enableclientscript="true" 
								    controltovalidate="ddlDependencia">
							    </asp:requiredfieldvalidator>				        
						    </td>
					    </tr>
                        <tr>
                            <th style="width:1px"></th>
						    <th style="text-align:left; vertical-align:top">Cargo</th>
						    <td align="left" class="Line">
						        <asp:textbox id="txtCargo" runat="server" width="400px">
						        </asp:textbox>						        
						    </td>
					    </tr>
					    <tr>
                            <th style="width:1px"></th>
						    <th style="text-align:left; vertical-align:top">Activa</th>
						    <td align="left" class="Line"><asp:checkbox id="chkActive" runat="server" Checked="true"></asp:checkbox></td>
					    </tr>
                        <tr>
                            <th style="width:1px"></th>
						    <th style="text-align:left; vertical-align:top">Roles</th>
						    <td align="left" class="Line">
                                <table id="roles" width="30%">
                                    <asp:repeater id="rptRoles" OnItemDataBound="RptRolesItemDataBound" runat="server">
                                        <headertemplate>
	                                        <tr>
		                                        <th></th>
		                                        <th></th>
	                                        </tr>
                                        </headertemplate>
	                                    <itemtemplate>
		                                    <tr>
			                                    <td><%# DataBinder.Eval(Container.DataItem, "NombreRol") %></td>
			                                    <td style="text-align:left">
				                                    <asp:checkbox id="chkRole" runat="server"></asp:checkbox>
			                                    </td>
		                                    </tr>
	                                    </itemtemplate>
                                    </asp:repeater>
                                </table>
                            </td>
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
