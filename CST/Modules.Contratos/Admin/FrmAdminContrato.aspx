<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FrmAdminContrato.aspx.cs" Inherits="Modules.Contratos.Admin.FrmAdminContrato" %>

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
                    <asp:Button ID="btnBack" runat="server" Text="Cancelar" OnClick="BtnBack_Click" />
                    <asp:Button ID="btnSave" runat="server" Text="Registrar Fases"  ValidationGroup="vgGeneral" OnClientClick="return ShowSplashModal();" OnClick="BtnSave_Click" />
                </div>

                <asp:ValidationSummary ID="vsGeneral" runat="server" DisplayMode="BulletList" ShowMessageBox="false" CssClass="validator" ShowSummary="true" ValidationGroup="vgGeneral"/>

                <table width="100%" class="tblSecciones" style="padding-top:10px">
                    <tr>
                        <td style="width: 10%;"></td>
                        <td class="Separador"></td>
                        <td style="width: 70%;"></td>
                        <td class="Separador"></td>
                    </tr>
                    <tr>
                        <th style="text-align:left; vertical-align:top">
                            * Numero :
                        </th>

                        <td class="Separador"></td>

                        <td class="Line" >
                            <asp:TextBox ID="txtNumeroContrato" runat="server" Width="450px" />
                        </td>

                        <td class="Separador"></td>
                    </tr> 
                    <tr>
                        <th style="text-align:left; vertical-align:top">
                            * Nombre :
                        </th>

                        <td class="Separador"></td>

                        <td class="Line" >
                            <asp:TextBox ID="txtNombreContrato" runat="server" Width="450px" />
                        </td>

                        <td class="Separador"></td>
                    </tr>  
                    <tr>
                        <th style="text-align:left; vertical-align:top">
                            * Descripción :
                        </th>

                        <td class="Separador"></td>

                        <td class="Line" >
                            <asp:TextBox ID="txtDescripcion" runat="server" Width="450px" TextMode="MultiLine" Rows="3" />
                        </td>

                        <td class="Separador"></td>
                    </tr>
                    <tr>
                        <th style="text-align:left; vertical-align:top">
                            * Fecha Firma :
                        </th>

                        <td class="Separador"></td>

                        <td class="Line" >
                            <asp:TextBox ID="txtFechaFirma" OnKeyDown="return false;" runat="server" ForeColor="Green" OnTextChanged="FechaFirma_TextChanged" AutoPostBack="true" />
                                <ajaxToolkit:CalendarExtender 
                                    ID="cexTxtfechaFirma" 
                                    runat="server"  
                                    TargetControlID="txtFechaFirma" 
                                    PopupButtonID="txtFechaFirma"
                                    Format="dd/MM/yyyy"/>
                        </td>

                        <td class="Separador"></td>
                    </tr> 
                    <tr>
                        <th style="text-align:left; vertical-align:top">
                            * Fecha Efectiva :
                        </th>

                        <td class="Separador"></td>

                        <td class="Line" >
                            <asp:TextBox ID="txtFechaEfectiva" OnKeyDown="return false;" runat="server" ForeColor="Blue" />
                            <ajaxToolkit:CalendarExtender 
                                ID="cexTxtfechaEfectiva" 
                                runat="server"  
                                TargetControlID="txtFechaEfectiva" 
                                PopupButtonID="txtFechaEfectiva"
                                Format="dd/MM/yyyy"/>
                        </td>

                        <td class="Separador"></td>
                    </tr>  
                    <tr>
                        <th style="text-align:left; vertical-align:top">
                            * Empresa :
                        </th>

                        <td class="Separador"></td>

                        <td class="Line" >
                            <asp:DropDownList ID="ddlEmpresa" runat="server" Width="450px"  class="chzn-select" />
                        </td>

                        <td class="Separador"></td>
                    </tr> 
                    <tr>
                        <th style="text-align:left; vertical-align:top">
                            * Tipo Contrato :
                        </th>

                        <td class="Separador"></td>

                        <td class="Line" >
                            <asp:DropDownList ID="ddlTipoContrato" runat="server" Width="450px"  class="chzn-select" />
                        </td>

                        <td class="Separador"></td>
                    </tr>  
                    <tr>
                        <th style="text-align:left; vertical-align:top">
                            * Bloque :
                        </th>

                        <td class="Separador"></td>

                        <td class="Line" >
                            <asp:DropDownList ID="ddlBloque" runat="server" Width="450px"  class="chzn-select" />
                        </td>

                        <td class="Separador"></td>
                    </tr>   
                    <tr>
                        <th style="text-align:left; vertical-align:top">
                            * Responsable :
                        </th>

                        <td class="Separador"></td>

                        <td class="Line" >
                            <asp:DropDownList ID="ddlResponsable" runat="server" Width="450px"  class="chzn-select" />
                        </td>

                        <td class="Separador"></td>
                    </tr>  
                    <tr>
                        <th style="text-align:left; vertical-align:top">
                            * Estado Contrato :
                        </th>

                        <td class="Separador"></td>

                        <td class="Line" >                                
                            <asp:Label ID="lblEstadoContrato" runat="server" Text="Registrado" />
                        </td>

                        <td class="Separador"></td>
                    </tr> 
                    <tr>
                        <th style="text-align:left; vertical-align:top">
                            * Ubicación Contrato :
                        </th>

                        <td class="Separador"></td>

                        <td class="Line" >
                            <table width="100%">
                                 <tr>
                                    <td style="width:10%;">
                                        Latitud:
                                    </td>
                                    <td>
                                        <ig:WebNumericEditor    Id="txtLatitud" runat="server"  MaxDecimalPlaces="6"
                                                                Nullable="false" Width="380px" HorizontalAlign="Left" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width:10%;">
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
                    <tr>
                        <th style="text-align:left; vertical-align:top">
                            * Imagen Sitio :
                        </th>

                        <td class="Separador"></td>

                        <td class="Line" >                         
                            <asp:FileUpload ID="fuImagenContrato" runat="server" Width="450px" />
                        </td>

                        <td class="Separador"></td>
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
