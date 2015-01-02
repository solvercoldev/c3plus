<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FrmTotalRadicados.aspx.cs" Inherits="Modules.Contratos.Views.FrmTotalRadicados" %>

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
                
                <table id='tableFilter' class="tbl" width="2000px">
                    <thead>
                            <tr>
                                <th style="width:20px;" filter='false'>                        
                                </th>
                                <th style="width:400px;text-align:left;vertical-align:top" filter-type='ddl'>
                                    Contrato
                                </th>
                                <th style="width:350px;text-align:left; vertical-align:top" filter-type='ddl'>
                                    Bloque
                                </th>
                                <th style="width:100px;text-align:center; vertical-align:top" filter-type='ddl'>
                                    Tipo
                                </th>
                                <th style="width:350px; text-align:left;vertical-align:top" filter-type='ddl'>
                                    #Radicado
                                </th>   
                                <th style="width:300px; text-align:left;vertical-align:top" filter-type='ddl'>
                                    Respuesta.Pendiente
                                </th>
                                <th style="width:350px; text-align:left;vertical-align:top" filter-type='ddl'>
                                    Estado
                                </th>
                                <th style="width:150px; text-align:left;vertical-align:top">
                                    Fecha Respuesta
                                </th>
                                <th style="width:250px; text-align:left;vertical-align:top" filter-type='ddl'>
                                    Responsable
                                </th>
                                <th style="width:150px; text-align:left;vertical-align:top" filter-type='ddl'>
                                    Fecha.Radicado
                                </th>
                                <th style="width:350px; text-align:left;vertical-align:top" filter-type='ddl'>
                                    Asunto
                                </th>
                                <th style="width:350px; text-align:left;vertical-align:top" filter-type='ddl'>
                                    Resumen
                                </th>
                                <th style="width:150px; text-align:left;vertical-align:top">
                                    Enviado Por
                                </th>
                                <th style="width:150px; text-align:left;vertical-align:top" filter-type='ddl'>
                                    Enviado Por Externo
                                </th>
                                <th style="width:150px; text-align:left;vertical-align:top" filter-type='ddl'>
                                    Dirigido A
                                </th>
                                <th style="width:150px; text-align:left;vertical-align:top" filter-type='ddl'>
                                    Dirigido A Externo
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
                                                    ID="imgSelectRadicado" 
                                                    runat="server"
                                                    CausesValidation="false"
                                                    BorderStyle="None"
                                                    ImageUrl="~/Resources/Images/select.png"
                                                    OnClick="BtnSelectRadicado_Click" ToolTip="Click para ver mas información del radicado." />
                                </td>
                                <td style="text-align:left;vertical-align:top">                                    
                                    <asp:Label ID="lblContrato" runat="server" />
                                </td>
                                <td style="text-align:left; vertical-align:top">
                                    <asp:Label ID="lblBloque" runat="server" />
                                </td>
                                <td style="text-align:center; vertical-align:top">
                                    <asp:Label ID="lblTipoRadicado" runat="server" />
                                </td>
                                <td style="text-align:left; vertical-align:top">
                                    <asp:Label ID="lblNumRadicado" runat="server" />
                                </td>
                                <td style="text-align:left;vertical-align:top">
                                    <asp:Label ID="lblRespuestaPendiente" runat="server" />
                                </td>
                                <td style="text-align:left;vertical-align:top">
                                    <asp:Label ID="lblEstado" runat="server" />
                                </td>
                                <td style="text-align:left;vertical-align:top">
                                    <asp:Label ID="lblFechaRespuesta" runat="server" />
                                </td>
                                <td style="text-align:left;vertical-align:top">
                                    <asp:Label ID="lblResponsable" runat="server" />
                                </td>
                                <td style="text-align:left;vertical-align:top">
                                    <asp:Label ID="lblFechaReciboSalida" runat="server" />
                                </td>   
                                <td style="text-align:left;vertical-align:top">
                                    <asp:Label ID="lblAsunto" runat="server" />
                                </td>
                                <td style="text-align:left;vertical-align:top">
                                    <asp:Label ID="lblResumen" runat="server" />
                                </td>
                                <td style="text-align:left;vertical-align:top">
                                    <asp:Label ID="lblEnviadoPor" runat="server" />
                                </td>
                                <td style="text-align:left;vertical-align:top">
                                    <asp:Label ID="lblEnviadoPorExterno" runat="server" />
                                </td>
                                <td style="text-align:left;vertical-align:top">
                                    <asp:Label ID="lblDirigidoA" runat="server" />
                                </td>
                                <td style="text-align:left;vertical-align:top">
                                    <asp:Label ID="lbldirigidoAExterno" runat="server" />
                                </td>                                
                                <td style="text-align:left;vertical-align:top">                        
                                </td>                                                                                                                       
                            </tr>
                            
                        </ItemTemplate>
                        <AlternatingItemTemplate>
                            <tr class="Alternative">
                                 <td style="">                        
                                    <asp:ImageButton 
                                                    ID="imgSelectRadicado" 
                                                    runat="server"
                                                    CausesValidation="false"
                                                    BorderStyle="None"
                                                    ImageUrl="~/Resources/Images/select.png"
                                                    OnClick="BtnSelectRadicado_Click" ToolTip="Click para ver mas información del radicado." />
                                </td>
                                <td style="text-align:left;vertical-align:top">                                    
                                    <asp:Label ID="lblContrato" runat="server" />
                                </td>
                                <td style="text-align:left; vertical-align:top">
                                    <asp:Label ID="lblBloque" runat="server" />
                                </td>
                                <td style="text-align:center; vertical-align:top">
                                    <asp:Label ID="lblTipoRadicado" runat="server" />
                                </td>
                                <td style="text-align:left; vertical-align:top">
                                    <asp:Label ID="lblNumRadicado" runat="server" />
                                </td>
                                <td style="text-align:left;vertical-align:top">
                                    <asp:Label ID="lblRespuestaPendiente" runat="server" />
                                </td>
                                <td style="text-align:left;vertical-align:top">
                                    <asp:Label ID="lblEstado" runat="server" />
                                </td>
                                <td style="text-align:left;vertical-align:top">
                                    <asp:Label ID="lblFechaRespuesta" runat="server" />
                                </td>
                                <td style="text-align:left;vertical-align:top">
                                    <asp:Label ID="lblResponsable" runat="server" />
                                </td>
                                <td style="text-align:left;vertical-align:top">
                                    <asp:Label ID="lblFechaReciboSalida" runat="server" />
                                </td>   
                                <td style="text-align:left;vertical-align:top">
                                    <asp:Label ID="lblAsunto" runat="server" />
                                </td>
                                <td style="text-align:left;vertical-align:top">
                                    <asp:Label ID="lblResumen" runat="server" />
                                </td>
                                <td style="text-align:left;vertical-align:top">
                                    <asp:Label ID="lblEnviadoPor" runat="server" />
                                </td>
                                <td style="text-align:left;vertical-align:top">
                                    <asp:Label ID="lblEnviadoPorExterno" runat="server" />
                                </td>
                                <td style="text-align:left;vertical-align:top">
                                    <asp:Label ID="lblDirigidoA" runat="server" />
                                </td>
                                <td style="text-align:left;vertical-align:top">
                                    <asp:Label ID="lbldirigidoAExterno" runat="server" />
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