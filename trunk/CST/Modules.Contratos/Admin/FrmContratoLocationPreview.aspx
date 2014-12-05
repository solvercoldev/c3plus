<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FrmContratoLocationPreview.aspx.cs" Inherits="Modules.Contratos.Admin.FrmContratoLocationPreview" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register    Assembly="Infragistics4.Web.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
                Namespace="Infragistics.Web.UI.EditorControls" TagPrefix="ig" %>  

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
            zoom: 4,
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

<script type="text/javascript">

    function RebindScripts() {
        $(".chzn-select").chosen({ allow_single_deselect: true });

        $(".chzn-select-deselect").chosen({ allow_single_deselect: true });
    }       
 
</script>

<script language="javascript" type="text/javascript">
    var divModal = 'DivModal';

    function ShowSplashModalLoading() {
        var adiv = $get(divModal);
        adiv.style.visibility = 'visible';
    }
</script>

    
    <asp:UpdatePanel ID="upgeneral" runat="server">
        <ContentTemplate>    
        <script type="text/javascript" language="javascript">
            Sys.Application.add_load(InitGMapers);
            Sys.Application.add_load(RebindScripts);
        </script> 
    
    <div id="DivModal">
        <div id="VentanaMensaje">
            <div id="Msg">
                <img id="Img1"  src="~/Resources/images/Barloading.gif" runat="server" alt="" />
            </div>
        </div>
    </div>


    <div style="padding:3px; text-align:right; margin-top:-35px; height:30px;">
        <asp:Button ID="btnBackToContrato" runat="server" Text="Regresar"  OnClick="BtnBackToContrato_Click" />
        <asp:Button ID="btnEditar" runat="server" Text="Editar"  OnClick="BtnEditar_Click" OnClientClick="return ShowSplashModalLoading();" />
        <asp:Button ID="btnSave" runat="server" Text="Guardar"  OnClick="BtnSave_Click" OnClientClick="return ShowSplashModalLoading();" />
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
                            <asp:TextBox ID="txtNumeroContrato" runat="server" Width="450px" CssClass="TextUppercase" />
                        </td>
                    </tr>                    
                    <tr>
                        <td class="SeccionesH3">
                            Empresa:
                        </td>
                        <td ></td>
                        <td class="SeccionesH4">
                            <asp:Label ID="lblEmpresa" runat="server" />
                            <asp:DropDownList ID="ddlEmpresa" runat="server" Width="450px"  class="chzn-select" />
                        </td>
                    </tr>  
                    <tr>
                        <td class="SeccionesH3">
                            Bloque:
                        </td>
                        <td ></td>
                        <td class="SeccionesH4">
                            <asp:Label ID="lblBloque" runat="server" />
                            <asp:DropDownList ID="ddlBloque" runat="server" Width="450px"  class="chzn-select" />
                        </td>
                    </tr>    
                    <tr>
                        <td class="SeccionesH3">
                            Tipo Contrato:
                        </td>
                        <td ></td>
                        <td class="SeccionesH4">
                            <asp:Label ID="lblTipoContrato" runat="server" />
                            <asp:DropDownList ID="ddlTipoContrato" runat="server" Width="450px"  class="chzn-select" />
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
                    <tr id="trUbicacion" runat="server" >
                        <td  class="SeccionesH3" style="text-align:left; vertical-align:top">
                            Ubicación Contrato :
                        </td>

                        <td class="Separador"></td>

                        <td class="SeccionesH4" >
                            <table width="100%">
                                 <tr>
                                    <td  class="SeccionesH3" style="width:10%;">
                                        Latitud:
                                    </td>
                                    <td>
                                        <ig:WebNumericEditor    Id="txtLatitud" runat="server"  MaxDecimalPlaces="6"
                                                                Nullable="false" Width="380px" HorizontalAlign="Left" />
                                    </td>
                                </tr>
                                <tr>
                                    <td  class="SeccionesH3" style="width:10%;">
                                        Longitud:
                                    </td>
                                    <td>
                                        <ig:WebNumericEditor    Id="txtLongitud" runat="server"  MaxDecimalPlaces="6"
                                                                Nullable="false" Width="380px" HorizontalAlign="Left" />
                                    </td>
                                </tr>                               
                            </table>
                        </td>

                        <td class="Separador"></td>
                    </tr>  
                    <tr id="trArchivoSitio" runat="server">
                        <td  class="SeccionesH3" style="text-align:left; vertical-align:top">
                            Imagen Sitio :
                        </td>

                        <td class="Separador"></td>

                        <td class="SeccionesH4" >                         
                            <asp:FileUpload ID="fuImagenContrato" runat="server" Width="450px" />
                        </td>

                        <td class="Separador"></td>
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
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSave" />
        </Triggers>
    </asp:UpdatePanel>

</asp:Content>



<asp:Content ID="footerContent" runat="server" ContentPlaceHolderID="Footer">
    <!-- Aca va el Log -->
</asp:Content>