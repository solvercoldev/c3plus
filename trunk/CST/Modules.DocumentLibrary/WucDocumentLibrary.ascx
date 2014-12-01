<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WucDocumentLibrary.ascx.cs" Inherits="Modules.DocumentLibrary.WucDocumentLibrary" %>
<%@ Register TagPrefix="csc" Namespace="ServerControls" Assembly="ServerControls" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="Infragistics4.Web.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.Web.UI.LayoutControls" TagPrefix="ig" %>
<%@ Register Assembly="Infragistics4.Web.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.Web.UI.NavigationControls" TagPrefix="ig" %>

 <%@ Register src="UserControls/WucLoadFile.ascx" tagname="WucLoadFile" tagprefix="uc1" %>

<table width="100%" style=" background-color:#F1F5FB;"  cellspacing="0" cellpadding="0">         
        <tr>
            <td style="width:10%; padding-left:25px;" align="left" >                
                   
                <uc1:WucLoadFile ID="WucLoadFile1" runat="server" />
                   
            </td>
            <td style="width:2%"></td>
            <td style="width:5%" >
                <div id="divBarraMenus" style="display:none">
                    <asp:ImageButton 
                    ID="imgDeleteFile" 
                    runat="server"
                    BorderWidth="0"
                    ToolTip="Eliminar archivos"
                    ImageUrl="~/Resources/Images/RecyceeBin.png" OnClick="BtnDeleteClick" />
                </div>
            </td>
            <td align="left" style="width:50%" valign="middle">
                <table class="tblPreView" >
                    <tr>
                        <th>
                            Buscar Archivo:
                        </th>
                        <td style="width:500px">
                            <asp:TextBox ID="txtSearchBox" Width="100%" runat="server" CssClass="searchBox"></asp:TextBox>
                        </td>
                        <td align="center">
                            <asp:ImageButton ID="imgSearch" runat="server" ImageUrl="~/Resources/Images/search.png" BorderWidth="0" OnClick="ImgSerachClick" />
                        </td>
                    </tr>
                </table>
                 
                
            </td>
        </tr>
 </table>


 <asp:UpdatePanel ID="upDocumentLibrary" runat="server">
    <ContentTemplate>


        <ig:WebSplitter ID="wsContainer" runat="server" Height="200px" Width="99.8%">
            <Panes>
    
                 <ig:SplitterPane 
                        runat="server"
                        StyleSetName="Windows7"     
                        ScrollBars="Hidden"
                        ToolTip=""
                        Size="15%"
                        MaxSize="20%"
                        MinSize="15%"
                        BackColor="#FFFFFF">
                        <ig:WebDataMenu runat="server" ID="ContextMenu" IsContextMenu="true" OnItemClick="ContextMenuClick"
                                BorderWidth="1" BorderColor="#CCCCCC" EnableScrolling="false">
                                <ClientEvents ItemClick="MenuItem_Click" />
                                <AutoPostBackFlags ItemClick="On" />
                                <Items>
                                    <ig:DataMenuItem Text="Seleccionar Carpeta" Key="Select" ImageUrl="~/Resources/Images/select.png" />
                                    <ig:DataMenuItem Text="Cargar Archivo" Key="loadFile" ImageUrl="~/Resources/Images/upload.png" />
                                    <ig:DataMenuItem Text="Nueva Carpeta" Key="NewFolder" ImageUrl="~/Resources/Images/foldernew.gif" />
                                    <ig:DataMenuItem Text="Eliminar Carpeta" Key="DeleteFolder" ImageUrl="~/Resources/Images/folderDelete.png" />
                                    <ig:DataMenuItem Text="Expandir/Contraer" Key="expand" ImageUrl="~/Resources/Images/expand.png" />                        
                                </Items>
                            </ig:WebDataMenu>
                                                  
                            <ig:WebDataTree 
                            ID="wdtDocuments"  
                            SelectionType="Single" 
                            runat="server" 
                            Height="280px" Width="100%">
                            <ClientEvents NodeClick="Node_Click" />    
                            </ig:WebDataTree>
                            <input type="hidden" id="hndNodeSelected" runat="server" />
                </ig:SplitterPane>

                <ig:SplitterPane 
                    runat="server" 
                    ToolTip="" 
                    Size="85%"  
                    ScrollBars="Hidden">
                    <div style=" height:180px"  >
                            <table width="100%" class="tblPreView">
                                <asp:Repeater 
                                ID="rptDocuments" 
                                
                                OnItemDataBound="RptDocumentsitemDataBound"
                                runat="server">
                                    <HeaderTemplate>
                                        <tr>    
                                                <th style="width:50px;" align="left" > 
                                                    <asp:CheckBox ID="chkSelectAll" runat="server" OnClick="checkAll(this,'divBarraMenus');" Visible="false" />
                                                </th>
                                                <th style="width:30%" class="FondoTh">Comentarios</th>
                                                <th style="width:30%" class="FondoTh">Nombre</th>
                                                <th style="width:20%" class="FondoTh">Autor</th>                                                
                                                <th style="width:5%"  class="FondoTh">Tipo</th>
                                                <th style="width:5%"  class="FondoTh">Archivo</th>
                                         </tr>
                                    
                                    </HeaderTemplate>

                                    <ItemTemplate>
                                         <tr >
                                            <td style="width:15px" class="Noline">
                                                <asp:CheckBox ID="chkSelect" runat="server" onclick="ShowMenuBar('divBarraMenus');" />
                                            </td>
                                            <td style="width:30%" class="Noline">
                                                <asp:Literal ID="litDescripcion" runat="server"></asp:Literal>
                                            </td>
                                            <td style="width:30%" class="Noline"><%# DataBinder.Eval(Container.DataItem, "Nombre")%></td>
                                            <td style="width:20%" class="Noline"><%# DataBinder.Eval(Container.DataItem, "CreatedBy")%></td>
                                            <td style="width:10%" class="Noline"><%# DataBinder.Eval(Container.DataItem, "Tipo")%></td>
                                            <td align="center" style="width:5%" class="Noline">
                                                    <asp:ImageButton ID="imgDownload" runat="server" BorderWidth="0" Width="14" Height="14" />
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <AlternatingItemTemplate>                                                                
                                        <tr >
                                            <td style="width:15px" class="Noline">
                                                <asp:CheckBox ID="chkSelect" runat="server" onclick="ShowMenuBar('divBarraMenus');" />
                                            </td>
                                            <td style="width:30%" class="Noline">
                                                <asp:Literal ID="litDescripcion" runat="server"></asp:Literal>
                                            </td>
                                            <td style="width:30%" class="Noline"><%# DataBinder.Eval(Container.DataItem, "Nombre")%></td>
                                            <td style="width:20%" class="Noline"><%# DataBinder.Eval(Container.DataItem, "CreatedBy")%></td>
                                            <td style="width:10%" class="Noline"><%# DataBinder.Eval(Container.DataItem, "Tipo")%></td>
                                            <td align="center" style="width:5%" class="Noline">
                                                    <asp:ImageButton ID="imgDownload" runat="server" BorderWidth="0" Width="14" Height="14" />
                                            </td>
                                        </tr>
                                    </AlternatingItemTemplate>
                                </asp:Repeater>
                                                        
                                </table>

                        </div>
                         <div class="pager" style=" position:relative; bottom:0px; ">
		                                <csc:pagerlinq
                                        id="pgrListado" 
                                        runat="server"
                                        OnPageChanged="PgrListadoPageChanged"  
                                        pagesize="7"></csc:pagerlinq>	
                                </div>
                </ig:SplitterPane>
            </Panes>
        </ig:WebSplitter>

    </ContentTemplate>
</asp:UpdatePanel>


