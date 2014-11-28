using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using Application.Core;
using Applications.MainModule.Admin.IServices;
using Applications.MainModule.Admin.IServices;
using Infrastructure.CrossCutting.NetFramework.Enums;
using Presenters.Admin.IViews;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

namespace Presenters.Admin.Presenters
{
    public class EditOptionListPresenter : Presenter<IEditOptionListView>
    {
        private readonly ISfTBL_Admin_OptionListManagementServices _optionList;

        public EditOptionListPresenter(ISfTBL_Admin_OptionListManagementServices optionList)
        {
            _optionList = optionList;
        }

        public override void SubscribeViewToEvents()
        {
            View.Load += ViewLoad;
            View.SaveEvent += ViewSaveEvent;
        }

        void ViewLoad(object sender, EventArgs e)
        {
            if (View.IsPostBack) return;
            Load();
        }

        void ViewSaveEvent(object sender, EventArgs e)
        {
            GuardarOptionList();
        }

        private void Load()
        {
            GetOptionList();
        }

        private void GetOptionList()
        {
            try
            {
                if (string.IsNullOrEmpty(View.IdOpcion)) return;
                var op = _optionList.FindById(Convert.ToInt32(View.IdOpcion));
                if (op == null) return;
                View.IdModulo = op.IdModule.ToString();
                View.key = op.Key;
                View.value = op.Value;
                View.descripcion = op.Descripcion;
                View.Activo = op.IsActive;
                View.CreateBy = op.CreateBy;
                View.CreateOn = op.CreateOn != null ? op.CreateOn.GetValueOrDefault().ToShortDateString() : string.Empty;
                View.ModifiedBy = op.ModifiedBy;
                View.ModifiedOn = op.ModifiedOn != null ? op.ModifiedOn.GetValueOrDefault().ToShortDateString() : string.Empty;
           
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
                InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.SaveError), TypeError.Error));
            }


        }

        /// <summary>
        /// Guarda un item de Option List en Base de datos.
        /// </summary>
        private void GuardarOptionList()
        {
            try
            {
                if (string.IsNullOrEmpty(View.IdOpcion)) return;

                var op = _optionList.FindById(Convert.ToInt32(View.IdOpcion));

                if (op == null) return;
                op.Value = View.value;
                op.Descripcion = View.descripcion;
                op.IsActive = View.Activo;
                op.ModifiedBy = View.UserSession.IdUser.ToString();
                op.ModifiedOn = DateTime.Now;

                _optionList.Modify(op);
                InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.ProcessOk), TypeError.Ok));
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
                InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.SaveError), TypeError.Error));
            }

        }
    }
}
