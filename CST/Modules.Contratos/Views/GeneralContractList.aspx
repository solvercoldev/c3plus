<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GeneralContractList.aspx.cs" Inherits="Modules.Contratos.Views.GeneralContractList" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="csc" Namespace="ServerControls" Assembly="ServerControls" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <script type="text/javascript">

        var cal1;
        var cal2;

        function pageLoad() {
            cal1 = $find("calendar1");
            cal2 = $find("calendar2");

            modifyCalDelegates(cal1);
            modifyCalDelegates(cal2);
        }

        function modifyCalDelegates(cal) {
            //we need to modify the original delegate of the month cell.
            cal._cell$delegates = {
                mouseover: Function.createDelegate(cal, cal._cell_onmouseover),
                mouseout: Function.createDelegate(cal, cal._cell_onmouseout),

                click: Function.createDelegate(cal, function (e) {

                    e.stopPropagation();
                    e.preventDefault();

                    if (!cal._enabled) return;

                    var target = e.target;
                    var visibleDate = cal._getEffectiveVisibleDate();
                    Sys.UI.DomElement.removeCssClass(target.parentNode, "ajax__calendar_hover");
                    switch (target.mode) {
                        case "prev":
                        case "next":
                            cal._switchMonth(target.date);
                            break;
                        case "title":
                            switch (cal._mode) {
                                case "days": cal._switchMode("months"); break;
                                case "months": cal._switchMode("years"); break;
                            }
                            break;
                        case "month":
                            //if the mode is month, then stop switching to day mode.
                            if (target.month == visibleDate.getMonth()) {
                                //this._switchMode("days");
                            } else {
                                cal._visibleDate = target.date;
                                //this._switchMode("days");
                            }
                            cal.set_selectedDate(target.date);
                            cal._switchMonth(target.date);
                            cal._blur.post(true);
                            cal.raiseDateSelectionChanged();
                            break;
                        case "year":
                            if (target.date.getFullYear() == visibleDate.getFullYear()) {
                                cal._switchMode("months");
                            } else {
                                cal._visibleDate = target.date;
                                cal._switchMode("months");
                            }
                            break;
                        case "today":
                            cal.set_selectedDate(target.date);
                            cal._switchMonth(target.date);
                            cal._blur.post(true);
                            cal.raiseDateSelectionChanged();
                            break;
                    }
                })
            }
        }

        function onCalendarShown(sender, args) {
            //set the default mode to month
            sender._switchMode("months", true);
            changeCellHandlers(cal1);
        }

        function changeCellHandlers(cal) {
            if (cal._monthsBody) {
                //remove the old handler of each month body.
                for (var i = 0; i < cal._monthsBody.rows.length; i++) {
                    var row = cal._monthsBody.rows[i];
                    for (var j = 0; j < row.cells.length; j++) {
                        $common.removeHandlers(row.cells[j].firstChild, cal._cell$delegates);
                    }
                }
                //add the new handler of each month body.
                for (var i = 0; i < cal._monthsBody.rows.length; i++) {
                    var row = cal._monthsBody.rows[i];
                    for (var j = 0; j < row.cells.length; j++) {
                        $addHandlers(row.cells[j].firstChild, cal._cell$delegates);
                    }
                }
            }
        }


        function onCalendarHidden(sender, args) {
            if (sender.get_selectedDate()) {
                //get the final date
                var finalDate = new Date(sender.get_selectedDate());
                var selectedMonth = finalDate.getMonth();
                finalDate.setDate(1);
                finalDate.setMonth(selectedMonth + 1);
                //set the date to the TextBox
                sender.get_element().value = finalDate.format(sender._format);
            }
        }


    </script>

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
        
<div style="padding-bottom:5px; background-color:#FAFAFA;">

    <div style="padding:3px; text-align:right; margin-top:-35px; height:30px;">
        <asp:Button ID="btnNuevo" runat="server" Text="Nuevo Contrato" OnClick="BtnNewContrato_Click" />
    </div>

    <asp:Panel id="PanelFiltro" CssClass="FondoSeccionFiltro" runat="server" Width="99%" >
                      
        <table class="tblSecciones" width="100%">
            <tr>                
                <th class="TituloEtiqueta" style="text-align:left" >
                    Bloque
                </th> 
                <th class="TituloEtiqueta" style="text-align:left" >
                    Estado
                </th>                
                <td style="width:1%"></td>    
                <th class="TituloEtiqueta" style="text-align:left">
                    Fecha
                </th>
                <td style="width:1%"></td>                            
                <td >                                
                                
                </td>
            </tr>
            <tr>               
                <td>
                    <asp:DropDownList ID="ddlBloque" runat="server" ClientIDMode="Static"  Width="450px"  class="chzn-select" OnSelectedIndexChanged="BloquesChangeEvent"  AutoPostBack="true" EnableViewState="true" ViewStateMode="Enabled" />
                </td>   
                <td>
                    <asp:DropDownList ID="ddlEstado" ClientIDMode="Static"  runat="server" Width="200px"  class="chzn-select" OnSelectedIndexChanged="EstadoChangeEvent" AutoPostBack="true" EnableViewState="true" ViewStateMode="Enabled" />
                </td>                
                <td></td>
                <td>
                    <asp:TextBox ID="wdpFiltroDateFrom" ClientIDMode="Static" runat="server" Width="75px" Font-Size="8pt" OnTextChanged="FechaChangeEvent" AutoPostBack="true"  />
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" BehaviorID="calendar1" runat="server"
                        Enabled="True" Format="yyyy-MM" TargetControlID="wdpFiltroDateFrom" OnClientShown="onCalendarShown"
                        OnClientHidden="onCalendarHidden">
                    </ajaxToolkit:CalendarExtender>
                </td>
                <td></td>
                <td align="left">
                </td>
            </tr>
        </table>

    </asp:Panel>

</div>  

<asp:Panel ID="pnlContainer" runat="server" ScrollBars="Vertical" Width="100%" Height="350px">

    <table class="tbl" width="100%">
        <asp:repeater id="rptList" runat="server" OnItemDataBound="RptList_ItemDataBound" >  
            <ItemTemplate>
                <tr class="Normal">
                    <td style="text-align:center; vertical-align:middle; width:5% ">
                        
                    </td>
                    <td style="text-align:justify; vertical-align:top; width:90% ">    
                        <asp:HyperLink ID="hplContrato" runat="server" Font-Bold="true" ForeColor="Black" Font-Size="9pt" />
                        <br />
                        <asp:Label ID="lblTipoContrato" runat="server"  />
                        <br />
                        <asp:Label ID="lblEmpresa" runat="server"  />
                        <br />
                        <asp:Label ID="lblPeriodo" runat="server"  />                        
                        <br />
                        <asp:Label ID="lblEstado" runat="server" ForeColor="Orange" />&nbsp;&nbsp;-&nbsp;<asp:Label ID="lblFaseActual" runat="server"  />
                    </td>
                    <td style="text-align:center; vertical-align:middle; width:3% ">
                    </td>                                       
                </tr>
            </ItemTemplate>
            <AlternatingItemTemplate>
                <tr class="Alternative">
                    <td style="text-align:center; vertical-align:middle; width:5% ">
                        
                    </td>
                    <td style="text-align:justify; vertical-align:top; width:90% ">                        
                        <asp:HyperLink ID="hplContrato" runat="server" Font-Bold="true" ForeColor="Black" Font-Size="9pt" />
                        <br />
                        <asp:Label ID="lblTipoContrato" runat="server"  />
                        <br />
                        <asp:Label ID="lblEmpresa" runat="server"  />
                        <br />
                        <asp:Label ID="lblPeriodo" runat="server"  />
                        <br />
                        <asp:Label ID="lblEstado" runat="server" ForeColor="Orange" />&nbsp;&nbsp;-&nbsp;<asp:Label ID="lblFaseActual" runat="server"  />
                    </td>
                    <td style="text-align:center; vertical-align:middle; width:3% ">
                    </td>                    
                </tr>
            </AlternatingItemTemplate>
            
        </asp:repeater>
    </table>

</asp:Panel>

    </ContentTemplate>
</asp:UpdatePanel>

</asp:Content>