using System;
using System.Collections.Generic;

namespace Infrastructure.CrossCutting.IDtoService
{
    public interface IDocumentDto
    {
        string TextControl { get; set; }

        string CurrentStatus { get; set; }

        string IdCurrentStatus { get; set; }

        string NextStatus { get; set; }

        string IdNextStatus { get; set; }

        string IdDocument { get; set; }

        string NextResponsibe { get; set; }

        string EmailNextResponsibe { get; set; }

        string IdNextResponsibe { get; set; }

        string CurrentResponsibe { get; set; }

        string EmailCurrentResponsibe { get; set; }

        string IdCurrentResponsibe { get; set; }

        string OrdenCompra { get; set; }

        string NumeroPedido { get; set; }

        string Cliente { get; set; }

        /// <summary>
        /// Propiedad que permite identificar si el WF se ejecut� de forma apropiada o presento algun error tanto en la validaci�n como en la ejecuci�n
        /// de alguno de sus m�todos.
        /// </summary>
        String Processestaus { get; set; }

        /// <summary>
        /// Listado que permite obtener los mensajes asociados a los errores como producto de la ejecici�n del WF
        /// </summary>
        List<string> MessagesError { get; set; }

        /// <summary>
        /// Lista que permite enviar a la lista los nombres de las Ventanas que permiten capturar par�metros para que pueda seguir la ejecuci�n del WF
        /// </summary>
        List<string> OutputParameters { get; set; }

        /// <summary>
        /// Diccionario que permite enviar al servicios lo capturado por la ventana "InputParameters" etiquetado con el Key y su valor respectivo
        /// </summary>
        Dictionary<string, string> Parameters { get; set; }
    }
}