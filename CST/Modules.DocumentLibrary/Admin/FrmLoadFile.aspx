<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FrmLoadFile.aspx.cs" Inherits="Modules.DocumentLibrary.Admin.FrmLoadFile" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
   <title>Load File</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1"/>
		<meta name="CODE_LANGUAGE" content="C#"/>
		<meta name="vs_defaultClientScript" content="JavaScript"/>
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5"/>
		<meta http-equiv="Page-Exit" content="blendTrans(duration=0.5)"/>
		<meta http-equiv="Cache-Control" content="no-cache"/>
		<meta http-equiv="Pragma" content="no-cache"/>
		<meta http-equiv="Expires" content="Tue, 01 Jan 1980 1:00:00 GMT"/>
		<base target="_self"/>
        <link href="~/Resources/Styles/Site.css" rel="stylesheet" type="text/css" />  
        <script  src="../../../../Resources/Scripts/Site.js" type="text/javascript"></script>
        <script type="text/javascript">

            function CloseWindowLoadFile(namefile) {

                window.parent.document.getElementById('ctl00_MainContent_wdwWizard_tmpl_WucPasoDos_WucLoadFile1_hdnNameFile').value = namefile;

                window.parent.document.getElementById('ctl00_MainContent_wdwWizard_tmpl_WucPasoDos_WucLoadFile1_lnkUpdateFiles').click();
            }

            var divError = 'DivModal';
            function MostrarDivError() {
                var adiv = $get(divError);
                adiv.style.visibility = 'visible';
            }
        </script>
</head>
<body>
    <form id="form1" runat="server">
     <asp:ScriptManager 
        ID="smGeneral" 
        runat="server"  
        EnableScriptGlobalization="true" 
        EnableScriptLocalization="true">
        </asp:ScriptManager>


    <div id="DivModal">
         <div id="VentanaMensaje">
            <div id="Msg">
                <img id="Img1"  src="~/Resources/images/Barloading.gif" runat="server" alt="" />
            </div>
         </div>
     </div>

    <div id="pnlNotificar" runat="server" style="display: block; text-align:center; background-color:White; width:100%;">
    
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
    </div>
    </form>
</body>
</html>
