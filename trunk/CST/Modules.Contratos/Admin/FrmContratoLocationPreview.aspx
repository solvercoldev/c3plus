<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FrmContratoLocationPreview.aspx.cs" Inherits="Modules.Contratos.Admin.FrmContratoLocationPreview" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

<script type="text/javascript" src="http://maps.googleapis.com/maps/api/js?sensor=false"></script>

<script type="text/javascript">


    function InitGMapers() {
        var markers =   [
                    <asp:Repeater ID="rptMarkers" runat="server">
                        <ItemTemplate>
                                    {
                                    "title": '<%# Eval("Name") %>',
                                    "lat": '<%# Eval("Latitude") %>',
                                    "lng": '<%# Eval("Longitude") %>',
                                    "description": '<%# Eval("Description") %>'
                                }
                        </ItemTemplate>
                        <SeparatorTemplate>
                            ,
                        </SeparatorTemplate>
                    </asp:Repeater>
                ];
        var mapOptions = {
            center: new google.maps.LatLng(markers[0].lat, markers[0].lng),
            zoom: 8,
            mapTypeId: google.maps.MapTypeId.ROADMAP
        };
        var infoWindow = new google.maps.InfoWindow();
        var map = new google.maps.Map(document.getElementById("dvMap"), mapOptions);
        for (i = 0; i < markers.length; i++) {
            var data = markers[i]
            var myLatlng = new google.maps.LatLng(data.lat, data.lng);
            var marker = new google.maps.Marker({
                position: myLatlng,
                map: map,
                title: data.title
            });
            (function (marker, data) {
                google.maps.event.addListener(marker, "click", function (e) {
                    infoWindow.setContent(data.description);
                    infoWindow.open(map, marker);
                });
            })(marker, data);
        }
    }
</script>

    
    <asp:UpdatePanel ID="upgeneral" runat="server">
        <ContentTemplate>    
        <script type="text/javascript" language="javascript">
            Sys.Application.add_load(InitGMapers);
        </script> 
    
    <div style="padding:3px; text-align:right; margin-top:-35px; height:30px;">
        <asp:Button ID="btnBackToContrato" runat="server" Text="Regresar"  OnClick="BtnBackToContrato_Click" />
    </div>   

    <table width="100%" cellpadding="0" cellspacing="0" style="padding-top:10px">
        <tr>
            <td style="width:45%">
            </td>
            <td style="width:5%">
            </td>
            <td style="width:45%">
            </td>
        </tr>
        <tr valign="top" align="left">
            <td colspan="3">
                <table width="100%" >
                    <tr>
                        <td style="width: 15%;"></td>
                        <td style="width: 10px;"></td>
                        <td style="width: 80%;"></td>
                    </tr>
                    <tr>
                        <td class="SeccionesH3">
                            Contrato:
                        </td>
                        <td ></td>
                        <td class="SeccionesH4">
                            <asp:Label ID="lblContrato" runat="server" />
                        </td>
                    </tr>                    
                    <tr>
                        <td class="SeccionesH3">
                            Empresa:
                        </td>
                        <td ></td>
                        <td class="SeccionesH4">
                            <asp:Label ID="lblEmpresa" runat="server" />
                        </td>
                    </tr>  
                    <tr>
                        <td class="SeccionesH3">
                            Bloque:
                        </td>
                        <td ></td>
                        <td class="SeccionesH4">
                            <asp:Label ID="lblBloque" runat="server" />
                        </td>
                    </tr>    
                    <tr>
                        <td class="SeccionesH3">
                            Tipo Contrato:
                        </td>
                        <td ></td>
                        <td class="SeccionesH4">
                            <asp:Label ID="lblTipoContrato" runat="server" />
                        </td>
                    </tr>                 
                    <tr>
                        <td class="SeccionesH3">
                            Fecha Firma:
                        </td>
                        <td ></td>
                        <td class="SeccionesH4">
                            <asp:Label ID="lblFechaFirma" runat="server" />
                        </td>
                    </tr>    
                    <tr>
                        <td class="SeccionesH3">
                            Fecha Efectiva:
                        </td>
                        <td  class="Separador"></td>
                        <td class="SeccionesH4">
                            <asp:Label ID="lblFechaEfectiva" runat="server" />
                        </td>
                    </tr>                
                    <tr>
                        <td class="SeccionesH3">
                            Fecha Fin:
                        </td>
                        <td ></td>
                        <td class="SeccionesH4">
                            <asp:Label ID="lblPeriodo" runat="server" />
                        </td>
                    </tr>                     
                </table>
            </td>
        </tr>
        <tr valign="top">
            <td colspan="3">
                <br />
            </td>
        </tr>
        <tr valign="top">
            <td>
                <asp:Image ID="imgImagenContrato" runat="server" Width="95%" Height="250px" />  
            </td>
            <td>
            </td>
            <td>
                <div id="dvMap" style="width: 95%; height: 250px" >
                </div>
            </td>
        </tr>
        
    </table>

        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>



<asp:Content ID="footerContent" runat="server" ContentPlaceHolderID="Footer">
    <!-- Aca va el Log -->
</asp:Content>