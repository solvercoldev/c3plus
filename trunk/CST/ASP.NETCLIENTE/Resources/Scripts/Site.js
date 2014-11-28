
/// Metodo que procesa los datos enviados desde el servidor por medio del WebService para actualizar el estado de los controles
///que idican el estado del proceso.


function DatosJson(resul) {

    var objData = eval("(" + resul + ");");
    if (objData.Porcentaje <= 100) {
        iniciarProceso();
        EscribirIndicadores(objData.Porcentaje, objData.Actual);

    }
    else {

        EscribirIndicadores(100, objData.Actual);
        clearTimeout(timer);

    }

}

function EscribirIndicadores(porcentaje, actual) {

    $get("DivActual").innerHTML = "[ " + actual + " ] Plantillas";
    $get("porcentaje").style.width = porcentaje + "%";
    $get("valPor").innerHTML = porcentaje + "%";
}

function ShowHiddeObjects(Obs, Img, UrlImageHidden, UrlImageShow) {
    if (document.layers) {
        vista = (document.layers[Obs].display == 'none') ? 'block' : 'none'
        strImage = (document.layers[Obs].display == 'none') ? UrlImageHidden : UrlImageShow;
        document.layers[Obs].visibility = vista;
        document.layers[Img].src = strImage;
    }
    else if (document.all) {
        vista = (document.all[Obs].style.display == 'none') ? 'block' : 'none';
        strImage = (document.all[Obs].style.display == 'none') ? UrlImageHidden : UrlImageShow;
        document.all[Obs].style.display = vista;
        document.all[Img].src = strImage;

    }
    else if (document.getElementById) {
        vista = (document.getElementById(Obs).style.visibility == 'none') ? 'block' : 'none';
        strImage = (document.getElementById(Obs).style.visibility == 'none') ? UrlImageHidden : UrlImageShow;
        document.getElementById(Obs).style.visibility = vista;
        document.getElementById(Img).src = strImage;
    }
}

function InhabilitarControl(Obs, div2) {

    if (document.layers) {
        vista = (document.layers[Obs].display == 'none') ? 'block' : 'none'
        document.layers[Obs].visibility = vista;

        vista1 = (document.all[div2].style.display == 'none') ? 'block' : 'none';
        document.all[div2].style.display = vista1;

    }
    else if (document.all) {
        vista = (document.all[Obs].style.display == 'none') ? 'block' : 'none';
        document.all[Obs].style.display = vista;

        vista1 = (document.all[div2].style.display == 'none') ? 'block' : 'none';
        document.all[div2].style.display = vista1;

    }
    else if (document.getElementById) {

        vista = (document.getElementById(Obs).style.display == 'none') ? 'block' : 'none';
        document.getElementById(Obs).style.display = vista;

        vista1 = (document.getElementById(div2).style.display == 'none') ? 'block' : 'none';
        document.getElementById(div2).style.display = vista1;

    }
}



function checkAll(op, miDiv) {

    var inputs = document.getElementsByTagName('input');
    var cuenta = 0;
    for (var i = 0; i < inputs.length; i++) {
        if (inputs[i].type == 'checkbox') {


            if (op != inputs[i] && (inputs[i].getAttribute('id').indexOf("Favorite") == -1)) {
                if (op.checked) {
                    inputs[i].checked = true;
                    cuenta++;
                }
                else
                    inputs[i].checked = false;

            }
        }
    }

    var oDiv = document.getElementById(miDiv);
    if (oDiv != null) {
        oDiv.style.display = cuenta > 0 ? 'block' : 'none';
    }
}



function ShowMenuBar(miDiv) {
    var oDiv = document.getElementById(miDiv);
    if (oDiv == null) return;
    var inputs = document.getElementsByTagName('input');
    var cuenta = 0;
    for (var i = 0; i < inputs.length; i++) {
        if (inputs[i].type == 'checkbox') {
            if (inputs[i].getAttribute('id').indexOf("Favorite") === -1)
                if (inputs[i].checked)
                    cuenta++;
        }
    }
    oDiv.style.display = cuenta > 0 ? 'block' : 'none';
}

function ShowMenuBarPwc(miDiv) {

    var oDiv = document.getElementById(miDiv);
    if (oDiv == null) return;
    var inputs = document.getElementsByTagName('input');
    var cuenta = 0;
    for (var i = 0; i < inputs.length; i++) {
        if (inputs[i].type == 'checkbox') {
            if (inputs[i].getAttribute('id').indexOf("chkSelect") > -1)
                if (inputs[i].checked)
                    cuenta++;
        }
    }

    oDiv.style.display = cuenta > 0 ? 'block' : 'none';
}

function soloNumeros(evt) {
    var nav4 = window.Event ? true : false;

    var key = nav4 ? evt.which : evt.keyCode;

    return (key <= 13 || (key >= 48 && key <= 57) || key == 44)
}