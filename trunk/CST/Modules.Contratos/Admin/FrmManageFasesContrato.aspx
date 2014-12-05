<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FrmManageFasesContrato.aspx.cs" Inherits="Modules.Contratos.Admin.FrmManageFasesContrato" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

<script type="text/javascript" src="http://maps.googleapis.com/maps/api/js?sensor=false"></script>

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
        <asp:Button ID="btnBack" runat="server" Text="Regresar" OnClick="BtnBack_Click"  />
    </div>

    <table width="100%" cellpadding="0" cellspacing="0" style="padding-top:10px">
        <tr>
            <td class="Separador"></td>
            <td style="width: 70%;"></td>            
            <td class="Separador"></td>
            <td style="width: 30%;"></td>
            <td class="Separador"></td>
        </tr>
        <tr valign="top" align="left">
            <td></td>
            <td>
                <table class="tbl" width="100%">
                    <asp:repeater id="rptFases" runat="server" OnItemDataBound="RptFases_ItemDataBound" >
                        <HeaderTemplate>
                            <tr>                    
                                <th style="width:5%;">                        
                                </th>
                                <th style="width:25%;">
                                    Período
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
                                <th style="width:4%;">
                                </th>
                            </tr>
                        </HeaderTemplate>   
                        <ItemTemplate>
                            <tr class="Normal" id="trFase" runat="server">
                                 <td style="text-align:center; ">
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
                                    <asp:Image ID="imgeUnify" runat="server" Width="12px" Height="12px" ImageUrl="~/Resources/Images/unification.png" Visible="false" />
                                </td>                          
                            </tr>
                        </ItemTemplate>
                        <AlternatingItemTemplate>
                            <tr class="Alternative" id="trFase" runat="server">
                                 <td style="text-align:center; ">
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
                                    <asp:Image ID="imgeUnify" runat="server" Width="12px" Height="12px" ImageUrl="~/Resources/Images/unification.png" Visible="false" />
                                </td>                          
                            </tr>
                        </AlternatingItemTemplate>
                    </asp:repeater>
                </table>
            </td>
            
            <td></td>
            <td style="text-align:center;vertical-align:top;" >
                <asp:Button ID="btnExtender" runat="server" Text="Extender Fase" Width="125px" CommandArgument="Extensión" OnClick="BtnAddNovedad_Click" />
                <br />
                <asp:Button ID="btnProrrogar" runat="server" Text="Prorrogar Fase" Width="125px" CommandArgument="Prorroga" OnClick="BtnAddNovedad_Click" />                
                <br />
                <asp:Button ID="btnCorregirFechaFinal" runat="server" Text="Corregir Fecha Final" Width="125px" CommandArgument="CorrecciónFechaFin" OnClick="BtnAddNovedad_Click" />                
                <br />
                <asp:Button ID="btnAgregarFase" runat="server" Text="Agregar Fase" Width="125px" />
                <br />
                <asp:Button ID="btnUnificar" runat="server" Text="Unificar Fase" Width="125px" CommandArgument="Unificación" OnClick="BtnAddNovedad_Click" />
            </td>
            <td></td>
        </tr>
        
    </table>

        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:UpdatePanel ID="upSecciones" runat="server">
        <ContentTemplate>
                <table width="100%"  cellpadding="0" cellspacing="0" style="padding-top:10px; padding-bottom: 5px;"> 
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
        <asp:UpdatePanel ID="upContenidoFases" runat="server" ChildrenAsTriggers="true">
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

<asp:UpdatePanel ID="upModal" runat="server">
    <ContentTemplate> 
        <asp:Panel ID="pnlAdminNovedad"  runat="server" CssClass="popup_Container" Width="550px" Height="250px" style="display:none;">  

            <div class="popup_Titlebar" id="PopupHeader">
                <div class="TitlebarLeft">
                    Adicionar Novedad De Fase
                </div>
                <div class="TitlebarRight" id="divCloseAdminNovedad">
                </div>
            </div>

            <div style="padding:3px; text-align:right;">
                <asp:Button ID="btnCancelarNovedad" runat="server" Text="Regresar"  />
                <asp:Button ID="btnSaveNovedad" runat="server" Text="Confirmar Novedad" OnClick="BtnSaveNovedad_Click" />
            </div>

            <div class="popup_Body">                                                    
                <table width="100%" class="tblSecciones">
                    <tr>
                        <th style="width: 30%;">
                        </th>

                        <td style="width: 5px;"></td>

                        <td class="Line" style="width:60%;">
                        </td>

                        <td class="Separador"></td>
                    </tr>
                    <tr>
                        <th style="text-align:left; vertical-align:top">
                            Fase :
                        </th>

                        <td class="Separador"></td>

                        <td class="Line">
                            <asp:DropDownList ID="ddlFaseOperacion" runat="server" Width="90%" OnSelectedIndexChanged="DdlFases_IndexChanged" AutoPostBack="true" />
                        </td>

                        <td class="Separador"></td>
                    </tr>
                    <tr id="trFaseunificacion" runat="server" visible="false" >
                        <th style="text-align:left; vertical-align:top">
                            Fase Unificación :
                        </th>

                        <td class="Separador"></td>

                        <td class="Line">
                            <asp:Label ID="lblFaseUnificacion" runat="server" />
                        </td>

                        <td class="Separador"></td>
                    </tr>
                    <tr id="trPeridoUnificacion" runat="server" visible="false" >
                        <th style="text-align:left; vertical-align:top">
                            Periodo Unificación :
                        </th>

                        <td class="Separador"></td>

                        <td class="Line">
                            <asp:Label ID="lblPeriodoUnifiacion" runat="server" />
                        </td>

                        <td class="Separador"></td>
                    </tr>
                    <tr id="trFechaExtension" runat="server" >
                        <th style="text-align:left; vertical-align:top">
                            Fecha Final Extensión :
                        </th>

                        <td class="Separador"></td>

                        <td class="Line">
                            <asp:TextBox ID="txtFechaNovedad" OnKeyDown="return false;" runat="server" />
                            <ajaxToolkit:CalendarExtender 
                                    ID="cexTxtFechaNovedad" 
                                    runat="server"  
                                    TargetControlID="txtFechaNovedad" 
                                    PopupButtonID="txtFechaNovedad"
                                    Format="dd/MM/yyyy"/>
                        </td>

                        <td class="Separador"></td>
                    </tr>
                    <tr>
                        <th style="text-align:left; vertical-align:top">
                            Observaciones :
                        </th>

                        <td class="Separador"></td>

                        <td class="Line">
                            <asp:TextBox ID="txtDescripcion" runat="server" TextMode="MultiLine" Rows="4" Width="98%" />
                        </td>

                        <td class="Separador"></td>
                    </tr>
                </table>
            </div>
        </asp:Panel>
    
        <asp:Button ID="btnPopUpAdminNovedadTargetControl" runat="server" style="display:none; "/>    

        <ajaxToolkit:ModalPopupExtender 
        ID="mpeAdminNovedad" 
        runat="server" 
        TargetControlID="btnPopUpAdminNovedadTargetControl" 
        PopupControlID="pnlAdminNovedad" 
        BackgroundCssClass="ModalPopupBG" DropShadow="true" 
        cancelcontrolid="divCloseAdminNovedad"> 
        </ajaxToolkit:ModalPopupExtender>   
    </ContentTemplate>
    <Triggers>
    </Triggers>
</asp:UpdatePanel>

</asp:Content>



<asp:Content ID="footerContent" runat="server" ContentPlaceHolderID="Footer">
    <!-- Aca va el Log -->
</asp:Content>
