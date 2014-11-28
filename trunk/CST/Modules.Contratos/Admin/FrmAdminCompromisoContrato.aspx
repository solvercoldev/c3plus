<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FrmAdminCompromisoContrato.aspx.cs" Inherits="Modules.Contratos.Admin.FrmAdminCompromisoContrato" %>

<%@ Register    Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register    Assembly="Infragistics4.Web.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
                Namespace="Infragistics.Web.UI.EditorControls" TagPrefix="ig" %>  

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
            <script type="text/javascript" language="javascript">
                Sys.Application.add_load(RebindScripts);
            </script>
                <div style="padding:3px; text-align:right; margin-top:-35px; height:30px;">
                    <asp:Button ID="btnBack" runat="server" Text="Regresar" OnClick="BtnBack_Click" />                    
                    <asp:Button ID="btnSave" runat="server" Text="Guardar" OnClick="BtnSave_Click" Visible="false" />   
                </div>                                

                <asp:ValidationSummary ID="vsGeneral" runat="server" DisplayMode="BulletList" ShowMessageBox="false" CssClass="validator" ShowSummary="true" ValidationGroup="vgGeneral"/>

                <table width="100%" class="tblSecciones" style="padding-top:10px">
                    <tr>
                        <td style="width: 100px;"></td>
                        <td class="Separador" style="width: 10px;"></td>
                        <td style="width: 400px;"></td>
                        <td class="Separador" style="width: 200px;"></td>
                    </tr>                   
                    <tr style="height: 20px;">
                        <td style="text-align:left; vertical-align:top;" class="Line" colspan="3" >
                            
                            <table width="100%">
                                <tr>
                                    <th style="width:8%">
                                        Responsable:
                                    </th>
                                    <td style="width:10%">
                                        <asp:Label ID="lblResponsable" runat="server" />                                        
                                    </td>
                                    <th style="width:8%">
                                        Vence:
                                    </th>
                                    <td style="width:8%">
                                        <asp:Label ID="lblFechaCompromiso" runat="server" ForeColor="Red" />
                                    </td>
                                    <th style="width:10%">
                                        (<asp:Label ID="lblTipoCompromiso" runat="server" ForeColor="Red" />)
                                    </th>
                                    <th style="width:10%">
                                        (Importancia: <asp:Label ID="lblImportancia" runat="server" ForeColor="Red" />)
                                    </th>
                                </tr>
                            </table>

                        </td>

                        <td rowspan="3" style="text-align:right">
                            <div style="padding:3px; text-align:right; width:150px;"  id="divActionButtons" runat="server" >
                                <asp:Button ID="btnEdit" runat="server" Text="Editar" OnClick="BtnEdit_Click" Width="100px" />
                                <asp:Button ID="btnAnular" runat="server" Text="Anular" Width="100px" OnClick="BtnAddNovedad_Click" CommandArgument="Anular" />
                                <asp:Button ID="btnMarcarOk" runat="server" Text="Marcar OK" Width="100px" OnClick="BtnAddNovedad_Click" CommandArgument="Confirmar" />
                                <asp:Button ID="btnReprogramar" runat="server" Text="Reprogramar" Width="100px" OnClick="BtnAddNovedad_Click" CommandArgument="Reprogramar" />
                                <asp:Button ID="btnReasignar" runat="server" Text="Re-Asignar" Width="100px" OnClick="BtnAddNovedad_Click" CommandArgument="ReAsignar" />
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <th style="text-align:left; vertical-align:top">
                            Descripción :
                        </th>

                        <td class="Separador"></td>

                        <td class="Line" style="vertical-align:top;" >
                            <asp:Label ID="lblDescripcion" runat="server" />
                            <asp:TextBox ID="txtDescripcion" runat="server" TextMode="MultiLine" Visible="false" Rows="4" MaxLength="512" Width="90%" />
                        </td>

                    </tr>
                    <tr>
                        <th style="text-align:left; vertical-align:top">
                            Asociado a :
                        </th>

                        <td class="Separador"></td>

                        <td class="Line" style="vertical-align:top;" >
                            <asp:Label ID="lblTipoFase" runat="server" />
                            - <asp:Label ID="lblFase" runat="server" />
                            - <asp:Label ID="lblBCP" runat="server" />
                        </td>
                    </tr>                               
                </table>

                <table width="100%" class="tblSecciones" style="margin-top:10px" id="tblEntregablesANH" runat="server" visible="false">
                    <tr>
                        <td style="width: 100px;"></td>
                        <td class="Separador" style="width: 10px;"></td>
                        <td style="width: 400px;"></td>
                        <td class="Separador" style="width: 200px;"></td>
                    </tr>
                    <tr>
                        <td colspan="4" style="padding-bottom:5px">
                            <div style="display:block; padding:5px;" class="TituloVentana">
                                <asp:Literal ID="litTitleEntregables" runat="server" Text="Entregables ANH" />
                            </div>  
                        </td>
                    </tr>
                    <tr>
                        <th style="text-align:left; vertical-align:top">
                            * Entregables :
                        </th>

                        <td class="Separador"></td>
                            
                        <td class="Line" >
                            <asp:ListBox ID="lstEntregablesANH" runat="server" Width="100%" Height="120px" SelectionMode="Single" />
                        </td>

                        <td class="Separador"></td>
                    </tr> 
                </table>

                <table width="100%" class="tblSecciones" style="margin-top:20px" id="tblPagosObligaciones" runat="server" visible="false">
                    <tr>
                        <td style="width: 100px;"></td>
                        <td class="Separador" style="width: 10px;"></td>
                        <td style="width: 400px;"></td>
                        <td class="Separador" style="width: 200px;"></td>
                    </tr>  
                    <tr>
                        <td colspan="4" style="padding-bottom:5px">
                            <div style="display:block; padding:5px;" class="TituloVentana">
                                <asp:Literal ID="litTitlePagosObligaciones" runat="server" Text="Pagos Obligaciones" />
                            </div>  
                        </td>
                    </tr>
                    <tr>
                        <th style="text-align:left; vertical-align:top">
                            Tipo Pago :
                        </th>

                        <td class="Separador"></td>
                            
                        <td class="Line" >
                            <asp:Label ID="lblTipoPago" runat="server" />
                            <asp:DropDownList ID="ddlTipoPago" runat="server" Width="450px"  class="chzn-select" Visible="false" />
                        </td>

                        <td class="Separador"></td>
                    </tr> 
                    <tr>
                        <th style="text-align:left; vertical-align:top">
                            Entidad :
                        </th>

                        <td class="Separador"></td>
                            
                        <td class="Line" >
                            <asp:Label ID="lblEntidad" runat="server" />
                            <asp:DropDownList ID="ddlEntidad" runat="server" Width="450px"  class="chzn-select" Visible="false" />
                        </td>

                        <td class="Separador"></td>
                    </tr> 
                    <tr>
                        <th style="text-align:left; vertical-align:top">
                            Valor Cobertura :
                        </th>

                        <td class="Separador"></td>

                        <td class="Line" >
                            <asp:Label ID="lblValorCobertura" runat="server" />
                            <ig:WebNumericEditor    Id="txtValorCoberturaPago" runat="server"  Visible="false"
                                                    Nullable="false" MinValue="0" Width="250px" HorizontalAlign="Left" />
                        </td>

                        <td class="Separador"></td>
                    </tr> 
                    <tr>
                        <th style="text-align:left; vertical-align:top">
                            Numero Documento :
                        </th>

                        <td class="Separador"></td>
                            
                        <td class="Line" >
                            <asp:Label ID="lblNumeroDocumento" runat="server" />
                            <asp:TextBox ID="txtPagoNumeroDocumento" runat="server" Width="450px" MaxLength="128" Visible="false" />
                        </td>

                        <td class="Separador"></td>
                    </tr> 
                    <tr>
                        <th style="text-align:left; vertical-align:top">
                            Valor :
                        </th>

                        <td class="Separador"></td>

                        <td class="Line" >
                            <asp:Label ID="lblValor" runat="server" />
                            <ig:WebNumericEditor    Id="txtValorPago" runat="server" Visible="false"
                                                    Nullable="false" MinValue="0" Width="250px" HorizontalAlign="Left" />
                            
                            <asp:DropDownList ID="ddlMoneda" runat="server" Visible="false" />
                        </td>

                        <td class="Separador"></td>
                    </tr>       

                </table>

        </ContentTemplate>
        <Triggers>
        </Triggers>
    </asp:UpdatePanel>

    <asp:UpdatePanel ID="upModal" runat="server">
        <ContentTemplate> 
            <script type="text/javascript" language="javascript">
                Sys.Application.add_load(RebindScripts);
            </script>
            <asp:Panel ID="pnlAdminNovedad"  runat="server" CssClass="popup_Container" Width="600px" Height="250px" style="display:none;">  

                <div class="popup_Titlebar" id="PopupHeader">
                    <div class="TitlebarLeft">
                        Gestionar Compromiso
                    </div>
                    <div class="TitlebarRight" id="divCloseAdminNovedad">
                    </div>
                </div>

                <div style="padding:3px; text-align:right;">
                    <asp:Button ID="btnCancelarNovedad" runat="server" Text="Regresar"  />
                    <asp:Button ID="btnSaveNovedad" runat="server" Text="Guardar" OnClick="BtnSaveNovedad_Click" />
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
                        <tr id="trFechaProgramacion" runat="server" >
                            <th style="text-align:left; vertical-align:top">
                                Fecha Reprogramación :
                            </th>

                            <td class="Separador"></td>

                            <td class="Line">
                                <asp:TextBox ID="txtFechaReProgramacion" OnKeyDown="return false;" runat="server" />
                                <ajaxToolkit:CalendarExtender 
                                        ID="cexTxtFechaNovedad" 
                                        runat="server"  
                                        TargetControlID="txtFechaReProgramacion" 
                                        PopupButtonID="txtFechaReProgramacion"
                                        Format="dd/MM/yyyy"/>
                            </td>

                            <td class="Separador"></td>
                        </tr>
                        <tr id="trResponsable" runat="server" visible="false" >
                            <th style="text-align:left; vertical-align:top">
                                Nuevo Responsable :
                            </th>

                            <td class="Separador"></td>

                            <td class="Line">
                                <asp:DropDownList ID="ddlResponsable" runat="server" Width="450px"  class="chzn-select" />
                            </td>

                            <td class="Separador"></td>
                        </tr>
                        <tr>
                            <th style="text-align:left; vertical-align:top">
                                Observaciones :
                            </th>

                            <td class="Separador"></td>

                            <td class="Line">
                                <asp:TextBox ID="txtObservacionesNovedad" runat="server" TextMode="MultiLine" Rows="4" Width="98%" />
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