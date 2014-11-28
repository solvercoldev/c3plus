using System;
using Domain.MainModules.Entities;

namespace Application.Core
{
    public interface IView
    {
        /// <summary>
        /// Evento para cuando la vista es inicializada.
        /// </summary>
        event EventHandler Init;

        /// <summary>
        /// Evento para cuando la vista es cargada. 
        /// </summary>
        event EventHandler Load;

       
        /// <summary>
        /// Determina si la solicitud actual es una devolucion de datos
        /// </summary>
        bool IsPostBack { get; }

        /// <summary>
        ///Indica que la vista se deber actualizar por medio del enlace de datos.
        /// </summary>
        void DataBind();

        /// <summary>
        /// Usuario Session
        /// </summary>
        TBL_Admin_Usuarios UserSession { get; }

        string IdModule { get; }
    }
}