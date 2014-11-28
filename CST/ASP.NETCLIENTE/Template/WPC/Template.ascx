<%@ Control Language="C#" AutoEventWireup="true" Inherits="ASP.NETCLIENTE.UI.BaseTemplate" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" xml:lang="en">

<head id="Head1" runat="server">
    <title><asp:Literal ID="PageTitle" runat="server"></asp:Literal></title>
    <meta http-equiv="content-type" content="text/html; charset=utf-8" />
    <asp:Literal ID="MetaTags" runat="server" />
    <asp:Literal ID="Stylesheets" runat="server" />
    <asp:Literal ID="JavaScripts" runat="server" />
</head>

<body>
 <form id="Form1" runat="server">
        <asp:ScriptManager 
        ID="smGeneral" 
        runat="server"  
        EnableScriptGlobalization="true" 
        EnableScriptLocalization="true">
        </asp:ScriptManager>
    
    <div id="wrapper">
        <div id="header">
            <div id="headerleft">
            <div id="headerlogo"></div>
            </div>
            <div id="headerright">                
            <br />
            <div id="headerlang"></div>
            </div>
            <div id="navblock">
                 <asp:PlaceHolder ID="phMenu" runat="server"></asp:PlaceHolder>
           </div>
        </div>

        <div id="container">
          <div id="esidesktop">
            <div id="feature">
               <asp:PlaceHolder ID="Content_Main" runat="server"></asp:PlaceHolder>
            </div>
          </div>
        </div>
        
        <div id="footer-wrapper">
            <div id="footer">
                <ul id="footernav">
                    <li id="nav1">
                        <a href="#">Prensa</a>
                    </li>
                    <li id="nav2">
                        <a href="#">Offices Worldwide</a>
                    </li>
                    <li id="nav3">
                        <a href="#">Contáctenos</a>
                    </li>
                </ul>
            </div>

            <div id="footerlinks">
                © 2007-2013 PricewaterhouseCoopers y PwC se refieren a la red de Firmas miembro de PricewaterhouseCoopers International Limited (PwCIL). Cada una de las Firmas miembro constituye una entidad legal autónoma e independiente y no funge como agente de PwCIL ni de ninguna otra Firma miembro. PwCIL no presta ningún servicio a los clientes. PwCIL no es responsable ni adquiere obligación alguna por las acciones u omisiones de cualquiera de sus Firmas miembro ni puede ejercer ningún control sobre sus opiniones y acciones profesionales. Ninguna Firma miembro es responsable ni adquiere obligación alguna por las acciones u omisiones de ninguna otra Firma miembro ni puede ejercer control alguno sobre las opiniones profesionales de otra Firma miembro ni comprometer de modo alguno a ninguna Firma miembro o a PwCIL.    
                <ul>
                    <li class="nav4"><a href="#">Política de Privacidad</a></li>
                    <li class="nav5"><a href="#">Términos Legales</a></li>
                    <li class="nav6"><a href="#">Acerca del proveedor de este sitio</a></li>
                    <li class="nav7"><a href="#">Mapa del sitio</a></li>
                </ul>
            </div>
        </div>

    </div>

    </form>
</body>

</html>