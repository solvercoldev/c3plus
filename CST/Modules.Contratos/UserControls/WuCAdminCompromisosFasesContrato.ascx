<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WuCAdminCompromisosFasesContrato.ascx.cs" Inherits="Modules.Contratos.UserControls.WuCAdminCompromisosFasesContrato" %>

<%@ Register    Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<table width="100%">
    <%--<tr class="SectionMainTitle">
        <td  >
            LISTADO DE COMPROMISOS DE FASES
        </td>
    </tr>--%>
    <tr>
        <td >
            <div style="text-align:right;">
                <asp:Button ID="btnAddCompromiso" runat="server" Text="Nuevo Compromiso" OnClick="BtnAddCompromiso_Click" />
            </div>
        </td>
    </tr>
    <tr>
        <td style="vertical-align:top" >
            <table class="tbl" width="100%">
                <asp:repeater id="rptCompromisosList" runat="server" OnItemDataBound="RptCompromisosList_ItemDataBound" >
                    <HeaderTemplate>
                        <tr>
                            <th style="width:3%;">                        
                            </th>
                            <th style="width:10%;text-align:left;vertical-align:top">
                                Fase
                            </th>
                            <th style="width:30%;text-align:left;vertical-align:top">
                                Descripción
                            </th>
                            <th style="width:12%;text-align:left; vertical-align:top">
                                Fecha Cumplimiento
                            </th>
                            <th style="width:10%;text-align:left; vertical-align:top">
                                Estado
                            </th>
                            <th style="width:25%; text-align:left;;vertical-align:top">
                                Responsable
                            </th>                            
                        </tr>
                    </HeaderTemplate>   
                    <ItemTemplate>
                        <tr>
                            <td style="text-align:center; ">
                            </td>                                    
                            <td style="text-align:left">
                                <asp:Label ID="lblFase" runat="server" />
                            </td>
                            <td style="text-align:left">
                                <asp:Label ID="lblDescripcion" runat="server" />
                            </td>
                            <td style="text-align:left">
                                <asp:Label ID="lblFechaCumplimiento" runat="server" />
                            </td>  
                            <td style="text-align:left">
                                <asp:Label ID="lblEstado" runat="server" />
                            </td>  
                            <td style="text-align:left;">
                                <asp:Label ID="lblResponsable" runat="server" />
                            </td>                                       
                        </tr>
                    </ItemTemplate>
                </asp:repeater>
            </table>
        </td>
    </tr>    
</table>

<asp:UpdatePanel ID="upModal" runat="server">
    <ContentTemplate> 
         <script type="text/javascript" language="javascript">
             Sys.Application.add_load(RebindScripts);
        </script>
        <asp:Panel ID="pnlAdminCompromiso"  runat="server" CssClass="popup_Container" Width="500px" Height="350px" style="display:none;">  

            <div class="popup_Titlebar" id="PopupHeader">
                <div class="TitlebarLeft">
                    Adicionar Compromiso
                </div>
                <div class="TitlebarRight" id="divCloseAdminCompromiso">
                </div>
            </div>

            <div style="padding:3px; text-align:right;">
                <asp:Button ID="btnCancelarCompromiso" runat="server" Text="Regresar"  />
                <asp:Button ID="btnSaveCompromiso" runat="server" Text="Guardar Compromiso" OnClick="BtnSaveCompromiso_Click" />
            </div>

            <div class="popup_Body">                                                    
                <table width="100%" class="tblSecciones">
                    <tr>
                        <th style="text-align:left; width: 25%;">
                        </th>

                        <td class="Separador"></td>

                        <td class="Line" style="width:70%;">
                        </td>

                        <td class="Separador"></td>
                    </tr>
                    <tr>
                        <th style="text-align:left; vertical-align:top">
                            Fase :
                        </th>

                        <td class="Separador"></td>

                        <td class="Line">
                            <asp:DropDownList ID="ddlFase" runat="server" Width="400px"  class="chzn-select" />
                        </td>

                        <td class="Separador"></td>
                    </tr>
                    <tr>
                        <th style="text-align:left; vertical-align:top">
                            Fecha Cumplimiento :
                        </th>

                        <td class="Separador"></td>

                        <td class="Line">
                            <asp:TextBox ID="txtFechaCompromiso" OnKeyDown="return false;" runat="server" />
                            <ajaxToolkit:CalendarExtender 
                                    ID="cextxtFechaCompromiso" 
                                    runat="server"  
                                    TargetControlID="txtFechaCompromiso" 
                                    PopupButtonID="txtFechaCompromiso"
                                    Format="dd/MM/yyyy"/>
                        </td>

                        <td class="Separador"></td>
                    </tr>
                    <tr>
                        <th style="text-align:left; vertical-align:top">
                            Nombre :
                        </th>

                        <td class="Separador"></td>

                        <td class="Line">
                            <asp:TextBox ID="txtNombre" runat="server" Width="90%" />
                        </td>

                        <td class="Separador"></td>
                    </tr>
                    <tr>
                        <th style="text-align:left; vertical-align:top">
                            Descripción :
                        </th>

                        <td class="Separador"></td>

                        <td class="Line">
                            <asp:TextBox ID="txtDescripcion" runat="server" TextMode="MultiLine" Rows="3" Width="90%" />
                        </td>

                        <td class="Separador"></td>
                    </tr>
                    <tr>
                        <th style="text-align:left; vertical-align:top">
                            Responsable :
                        </th>

                        <td class="Separador"></td>

                        <td class="Line">
                            <asp:DropDownList ID="ddlResponsable" runat="server" Width="400px"  class="chzn-select" />
                        </td>

                        <td class="Separador"></td>
                    </tr>
                </table>
            </div>
        </asp:Panel>
    
        <asp:Button ID="btnPopUpAdminCompromisoTargetControl" runat="server" style="display:none; "/>    

        <ajaxToolkit:ModalPopupExtender 
        ID="mpeAdminCompromiso" 
        runat="server" 
        TargetControlID="btnPopUpAdminCompromisoTargetControl" 
        PopupControlID="pnlAdminCompromiso" 
        BackgroundCssClass="ModalPopupBG" DropShadow="true" 
        cancelcontrolid="divCloseAdminCompromiso"> 
        </ajaxToolkit:ModalPopupExtender>   
    </ContentTemplate>
    <Triggers>
    </Triggers>
</asp:UpdatePanel>