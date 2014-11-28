<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WuCAdminCompromisosContrato.ascx.cs" Inherits="Modules.Contratos.UserControls.WuCAdminCompromisosContrato" %>

<%@ Register    Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<table width="100%">
    <%--<tr class="SectionMainTitle">
        <td  >
            LISTADO DE COMPROMISOS DEL CONTRATO
        </td>
    </tr>--%>
    <tr>
        <td style="width:50%">
            <asp:DropDownList ID="ddlFase" runat="server" Width="250px"  class="chzn-select" OnSelectedIndexChanged="DdlFases_IndexChanged" AutoPostBack="true" />
        </td>
        <td style="width:50%">
            <div style="text-align:right;">
                <asp:Button ID="btnAddCompromiso" runat="server" Text="Nuevo Compromiso" OnClick="BtnAddCompromiso_Click" />
            </div>
        </td>
    </tr>
    <tr>
        <td style="vertical-align:top" colspan="2" >
            <table class="tbl" width="100%">
                <tr>
                    <th style="width:3%;">                        
                    </th>
                    <th style="width:10%;text-align:left;vertical-align:top">
                        Fase
                    </th>
                    <th style="width:30%;text-align:left;vertical-align:top">
                        Descripción
                    </th>
                    <th style="width:12%;text-align:left; vertical-align:top">
                        Fecha Cumplimiento
                    </th>
                    <th style="width:10%;text-align:left; vertical-align:top">
                        Estado
                    </th>
                    <th style="width:23%; text-align:left;;vertical-align:top">
                        Responsable
                    </th>   
                    <th style="width:2%; text-align:left;;vertical-align:top">                        
                    </th>                            
                </tr>
            </table>
            <asp:Panel id="pnlContainerCompromisosList" style="width:100%;float: left; " Height="150px" ScrollBars="Vertical" runat="server" >
                <table class="tbl" width="100%">
                    <asp:repeater id="rptCompromisosList" runat="server" OnItemDataBound="RptCompromisosList_ItemDataBound" >   
                        <ItemTemplate>
                            <tr class="Normal">
                                <td style="text-align:center;width:3%;vertical-align:top ">
                                    <asp:ImageButton 
                                                    ID="imgSelectCompromiso" 
                                                    runat="server"
                                                    CausesValidation="false"
                                                    BorderStyle="None"
                                                    ImageUrl="~/Resources/Images/select.png"
                                                    OnClick="BtnSelectCompromiso_Click" ToolTip="Click para ver mas información del compromiso." />
                                </td>                                    
                                <td style="text-align:left;width:10%;vertical-align:top">
                                    <asp:Label ID="lblFase" runat="server" />
                                </td>
                                <td style="text-align:left;width:30%;vertical-align:top">
                                    <asp:Label ID="lblDescripcion" runat="server" />
                                </td>
                                <td style="text-align:left;width:12%;vertical-align:top">
                                    <asp:Label ID="lblFechaCumplimiento" runat="server" />
                                </td>  
                                <td style="text-align:left;width:10%;vertical-align:top">
                                    <asp:Label ID="lblEstado" runat="server" />
                                </td>  
                                <td style="text-align:left;width:23%;vertical-align:top">
                                    <asp:Label ID="lblResponsable" runat="server" />
                                </td>                                       
                            </tr>
                        </ItemTemplate>
                        <AlternatingItemTemplate>
                            <tr class="Alternative">
                                <td style="text-align:center;width:3%;vertical-align:top ">
                                    <asp:ImageButton 
                                                    ID="imgSelectCompromiso" 
                                                    runat="server"
                                                    CausesValidation="false"
                                                    BorderStyle="None"
                                                    ImageUrl="~/Resources/Images/select.png"
                                                    OnClick="BtnSelectCompromiso_Click" ToolTip="Click para ver mas información del compromiso." />
                                </td>                                    
                                <td style="text-align:left;width:10%;vertical-align:top">
                                    <asp:Label ID="lblFase" runat="server" />
                                </td>
                                <td style="text-align:left;width:30%;vertical-align:top">
                                    <asp:Label ID="lblDescripcion" runat="server" />
                                </td>
                                <td style="text-align:left;width:12%;vertical-align:top">
                                    <asp:Label ID="lblFechaCumplimiento" runat="server" />
                                </td>  
                                <td style="text-align:left;width:10%;vertical-align:top">
                                    <asp:Label ID="lblEstado" runat="server" />
                                </td>  
                                <td style="text-align:left;width:23%;vertical-align:top">
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