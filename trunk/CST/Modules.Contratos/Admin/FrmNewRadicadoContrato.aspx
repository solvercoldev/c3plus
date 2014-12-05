<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FrmNewRadicadoContrato.aspx.cs" Inherits="Modules.Contratos.Admin.FrmNewRadicadoContrato" %>

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

    

    
    <asp:UpdatePanel ID="upgeneral" runat="server">
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

                <div style="padding:3px; text-align:right; margin-top:-35px; height:30px;">
                    <asp:Button ID="btnBack" runat="server" Text="Regresar"  OnClick="BtnBack_Click"  />
                    <asp:Button ID="btnSave" runat="server" Text="Guardar"  OnClick="BtnSave_Click" OnClientClick="return ShowSplashModalLoading();" />
                </div>

                <asp:ValidationSummary ID="vsGeneral" runat="server" DisplayMode="BulletList" ShowMessageBox="false" CssClass="validator" ShowSummary="true" ValidationGroup="vgGeneral"/>

                <table width="100%" class="tblSecciones" style="padding-top:10px">
                    <tr>
                        <td style="width: 20%;"></td>
                        <td class="Separador"></td>
                        <td style="width: 70%;"></td>
                        <td class="Separador"></td>
                    </tr>                    
                    <tr>
                        <th style="text-align:left; vertical-align:top">
                            Tipo Radicado :
                        </th>

                        <td class="Separador"></td>
                            
                        <td class="Line" >
                            <asp:RadioButtonList ID="rblTipoRadicado" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"
                                                OnSelectedIndexChanged="RblTipoRadicado_IndexChanged" AutoPostBack="true">
                                <asp:ListItem Text="Radicado de Entrada" Value="RE" Selected="True" />
                                <asp:ListItem Text="Radicado de Salida" Value="RS" />
                            </asp:RadioButtonList>
                        </td>

                        <td class="Separador"></td>
                    </tr> 
                    <tr>
                        <th style="text-align:left; vertical-align:top">
                            * Numero Radicado :
                        </th>

                        <td class="Separador"></td>
                            
                        <td class="Line" >
                            <asp:TextBox ID="txtNumeroRadicado" runat="server" Width="450px" MaxLength="30" />
                        </td>

                        <td class="Separador"></td>
                    </tr> 
                    <tr>
                        <th style="text-align:left; vertical-align:top">
                            * Fecha Radicado :
                        </th>

                        <td class="Separador"></td>

                        <td class="Line" >
                            <asp:TextBox ID="txtFechaRadicado" OnKeyDown="return false;" runat="server" ForeColor="Blue" />
                            <ajaxToolkit:CalendarExtender 
                                ID="cexTxtfechaRadicado" 
                                runat="server"  
                                TargetControlID="txtFechaRadicado" 
                                PopupButtonID="txtFechaRadicado"
                                Format="dd/MM/yyyy"/>
                        </td>

                        <td class="Separador"></td>
                    </tr> 
                    <tr>
                        <th style="text-align:left; vertical-align:top">
                            * Asunto :
                        </th>

                        <td class="Separador"></td>

                        <td class="Line" >
                            <asp:TextBox ID="txtAsunto" runat="server" Width="450px" MaxLength="100" />
                        </td>

                        <td class="Separador"></td>
                    </tr>  
                    <tr>
                        <th style="text-align:left; vertical-align:top">
                            * Dirigido A :
                        </th>

                        <td class="Separador"></td>

                        <td class="Line" >
                            <asp:DropDownList ID="ddlDirigidoA" runat="server" Width="450px"  class="chzn-select" />
                        </td>

                        <td class="Separador"></td>
                    </tr>
                    <tr>
                        <th style="text-align:left; vertical-align:top">
                            * Enviado Por :
                        </th>

                        <td class="Separador"></td>

                        <td class="Line" >
                            <asp:TextBox ID="txtEnviadoPor" runat="server" Width="450px" MaxLength="512" />
                        </td>

                        <td class="Separador"></td>
                    </tr>
                    <tr>
                        <th style="text-align:left; vertical-align:top">
                             Descripción :
                        </th>

                        <td class="Separador"></td>

                        <td class="Line" >
                            <asp:TextBox ID="txtResumen" runat="server" Width="450px" MaxLength="300" TextMode="MultiLine" Rows="3" />
                        </td>

                        <td class="Separador"></td>
                    </tr>
                     <tr id="trRespondeRadicadoEntrada" runat="server" visible="false" >
                        <th style="text-align:left; vertical-align:top">
                            Responde a un RE :
                        </th>

                        <td class="Separador"></td>
                            
                        <td class="Line" >
                            <asp:RadioButtonList ID="rblRespondeRE" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"
                                                OnSelectedIndexChanged="RblRespondeRE_IndexChanged" AutoPostBack="true">
                                <asp:ListItem Text="NO" Value="False" Selected="True" />
                                <asp:ListItem Text="SI" Value="True" />
                            </asp:RadioButtonList>
                        </td>

                        <td class="Separador"></td>
                    </tr> 
                    <tr id="trRadicadoEntrada" runat="server" visible="false" >
                        <th style="text-align:left; vertical-align:top">
                            Radicado Entrada :
                        </th>

                        <td class="Separador"></td>

                        <td class="Line" >
                            <asp:DropDownList ID="ddlRadicadoEntrada" runat="server" Width="450px"  class="chzn-select" />
                        </td>

                        <td class="Separador"></td>
                    </tr>
                    <tr >
                        <th style="text-align:left; vertical-align:top">
                            Se espera respuesta? :
                        </th>

                        <td class="Separador"></td>
                            
                        <td class="Line" >
                            <asp:RadioButtonList ID="rblRespuestaPendiente" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"
                                                 OnSelectedIndexChanged="RblRespuestaPendiente_IndexChanged" AutoPostBack="true" >
                                <asp:ListItem Text="NO" Value="False" Selected="True" />
                                <asp:ListItem Text="SI" Value="True" />
                            </asp:RadioButtonList>
                        </td>

                        <td class="Separador"></td>
                    </tr>                                              
                </table>
    
                <table width="100%" class="tblSecciones" style="margin-top:10px" id="tblRespuestaRadicado" runat="server" visible="false" >
                    <tr>
                        <td style="width: 20%;"></td>
                        <td class="Separador"></td>
                        <td style="width: 70%;"></td>
                        <td class="Separador"></td>
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
                            * Responsable :
                        </th>

                        <td class="Separador"></td>
                            
                        <td class="Line" >
                            <asp:DropDownList ID="ddlResponsableRadicado" runat="server" Width="450px"  class="chzn-select" />
                        </td>

                        <td class="Separador"></td>
                    </tr> 
                    <tr>
                        <th style="text-align:left; vertical-align:top">
                            * Fecha Respuesta :
                        </th>

                        <td class="Separador"></td>
                            
                        <td class="Line" >
                            <asp:TextBox ID="txtFechaRespuesta" OnKeyDown="return false;" runat="server" ForeColor="Green" OnTextChanged="TxtDiasAlarma_TextChanged" AutoPostBack="true" />
                            <ajaxToolkit:CalendarExtender 
                                ID="cextFechaRespuesta" 
                                runat="server"  
                                TargetControlID="txtFechaRespuesta" 
                                PopupButtonID="txtFechaRespuesta"
                                Format="dd/MM/yyyy"/>
                        </td>

                        <td class="Separador"></td>
                    </tr> 
                    <tr>
                        <th style="text-align:left; vertical-align:top">
                            Activar Alarma :
                        </th>

                        <td class="Separador"></td>

                        <td class="Line" >
                            <ig:WebNumericEditor    Id="txtDiasAlarma" runat="server"  OnTextChanged="TxtDiasAlarma_TextChanged" AutoPostBackFlags-ValueChanged="On"
                                                    Nullable="false" MinValue="1" MaxValue="30" Width="50px" HorizontalAlign="Left" />
                            - días antes.
                            <asp:Image ID="imgAlarma" runat="server" ImageUrl="~/Resources/Images/alarm.png" />
                            Recordar desde:
                            <asp:Label ID="lblFechaAlarma" runat="server" ForeColor="Red" />
                        </td>

                        <td class="Separador"></td>
                    </tr>
                </table>

                <table width="100%" class="tblSecciones" style="padding-top:10px">
                    <tr>
                        <td style="width: 20%;"></td>
                        <td class="Separador"></td>
                        <td style="width: 70%;"></td>
                        <td class="Separador"></td>
                    </tr> 
                    <tr>
                        <td colspan="4" style="padding-bottom:5px">
                            <div style="display:block; padding:5px;" class="TituloVentana">
                                <asp:Literal ID="litAnexo" runat="server" Text="Anexo" />
                            </div>  
                        </td>
                    </tr>
                    <tr >
                        <th style="text-align:left; vertical-align:top">
                            * Archivo Anexo :
                        </th>

                        <td class="Separador"></td>

                        <td class="Line" >                         
                            <asp:FileUpload ID="fuArchivoAnexo" runat="server" Width="450px" />
                        </td>

                        <td class="Separador"></td>
                    </tr> 
                    <tr id="trInfoArchivoAnexo" runat="server" visible="false" >
                        <th style="text-align:left; vertical-align:top">                            
                        </th>

                        <td class="Separador"></td>

                        <td class="Line" >                         
                            <asp:LinkButton ID="bntArchivoRadicado" runat="server" OnClick="BntArchivoRadicado_Click" />
                        </td>

                        <td class="Separador"></td>
                    </tr>  
                </table>

        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSave" />
            <asp:PostBackTrigger ControlID="rblTipoRadicado" />
            <asp:PostBackTrigger ControlID="rblRespondeRE" />
            <asp:PostBackTrigger ControlID="rblRespuestaPendiente" />
            <asp:PostBackTrigger ControlID="txtFechaRespuesta" />
            <asp:PostBackTrigger ControlID="txtDiasAlarma" />
            <asp:PostBackTrigger ControlID="bntArchivoRadicado" />
        </Triggers>
    </asp:UpdatePanel>

</asp:Content>



<asp:Content ID="footerContent" runat="server" ContentPlaceHolderID="Footer">
    <!-- Aca va el Log -->
</asp:Content>