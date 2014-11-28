<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WuCAdminNovedadesFasesContrato.ascx.cs" Inherits="Modules.Contratos.UserControls.WuCAdminNovedadesFasesContrato" %>

<%@ Register    Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register    Assembly="Infragistics4.Web.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
                Namespace="Infragistics.Web.UI.EditorControls" TagPrefix="ig" %>   

<table width="100%">
    <%--<tr class="SectionMainTitle">
        <td >
            LISTADO DE NOVEDADES DE FASES
        </td>
    </tr>--%>
    <tr>
        <td>
            <asp:DropDownList ID="ddlFases" runat="server" Width="250px"  class="chzn-select" OnSelectedIndexChanged="DdlFases_IndexChanged" AutoPostBack="true" />
        </td>
    </tr>
    <tr>
        <td style="vertical-align:top" >
            <table class="tbl" width="100%">
                 <tr>
                    <th style="width:3%;">                        
                    </th>
                    <th style="width:10%;text-align:left;vertical-align:top">
                        Fase
                    </th>
                    <th style="width:11%;text-align:left;vertical-align:top">
                        Tipo
                    </th>
                    <th style="width:30%;text-align:left;vertical-align:top">
                        Descripción
                    </th>
                    <th style="width:23%; text-align:left;;vertical-align:top">
                        Responsable
                    </th>
                    <th style="width:12%;text-align:left; vertical-align:top">
                        Fecha Novedad
                    </th>
                    <th style="width:2%;text-align:left; vertical-align:top">
                        
                    </th>
                </tr>
            </table>
            <asp:Panel id="pnlContainerNovedadesFasesList" style="width:100%;float: left; " Height="150px" ScrollBars="Vertical" runat="server" >
                <table class="tbl" width="100%">
                    <asp:repeater id="rptNovedadesList" runat="server" OnItemDataBound="RptNovedadesList_ItemDataBound" >                    
                        <ItemTemplate>
                            <tr class="Normal">
                                <td style="text-align:center;width:3%; ">
                                </td>                                    
                                <td style="text-align:left;width:10%;vertical-align:top">
                                    <asp:Label ID="lblFase" runat="server" />
                                </td>
                                <td style="text-align:left;width:10%;vertical-align:top">
                                    <asp:Label ID="lblTipo" runat="server" />
                                </td>
                                <td style="text-align:left;width:30%;vertical-align:top">
                                    <asp:Label ID="lblDescripcion" runat="server" />
                                </td>
                                <td style="text-align:left;width:23%;vertical-align:top">
                                    <asp:Label ID="lblResponsable" runat="server" />
                                </td>
                                <td style="text-align:left;width:12%;vertical-align:top">
                                    <asp:Label ID="lblFechaNovedad" runat="server" />
                                </td>             
                            </tr>
                        </ItemTemplate>
                        <AlternatingItemTemplate>
                            <tr class="Alternative">
                                <td style="text-align:center;width:3%; ">
                                </td>                                    
                                <td style="text-align:left;width:10%;vertical-align:top">
                                    <asp:Label ID="lblFase" runat="server" />
                                </td>
                                <td style="text-align:left;width:10%;vertical-align:top">
                                    <asp:Label ID="lblTipo" runat="server" />
                                </td>
                                <td style="text-align:left;width:30%;vertical-align:top">
                                    <asp:Label ID="lblDescripcion" runat="server" />
                                </td>
                                <td style="text-align:left;width:23%;vertical-align:top">
                                    <asp:Label ID="lblResponsable" runat="server" />
                                </td>
                                <td style="text-align:left;width:12%;vertical-align:top">
                                    <asp:Label ID="lblFechaNovedad" runat="server" />
                                </td>  
                            </tr>
                        </AlternatingItemTemplate>
                    </asp:repeater>
                </table>
            </asp:Panel>           
        </td>
    </tr>    
</table>