using System;
using Application.Core;
using Application.MainModule.Contratos.IServices;
using Infrastructure.CrossCutting.NetFramework.Enums;
using Presenters.Admin.IViews;

namespace Presenters.Admin.Presenters
{
    public class FrmEditTerceroPresenter : Presenter<IFrmEditTerceroView>
    {
        private readonly ISfTercerosManagementServices _terceros;

        public FrmEditTerceroPresenter(ISfTercerosManagementServices tercero)
        {
            _terceros = tercero;
        }

        public override void SubscribeViewToEvents()
        {
            View.Load += ViewLoad;
            View.SaveEvent += ViewSaveEvent;
            View.DeleteEvent += ViewDeleteEvent;
            View.ActualizarEvent += ViewActEvent;
        }

        void ViewLoad(object sender, EventArgs e)
        {
            if (View.IsPostBack) return;
            Load();
        }

        void ViewSaveEvent(object sender, EventArgs e)
        {
            GuardarTercero();
        }

        void ViewActEvent(object sender, EventArgs e)
        {
            ActualizarTercero();
        }

        void ViewDeleteEvent(object sender, EventArgs e)
        {
            EliminarTercero();
        }


        private void Load()
        {
            if (string.IsNullOrEmpty(View.IdTercero)) return;

            var tercero = _terceros.GetById(View.IdTercero);

            if (tercero == null) return;

            View.IdTercero = tercero.IdTercero;
            View.Nombre = tercero.Nombre;
        }

        private void GuardarTercero()
        {

            try
            {
                var tercero = _terceros.NewEntity();
                tercero.IdTercero = View.IdTercero;
                tercero.Nombre = View.Nombre;

                _terceros.Add(tercero);
                InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.ProcessOk), TypeError.Ok));
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
                InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.SaveError), TypeError.Error));
            }

        }

        private void ActualizarTercero()
        {

            try
            {

                if (View.IdTercero == "") return;
                var tercero = _terceros.GetById(View.IdTercero);
                if (tercero == null) return;

                tercero.Nombre = View.Nombre;

                _terceros.Modify(tercero);

                InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.ProcessOk), TypeError.Ok));
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
                InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.EditError), TypeError.Error));
            }
        }

        /// <summary>
        /// Elimina la plantilla seleccionada en Base de Datos
        /// </summary>
        private void EliminarTercero()
        {
            try
            {
                if (View.IdTercero == "") return;
                var tercero = _terceros.GetById(View.IdTercero);
                if (tercero == null) return;
                _terceros.Remove(tercero);
                InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.ProcessOk), TypeError.Ok));
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
                InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.DeleteError), TypeError.Error));
            }
        }
    }
}