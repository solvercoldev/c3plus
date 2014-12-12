<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FrmNewCompromisoContrato.aspx.cs" Inherits="Modules.Contratos.Admin.FrmNewCompromisoContrato" %>

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

<script type="text/javascript">
    function OnTreeClick(evt) {
        var src = window.event != window.undefined ? window.event.srcElement : evt.target;
        var isChkBoxClick = (src.tagName.toLowerCase() == "input" && src.type == "checkbox");
        if (isChkBoxClick) {
            if (src.checked == true) {
                var nodeText = getNextSibling(src).innerText || getNextSibling(src).innerHTML;

                var nodeValue = GetNodeValue(getNextSibling(src));

                document.getElementById("label").innerHTML += nodeText + ",";
            }
            else {
                var nodeText = getNextSibling(src).innerText || getNextSibling(src).innerHTML;
                var nodeValue = GetNodeValue(getNextSibling(src));
                var val = document.getElementById("label").innerHTML;
                document.getElementById("label").innerHTML = val.replace(nodeText + ",", "");
            }
            var parentTable = GetParentByTagName("table", src);
            var nxtSibling = parentTable.nextSibling;
            //check if nxt sibling is not null & is an element node
            if (nxtSibling && nxtSibling.nodeType == 1) {
                //if node has children    
                if (nxtSibling.tagName.toLowerCase() == "div") {
                    //check or uncheck children at all levels
                    CheckUncheckChildren(parentTable.nextSibling, src.checked);
                }

            }
            //check or uncheck parents at all levels
            CheckUncheckParents(src, src.checked);
        }
    }

    function CheckUncheckChildren(childContainer, check) {
        var childChkBoxes = childContainer.getElementsByTagName("input");
        var childChkBoxCount = childChkBoxes.length;
        for (var i = 0; i < childChkBoxCount; i++) {
            childChkBoxes[i].checked = check;
        }
    }

    function CheckUncheckParents(srcChild, check) {
        var parentDiv = GetParentByTagName("div", srcChild);
        var parentNodeTable = parentDiv.previousSibling;
        if (parentNodeTable) {
            var checkUncheckSwitch;
            //checkbox checked 
            if (check) {
                var isAllSiblingsChecked = AreAllSiblingsChecked(srcChild);
                if (isAllSiblingsChecked)
                    checkUncheckSwitch = true;
                else
                    return; //do not need to check parent if any(one or more) child not checked
            }
            else //checkbox unchecked
            {
                checkUncheckSwitch = false;
            }

            var inpElemsInParentTable = parentNodeTable.getElementsByTagName("input");
            if (inpElemsInParentTable.length > 0) {
                var parentNodeChkBox = inpElemsInParentTable[0];
                parentNodeChkBox.checked = checkUncheckSwitch;
                //do the same recursively
                CheckUncheckParents(parentNodeChkBox, checkUncheckSwitch);
            }
        }
    }

    function AreAllSiblingsChecked(chkBox) {
        var parentDiv = GetParentByTagName("div", chkBox);
        var childCount = parentDiv.childNodes.length;
        for (var i = 0; i < childCount; i++) {
            if (parentDiv.childNodes[i].nodeType == 1) {
                //check if the child node is an element node
                if (parentDiv.childNodes[i].tagName.toLowerCase() == "table") {
                    var prevChkBox = parentDiv.childNodes[i].getElementsByTagName("input")[0];
                    //if any of sibling nodes are not checked, return false
                    if (!prevChkBox.checked) {
                        return false;
                    }
                }
            }
        }
        return true;
    }

    //utility function to get the container of an element by tagname
    function GetParentByTagName(parentTagName, childElementObj) {
        var parent = childElementObj.parentNode;
        while (parent.tagName.toLowerCase() != parentTagName.toLowerCase()) {
            parent = parent.parentNode;
        }
        return parent;
    }

    function getNextSibling(element) {
        var n = element;
        do n = n.nextSibling;
        while (n && n.nodeType != 1);
        return n;
    }

    //returns NodeValue
    function GetNodeValue(node) {
        var nodeValue = "";
        var nodePath = node.href.substring(node.href.indexOf(",") + 2, node.href.length - 2);
        var nodeValues = nodePath.split("\\");
        if (nodeValues.length > 1)
            nodeValue = nodeValues[nodeValues.length - 1];
        else
            nodeValue = nodeValues[0].substr(1);
        return nodeValue;
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
                    <asp:Button ID="btnSave" runat="server" Text="Guardar"  ValidationGroup="vgGeneral" OnClientClick="return ShowSplashModal();" OnClick="BtnSave_Click" />
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
                            * Fase :
                        </th>

                        <td class="Separador"></td>
                            
                        <td class="Line" >
                            <asp:DropDownList ID="ddlFase" runat="server" Width="450px"  class="chzn-select" OnSelectedIndexChanged="DdlFase_IndexChanged" AutoPostBack="true" />
                        </td>

                        <td class="Separador"></td>
                    </tr> 
                    <tr>
                        <th style="text-align:left; vertical-align:top">
                            * Periodo Fase :
                        </th>

                        <td class="Separador"></td>
                            
                        <td class="Line" >
                            <asp:Label ID="lblPeriodoFase" runat="server" />
                        </td>

                        <td class="Separador"></td>
                    </tr> 
                    <tr>
                        <th style="text-align:left; vertical-align:top">
                            * Compromiso :
                        </th>

                        <td class="Separador"></td>

                        <td class="Line" >
                            <asp:TextBox ID="txtNombre" runat="server" Width="450px" MaxLength="128" />
                        </td>

                        <td class="Separador"></td>
                    </tr>  
                    <tr>
                        <th style="text-align:left; vertical-align:top">
                            * Tipo :
                        </th>

                        <td class="Separador"></td>

                        <td class="Line" >
                            <asp:DropDownList ID="ddlTipoCompromiso" runat="server" Width="450px"  class="chzn-select" />
                        </td>

                        <td class="Separador"></td>
                    </tr>
                    <tr>
                        <th style="text-align:left; vertical-align:top">
                            * Descripción :
                        </th>

                        <td class="Separador"></td>

                        <td class="Line" >
                            <asp:TextBox ID="txtDescripcion" runat="server" Width="450px" MaxLength="512" TextMode="MultiLine" Rows="4" />
                        </td>

                        <td class="Separador"></td>
                    </tr>
                    <tr>
                        <th style="text-align:left; vertical-align:top">
                            * Fecha Compromiso :
                        </th>

                        <td class="Separador"></td>

                        <td class="Line" >
                            <asp:TextBox ID="txtFechaCompromiso" OnKeyDown="return false;" runat="server" ForeColor="Blue" OnTextChanged="TxtDiasAlarma_TextChanged" AutoPostBack="true" />
                            <ajaxToolkit:CalendarExtender 
                                ID="cexTxtfechaCompromiso" 
                                runat="server"  
                                TargetControlID="txtFechaCompromiso" 
                                PopupButtonID="txtFechaCompromiso"
                                Format="dd/MM/yyyy"/>
                        </td>

                        <td class="Separador"></td>
                    </tr> 
                    <tr>
                        <th style="text-align:left; vertical-align:top">
                            * Importancia :
                        </th>

                        <td class="Separador"></td>

                        <td class="Line" >
                            <asp:DropDownList ID="ddlImportancia" runat="server" Width="450px"  class="chzn-select" />
                        </td>

                        <td class="Separador"></td>
                    </tr>
                    <tr>
                        <th style="text-align:left; vertical-align:top">
                            * Asociado a :
                        </th>

                        <td class="Separador"></td>

                        <td class="Line" >
                            <asp:RadioButtonList ID="rblAsociadoA" runat="server" RepeatColumns="3" RepeatLayout="Table" RepeatDirection="Horizontal"
                                                 OnSelectedIndexChanged="RblAsociadoA_IndexChanged" AutoPostBack="true" >
                                <asp:ListItem Text="Bloque" Value="Bloque" Selected="True" />
                                <asp:ListItem Text="Campo" Value="Campo" />
                                <asp:ListItem Text="Pozo" Value="Pozo" />
                            </asp:RadioButtonList>
                        </td>

                        <td class="Separador"></td>
                    </tr>  
                    <tr>
                        <th style="text-align:left; vertical-align:top">
                            * Bloque/Campo/Pozo :
                        </th>

                        <td class="Separador"></td>

                        <td class="Line" >
                            <asp:DropDownList ID="ddlBCP" runat="server" Width="450px"  class="chzn-select" />
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
                            * Activar Alarma :
                        </th>

                        <td class="Separador"></td>

                        <td class="Line" >
                            <ig:WebNumericEditor    Id="txtDiasAlarma" runat="server" OnTextChanged="TxtDiasAlarma_TextChanged" AutoPostBackFlags-ValueChanged="On"
                                                    Nullable="false" MinValue="1" MaxValue="30" Width="50px" HorizontalAlign="Left" />
                            - días antes.
                            <asp:Image ID="imgAlarma" runat="server" ImageUrl="~/Resources/Images/alarm.png" />
                            Recordar desde:
                            <asp:Label ID="lblFechaAlarma" runat="server" ForeColor="Red" />
                        </td>

                        <td class="Separador"></td>
                    </tr>                      
                    <tr>
                        <th style="text-align:left; vertical-align:top">
                            * Asociar :
                        </th>

                        <td class="Separador"></td>

                        <td class="Line" >                                
                            <asp:RadioButtonList ID="rblAsociarCompromiso" runat="server" RepeatColumns="3" RepeatLayout="Table" RepeatDirection="Horizontal"
                                                 OnSelectedIndexChanged="RblAsociarCompromiso_IndexChanged" AutoPostBack="true">
                                <asp:ListItem Text="N/A" Value="No" Selected="True" />
                                <asp:ListItem Text="Entregable ANH" Value="Entregable" />
                                <asp:ListItem Text="Pagos Obligaciones" Value="Pago" />
                            </asp:RadioButtonList>
                        </td>

                        <td class="Separador"></td>
                    </tr>                     
                </table>

                <table width="100%" class="tblSecciones" style="margin-top:10px" id="tblEntregablesANH" runat="server" visible="false">
                    <tr>
                        <td style="width: 20%;"></td>
                        <td class="Separador"></td>
                        <td style="width: 70%;"></td>
                        <td class="Separador"></td>
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
                          
                        </th>

                        <td class="Separador"></td>
                            
                        <td  >
                        </td>

                        <td align="right" valign="top">
                            <asp:Button ID="btnAddEntregable" runat="server" Text="Adicionar Entregable" OnClick="BtnAddEntregable_Click" />
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

                        <td class="Separador">
                            <asp:Button ID="btnRemoveEntregable" runat="server" Text="Remover Entregable" OnClick="BtnRemoveEntregable_Click" />
                        </td>
                    </tr> 
                </table>

                <table width="100%" class="tblSecciones" style="margin-top:10px" id="tblPagosObligaciones" runat="server" visible="false">
                    <tr>
                        <td style="width: 20%;"></td>
                        <td class="Separador"></td>
                        <td style="width: 70%;"></td>
                        <td class="Separador"></td>
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
                            * Tipo Pago :
                        </th>

                        <td class="Separador"></td>
                            
                        <td class="Line" >
                            <asp:DropDownList ID="ddlTipoPago" runat="server" Width="450px"  class="chzn-select" />
                        </td>

                        <td class="Separador"></td>
                    </tr> 
                    <tr>
                        <th style="text-align:left; vertical-align:top">
                            * Entidad :
                        </th>

                        <td class="Separador"></td>
                            
                        <td class="Line" >
                            <asp:DropDownList ID="ddlEntidad" runat="server" Width="450px"  class="chzn-select" />
                        </td>

                        <td class="Separador"></td>
                    </tr> 
                    <tr>
                        <th style="text-align:left; vertical-align:top">
                            Valor Cobertura :
                        </th>

                        <td class="Separador"></td>

                        <td class="Line" >
                            <ig:WebNumericEditor    Id="txtValorCoberturaPago" runat="server" 
                                                    Nullable="false" MinValue="0" Width="250px" HorizontalAlign="Left" />
                            -
                            <asp:DropDownList ID="ddlMonedaCobertura" runat="server" />
                        </td>

                        <td class="Separador"></td>
                    </tr>       
                    <tr>
                        <th style="text-align:left; vertical-align:top">
                            Numero Documento :
                        </th>

                        <td class="Separador"></td>
                            
                        <td class="Line" >
                            <asp:TextBox ID="txtPagoNumeroDocumento" runat="server" Width="450px" MaxLength="128" />
                        </td>

                        <td class="Separador"></td>
                    </tr> 
                    <tr>
                        <th style="text-align:left; vertical-align:top">
                            * Valor :
                        </th>

                        <td class="Separador"></td>

                        <td class="Line" >
                            <ig:WebNumericEditor    Id="txtValorPago" runat="server" 
                                                    Nullable="false" MinValue="0" Width="250px" HorizontalAlign="Left" />
                            -
                            <asp:DropDownList ID="ddlMoneda" runat="server" />
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
            <asp:Panel ID="pnlAdminEntregableANH"  runat="server" CssClass="popup_Container" Width="960px" Height="370px" style="display:none;">  

                <div class="popup_Titlebar" id="PopupHeader">
                    <div class="TitlebarLeft">
                        Adicionar Entregables ANH
                    </div>
                    <div class="TitlebarRight" id="divCloseAdminEntregableANH">
                    </div>
                </div>

                <div style="padding:3px; text-align:right;">
                    <asp:Button ID="btnCancelarEntregable" runat="server" Text="Regresar"  />
                    <asp:Button ID="btnSaveEntregable" runat="server" Text="Adicionar Entreables" OnClick="BtnSaveEntregable_Click" />
                </div>

                <div class="popup_Body">                                                    
                    <table width="100%" class="tblSecciones">                        
                        <tr>
                            <td colspan="3">
                                <asp:Label ID="lblWarning" runat="server" Text="**Para seleccionar haga click en el checkbox y click en el item para consultar su contenido" ForeColor="Red" Font-Size="7pt" Font-Italic="true" />
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align:left; vertical-align:top; width:49%; border:1px solid #F4F4F3;">
                                <asp:Panel ID="pnlManualesANH" runat="server" Width="570px" Height="270px" ScrollBars="Auto">
                                    <asp:TreeView ID="tvManualANH" runat="server" Font-Size="8pt" OnSelectedNodeChanged="ManualesSelect_Change"
                                              ShowCheckBoxes="All" OnClick="OnTreeClick(event)">
                                    </asp:TreeView>
                                </asp:Panel>                                
                            </td>
                            <td style="width:2%"></td>
                            <td style="text-align:left; vertical-align:top; width:49%; border:1px solid #F4F4F3;">
                                <table width="100%">
                                    <tr>
                                        <td style="width:18%"></td>
                                        <td style="width:2%"></td>
                                        <td style="width:80%"></td>
                                    </tr>
                                    <tr>
                                        <th style="vertical-align:top;text-align:left">
                                            Id:
                                        </th>
                                        <td ></td>
                                        <td style="vertical-align:top;text-align:left" class="Line">
                                            <asp:Label ID="lblManualId" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <th style="vertical-align:top;text-align:left">
                                            #Producto:
                                        </th>
                                        <td ></td>
                                        <td style="vertical-align:top;text-align:left" class="Line">
                                            <asp:Label ID="lblManualNoProducto" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <th style="vertical-align:top;text-align:left">
                                            Producto:
                                        </th>
                                        <td ></td>
                                        <td style="vertical-align:top;text-align:left" class="Line">
                                            <asp:Label ID="lblManualProducto" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <th style="vertical-align:top;text-align:left">
                                            Contenido:
                                        </th>
                                        <td ></td>
                                        <td style="vertical-align:top;text-align:left" class="Line">
                                            <asp:TextBox ID="txtManualContenido" runat="server" Width="100%" TextMode="MultiLine" Rows="4" OnKeyDown="return false;" />                                            
                                        </td>
                                    </tr>
                                    <tr>
                                        <th style="vertical-align:top;text-align:left">
                                            Formato:
                                        </th>
                                        <td ></td>
                                        <td style="vertical-align:top;text-align:left" class="Line">
                                            <asp:Label ID="lblManualFormato" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <th style="vertical-align:top;text-align:left">
                                            Medio:
                                        </th>
                                        <td ></td>
                                        <td style="vertical-align:top;text-align:left" class="Line">
                                            <asp:Label ID="lblManualMedio" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <th style="vertical-align:top;text-align:left">
                                            Entrega:
                                        </th>
                                        <td ></td>
                                        <td style="vertical-align:top;text-align:left" class="Line">
                                            <asp:Label ID="lblManualEntrega" runat="server" />
                                        </td>
                                    </tr>
                                </table>                                
                            </td>
                        </tr>

                    </table>
                </div>
            </asp:Panel>
    
            <asp:Button ID="btnPopUpAdminEntregableANHTargetControl" runat="server" style="display:none; "/>    

            <ajaxToolkit:ModalPopupExtender 
            ID="mpeAdminEntregableANH" 
            runat="server" 
            TargetControlID="btnPopUpAdminEntregableANHTargetControl" 
            PopupControlID="pnlAdminEntregableANH" 
            BackgroundCssClass="ModalPopupBG" DropShadow="true" 
            cancelcontrolid="divCloseAdminEntregableANH"> 
            </ajaxToolkit:ModalPopupExtender>   
        </ContentTemplate>
        <Triggers>
        </Triggers>
    </asp:UpdatePanel>

</asp:Content>



<asp:Content ID="footerContent" runat="server" ContentPlaceHolderID="Footer">
    <!-- Aca va el Log -->
</asp:Content>