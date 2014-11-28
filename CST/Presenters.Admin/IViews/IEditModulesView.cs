using System;
using System.Collections.Generic;
using Application.Core;
using Domain.MainModules.Entities;

namespace Presenters.Admin.IViews
{
    public interface IEditModulesView : IView
    {
        #region events

        event EventHandler SelectedModuleEvent;

        event EventHandler EditSectionEvent;

        event EventHandler SaveEvent;

        event EventHandler DeleteEvent;

        event EventHandler ModulesEvent;

        event EventHandler ComponentsEvent;

        

        #endregion

        void ListadoModulos(List<TBL_Admin_Modulos> items);

        void ListadoSecciones(List<TBL_Admin_TypeByModules> items);

        void CargarSeccionById(List<TBL_Admin_TypeByModules> items);

        void ListadoModulosSeccion(List<TBL_Admin_Modulos> items);

        void ListadoComponentesSeccion(List<TBL_Admin_ModuleType> items);

        bool MostrarTitulo { get; set; }

        string PlaceHolder { get; set; }

        int Position { get; set; }

        string Titulo { get; set; }

        int IdComponente { get; set; }

        int? IdModulo { get; set; }

        string IdSeccion { get; set; }

        string PathPreView { get; set; }

        bool Activar { get; set; }
    }
}