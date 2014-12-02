<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FrmAdminRadicadoContrato.aspx.cs" Inherits="Modules.Contratos.Admin.FrmAdminRadicadoContrato" %>

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
                    <tr>
                        <td style="text-align:left; vertical-align:top;" class="Line" colspan="3" >
                            Responsable:<asp:Label ID="lblResponsable" runat="server" /> &nbsp;-&nbsp;
                            Vence: <asp:Label ID="lblFechaRadicado" runat="server" ForeColor="Red" />
                        </td>
                        <td style="text-align:right; vertical-align:top;" class="Line">
                        </td>
                    </tr>             
                    <tr style="height: 20px; vertical-align:top">
                        <td colspan="3" style="vertical-align:top" >
                             <table width="100%">
                                <tr>
                                    <td style="width: 22%;"></td>
                                    <td class="Separador" style="width: 10px;"></td>
                                    <td ></td>
                                </tr>  
                                <tr>
                                    <th style="text-align:left; vertical-align:top">
                                        Contrato :
                                    </th>

                                    <td ></td>

                                    <td class="Line" style="vertical-align:top;" >
                                        <asp:Label ID="lblInfoContrato" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <th style="text-align:left; vertical-align:top">
                                        Fecha Radicado :
                                    </th>

                                    <td ></td>

                                    <td class="Line" style="vertical-align:top;" >
                                        <asp:Label ID="lblFechaCreacion" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <th style="text-align:left; vertical-align:top">
                                        Asunto :
                                    </th>

                                    <td ></td>

                                    <td class="Line" style="vertical-align:top;" >
                                        <asp:Label ID="lblAsunto" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <th style="text-align:left; vertical-align:top">
                                        Enviado Por :
                                    </th>

                                    <td ></td>

                                    <td class="Line" style="vertical-align:top;" >
                                        <asp:Label ID="lblEnviadoPor" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <th style="text-align:left; vertical-align:top">
                                        Dirigido A :
                                    </th>

                                    <td ></td>

                                    <td class="Line" style="vertical-align:top;" >
                                        <asp:Label ID="lblDirigidoA" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <th style="text-align:left; vertical-align:top">
                                        Descripción :
                                    </th>

                                    <td ></td>

                                    <td class="Line" style="vertical-align:top;" >
                                        <asp:Label ID="lblDescripcion" runat="server" />
                                    </td>
                                </tr>
                                <tr id="trREAsociado" runat="server" visible="false" >
                                    <th style="text-align:left; vertical-align:top">
                                        RE. al que Responde :
                                    </th>

                                    <td ></td>

                                    <td class="Line" style="vertical-align:top;" >
                                        <asp:Label ID="lblRadicadoAsociado" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <th style="text-align:left; vertical-align:top">
                                        Archivo Anexo :
                                    </th>

                                    <td ></td>

                                    <td class="Line" style="vertical-align:top;" >
                                        <asp:LinkButton ID="bntArchivoRadicado" runat="server" OnClick="BntArchivoRadicado_Click" />
                                    </td>
                                </tr>
                            </table>
                        </td>

                        <td rowspan="3" style="text-align:right; vertical-align:top;">
                            <div style="padding:3px; text-align:right; width:150px;"  id="divActionButtons" runat="server" >
                                <asp:Button ID="btnEdit" runat="server" Text="Editar" OnClick="BtnEdit_Click" Width="100px" />
                                <asp:Button ID="btnAnular" runat="server" Text="Anular" Width="100px" OnClick="BtnAddNovedad_Click" CommandArgument="Anular" />
                                <asp:Button ID="btnMarcarOk" runat="server" Text="Marcar OK" Width="100px" OnClick="BtnAddNovedad_Click" CommandArgument="Confirmar" />
                                <asp:Button ID="btnReprogramar" runat="server" Text="Reprogramar" Width="100px" OnClick="BtnAddNovedad_Click" CommandArgument="Reprogramar" />
                                <asp:Button ID="btnReasignar" runat="server" Text="Re-Asignar" Width="100px" OnClick="BtnAddNovedad_Click" CommandArgument="ReAsignar" />
                            </div>
                        </td>
                    </tr>                                             
                </table>
                
                <table width="100%" class="tblSecciones" style="margin-top:10px" id="tblRespuestaRadicado" runat="server" visible="false" >
                    <tr>
                        <td style="width: 100px;"></td>
                        <td class="Separador" style="width: 10px;"></td>
                        <td style="width: 400px;"></td>
                        <td class="Separador" style="width: 200px;"></td>
                    </tr> 
                    <tr>
                        <td colspan="4" style="padding-bottom:5px">
                            <div style="display:block; padding:5px;" class="TituloVentana">
                                <asp:Literal ID="litTitlePagosObligaciones" runat="server" Text="Respuesta" />
                            </div>  
                        </td>
                    </tr>
                    <tr>
                        <th style="text-align:left; vertical-align:top">
                            Responsable :
                        </th>

                        <td class="Separador"></td>
                            
                        <td class="Line" >
                            <asp:Label ID="lblResponsableRespuesta" runat="server" />
                        </td>

                        <td class="Separador"></td>
                    </tr> 
                    <tr>
                        <th style="text-align:left; vertical-align:top">
                            Fecha Respuesta :
                        </th>

                        <td class="Separador"></td>
                            
                        <td class="Line" >
                            <asp:Label ID="lblFechaRespuesta" runat="server" />
                        </td>

                        <td class="Separador"></td>
                    </tr> 
                    <tr>
                        <th style="text-align:left; vertical-align:top">
                            Activar Alarma :
                        </th>

                        <td class="Separador"></td>

                        <td class="Line" >
                            <asp:Label ID="lblFechaAlarma" runat="server" />
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
                        Gestionar Radicado
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
            <asp:PostBackTrigger ControlID="bntArchivoRadicado" />
        </Triggers>
    </asp:UpdatePanel>

</asp:Content>



<asp:Content ID="footerContent" runat="server" ContentPlaceHolderID="Footer">
    <!-- Aca va el Log -->
    <asp:UpdatePanel ID="upFooter" runat="server" ChildrenAsTriggers="true">
        <ContentTemplate>
            <table width="100%">
                <tr >
                    <td  style="text-align:left; vertical-align:top; padding-left: 10px; background-color:#e0e0e0; font-size:8pt; color:#808080;">
                        <asp:Label ID="lblMsgLogInfo" runat="server"/>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>    
</asp:Content>