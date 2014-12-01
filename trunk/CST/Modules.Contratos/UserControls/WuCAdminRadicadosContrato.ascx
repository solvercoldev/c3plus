<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WuCAdminRadicadosContrato.ascx.cs" Inherits="Modules.Contratos.UserControls.WuCAdminRadicadosContrato" %>

<%@ Register    Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<table width="100%">
    <tr>
        <td style="width:80%">
            <table class="tbl" width="100%">
                <tr>
                    <th style="width:20%">Tipo</th>
                    <th style="width:35%">Estado</th>
                    <th style="width:45%">Bucador</th>
                </tr>
                <tr>
                    <td>
                        <asp:DropDownList ID="ddlTipoRadicado" runat="server" Width="150px"  class="chzn-select" OnSelectedIndexChanged="FilterEvent_Changed" AutoPostBack="true" >
                            <asp:ListItem Text="Todos" Value="" Selected="True" />
                            <asp:ListItem Text="Entrada" Value="RE" />
                            <asp:ListItem Text="Salida" Value="RS"  />
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlEstadoRadicado" runat="server" Width="200px"  class="chzn-select" OnSelectedIndexChanged="FilterEvent_Changed" AutoPostBack="true" >
                            <asp:ListItem Text="Todos" Value="" Selected="True" />
                            <asp:ListItem Text="Radicado" Value="Radicado" />
                            <asp:ListItem Text="Pendiente Respuesta" Value="Pendiente Respuesta"  />
                            <asp:ListItem Text="Respondido" Value="Respondido"  />
                            <asp:ListItem Text="Anulado" Value="Anulado"  />
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:TextBox ID="txtSearchFilter" runat="server" Width="90%" OnTextChanged="FilterEvent_Changed" AutoPostBack="true" />
                    </td>
                </tr>
            </table>            
        </td>
        <td style="width:20%">
            <div style="text-align:right;">
                <asp:Button ID="btnAddRadicado" runat="server" Text="Nuevo Radicado" OnClick="BtnAddRadicado_Click" />
            </div>
        </td>
    </tr>
    <tr>
        <td style="vertical-align:top" colspan="2" >
            <table class="tbl" width="100%">
                <tr>
                    <th style="width:3%;">                        
                    </th>
                    <th style="width:5%;text-align:left;vertical-align:top">
                        Tipo
                    </th>
                    <th style="width:20%;text-align:left;vertical-align:top">
                        Asunto
                    </th>
                    <th style="width:10%;text-align:center; vertical-align:top">
                        No
                    </th>
                    <th style="width:20%;text-align:left; vertical-align:top">
                        Enviado Por
                    </th>
                    <th style="width:10%;text-align:left; vertical-align:top">
                        F.Cumplimiento
                    </th>
                    <th style="width:10%;text-align:left; vertical-align:top">
                        Estado
                    </th>
                    <th style="width:20%; text-align:left;;vertical-align:top">
                        Responsable
                    </th>   
                    <th style="width:2%; text-align:left;;vertical-align:top">                        
                    </th>                            
                </tr>
            </table>
            <asp:Panel id="pnlContainerRadicadosList" style="width:100%;float: left; " Height="150px" ScrollBars="Vertical" runat="server" >
                <table class="tbl" width="100%">
                    <asp:repeater id="rptRadicadosList" runat="server" OnItemDataBound="RptRadicadosList_ItemDataBound" >   
                        <ItemTemplate>
                            <tr class="Normal">
                                <td style="text-align:center;width:3%;vertical-align:top ">
                                    <asp:ImageButton 
                                                    ID="imgSelectRadicado" 
                                                    runat="server"
                                                    CausesValidation="false"
                                                    BorderStyle="None"
                                                    ImageUrl="~/Resources/Images/select.png"
                                                    OnClick="BtnSelectRadicado_Click" ToolTip="Click para ver mas información del radicado." />
                                </td>                                    
                                <td style="text-align:left;width:5%;vertical-align:top">
                                    <asp:Label ID="lblTipo" runat="server" />
                                </td>
                                <td style="text-align:left;width:20%;vertical-align:top">
                                    <asp:Label ID="lblAsunto" runat="server" />
                                </td>
                                <td style="text-align:left;width:10%;vertical-align:top">
                                    <asp:Label ID="lblNumeroRadicado" runat="server" />
                                </td>  
                                <td style="text-align:left;width:20%;vertical-align:top">
                                    <asp:Label ID="lblEnviadoPor" runat="server" />
                                </td>  
                                <td style="text-align:left;width:10%;vertical-align:top">
                                    <asp:Label ID="lblFechaCumplimiento" runat="server" />
                                </td>         
                                <td style="text-align:left;width:10%;vertical-align:top">
                                    <asp:Label ID="lblEstado" runat="server" />
                                </td>                       
                                <td style="text-align:left;width:20%;vertical-align:top">
                                    <asp:Label ID="lblResponsable" runat="server" />
                                </td>                                       
                            </tr>
                        </ItemTemplate>
                        <AlternatingItemTemplate>
                            <tr class="Alternative">
                               <td style="text-align:center;width:3%;vertical-align:top ">
                                    <asp:ImageButton 
                                                    ID="imgSelectRadicado" 
                                                    runat="server"
                                                    CausesValidation="false"
                                                    BorderStyle="None"
                                                    ImageUrl="~/Resources/Images/select.png"
                                                    OnClick="BtnSelectRadicado_Click" ToolTip="Click para ver mas información del radicado." />
                                </td>                                    
                                <td style="text-align:left;width:5%;vertical-align:top">
                                    <asp:Label ID="lblTipo" runat="server" />
                                </td>
                                <td style="text-align:left;width:20%;vertical-align:top">
                                    <asp:Label ID="lblAsunto" runat="server" />
                                </td>
                                <td style="text-align:left;width:10%;vertical-align:top">
                                    <asp:Label ID="lblNumeroRadicado" runat="server" />
                                </td>  
                                <td style="text-align:left;width:20%;vertical-align:top">
                                    <asp:Label ID="lblEnviadoPor" runat="server" />
                                </td>  
                                <td style="text-align:left;width:10%;vertical-align:top">
                                    <asp:Label ID="lblFechaCumplimiento" runat="server" />
                                </td>         
                                <td style="text-align:left;width:10%;vertical-align:top">
                                    <asp:Label ID="lblEstado" runat="server" />
                                </td>                       
                                <td style="text-align:left;width:20%;vertical-align:top">
                                    <asp:Label ID="lblResponsable" runat="server" />
                                </td>    
                            </tr>
                        </AlternatingItemTemplate>
                    </asp:repeater>
                </table>
            </asp:Panel>            
        </td>
    </tr>    
</table>