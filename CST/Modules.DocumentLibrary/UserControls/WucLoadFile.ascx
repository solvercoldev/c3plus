<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WucLoadFile.ascx.cs" Inherits="Modules.DocumentLibrary.UserControls.WucLoadFile" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>



<asp:UpdatePanel ID="upLoadFileDocumentLibrary" runat="server">
        <ContentTemplate>
            

        <asp:Panel ID="pnlLoadFileDocLibrary"  runat="server" CssClass="popup_Container" Width="400" Height="240" style="display:none;">             
              

            <div class="popup_Titlebar" id="PopupHeader">
                <div class="TitlebarLeft">
                  <asp:Literal ID="litTitulo" runat="server"></asp:Literal>
                </div>
                <div class="TitlebarRight" id="divCloseMensajesDl">
                </div>
            </div>

            <div class="popup_Body">      
                <asp:PlaceHolder ID="phloadControlLoadFile" runat="server"></asp:PlaceHolder>                  
            </div>
        </asp:Panel>
    
        <asp:Button ID="btnTargetControlDl" runat="server" style="display:none; "/>    

        <ajaxToolkit:ModalPopupExtender 
        ID="mpeLoadFileDl" 
        runat="server" 
        TargetControlID="btnTargetControlDl" 
        PopupControlID="pnlLoadFileDocLibrary" 
        BackgroundCssClass="ModalPopupBG" 
        cancelcontrolid="divCloseMensajesDl"> 
        </ajaxToolkit:ModalPopupExtender>   

        </ContentTemplate>
    </asp:UpdatePanel>
   

