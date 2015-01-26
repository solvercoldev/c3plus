<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FrmManualesANH.aspx.cs" Inherits="Modules.Contratos.Catalogs.FrmManualesANH" %>

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
            
            <table width="100%" class="tblSecciones">
                <tr>
                    <td style="text-align:left; vertical-align:top; width:49%; border:1px solid #F4F4F3;">
                        <asp:Panel ID="pnlManualesANH" runat="server" Width="570px" Height="270px" ScrollBars="Auto">
                            <asp:TreeView ID="tvManualANH" runat="server" Font-Size="8pt" OnSelectedNodeChanged="ManualesSelect_Change"
                                        ShowCheckBoxes="None" OnClick="OnTreeClick(event)">
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
                           
        </ContentTemplate>
        <Triggers>
        </Triggers>
    </asp:UpdatePanel>

</asp:Content>



<asp:Content ID="footerContent" runat="server" ContentPlaceHolderID="Footer">
    <!-- Aca va el Log -->
</asp:Content>