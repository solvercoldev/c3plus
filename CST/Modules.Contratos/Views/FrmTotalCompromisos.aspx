<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FrmTotalCompromisos.aspx.cs" Inherits="Modules.Contratos.Views.FrmTotalCompromisos" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <script type='text/javascript'>

        function RebindTableScripts() {

            var grid = $('#tableFilter');
            var options = {
                clearFiltersControls: [$('#cleanfilters')],
                filteringRows: function (filterStates) {
                    grid.addClass('filtering');
                },
                filteredRows: function (filterStates) {
                    grid.removeClass('filtering');
                }
            };

            grid.tableFilter(options); // No additional filters                  
        }

    </script>

    <asp:UpdatePanel ID="upgeneral" runat="server">
        <ContentTemplate>    
            <script type="text/javascript" language="javascript">
                Sys.Application.add_load(RebindTableScripts);            
            </script>  
            <br />
            <asp:Panel ID="pnlContainer" runat="server" ScrollBars="Both" Width="930px" Height="400px">

                <a id='cleanfilters' href='#' style="padding-bottom:5px;">Limpiar Filtros</a>
                
                <table id='tableFilter' class="tbl" width="1500px">
                    <thead>
                            <tr>
                                <th style="width:20px;" filter='false'>                        
                                </th>
                                <th style="width:350px;text-align:left;vertical-align:top" filter-type='ddl'>
                                    Contrato
                                </th>
                                <th style="width:90px;text-align:left;vertical-align:top" filter-type='ddl'>
                                    Fase
                                </th>
                                <th style="width:350px;text-align:left; vertical-align:top" filter-type='ddl'>
                                    Bloque
                                </th>
                                <th style="width:400px;text-align:left; vertical-align:top">
                                    Descripción
                                </th>
                                <th style="width:350px; text-align:left;vertical-align:top" filter-type='ddl'>
                                    Campo
                                </th>   
                                <th style="width:350px; text-align:left;vertical-align:top" filter-type='ddl'>
                                    Pozo
                                </th>
                                <th style="width:350px; text-align:left;vertical-align:top" filter-type='ddl'>
                                    Responsable
                                </th>
                                <th style="width:100px; text-align:left;vertical-align:top" filter-type='ddl'>
                                    Estado
                                </th>
                                <th style="width:100px; text-align:left;vertical-align:top">
                                    F.Vencimiento
                                </th>
                                <th style="width:120px; text-align:left;vertical-align:top" filter-type='ddl'>
                                    Tipo
                                </th>
                                <th style="width:100px; text-align:left;vertical-align:top" filter-type='ddl'>
                                    Importancia
                                </th>
                                <th style="width:150px; text-align:left;vertical-align:top" filter-type='ddl'>
                                    Asociado a
                                </th>
                                <th style="width:300px; text-align:left;vertical-align:top" filter-type='ddl'>
                                    Entidad
                                </th>
                                <th style="width:350px; text-align:left;vertical-align:top" filter-type='ddl'>
                                    Tipo Pago
                                </th>
                                <th style="width:120px; text-align:left;vertical-align:top">
                                    Valor
                                </th>
                                <th style="width:80px; text-align:left;vertical-align:top" filter-type='ddl'>
                                    Moneda
                                </th>
                                <th style="width:5px; text-align:left;vertical-align:top" filter='false'>                        
                                </th>
                            </tr>
                        </thead>
                    <asp:repeater id="rptList" runat="server" OnItemDataBound="RptList_ItemDataBound" >                        
                        <ItemTemplate>
                            
                            <tr class="Normal">
                                <td style="">                        
                                    <asp:ImageButton 
                                                    ID="imgSelectCompromiso" 
                                                    runat="server"
                                                    CausesValidation="false"
                                                    BorderStyle="None"
                                                    ImageUrl="~/Resources/Images/select.png"
                                                    OnClick="BtnSelectCompromiso_Click" ToolTip="Click para ver mas información del compromiso." />
                                </td>
                                <td style="text-align:left;vertical-align:top">                                    
                                    <asp:Label ID="lblContrato" runat="server" />
                                </td>
                                <td style="text-align:left;vertical-align:top">
                                    <asp:Label ID="lblFase" runat="server" />
                                </td>
                                <td style="text-align:left; vertical-align:top">
                                    <asp:Label ID="lblBloque" runat="server" />
                                </td>
                                <td style="text-align:left; vertical-align:top">
                                    <asp:Label ID="lblCompromiso" runat="server" />
                                </td>
                                <td style="text-align:left;vertical-align:top">
                                    <asp:Label ID="lblCampo" runat="server" />
                                </td>   
                                <td style="text-align:left;vertical-align:top">
                                    <asp:Label ID="lblPozo" runat="server" />
                                </td>
                                <td style="text-align:left;vertical-align:top">
                                    <asp:Label ID="blResponsable" runat="server" />
                                </td>
                                <td style="text-align:left;vertical-align:top">
                                    <asp:Label ID="lblEstado" runat="server" />
                                </td>
                                <td style="text-align:left;vertical-align:top">
                                    <asp:Label ID="lblFechaVencimiento" runat="server" />
                                </td>
                                <td style="text-align:left;vertical-align:top">
                                    <asp:Label ID="lblTipoCompromiso" runat="server" />
                                </td>
                                <td style="text-align:left;vertical-align:top">
                                    <asp:Label ID="lblImportancia" runat="server" />
                                </td>
                                <td style="text-align:left;vertical-align:top">
                                    <asp:Label ID="lblTipoAsociacionCompromiso" runat="server" />
                                </td>
                                <td style="text-align:left;vertical-align:top">
                                    <asp:Label ID="lblEntidad" runat="server" />
                                </td>
                                <td style="text-align:left;vertical-align:top">
                                    <asp:Label ID="lblTipoPago" runat="server" />
                                </td>
                                <td style="text-align:left;vertical-align:top">
                                    <asp:Label ID="lblValor" runat="server" />
                                </td>
                                <td style="text-align:right;vertical-align:top">
                                    <asp:Label ID="lblMoneda" runat="server" />
                                </td>
                                <td style="text-align:left;vertical-align:top">                        
                                </td>                                                                                                                       
                            </tr>
                            
                        </ItemTemplate>
                        <AlternatingItemTemplate>
                            <tr class="Alternative">
                                <td style="">   
                                    <asp:ImageButton 
                                                    ID="imgSelectCompromiso" 
                                                    runat="server"
                                                    CausesValidation="false"
                                                    BorderStyle="None"
                                                    ImageUrl="~/Resources/Images/select.png"
                                                    OnClick="BtnSelectCompromiso_Click" ToolTip="Click para ver mas información del compromiso." />                     
                                </td>
                                <td style="text-align:left;vertical-align:top">
                                    <asp:Label ID="lblContrato" runat="server" />
                                </td>
                                <td style="text-align:left;vertical-align:top">
                                    <asp:Label ID="lblFase" runat="server" />
                                </td>
                                <td style="text-align:left; vertical-align:top">
                                    <asp:Label ID="lblBloque" runat="server" />
                                </td>
                                <td style="text-align:left; vertical-align:top">
                                    <asp:Label ID="lblCompromiso" runat="server" />
                                </td>
                                <td style="text-align:left;vertical-align:top">
                                    <asp:Label ID="lblCampo" runat="server" />
                                </td>   
                                <td style="text-align:left;vertical-align:top">
                                    <asp:Label ID="lblPozo" runat="server" />
                                </td>
                                <td style="text-align:left;vertical-align:top">
                                    <asp:Label ID="blResponsable" runat="server" />
                                </td>
                                <td style="text-align:left;vertical-align:top">
                                    <asp:Label ID="lblEstado" runat="server" />
                                </td>
                                <td style="text-align:left;vertical-align:top">
                                    <asp:Label ID="lblFechaVencimiento" runat="server" />
                                </td>
                                <td style="text-align:left;vertical-align:top">
                                    <asp:Label ID="lblTipoCompromiso" runat="server" />
                                </td>
                                <td style="text-align:left;vertical-align:top">
                                    <asp:Label ID="lblImportancia" runat="server" />
                                </td>
                                <td style="text-align:left;vertical-align:top">
                                    <asp:Label ID="lblTipoAsociacionCompromiso" runat="server" />
                                </td>
                                <td style="text-align:left;vertical-align:top">
                                    <asp:Label ID="lblEntidad" runat="server" />
                                </td>
                                <td style="text-align:left;vertical-align:top">
                                    <asp:Label ID="lblTipoPago" runat="server" />
                                </td>
                                <td style="text-align:right;vertical-align:top">
                                    <asp:Label ID="lblValor" runat="server" />
                                </td>
                                <td style="text-align:left;vertical-align:top">
                                    <asp:Label ID="lblMoneda" runat="server" />
                                </td>
                                <td style="text-align:left;vertical-align:top">                        
                                </td>                                                                    
                            </tr>
                        </AlternatingItemTemplate>
                    </asp:repeater>
                </table>
            </asp:Panel>           
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>