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
                        <th style="width:18%;">
                            Fecha Inicio
                        </th>
                        <th style="width:18%;">
                            Fecha Fin
                        </th>
                        <th style="width:8%;">
                        </th>
                    </tr>
                </table>
                <asp:Panel ID="pnlContainerFases" runat="server" Width="100%" Height="170px" ScrollBars="Auto">
                    <table class="tbl" width="100%">
                        <asp:repeater id="rptFases" runat="server" OnItemDataBound="RptFases_ItemDataBound" >
                            <ItemTemplate>
                                <tr class="Normal" id="trFase" runat="server">
                                    <td style="text-align:center; width:5%; ">
                                        <asp:HiddenField ID="hddIdFase" runat="server" />
                                        <asp:Image ID="imgFase" runat="server" Width="12px" Height="12px" ImageUrl="~/Resources/Images/check.png" />
                                    </td>
                                    <td style="text-align:center;width:25%;">
                                        <asp:Label ID="lblPeriodo" runat="server" />
                                    </td>
                                    <td style="text-align:center;width:8%;">                                    
                                        <asp:Label ID="lblFase" runat="server" />
                                    </td>
                                    <td style="text-align:center;width:15%;">
                                        <asp:Label ID="lblDuracion" runat="server" />
                                        -Meses
                                    </td>                                
                                    <td style="text-align:center;width:18%;">
                                        <asp:Label ID="lblFechaInicio" runat="server" />
                                    </td>
                                    <td style="text-align:center;width:18%;">
                                        <asp:Label ID="lblFechaFin" runat="server" />
                                    </td>                                
                                    <td style="width:4%;">
                                        <asp:Image ID="imgeUnify" runat="server" Width="12px" Height="12px" ImageUrl="~/Resources/Images/unification.png" Visible="false" />
                                    </td>  
                                    <td style="width:1%;">
                                    </td>                          
                                </tr>
                            </ItemTemplate>
                            <AlternatingItemTemplate>
                                <tr class="Alternative" id="trFase" runat="server">
                                    <td style="text-align:center; width:5%; ">
                                        <asp:HiddenField ID="hddIdFase" runat="server" />
                                        <asp:Image ID="imgFase" runat="server" Width="12px" Height="12px" ImageUrl="~/Resources/Images/check.png" />
                                    </td>
                                    <td style="text-align:center;width:25%;">
                                        <asp:Label ID="lblPeriodo" runat="server" />
                                    </td>
                                    <td style="text-align:center;width:8%;">                                    
                                        <asp:Label ID="lblFase" runat="server" />
                                    </td>
                                    <td style="text-align:center;width:15%;">
                                        <asp:Label ID="lblDuracion" runat="server" />
                                        -Meses
                                    </td>                                
                                    <td style="text-align:center;width:18%;">
                                        <asp:Label ID="lblFechaInicio" runat="server" />
                                    </td>
                                    <td style="text-align:center;width:18%;">
                                        <asp:Label ID="lblFechaFin" runat="server" />
                                    </td>                                
                                    <td style="width:4%;">
                                        <asp:Image ID="imgeUnify" runat="server" Width="12px" Height="12px" ImageUrl="~/Resources/Images/unification.png" Visible="false" />
                                    </td>  
                                    <td style="width:1%;">
                                    </td>                  
                                </tr>
                            </AlternatingItemTemplate>
                        </asp:repeater>
                    </table>
                </asp:Panel>                
            </td>
            
            <td></td>
            <td style="text-align:center;vertical-align:top;" >
                <asp:Panel ID="pnlContainerActionButtons" runat="server" Width="100%">
                    <asp:Button ID="btnExtender" runat="server" Text="Extender Fase" Width="125px" CommandArgument="Extensión" OnClick="BtnAddNovedad_Click" CausesValidation="false" />
                    <br />
                    <asp:Button ID="btnProrrogar" runat="server" Text="Prorrogar Fase" Width="125px" CommandArgument="Prorroga" OnClick="BtnAddNovedad_Click" CausesValidation="false" />                
                    <br />
                    <asp:Button ID="btnCorregirFechaFinal" runat="server" Text="Corregir Fecha Final" Width="125px" CommandArgument="CorrecciónFechaFin" OnClick="BtnAddNovedad_Click" CausesValidation="false" />                                
                    <br />
                    <asp:Button ID="btnUnificar" runat="server" Text="Unificar Fase" Width="125px" CommandArgument="Unificación" OnClick="BtnAddNovedad_Click" CausesValidation="false" />
                    <br />
                    <asp:Button ID="btnAgregarFase" runat="server" Text="Agregar Fase" Width="125px"  OnClick="BtnAddFase_Click" CausesValidation="false" />
                </asp:Panel>                
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
                <asp:Button ID="btnCancelarNovedad" runat="server" Text="Regresar" CausesValidation="false"  />
                <asp:Button ID="btnSaveNovedad" runat="server" Text="Confirmar Novedad" OnClick="BtnSaveNovedad_Click" ValidationGroup="vgAddNovedadFase" />
            </div>

            <asp:ValidationSummary ID="vsAddNovedadFase" runat="server" DisplayMode="BulletList" ShowMessageBox="false" CssClass="validator" ShowSummary="true" ValidationGroup="vgAddNovedadFase"/>

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

                        <td class="Separador">

                        </td>
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
                            Fecha Final :
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

                        <td class="Separador">
                            <asp:RequiredFieldValidator ID="reqObservaciones" runat="server" ForeColor="Red" ControlToValidate="txtDescripcion" ValidationGroup="vgAddNovedadFase" ErrorMessage="Es necesario ingresar las observaciones">*</asp:RequiredFieldValidator>
                        </td>
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

<asp:UpdatePanel ID="upModalAddFase" runat="server">
    <ContentTemplate> 
        <asp:Panel ID="pnlAdminAddFase"  runat="server" CssClass="popup_Container" Width="550px" Height="300px" style="display:none;">  

            <div class="popup_Titlebar" id="PopupHeaderAddFase">
                <div class="TitlebarLeft">
                    Adicionar Nueva Fase
                </div>
                <div class="TitlebarRight" id="divCloseAdminAddFase">
                </div>
            </div>

            <div style="padding:3px; text-align:right;">
                <asp:Button ID="btnCancelarFase" runat="server" Text="Regresar" CausesValidation="false"  />
                <asp:Button ID="btnSaveFase" runat="server" Text="Confirmar Fase" OnClick="BtnSaveFase_Click" ValidationGroup="vgAddFase" />
            </div>

            <asp:ValidationSummary ID="vsAddFase" runat="server" DisplayMode="BulletList" ShowMessageBox="false" CssClass="validator" ShowSummary="true" ValidationGroup="vgAddFase"/>

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
                            Período :
                        </th>

                        <td class="Separador"></td>

                        <td class="Line">
                            <asp:RadioButtonList ID="rdbTipoNuevaFase" runat="server" RepeatLayout="Table" RepeatDirection="Vertical" RepeatColumns="3" OnSelectedIndexChanged="RdbTipoNuevaFase_IndexChanged" AutoPostBack="true" />
                        </td>

                        <td class="Separador">

                        </td>
                    </tr>
                    <tr id="trFaseEvaluacionNuevaFase" runat="server" visible="false">
                        <th style="text-align:left; vertical-align:top">
                            Fase Evaluación :
                        </th>

                        <td class="Separador"></td>

                        <td class="Line">
                            <asp:DropDownList ID="ddlFaseEvaluacion" runat="server" Width="90%" OnSelectedIndexChanged="DdlFasesEvaluacion_IndexChanged" AutoPostBack="true" />
                        </td>

                        <td class="Separador"></td>
                    </tr>
                    <tr >
                        <th style="text-align:left; vertical-align:top">
                            Fecha Inicial :
                        </th>

                        <td class="Separador"></td>

                        <td class="Line">
                            <asp:TextBox ID="txtFechaInicioNuevaFase" OnKeyDown="return false;" runat="server" OnTextChanged="TxtFechaInicioNuevaFase_TextChanged" AutoPostBack="true" />
                            <ajaxToolkit:CalendarExtender 
                                    ID="cextFechaInicioNuevaFase" 
                                    runat="server"  
                                    TargetControlID="txtFechaInicioNuevaFase" 
                                    PopupButtonID="txtFechaInicioNuevaFase"
                                    Format="dd/MM/yyyy"/>
                            <asp:Label ID="lblFechaInicialNuevaFase" runat="server" Visible="false" />
                        </td>                        

                        <td class="Separador"></td>
                    </tr>
                    <tr >
                        <th style="text-align:left; vertical-align:top">
                            Fecha Final :
                        </th>

                        <td class="Separador"></td>

                        <td class="Line">
                            <asp:TextBox ID="txtFechaFinNuevaFase" OnKeyDown="return false;" runat="server" />
                            <ajaxToolkit:CalendarExtender 
                                    ID="cextFechaFinNuevaFase" 
                                    runat="server"  
                                    TargetControlID="txtFechaFinNuevaFase" 
                                    PopupButtonID="txtFechaFinNuevaFase"
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
                            <asp:TextBox ID="txtObservacionesNuevaFase" runat="server" TextMode="MultiLine" Rows="4" Width="98%" />
                        </td>

                        <td class="Separador">
                            <asp:RequiredFieldValidator ID="reqObservacionesNuevaFase" runat="server" ForeColor="Red" ControlToValidate="txtObservacionesNuevaFase" ValidationGroup="vgAddFase" ErrorMessage="Es necesario ingresar las observaciones">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                </table>
            </div>
        </asp:Panel>
    
        <asp:Button ID="btnPopUpAdminFaseTargetControl" runat="server" style="display:none; "/>    

        <ajaxToolkit:ModalPopupExtender 
        ID="mpAdminNuevaFase" 
        runat="server" 
        TargetControlID="btnPopUpAdminFaseTargetControl" 
        PopupControlID="pnlAdminAddFase" 
        BackgroundCssClass="ModalPopupBG" DropShadow="true" 
        cancelcontrolid="divCloseAdminAddFase"> 
        </ajaxToolkit:ModalPopupExtender>   
    </ContentTemplate>
    <Triggers>
    </Triggers>
</asp:UpdatePanel>

</asp:Content>



<asp:Content ID="footerContent" runat="server" ContentPlaceHolderID="Footer">
    <!-- Aca va el Log -->
</asp:Content>
