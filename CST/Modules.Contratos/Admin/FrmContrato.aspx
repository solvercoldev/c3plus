<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FrmContrato.aspx.cs" Inherits="Modules.Contratos.Admin.FrmContrato" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register src="../UserControls/WuCLogContratos.ascx" tagname="WuCLogContratos" tagprefix="ucLogContratos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

<script language="javascript" type="text/javascript">
    var divModal = 'DivModal';

    function ShowSplashModalLoading() {
        var adiv = $get(divModal);
        adiv.style.visibility = 'visible';
    }
</script>

<script type="text/javascript">

    function RebindScripts() {
        $(".chzn-select").chosen({ allow_single_deselect: true });

        $(".chzn-select-deselect").chosen({ allow_single_deselect: true });
    }       
 
</script>
    
    <div id="DivModal">
        <div id="VentanaMensaje">
            <div id="Msg">
                <img id="Img1"  src="~/Resources/images/Barloading.gif" runat="server" alt="" />
            </div>
        </div>
    </div>

    <asp:UpdatePanel ID="upgeneral" runat="server">
        <ContentTemplate>  

    <div style="padding:3px; text-align:right; margin-top:-35px; height:30px;">
        <asp:Button ID="btnManageFases" runat="server" Text="Trabajar Fases"  OnClick="BtnManageFases_Click" />
    </div>

    <table width="100%" cellpadding="0" cellspacing="0" style="padding-top:10px">
        <tr>
            <td style="width: 45%;"></td>
            <td style="width: 10px;"></td>
            <td style="width: 50%;"></td>
        </tr>
        <tr valign="top" align="left">
            <td valign="top" class="BorderTable" >
                <table width="100%" >
                    <tr>
                        <td style="width: 30%;"></td>
                        <td class="Separador"></td>
                        <td style="width: 70%;"></td>
                    </tr>
                    <tr>
                        <td class="SeccionesH3">
                            Contrato:
                        </td>
                        <td style="text-align:center" >
                            <asp:ImageButton 
                                    ID="ImgSearch" 
                                    BorderWidth="0" 
                                    BorderStyle="None" 
                                    CausesValidation="false" 
                                    runat="server" 
                                    ToolTip="Ver información de localización del contrato."
                                    ImageUrl="~/Resources/Images/location.png" 
                                    OnClick="BtnGoToLocation_Click"
                                />
                        </td>
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
                            <asp:Label ID="lblFechaFirma" runat="server"  />
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
                            <asp:Label ID="lblPeriodo" runat="server"  />
                        </td>
                    </tr>                    
                </table>
            </td>
            <td></td>
            <td>
                <table class="tbl" width="100%">
                    <asp:repeater id="rptFases" runat="server" OnItemDataBound="RptFases_ItemDataBound" >
                        <HeaderTemplate>
                            <tr>                    
                                <th style="width:6%;">                        
                                </th>
                                <th style="width:25%;">
                                    Periodo
                                </th>
                                <th style="width:8%;">
                                    Fase
                                </th>
                                <th style="width:15%;">
                                    Duración
                                </th>
                                <th style="width:20%;">
                                    Fecha Inicio
                                </th>
                                <th style="width:20%;">
                                    Fecha Fin
                                </th>
                                <th style="width:2%;">
                                </th>
                            </tr>
                        </HeaderTemplate>   
                        <ItemTemplate>
                            <tr class="Normal" id="trFase" runat="server">
                                <td style="text-align:center;">
                                    <asp:HiddenField ID="hddIdFase" runat="server" />
                                    <asp:Image ID="imgFase" runat="server" Width="12px" Height="12px" ImageUrl="~/Resources/Images/check.png" />
                                </td>
                                <td style="text-align:center">
                                    <asp:Label ID="lblPeriodo" runat="server" />
                                </td>
                                <td style="text-align:center">                                    
                                    <asp:Label ID="lblFase" runat="server" />
                                </td>
                                <td style="text-align:center">
                                    <asp:Label ID="lblDuracion" runat="server" />
                                    -Meses
                                </td>                                
                                <td style="text-align:center">
                                    <asp:Label ID="lblFechaInicio" runat="server" />
                                </td>
                                <td style="text-align:center">
                                    <asp:Label ID="lblFechaFin" runat="server" />
                                </td>                                
                                <td >
                                </td>                    
                            </tr>
                        </ItemTemplate>
                        <AlternatingItemTemplate>
                            <tr class="Alternative" id="trFase" runat="server" >
                                <td style="text-align:center;">
                                    <asp:HiddenField ID="hddIdFase" runat="server" />
                                    <asp:Image ID="imgFase" runat="server" Width="12px" Height="12px" ImageUrl="~/Resources/Images/check.png" />
                                </td>
                                <td style="text-align:center">
                                    <asp:Label ID="lblPeriodo" runat="server" />
                                </td>
                                <td style="text-align:center">                                    
                                    <asp:Label ID="lblFase" runat="server" />
                                </td>
                                <td style="text-align:center">
                                    <asp:Label ID="lblDuracion" runat="server" />
                                    -Meses
                                </td>                                
                                <td style="text-align:center">
                                    <asp:Label ID="lblFechaInicio" runat="server" />
                                </td>
                                <td style="text-align:center">
                                    <asp:Label ID="lblFechaFin" runat="server" />
                                </td>                                
                                <td >
                                </td>                    
                            </tr>
                        </AlternatingItemTemplate>
                    </asp:repeater>
                </table>
            </td>
        </tr>
        
    </table>

        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:UpdatePanel ID="upSecciones" runat="server">
        <ContentTemplate>
                <table width="100%" cellpadding="0" cellspacing="0" style="padding-top:10px; padding-bottom: 5px;"> 
                    <tr>
                        <td style="border-bottom:1px solid #6ec138;">                                   
                            <asp:Menu ID="mnuSecciones" runat="server" Orientation="Horizontal" BackColor="#F5F5F5" Width="100%" OnMenuItemClick="MnuItemClick" > 
                                <StaticSelectedStyle ForeColor="#FFFFFF" BackColor="#6ec138" BorderStyle="None" Font-Bold="True" CssClass="center"  />
                                <StaticMenuItemStyle ForeColor="#7D7D7C"  BorderStyle="solid" BorderWidth="0" Font-Size="12px" Font-Bold="true" CssClass="center" 
                                    ItemSpacing="5px" HorizontalPadding="5px" Width="140" VerticalPadding="0px" BackColor="#F5F5F5" Font-Names="Arial"
                                    Height="25px" />    
                            </asp:Menu>                                          
                        </td>
                    </tr>
                </table>    
        </ContentTemplate>
    </asp:UpdatePanel>    

    <div style=" margin-top:2px;">    
        <asp:UpdatePanel ID="upContenidoContrato" runat="server" ChildrenAsTriggers="true">
            <ContentTemplate>  
                <script type="text/javascript" language="javascript">
                    Sys.Application.add_load(RebindScripts);
                </script>  
                <div style="width:100%;">
                        <asp:PlaceHolder ID="phlContent"  runat="server"></asp:PlaceHolder>
                </div>  
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>  

</asp:Content>



<asp:Content ID="footerContent" runat="server" ContentPlaceHolderID="Footer">
    <!-- Aca va el Log -->
    <ucLogContratos:WuCLogContratos ID="wucLogContrato" runat="server" />
</asp:Content>
