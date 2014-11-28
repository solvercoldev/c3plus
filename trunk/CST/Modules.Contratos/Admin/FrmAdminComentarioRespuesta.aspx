<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FrmAdminComentarioRespuesta.aspx.cs" Inherits="Modules.Contratos.Admin.FrmAdminComentarioRespuesta" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

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

<asp:UpdatePanel ID="upGeneral" runat="server">
    <ContentTemplate>
        <script type="text/javascript" language="javascript">
            Sys.Application.add_load(RebindScripts);
        </script>
        <div id="DivModal">
            <div id="VentanaMensaje">
                <div id="Msg">
                    <img id="Img1"  src="~/Resources/images/Barloading.gif" runat="server" alt="" />
                </div>
            </div>
        </div>

         <table class="tblSecciones" width="100%" cellpadding="0" cellspacing="0">    
             <tr>
                <td>
                    <tr>
                        <td class="SeparadorVertical" colspan="4">            
                        </td>
                    </tr>
                    <tr>
                        <td class="SeccionesH3" style="width:120px;">
                            Contrato:
                        </td>
                        <td class="Separador"></td>
                        <td class="SeccionesH4">
                            <asp:Label ID="lblContrato" runat="server" />
                        </td>
                        <td align="right" style="width:45%" valign="top">   
                                <asp:Button ID="btnRegresar" runat="server" Text="Regresar" OnClick="BtnRegresarClick"  />
                                <asp:Button ID="btnEdit" runat="server" Text="Comentar" OnClick="BtnEditComentarioClick" OnClientClick="return ShowSplashModalLoading();" />
                                <asp:Button ID="btnCancel" runat="server" Text="Salir" Visible="false" OnClick="BtnCancelComentarioClick" OnClientClick="return ShowSplashModalLoading();" />
                                <asp:Button ID="btnSave" runat="server" Text="Enviar" Visible="false" OnClick="BtnSaveComentarioClick" OnClientClick="return ShowSplashModalLoading();" />
                        </td>  
                    </tr>
                    <tr>
                        <td class="SeccionesH3">
                            Empresa:
                        </td>
                        <td class="Separador"></td>
                        <td class="SeccionesH4">
                            <asp:Label ID="lblEmpresa" runat="server" />
                        </td>
                        <td class="Separador"></td>
                    </tr>
                    <tr>
                        <td class="SeccionesH3">
                            Bloque:
                        </td>
                        <td class="Separador"></td>
                        <td class="SeccionesH4">
                            <asp:Label ID="lblBloque" runat="server" />
                        </td>
                        <td class="Separador"></td>
                    </tr>
                    <tr>
                        <td class="SeccionesH3">
                            Fecha Firma:
                        </td>
                        <td class="Separador"></td>
                        <td class="SeccionesH4">
                            <asp:Label ID="lblFechaFirma" runat="server" ForeColor="#0fab77" />
                        </td>
                        <td class="Separador"></td>
                    </tr>                                    
                    <tr>
                        <td class="SeccionesH3">
                            Periodo:
                        </td>
                        <td class="Separador"></td>
                        <td class="SeccionesH4">
                            <asp:Label ID="lblPeriodo" runat="server" ForeColor="#0fab77" />
                        </td>
                        <td class="Separador"></td>
                    </tr>                    
                </td>
            </tr>
            <tr>
               <td class="SeparadorVertical" colspan="4">          
                </td>
            </tr>
            <tr>
                <td colspan="4" class="TituloSeccion">
                    Datos Comentarios y Respuestas
                </td>
            </tr>
            <tr>
                <th style="text-align:left; vertical-align:top">
                    Asunto :
                </th>

                <td class="Separador"></td>

                <td class="Line"  colspan="2">
                    <asp:Label ID="lblAsunto" runat="server" />
                </td>
            </tr>
            <tr>
                <th style="text-align:left; vertical-align:top">
                    Mensaje :
                </th>

                <td class="Separador"></td>

                <td class="Line"  colspan="2">
                    <asp:Label ID="lblMensaje" runat="server" />
                </td>
            </tr>
            <tr>
                <th style="text-align:left; vertical-align:top">
                    Destinatario :
                </th>

                <td class="Separador"></td>

                <td class="Line"  colspan="2">
                    <asp:Label ID="lblDestinatario" runat="server" />
                </td>
            </tr>
            <tr>
                <th style="text-align:left; vertical-align:top">
                    Fecha :
                </th>

                <td class="Separador"></td>

                <td class="Line"  colspan="2" >
                    <asp:Label ID="lblFecha" runat="server" />
                </td>
            </tr>
            <tr id="trUsuariosCopia" runat="server">
                <th style=" text-align:left; vertical-align:top">
                    Usuarios Copia :
                </th>

                <td class="Separador"></td>

                <td class="Line" colspan="2">
                    <asp:ListBox ID="lstUsuariosCopia" runat="server" SelectionMode="Single" Width="98%" Height="80px" />
                </td>
            </tr>
            <tr id="trComentariosRespuesta" runat="server">
                <th style="text-align:left; vertical-align:top">
                    Comentarios y Respuestas :
                </th>

                <td class="Separador"></td>

                <td class="Line"  colspan="2">
                    <table width="100%" >
                        <asp:repeater id="rptComentariosAsociados" runat="server" OnItemDataBound="RptComentariosAsociados_ItemDataBound"  >                                                                 
                            <ItemTemplate>
                                <tr class="Line">
                                    <td style="width:17%; vertical-align:top">
                                        <asp:Label ID="lblFechaComentario" runat="server" />
                                    </td>
                                    <td style="width:12%; vertical-align:top">
                                        <asp:Label ID="lblCreadoPor" runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lblComentario" runat="server" />
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:repeater>
                    </table> 
                </td>
            </tr>  
            <tr id="trComentarios" runat="server">
                <th style="text-align:left; vertical-align:top">
                    Nuevo Comentario :
                </th>

                <td class="Separador"></td>

                <td class="Line"  colspan="2">
                    <asp:TextBox ID="txtComentario" runat="server" Width="98%" TextMode="MultiLine" Rows="3" />
                </td>
            </tr>
            <tr id="trDestinatarios" runat="server">
                <th style="text-align:left; vertical-align:top">
                    Destinatario :
                </th>

                <td class="Separador"></td>

                <td class="Line"  colspan="2">
                    <asp:DropDownList ID="wddDestinatarios" runat="server" Width="350px"  class="chzn-select" />
                </td>
            </tr>                      
            <tr>
                <th style="text-align:left; vertical-align:top">
                    Anexos :
                </th>

                <td class="Separador"></td>

                <td class="Line" colspan="2">
                    <asp:FileUpload ID="fupAnexoArchivo" runat="server" />

                    <asp:Button ID="btnAddArchivoAdjunto" runat="server" Text="Agregar" OnClick="BtnAddArchivoAdjunto_Click" OnClientClick="return ShowSplashModalLoading();" />
                </td>
            </tr>
            <tr id="trAnexos" runat="server">
                <td></td>
                <td class="Separador"></td>

                <td class="Line"  colspan="2">
                    <table class="tbl" width="100%">
                        <tr>
                            <th style="width:100%">Archivo</th>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Panel ID="pnlArchivosAdjuntos" runat="server" Width="100%" Height="65px" ScrollBars="Vertical">
                                    <table width="100%">
                                        <asp:repeater id="rptArchivosAdjuntos" runat="server" OnItemDataBound="RptArchivosAdjuntos_ItemDataBound"  >                                                                 
                                            <ItemTemplate>
                                                <tr>
                                                    <td >
                                                        <asp:HiddenField ID="hddIdArchivo" runat="server" />                                                                
                                                        <asp:LinkButton ID="lnkNombreArchivo" runat="server" OnClick="BtnDownloadArchivoAdjunto_Click" />
                                                    </td>
                                                    <td style="width:27px;" >
                                                            <asp:ImageButton 
                                                            ID="imgDeleteAnexo" 
                                                            runat="server"
                                                            CausesValidation="false"
                                                            BorderStyle="None"
                                                            ImageUrl="~/Resources/Images/RemoveGrid.png" OnClick="BtnRemoveArchivoAdjunto_Click" />
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:repeater>
                                    </table>                                            
                                </asp:Panel>
                            </td>
                        </tr>                                
                    </table>
                </td>
            </tr>
         
        </table>

    </ContentTemplate>
    <Triggers>
        <asp:PostBackTrigger ControlID="btnAddArchivoAdjunto" />
    </Triggers>
</asp:UpdatePanel>

</asp:Content>
<asp:Content ID="footerContent" runat="server" ContentPlaceHolderID="Footer">
     <table width="100%">
        <tr >
            <td style="text-align:left; vertical-align:top; padding-left: 10px; background-color:#e0e0e0" >
                <asp:Label ID="lblLogInfo" runat="server" ForeColor="#808080" Font-Size="8pt" />
            </td>
        </tr>
    </table>
</asp:Content>