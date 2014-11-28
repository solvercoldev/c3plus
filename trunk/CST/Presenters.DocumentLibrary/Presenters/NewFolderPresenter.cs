using System;
using System.Reflection;
using Applcations.MainModule.DocumentLibrary.IServices;
using Application.Core;
using Infrastructure.CrossCutting.NetFramework.Enums;
using Presenters.DocumentLibrary.IViews;

namespace Presenters.DocumentLibrary.Presenters
{
    public class NewFolderPresenter : Presenter<INewFolderView>
    {
        private readonly ISfTBL_ModuloDocumentosAnexos_CarpetasManagementServices _carpetasServices;

        public NewFolderPresenter(
            ISfTBL_ModuloDocumentosAnexos_CarpetasManagementServices carpetasServices)
        {
            _carpetasServices = carpetasServices;
        }

        public override void SubscribeViewToEvents()
        {
            View.SaveEvent += ViewSaveEvent;
        }

        void ViewSaveEvent(object sender, EventArgs e)
        {
            Save();
        }

       
        private void Save()
        {
            try
            {
                _carpetasServices.SaveFolder(View.IdParent, string.Empty, View.NombreFolder, View.IdContrato,
                                             View.UserSession.IdUser.ToString());
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }
    }
}