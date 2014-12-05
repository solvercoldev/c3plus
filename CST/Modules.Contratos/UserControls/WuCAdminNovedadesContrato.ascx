<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WuCAdminNovedadesContrato.ascx.cs" Inherits="Modules.Contratos.UserControls.WuCAdminNovedadesContrato" %>

<%@ Register    Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register    Assembly="Infragistics4.Web.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
                Namespace="Infragistics.Web.UI.EditorControls" TagPrefix="ig" %>   

<table width="100%">
   <%-- <tr class="SectionMainTitle">
        <td colspan="2" >
            LISTADO DE NOVEDADES DEL CONTRATO
        </td>
    </tr>--%>
    <tr>
        <td style="width:85%; vertical-align:top" >
            <table class="tbl" width="100%">
                <tr>
                    <th style="width:3%;">                        
                    </th>
                    <th style="width:10%;text-align:left;vertical-align:top">
                        Tipo
                    </th>
                    <th style="width:30%;text-align:left;vertical-align:top">
                        Descripción
                    </th>
                    <th style="width:23%; text-align:left;vertical-align:top">
                        Responsable
                    </th>
                    <th style="width:12%;text-align:left; vertical-align:top">
                        Fecha Inicio
                    </th>
                    <th style="width:12%;text-align:left; vertical-align:top">
                        Fecha Inicio
                    </th>
                    <th style="width:2%;text-align:left; vertical-align:top">
                        
                    </th>
                </tr>
            </table>
            <asp:Panel id="pnlContainerNovedadesContratoList" style="width:100%;float: left; " Height="170px" ScrollBars="Vertical" runat="server" >
                <table class="tbl" width="100%">
                    <asp:repeater id="rptNovedadesList" runat="server" OnItemDataBound="RptNovedadesList_ItemDataBound" >                    
                        <ItemTemplate>
                            <tr class="Normal">
                                <td style="text-align:center; width:3%;vertical-align:top">
                                </td>                                    
                                <td style="text-align:left; width:10%;vertical-align:top">
                                    <asp:Label ID="lblTipo" runat="server" />
                                </td>
                                <td style="text-align:left; width:30%;vertical-align:top">
                                    <asp:Label ID="lblDescripcion" runat="server" />
                                </td>
                                <td style="text-align:left; width:23%;vertical-align:top">
                                    <asp:Label ID="lblResponsable" runat="server" />
                                </td>
                                <td style="text-align:left; width:12%;vertical-align:top">
                                    <asp:Label ID="lblFechaInicio" runat="server" />
                                </td>    
                                <td style="text-align:left; width:12%;vertical-align:top">
                                    <asp:Label ID="lblFechaFin" runat="server" />
                                </td>
                            </tr>
                        </ItemTemplate>
                        <AlternatingItemTemplate>
                            <tr class="Alternative">
                                <td style="text-align:center; width:3%;vertical-align:top">
                                </td>                                    
                                <td style="text-align:left; width:10%;vertical-align:top">
                                    <asp:Label ID="lblTipo" runat="server" />
                                </td>
                                <td style="text-align:left; width:30%;vertical-align:top">
                                    <asp:Label ID="lblDescripcion" runat="server" />
                                </td>
                                <td style="text-align:left; width:23%;vertical-align:top">
                                    <asp:Label ID="lblResponsable" runat="server" />
                                </td>
                                <td style="text-align:left; width:12%;vertical-align:top">
                                    <asp:Label ID="lblFechaInicio" runat="server" />
                                </td>    
                                <td style="text-align:left; width:12%;vertical-align:top">
                                    <asp:Label ID="lblFechaFin" runat="server" />
                                </td>
                            </tr>
                        </AlternatingItemTemplate>
                    </asp:repeater>
                </table>
            </asp:Panel>            
        </td>
        <td style="width:15%; text-align:center;vertical-align:top;" >

            <asp:Button ID="btnModFechaEff" runat="server" Text="Mod.Fecha Effectiva" CommandArgument="Modificación Fecha Efectiva" Width="120px" OnClick="BtnAddNovedad_Click" />

            <asp:Button ID="btnSuspender" runat="server" Text="Suspender" CommandArgument="Suspensión" Width="120px" OnClick="BtnAddNovedad_Click" />
            
            <asp:Button ID="btnRestituir" runat="server" Text="Restituir" CommandArgument="Reiniciar" Width="120px" OnClick="BtnAddNovedad_Click" />
            
            <asp:Button ID="btnRenunciar" runat="server" Text="Renunciar" CommandArgument="Renuncia" Width="120px" OnClick="BtnAddNovedad_Click" />

            <asp:Button ID="btnTerminar" runat="server" Text="Terminar" CommandArgument="Terminación" Width="120px" OnClick="BtnAddNovedad_Click" />

            <asp:Button ID="btnAnular" runat="server" Text="Anular" CommandArgument="Anulación" Width="120px" OnClick="BtnAddNovedad_Click" />
        </td>
    </tr>    
</table>

<asp:UpdatePanel ID="upModal" runat="server">
    <ContentTemplate> 
        <asp:Panel ID="pnlAdminNovedad"  runat="server" CssClass="popup_Container" Width="550px" Height="250px" style="display:none;">  

            <div class="popup_Titlebar" id="PopupHeader">
                <div class="TitlebarLeft">
                    Adicionar Novedad
                </div>
                <div class="TitlebarRight" id="divCloseAdminNovedad">
                </div>
            </div>

            <div style="padding:3px; text-align:right;">
                <asp:Button ID="btnCancelarNovedad" runat="server" Text="Regresar"  />
                <asp:Button ID="btnSaveNovedad" runat="server" Text="Agregar Novedad" OnClick="BtnSaveNovedad_Click"  />
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
                    <tr id="trInicioNovedad" runat="server" >
                        <th style="text-align:left; vertical-align:top">
                            Fecha Inicio :
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
                    <tr id="trFinNovedad" runat="server" >
                        <th style="text-align:left; vertical-align:top">
                            Fecha Fin :
                        </th>

                        <td class="Separador"></td>

                        <td class="Line">
                            <asp:TextBox ID="txtFechaFinNovedad" OnKeyDown="return false;" runat="server" />
                            <ajaxToolkit:CalendarExtender 
                                    ID="cexTxtFechaFinNovedad" 
                                    runat="server"  
                                    TargetControlID="txtFechaFinNovedad" 
                                    PopupButtonID="txtFechaFinNovedad"
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