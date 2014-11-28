<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WucNewFolder.ascx.cs" Inherits="Modules.DocumentLibrary.UserControls.WucNewFolder" %>

 <script type="text/javascript">
     var divError = 'DivModalNf';
     function MostrarDivError() {
         var adiv = $get(divError);
         adiv.style.visibility = 'visible';
     }
    </script>

   <style type="text/css">
        #DivModalNf
        {
            position: absolute; 
            left: 0px; top: 0px; width: 100%; height: 100%;
            background-image: url(../Images/fondosemitrans.gif);
            visibility:hidden;
            z-index: 99;    
        }

        #VentanaMensajeNf
        {
	        position: absolute; 
	        top: 140px; 
	        width: 92%; 
            padding: 15px; 
            border: #000000 1px solid;
            background-color: white; 
            text-align: justify;
        }

        #MsgNf
        {
            text-align:center;
        }
    
    </style>

 <div id="DivModalNf">
                     <div id="VentanaMensajeNf">
                               <div id="MsgNf">
                                    <img id="Img1Nf"  src="~/Resources/images/Barloading.gif" runat="server" alt="" />
                               </div>
                            </div>
</div>

<table width="100%" class="tblPreView">
    
     <tr>
        <td align="center" colspan="2">
            <div style=" text-align:left;">
                <asp:Image id="Image1" runat="server" ImageUrl="~/Resources/images/CrearCarpetaTitulo.gif"></asp:Image>
            </div>  
        </td>
    </tr>

    <tr>
        <th>
            Nombre&nbsp;Carpeta:
        </th>
        <td>
            <asp:TextBox ID="txtNombreCarpeta" runat="server" Width="90%"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvName" runat="server" ValidationGroup="MyValidationGroup" ControlToValidate="txtNombreCarpeta" ErrorMessage="*" Text="*" CssClass="validator" ></asp:RequiredFieldValidator>
        </td>
    </tr>

    <tr>
        <td colspan="2">
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
        <td></td>
        <td align="right" style="padding-right:20px;">
             <asp:Button 
            ID="btnSave" 
            CausesValidation="true" 
            ValidationGroup="MyValidationGroup"
            runat="server" 
            CssClass="BotonConfirm" OnClientClick="MostrarDivError();"
            Text="Aceptar" 
            onclick="OkButtonClick"/>
        </td>
    </tr>

</table>