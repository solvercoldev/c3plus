<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WuCAdminComentariosContrato.ascx.cs" Inherits="Modules.Contratos.UserControls.WuCAdminComentariosContrato" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<table width="100%">
    <%--<tr class="SectionMainTitle">
        <td >
            LISTA DE COMENTARIOS Y RESPUESTAS
        </td>
    </tr>--%>
    <tr>
        <td >
            <div style="padding:3px; text-align:right;">
                <asp:Button ID="btnNuevoComentarioRespuesta" runat="server" Text="Registrar Comentario" OnClick="BtnAddComentario_Click" />
            </div>
        </td>
    </tr>
    <tr style="padding:10px;">
        <td >
            <asp:UpdatePanel ID="upContainer" runat="server">
                <ContentTemplate>                
                <table class="tbl" width="100%">
                    <tr>
                        <th style="width:5%;">                        
                        </th>
                        <th style="width:28%;text-align:left;">
                            Asunto
                        </th>
                        <th style="width:36%;text-align:left;">
                            Mensaje
                        </th>
                        <th style="width:15%; text-align:left;">
                            Fecha
                        </th>
                        <th style="width:15%;text-align:left;">
                            Autor
                        </th>
                        <th style="width:2%;text-align:left;">                            
                        </th>
                    </tr>
                </table>
                <asp:Panel id="pnlContainerComentariosList" style="width:100%;float: left; " Height="150px" ScrollBars="Vertical" runat="server" >
                    <table class="tbl" width="100%">
                        <asp:repeater id="rptComentariosList" runat="server" OnItemDataBound="RptComentariosList_ItemDataBound" >                        
                            <ItemTemplate>
                                <tr runat="server" id="rowParent"  class="Normal">
                                    <td style="text-align:center;border-right:none;border-top:none;width:5%;">
                                        <asp:HiddenField ID="hddIdComentario" runat="server" />                                    
                                        <asp:ImageButton 
                                                    ID="imgSelectComentario" 
                                                    runat="server"
                                                    CausesValidation="false"
                                                    BorderStyle="None"
                                                    ImageUrl="~/Resources/Images/comentarios.png"
                                                    OnClick="BtnSelectComentario_Click" ToolTip="Click para ver mas información del comentario." />
                                    </td>  
                                    <td style="text-align:left;vertical-align:top;border-right:none;border-top:none;width:30%;">
                                        <asp:Label ID="lblAsunto" runat="server" />
                                    </td>
                                    <td style="text-align:left;vertical-align:top;border-right:none;border-top:none;width:36%;">
                                        <asp:Label ID="lblMensaje" runat="server" />
                                    </td>
                                    <td style="text-align:left;vertical-align:top;border-right:none;border-top:none;width:15%;">
                                        <asp:Label ID="lblFechaComentario" runat="server" />
                                    </td>
                                    <td style="text-align:left;vertical-align:top;border-top:none;width:15%;">
                                        <asp:Label ID="lblAutor" runat="server" />
                                    </td>                    
                                </tr>
                                <tr runat="server" class="Normal" id="rowChild" >
                                    <td colspan="5" style="border-top:none;">
                                        <table width="100%" style="border:none">
                                            <asp:repeater id="rptChildComentarios" runat="server" OnItemDataBound="RptComentariosAsociadosList_ItemDataBound" >
                                                <itemtemplate>
                                                    <tr >
                                                        <td style="width:5%;text-align:right;border:none;">
                                                            <asp:ImageButton 
                                                                    ID="imgSelectComentarioRespuesta" 
                                                                    runat="server"
                                                                    CausesValidation="false"
                                                                    BorderStyle="None"
                                                                    ImageUrl="~/Resources/Images/respuesta-comentario.png"/>
                                                        </td>
                                                        <td style="text-align:left;width:30%;vertical-align:top;border:none;">
                                                            <asp:Label ID="lblAsunto" runat="server" />
                                                        </td>
                                                        <td style="text-align:left;width:36%;vertical-align:top;border:none;">
                                                            <asp:Label ID="lblMensjae" runat="server" />
                                                        </td>                  
                                                        <td style="text-align:left;width:15%;vertical-align:top;border:none;">
                                                            <asp:Label ID="lblFechaComentario" runat="server" />
                                                        </td>
                                                        <td style="text-align:left;width:15%;vertical-align:top;border:none;">
                                                            <asp:Label ID="lblAutor" runat="server" />
                                                        </td> 
                                                    </tr>
                                                </itemtemplate>
                                            </asp:repeater>
                                        </table>
                                    </td>                
                                </tr>
                            </ItemTemplate>                           
                        </asp:repeater>
                    </table>
                </asp:Panel>                
                </ContentTemplate>
            </asp:UpdatePanel>
        </td>
    </tr>    
</table>
    

<asp:UpdatePanel ID="upModal" runat="server">
    <ContentTemplate> 
        <script type="text/javascript" language="javascript">
            Sys.Application.add_load(RebindScripts);
        </script>
        <asp:Panel ID="pnlAdminComentarioRespuesta"  runat="server" CssClass="popup_Container" Width="530" Height="470" style="display:none;">  

            <div class="popup_Titlebar" id="PopupHeader">
                <div class="TitlebarLeft">
                    Administrar Comentario / Respuesta
                </div>
                <div class="TitlebarRight" id="divCloseAdminComentario">
                </div>
            </div>

            <div style="padding:3px; text-align:right;">
                <asp:Button ID="btnCancelarComentario" runat="server" Text="Regresar"  />
                <asp:Button ID="btnSaveComentario" runat="server" Text="Guardar Comentario" OnClick="BtnSaveComentario_Click"  />
            </div>

            <asp:ValidationSummary ID="vsComentarios" runat="server" DisplayMode="BulletList" ShowMessageBox="false" CssClass="validator" ShowSummary="true" ValidationGroup="vsComentarios"/>

            <div class="popup_Body">                                                    
                <table width="100%" class="tblSecciones">
                    <tr>
                        <th style="text-align:left; width: 25%; vertical-align:top">
                            Asunto :
                        </th>

                        <td class="Separador"></td>

                        <td class="Line" style="width:70%">
                            <asp:TextBox ID="txtAsunto" runat="server" Width="90%" />
                        </td>

                        <td class="Separador"></td>
                    </tr>
                    <tr>
                        <th style="text-align:left; vertical-align:top">
                            Observaciones :
                        </th>

                        <td class="Separador"></td>

                        <td class="Line">
                            <asp:TextBox ID="txtObservaciones" runat="server" TextMode="MultiLine" Rows="3" Width="90%" />
                        </td>

                        <td class="Separador"></td>
                    </tr>
                    <tr id="trInfoDestinatario" runat="server" >
                        <th style="text-align:left; vertical-align:top">
                            Destinatario :
                        </th>

                        <td class="Separador"></td>

                        <td class="Line">
                            <asp:DropDownList ID="wddDestinatarios" runat="server" Width="350px"  class="chzn-select" />
                        </td>

                        <td class="Separador"></td>
                    </tr>
                    <tr>
                        <th style="text-align:left; vertical-align:top;">                    
                             <div id="divPanelShowUsuariosCopia">                        
                                <div style="float: left; vertical-align:middle;">
                                    <asp:Label 
                                    ID="lblUsuariosCopiaTitle" 
                                    runat="server" 
                                    ForeColor="#526C8C"
                                    Font-Size="0.82em">Copiar a :</asp:Label>
                                </div>
                                <div style="float: left; vertical-align: middle;" id="PnlFiltroHeader">
                                    <asp:ImageButton 
                                    ID="ShowHideUsuariosCopia" 
                                    BorderStyle="None"
                                    BorderWidth="0"
                                    CausesValidation="false"
                                    runat="server" 
                                    AlternateText="Ver Copiar" />
                                </div>
                            </div>
                        </th>

                        <td class="Separador"></td>

                        <td class="Line">

                            <div>
                                <asp:Panel id="PanelUsuariosCopia" runat="server" >
                      
                                    <table class="tblSecciones" width="100%">
                                        <tr>
                                            <td>
                                                <asp:DropDownList ID="wddUsuarioCopia" runat="server" Width="350px"  class="chzn-select" />
                                            </td>
                                            <td>
                                                <asp:Button ID="btnAddCopia" runat="server" Text="Agregar" OnClick="BtnAddUsuarioCopia_Click"  />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:ListBox ID="lstUsuariosCopia" runat="server" SelectionMode="Single" Width="98%" Height="80px" />
                                            </td>
                                            <td>
                                                <asp:Button ID="btnRemoveCopia" runat="server" Text="Eliminar" OnClick="BtnRemoveUsuarioCopia_Click"  />
                                            </td>
                                        </tr>
                                    </table>

                                </asp:Panel>

                                <ajaxToolkit:CollapsiblePanelExtender 
                                                ID="cpeCopiarUsuarios" 
                                                runat="server" 
                                                TargetControlID="PanelUsuariosCopia"
                                                ExpandControlID="divPanelShowUsuariosCopia" 
                                                CollapseControlID="divPanelShowUsuariosCopia" 
                                                TextLabelID="lblUsuariosCopiaTitle"
                                                ImageControlID="ShowHideUsuariosCopia" 
                                                ExpandedText="Ocultar Copiar" 
                                                CollapsedText="Ver Copiar"
                                                ExpandedImage="~/Resources/images/Collapse.gif" 
                                                CollapsedImage="~/Resources/images/Expand.gif"
                                                SuppressPostBack="true" Collapsed="true">
                                                </ajaxToolkit:CollapsiblePanelExtender> 
                            </div>

                        </td>

                        <td class="Separador"></td>
                    </tr>
                    <tr>
                        <th style="text-align:left; vertical-align:top">
                            Anexos :
                        </th>

                        <td class="Separador"></td>

                        <td class="Line">
                            <asp:FileUpload ID="fupAnexoArchivo" runat="server" />

                            <asp:Button ID="btnAddArchivoAdjunto" runat="server" Text="Agregar" OnClick="BtnAddArchivoAdjunto_Click" />
                        </td>

                        <td class="Separador"></td>
                    </tr>
                    <tr>
                        <td colspan="3" style="padding-left:2px">
                            <table class="tbl" width="100%">
                                <tr>
                                    <th style="width:90%">Archivo</th>
                                    <th style="width:10%"></th>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:Panel ID="pnlArchivosAdjuntos" runat="server" Width="100%" Height="65px" ScrollBars="Vertical">
                                            <table width="100%">
                                                <asp:repeater id="rptArchivosAdjuntos" runat="server" OnItemDataBound="RptArchivosAdjuntos_ItemDataBound" >                                                                 
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
                                                                    ImageUrl="~/Resources/Images/RemoveGrid.png"
                                                                    OnClick="BtnRemoveArchivoAdjunto_Click"  />
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
                        <td class="Separador"></td>
                    </tr>
                </table>
            </div>
        </asp:Panel>
    
        <asp:Button ID="btnPopUpAdminComentarioRespuestaTargetControl" runat="server" style="display:none; "/>    

        <ajaxToolkit:ModalPopupExtender 
                    ID="mpeAdminSolucion" 
                    runat="server" 
                    TargetControlID="btnPopUpAdminComentarioRespuestaTargetControl" 
                    PopupControlID="pnlAdminComentarioRespuesta" 
                    BackgroundCssClass="ModalPopupBG" DropShadow="true"
                    cancelcontrolid="divCloseAdminComentario"> 
        </ajaxToolkit:ModalPopupExtender>
    </ContentTemplate>
    <Triggers>
        <asp:PostBackTrigger ControlID="btnAddArchivoAdjunto" />
    </Triggers>
</asp:UpdatePanel>
