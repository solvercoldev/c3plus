<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WucNewFile.ascx.cs" Inherits="Modules.DocumentLibrary.UserControls.WucNewFile" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

   <script type="text/javascript">
       var divError = 'DivModalDl';
       function MostrarDivError() {
           var adiv = $get(divError);
           adiv.style.visibility = 'visible';
       }
    </script>

    <style type="text/css">
        #DivModalDl
        {
            position: absolute; 
            left: 0px; top: 0px; width: 100%; height: 100%;
            background-image: url(../Images/fondosemitrans.gif);
            visibility:hidden;
            z-index: 99;    
        }

        #VentanaMensajeDl
        {
	        position: absolute; 
	        top: 140px; 
	        width: 92%; 
            padding: 15px; 
            border: #000000 1px solid;
            background-color: white; 
            text-align: justify;
        }

        #MsgDl
        {
            text-align:center;
        }
    
    </style>

      <div id="DivModalDl">
                     <div id="VentanaMensajeDl">
                               <div id="MsgDl">
                                    <img id="ImgDlBarLoading"  src="~/Resources/images/Barloading.gif" runat="server" alt="" />
                               </div>
                            </div>
     </div>

    <table width="100%">
           
        <tr>
            <td >
                <div id="divDeseasContinuar" runat="server" style="display:block;">
                    <table width="100%">
                        <tr>
                            <td align="center">
                                <div style=" text-align:left;">
                                    <asp:Image id="Image1" runat="server" ImageUrl="~/Resources/images/SubirArchivoTitulo.gif"></asp:Image>
                                </div>  
                            </td>
                        </tr>
                        <tr>
                            <td>
                                    <table width="100%" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td align="left" style="width:20%">
                                            Tipo
                                        </td>
                                        <td style="width:80%">
                                            <asp:DropDownList ID="ddlTipo" runat="server"></asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                                   
                            </td>
                        </tr>                                    
                        <tr>
                            <td>
                                    <table width="100%" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td align="left" style="width:20%">
                                            Comentarios
                                        </td>
                                        <td style="width:80%">
                                            <asp:TextBox ID="txtComentarios" runat="server" Width="99%" Height="30"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                                   
                            </td>
                        </tr>

                        <tr>
                            <td>
                                     
                                    <table width="100%">
                                    <tr>
                                        <td>
                                            <asp:FileUpload ID="fuSingleFile"  runat="server" Width="98%" CssClass="FileDefaultStyle" />
                                        </td>
                                    </tr>
                                </table>
                                   
                            </td>
                        </tr>
                        <tr>
                            <td style=" height:15px">
                                <table class="MessageEdit" width="100%" id="tblMessageEdit" runat="server" visible="false">
                                <tr>
                                    <td style="width:10px;" align="left">
                                            
                                    </td>
                                    <td align="left">
                                        <asp:Literal ID="litMessageError" runat="server"></asp:Literal>
                                    </td>
                                </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                            <asp:Button 
                                            ID="btnLoadFile" 
                                            CausesValidation="true" ValidationGroup="MyValidationGroup"
                                            runat="server" 
                                            CssClass="BotonConfirm" OnClientClick="MostrarDivError();"
                                            Text="Load File" 
                                            onclick="OkButtonClick"/>
                                               
                            </td>
                        </tr>
                    </table>
                </div>
                  
            </td>
        </tr>
    </table>
            
