<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WuCLogContratos.ascx.cs" Inherits="Modules.Contratos.UserControls.WuCLogContratos" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="csc" Namespace="ServerControls" Assembly="ServerControls" %>

<asp:UpdatePanel ID="upListado" runat="server">
   <ContentTemplate>
                
               <table cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td colspan="2" id="tdCollapse" runat="server" class="ToolBar">
                        <asp:ImageButton 
                        ID="imgCollampseExpand" 
                        runat="server" 
                        Width="10" Height="10" BorderWidth="0"
                        ImageUrl="~/Resources/Images/collapse.gif" />  
                        <asp:Label ID="lblLogTitle" runat="server" Text="Ver Log de cambios" Font-Size="7pt" />
                    </td>
                </tr>
                <tr>
                    <td>

                        <asp:Panel id="pnlDetalle" style="display:none;width:100%;float: left; " runat="server" >
                            
                            <table class="tbl" cellpadding="0" cellspacing="0" width="100%" style="height:15px">
                               <tr>
                                    <th  style="width:15%" align="left" >
                                            Fecha
                                    </th>
                                    <th  style="width:85%" align="left">
                                            Descripción
                                    </th>
                                </tr>
                            </table>  
                            <asp:Panel id="pnlContainer" style="width:100%;float: left; " Height="100px" ScrollBars="Auto" runat="server" >
                                <table width="100%" cellpadding="0" cellspacing="0" class="tbl" >
				                    <asp:repeater 
                                    id="rptListado" 
                                    OnItemDataBound="RptListadoItemDatBound"
                                    runat="server">			                                               
					                    <itemtemplate>
						                <tr class="Normal">
							                <td style="width:15%" align="left">
                                                <asp:Literal ID="litDate" runat="server" />
                                            </td>
							                <td style="width:83%" align="left">
                                                <asp:Literal ID="litDescripcion" runat="server" />
                                            </td>
							                <td style="width:2%" align="left">
                                            </td>
						                </tr>
					                    </itemtemplate> 
                                        <AlternatingItemTemplate>
                                         <tr class="Alternative">                                           
							                <td style="width:15%" align="left">
                                                <asp:Literal ID="litDate" runat="server" />
                                            </td>
							                <td style="width:83%" align="left">
                                                <asp:Literal ID="litDescripcion" runat="server" />
                                            </td>
							                <td style="width:2%" align="left">
                                            </td>
						                 </tr>
                                       </AlternatingItemTemplate>
				                    </asp:repeater>
			                      </table>                      
                            </asp:Panel>                          

                        </asp:Panel>

                       
                    </td>
                </tr>
           </table>  
           
           

            <ajaxToolkit:CollapsiblePanelExtender 
            ID="cpeCabecera" 
            runat="server" 
            TargetControlID="pnlDetalle"
            ExpandControlID="tdCollapse" 
            CollapseControlID="tdCollapse" 
            ImageControlID="imgCollampseExpand" 
            ExpandedImage="~/Resources/images/collapse.gif"
            CollapsedImage="~/Resources/images/expand.gif"
            SuppressPostBack="true" 
            Collapsed="true">
            </ajaxToolkit:CollapsiblePanelExtender>
              
     </ContentTemplate>
 </asp:UpdatePanel>