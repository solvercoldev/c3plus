<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FrmAdminFasesContrato.aspx.cs" Inherits="Modules.Contratos.Admin.FrmAdminFasesContrato" %>

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

    <div style="padding:3px; text-align:right;margin-top:-35px; height:30px;">
        <asp:Button ID="btnCancel" runat="server" Text="Regresar" OnClick="BtnCancel_Click" OnClientClick="return ShowSplashModal();" />
        <asp:Button ID="btnSave" runat="server" Text="Guardar & Generar"  OnClientClick="return ShowSplashModal();" OnClick="BtnSave_Click" />
    </div>

    <asp:ValidationSummary ID="vsGeneral" runat="server" DisplayMode="BulletList" ShowMessageBox="false" CssClass="validator" ShowSummary="true" ValidationGroup="vgGeneral"/>

    <table width="100%" cellpadding="0" cellspacing="0" style="padding-top:10px">
        <tr>
            <td class="Separador"></td>
            <td style="width: 45%;"></td>
            <td style="width:10px;"></td>
            <td style="width: 50%;"></td>
            <td class="Separador"></td>
        </tr>
        <tr valign="top" align="left">
            <td></td>
            <td class="BorderTable">
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
                        <td  class="Separador"></td>
                        <td class="SeccionesH4">
                            <asp:Label ID="lblContrato" runat="server" />
                        </td>
                    </tr>                    
                    <tr>
                        <td class="SeccionesH3">
                            Empresa:
                        </td>
                        <td  class="Separador"></td>
                        <td class="SeccionesH4">
                            <asp:Label ID="lblEmpresa" runat="server" />
                        </td>
                    </tr>                    
                    <tr>
                        <td class="SeccionesH3">
                            Fecha Firma:
                        </td>
                        <td  class="Separador"></td>
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
                        <td  class="Separador"></td>
                        <td class="SeccionesH4">
                            <asp:Label ID="lblPeriodo" runat="server" />
                        </td>
                    </tr>                    
                </table>
            </td>
            <td></td>
            <td class="BorderTable">
                <table width="100%" class="tblSecciones" >
                    <tr>
                        <td style="width: 25%;"></td>
                        <td class="Separador"></td>
                        <td style="width: 60%;"></td>
                        <td class="Separador"></td>
                    </tr>
                    <tr>
                        <td class="SeccionesH3">
                            Número de Fases                            
                        </td>
                        <td class="Separador"></td>
                        <td class="Line">
                            <ig:WebNumericEditor    Id="txtNumeroFases" runat="server" 
                                                    Nullable="false" MinValue="1" MaxValue="6" Width="100%" HorizontalAlign="Left" />
                        </td>
                        <td class="Separador"></td>
                    </tr>
                    <tr>
                        <td class="SeccionesH3">
                            Tiene Fase Cero?
                        </td>
                        <td class="Separador"></td>
                        <td class="Line" >
                            <asp:RadioButtonList ID="rblAplicado" runat="server" RepeatColumns="2" RepeatDirection="Horizontal" RepeatLayout="Table">
                                <asp:ListItem Selected="True" Value="true" Text="Si" />
                                <asp:ListItem Value="false" Text="No" />
                            </asp:RadioButtonList>
                        </td>
                        <td class="Separador"></td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <div style="padding:3px; text-align:right;">
                                <asp:Button ID="btnGenerarFases" runat="server" Text="Generar Fases"  OnClientClick="return ShowSplashModal();" OnClick="BtnGenerarFases_Click" />
                            </div>
                        </td>
                    </tr>
                </table>                
            </td>
            <td></td>
        </tr>

        <tr>
            <td></td>
            <td colspan="3">
                <br />
                <table class="tbl" width="100%">
                    <asp:repeater id="rptFases" runat="server" OnItemDataBound="RptFases_ItemDataBound" >
                        <HeaderTemplate>
                            <tr>                    
                                <th style="width:4%;">                        
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
                            <tr class="Normal">
                                <td style="text-align:center; ">
                                    <asp:HiddenField ID="hddIdFase" runat="server" />
                                </td>
                                <td style="text-align:center">
                                    <asp:Label ID="lblPeriodo" runat="server" />
                                </td>
                                <td style="text-align:center">
                                    <asp:Label ID="lblFase" runat="server" />
                                </td>
                                <td style="text-align:center">
                                    <ig:WebNumericEditor    Id="txtDuracionFases" runat="server" 
                                                            OnTextChanged="TxtDuracionFases_TextChanged" AutoPostBackFlags-ValueChanged="On"
                                                            Nullable="false"  Width="30px" HorizontalAlign="Left" />
                                    -Meses
                                </td>
                                <td style="text-align:center">
                                    <asp:Label ID="lblFechaInicio" runat="server" />
                                </td>
                                <td style="text-align:center">
                                    <asp:TextBox ID="txtFechaFin" runat="server" OnKeyDown="return false;" OnTextChanged="FechaFinalFase_ValueChanged" AutoPostBack="true" />
                                    <ajaxToolkit:CalendarExtender 
                                        ID="cexTxtfechaInicio" 
                                        runat="server"  
                                        TargetControlID="txtFechaFin" 
                                        PopupButtonID="txtFechaFin"
                                        Format="dd/MM/yyyy"/>
                                </td>
                                <td >
                                </td>                    
                            </tr>
                        </ItemTemplate>
                        <AlternatingItemTemplate>
                            <tr class="Alternative">
                                <td style="text-align:center; ">
                                    <asp:HiddenField ID="hddIdFase" runat="server" />
                                </td>
                                <td style="text-align:center">
                                    <asp:Label ID="lblPeriodo" runat="server" />
                                </td>
                                <td style="text-align:center">
                                    <asp:Label ID="lblFase" runat="server" />
                                </td>
                                <td style="text-align:center">
                                    <ig:WebNumericEditor    Id="txtDuracionFases" runat="server" 
                                                            OnTextChanged="TxtDuracionFases_TextChanged" AutoPostBackFlags-ValueChanged="On"
                                                            Nullable="false"  Width="30px" HorizontalAlign="Left" />
                                    -Meses
                                </td>
                                <td style="text-align:center">
                                    <asp:Label ID="lblFechaInicio" runat="server" />
                                </td>
                                <td style="text-align:center">
                                    <asp:TextBox ID="txtFechaFin" runat="server" OnKeyDown="return false;" OnTextChanged="FechaFinalFase_ValueChanged" AutoPostBack="true" />
                                    <ajaxToolkit:CalendarExtender 
                                        ID="cexTxtfechaInicio" 
                                        runat="server"  
                                        TargetControlID="txtFechaFin" 
                                        PopupButtonID="txtFechaFin"
                                        Format="dd/MM/yyyy"/>
                                </td>
                                <td >
                                </td>                    
                            </tr>
                        </AlternatingItemTemplate>
                    </asp:repeater>
                </table>
            </td>
            <td></td>
        </tr>
        
    </table>

        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>



<asp:Content ID="footerContent" runat="server" ContentPlaceHolderID="Footer">
    <!-- Aca va el Log -->
</asp:Content>
