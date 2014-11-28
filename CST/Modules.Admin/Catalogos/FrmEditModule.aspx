<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FrmEditModule.aspx.cs" Inherits="Modules.Admin.Catalogos.FrmEditModule" %>
<%@ Register Assembly="Infragistics4.Web.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.Web.UI.NavigationControls" TagPrefix="ig" %>    
<%@ Register Assembly="Infragistics4.Web.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.Web.UI.EditorControls" TagPrefix="ig" %>




<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">


<asp:UpdatePanel ID="upGeneral" runat="server">
    <ContentTemplate>
    
<table width="100%" cellpadding="0" cellspacing="0" style=" margin-top:10px;">
        <tr>
            <td style=" width:30%" valign="top">
               <div style=" border:1px solid #EEEEEE; height:350px;">
                <ig:WebDataMenu runat="server" ID="ContextMenu" IsContextMenu="true" OnItemClick="ContextMenuClick"
                    BorderWidth="1" BorderColor="#CCCCCC" EnableScrolling="false">
                    <ClientEvents ItemClick="MenuItem_Click" />
                    <AutoPostBackFlags ItemClick="On" />
                    <Items>
                        <ig:DataMenuItem Text="Seleccionar Modulo" Key="Select" ImageUrl="~/Resources/Images/select.png" />
                        <ig:DataMenuItem Text="Expandir/Contraer" Key="expand" ImageUrl="~/Resources/Images/expand.png" />                        
                    </Items>
                </ig:WebDataMenu>

                <div>
                    <ig:WebDataTree ID="wdtModulos" runat="server" Width="300px"
                        InitialExpandDepth="1">
                        <ClientEvents NodeClick="Node_Click" />                        
                    </ig:WebDataTree>
                </div>
                <input type="hidden" id="hndNodeSelected" runat="server" />
                </div> 
            </td>
            <td style="width:2%;"></td>
            <td style=" width:68%"  valign="top">    
               
               <div>
                    <table class="tbl">
                        <asp:repeater id="rptSections" runat="server">
						<headertemplate>
							<tr>
								<th style="width:150px;">
									Titulo
                                </th>
								<th style="width:150px;">
									Componente
                                </th>
                                <th style="width:150px;">
									Marcador de Poición
                                </th>
								<th>
									&nbsp;</th>
							</tr>
						</headertemplate>
						<itemtemplate>
							<tr>
								<td>
                                    <%# DataBinder.Eval(Container.DataItem, "Titulo") %>
                                </td>
								<td>
								     <%# DataBinder.Eval(Container.DataItem, "Componente") %>	
                                </td>
                                <td>
								     <%# DataBinder.Eval(Container.DataItem, "Placeholder")%>	
                                </td>
								<td>
									<asp:LinkButton ID="lnkEdit" runat="server" Text="Seleccionar" CausesValidation="false"></asp:LinkButton>
                                 </td>
							</tr>
						</itemtemplate>
					</asp:repeater>
                    </table>
               </div>

               <div style=" margin-top:10px;">
                   
               <asp:DetailsView 
               AutoGenerateRows="False" 
               DataKeyNames="OID" 
               HeaderText="Editar Módulo" 
               ID="dvRutas" 
               OnDataBound="OnDataBoundEvent"
               runat="server" 
               BorderStyle="Solid" 
               BorderWidth="1px" EmptyDataText="Seleccione un registro de la lista superior!!"
               BorderColor="Black"
               Width="600px" GridLines="None">
                    
                    <Fields >                       

                        <asp:TemplateField HeaderText="Módulo" HeaderStyle-BackColor="#F5F5F5" HeaderStyle-Width="150">
                            <ItemTemplate>
                                <asp:Literal ID="litModulo" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Modulo")%>'></asp:Literal>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:DropDownList ID="ddlIdModulo" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DllModuloSelectedIndexChanged" Width="150"></asp:DropDownList>
                                <input type="hidden" id="hdnIdModulo"  runat="server" value='<%# Bind("IdModulo") %>' />
                                <asp:RequiredFieldValidator 
                                ID="rfvModulo" 
                                runat="server"
                                ControlToValidate="ddlIdModulo" 
                                ErrorMessage="Campo [Modulo] es requerido!" 
                                CssClass="validator" 
                                Text="*" 
                                Display="Dynamic"></asp:RequiredFieldValidator>
                            </EditItemTemplate>
                            <InsertItemTemplate>
                                <asp:DropDownList ID="ddlIdModulo" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DllModuloSelectedIndexChanged" Width="150"></asp:DropDownList>
                                 <asp:RequiredFieldValidator 
                                ID="rfvModulo" 
                                runat="server"
                                ControlToValidate="ddlIdModulo" 
                                ErrorMessage="Campo [Modulo] es requerido!" 
                                CssClass="validator" 
                                Text="*" 
                                Display="Dynamic"></asp:RequiredFieldValidator>
                            </InsertItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Componente" HeaderStyle-BackColor="#F5F5F5">
                            <ItemTemplate>
                                <asp:Literal ID="litComponente" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Componente")%>'></asp:Literal>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:DropDownList ID="ddlComponente" runat="server" Width="150" ></asp:DropDownList>
                                <input type="hidden" id="hdnIdComponente"  runat="server" value='<%# Bind("IdModuleType") %>' />
                                <asp:RequiredFieldValidator 
                                ID="rfvEstadoFinal" 
                                runat="server"
                                ControlToValidate="ddlComponente" 
                                ErrorMessage="Campo [Componente] es requerido!" 
                                CssClass="validator" 
                                Text="*" 
                                Display="Dynamic"></asp:RequiredFieldValidator>
                            </EditItemTemplate>
                            <InsertItemTemplate>
                                 <asp:DropDownList ID="ddlComponente" runat="server" Width="150"></asp:DropDownList>
                                <asp:RequiredFieldValidator 
                                ID="rfvEstadoFinal" 
                                runat="server"
                                ControlToValidate="ddlComponente" 
                                ErrorMessage="Campo [Componente] es requerido!" 
                                CssClass="validator" 
                                Text="*" 
                                Display="Dynamic"></asp:RequiredFieldValidator>
                            </InsertItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Path PreView" HeaderStyle-BackColor="#F5F5F5">
                            <ItemTemplate>
                                <asp:Literal ID="litPathPreview" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PathPreView")%>'></asp:Literal>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtPathPreView" runat="server" Text='<%# Bind("PathPreView") %>' Width="90%" ></asp:TextBox>
                                <asp:RequiredFieldValidator 
                                ID="rfvPath" 
                                runat="server"
                                ControlToValidate="txtPathPreView" 
                                ErrorMessage="Campo [Path PreView] es requerido!" 
                                CssClass="validator" 
                                Text="*" 
                                Display="Dynamic"></asp:RequiredFieldValidator>
                            </EditItemTemplate>
                            <InsertItemTemplate>
                               <asp:TextBox ID="txtPathPreView" runat="server" Text='<%# Bind("PathPreView") %>' Width="90%"></asp:TextBox>
                                <asp:RequiredFieldValidator 
                                ID="rfvPath" 
                                runat="server"
                                ControlToValidate="txtPathPreView" 
                                ErrorMessage="Campo [Path PreView] es requerido!" 
                                CssClass="validator" 
                                Text="*" 
                                Display="Dynamic"></asp:RequiredFieldValidator>
                            </InsertItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Titulo Sección" HeaderStyle-BackColor="#F5F5F5">
                            <ItemTemplate>
                                <asp:Literal ID="litTitulo" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Titulo")%>'></asp:Literal>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtTitulo" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Titulo")%>' Width="90%"></asp:TextBox>
                                <asp:RequiredFieldValidator 
                                ID="rfvTitulo" 
                                runat="server"
                                ControlToValidate="txtTitulo" 
                                ErrorMessage="Campo [Título] es requerido!" 
                                CssClass="validator" 
                                Text="*" 
                                Display="Dynamic"></asp:RequiredFieldValidator>
                            </EditItemTemplate>
                            <InsertItemTemplate>
                                <asp:TextBox ID="txtTitulo" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Titulo")%>' Width="90%"></asp:TextBox>
                                <asp:RequiredFieldValidator 
                                ID="rfvTitulo" 
                                runat="server"
                                ControlToValidate="txtTitulo" 
                                ErrorMessage="Campo [Título] es requerido!" 
                                CssClass="validator" 
                                Text="*" 
                                Display="Dynamic"></asp:RequiredFieldValidator>
                            </InsertItemTemplate>
                        </asp:TemplateField>

                         <asp:TemplateField HeaderText="Marcador de Posición" HeaderStyle-BackColor="#F5F5F5">
                            <ItemTemplate>
                                <asp:Literal ID="litPlaceholder" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Placeholder")%>'></asp:Literal>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtPlaceHolder" runat="server" Text='<%# Bind("Placeholder") %>'></asp:TextBox>
                                <asp:RequiredFieldValidator 
                                ID="rfvPlaceholder" 
                                runat="server"
                                ControlToValidate="txtPlaceHolder" 
                                ErrorMessage="Campo [Marcador de Psición] es requerido!" 
                                CssClass="validator" 
                                Text="*" 
                                Display="Dynamic"></asp:RequiredFieldValidator>
                            </EditItemTemplate>
                            <InsertItemTemplate>
                               <asp:TextBox ID="txtPlaceHolder" runat="server" Text='<%# Bind("Placeholder") %>'></asp:TextBox>
                                <asp:RequiredFieldValidator 
                                ID="rfvPlaceholder" 
                                runat="server"
                                ControlToValidate="txtPlaceHolder" 
                                ErrorMessage="Campo [Marcador de Psición] es requerido!" 
                                CssClass="validator" 
                                Text="*" 
                                Display="Dynamic"></asp:RequiredFieldValidator>
                            </InsertItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Ver Título de Sección" HeaderStyle-BackColor="#F5F5F5">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkMostrarTitulo" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "MostrarTitulo")%>'/>
                            </ItemTemplate>
                            <EditItemTemplate>
                                  <asp:CheckBox ID="chkValidaMostrarTitulo" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "MostrarTitulo")%>'/>
                            </EditItemTemplate>
                            <InsertItemTemplate>
                                <asp:CheckBox ID="chkValidaMostrarTitulo" runat="server" Checked="false"/>
                            </InsertItemTemplate>
                        </asp:TemplateField>                   

                       
                        <asp:TemplateField HeaderText="Posición" HeaderStyle-BackColor="#F5F5F5">
                            <ItemTemplate>
                                <asp:Literal ID="litPosicion" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "position")%>'></asp:Literal>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <ig:WebNumericEditor ID="txtPosicion" runat="server" HorizontalAlign="Left" Width="20%" Text='<%# DataBinder.Eval(Container.DataItem, "position")%>'></ig:WebNumericEditor>
                                <asp:RequiredFieldValidator 
                                ID="rfvtxtSecuenciaEdit" 
                                runat="server"
                                ControlToValidate="txtPosicion" 
                                ErrorMessage="Campo [Posición] es requerido!" 
                                CssClass="validator" 
                                Text="*" 
                                Display="Dynamic"></asp:RequiredFieldValidator>
                            </EditItemTemplate>
                            <InsertItemTemplate>
                                <ig:WebNumericEditor ID="txtPosicion" runat="server" HorizontalAlign="Left" Width="20%" Text='<%# DataBinder.Eval(Container.DataItem, "position")%>'></ig:WebNumericEditor>
                            </InsertItemTemplate>
                        </asp:TemplateField>

                         <asp:TemplateField HeaderText="Activar Sección" HeaderStyle-BackColor="#F5F5F5">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkActivar" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "IsActive")%>' Enabled="false"/>
                            </ItemTemplate>
                            <EditItemTemplate>
                                  <asp:CheckBox ID="chkActivarEdit" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "IsActive")%>'/>
                            </EditItemTemplate>
                            <InsertItemTemplate>
                                <asp:CheckBox ID="chkActivarEdit" runat="server" Checked="false"/>
                            </InsertItemTemplate>
                        </asp:TemplateField> 

                    </Fields>
                    <FooterStyle BorderColor="#EEEEEE" BorderStyle="Solid" BorderWidth="1px" />
                    <HeaderStyle CssClass="HeaderdetailView" BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" />
                    <RowStyle BorderColor="#EEEEEE" BorderStyle="Solid" BorderWidth="1px" CssClass="RowDetailView"/>
                    <EditRowStyle CssClass="RowDetailView" />
                    <AlternatingRowStyle CssClass="RowAlternateGridStyle" />
                  </asp:DetailsView>
               </div>

               <div style="margin-top:3px;">
                      <asp:UpdatePanel ID="upControles" runat="server">
                        <ContentTemplate>
                            <table width="100%" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td style=" width:97%" align="left">
                                         <asp:Button ID="btnGuardar" runat="server" Text="Guardar" OnClick="BtnGuardarClick" Visible="false"  />
                                         <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" OnClick="BtnEliminarClick" Visible="false"  CausesValidation="false"/>
                                    </td>
                                    <td style="width:15px;"></td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                
                  </div>
                
                
               <div style=" padding-top:3px;">
                        <asp:ValidationSummary 
                        ID="ValidationSummary1" 
                        runat="server" 
                        ShowModelStateErrors="true" 
                        CssClass="validator"
                        HeaderText="Verifique los siguientes errores:"/>
                  </div>
                      
            </td>
        </tr>
    </table>

    </ContentTemplate>
</asp:UpdatePanel>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Footer" runat="server">
</asp:Content>
