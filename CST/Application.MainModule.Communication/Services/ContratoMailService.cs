using System;
using System.Collections.Generic;
using Application.MainModule.Communication.IServices;
using Domain.Core.Specification;
using Domain.MainModule.Contracts;
using Domain.MainModules.Entities;
using Infraestructure.CrossCutting.NetCommunication;
using Infrastructure.CrossCutting.Logging;
using System.Configuration;
using Infrastructure.CrossCutting;
using Domain.MainModule.Contratos.Contracts;

namespace Application.MainModule.Communication.Services
{
    public class ContratoMailService : IContratoMailService
    {
        #region Members

        readonly ITraceManager _iTraceManager;
        readonly IMailHelper _iMailHelper;
        readonly ICompromisosRepository _compromisoRepository;
        readonly IRadicadosRepository _radicadoRepository;
        readonly ITBL_Admin_OptionListRepository _optionListRepository;

        #endregion

        #region Builders

        public ContratoMailService(ITraceManager iTraceManager, IMailHelper iMailHelper,
                                   ICompromisosRepository compromisoRepository, IRadicadosRepository radicadoRepository, ITBL_Admin_OptionListRepository optionListRepository)
        {
            _iTraceManager = iTraceManager;
            _iMailHelper = iMailHelper;
            _compromisoRepository = compromisoRepository;
            _radicadoRepository = radicadoRepository;
            _optionListRepository = optionListRepository;
        }

        #endregion

        #region Methos

        TBL_Admin_OptionList ObtenerOpcionBykey(string key)
        {
            Specification<TBL_Admin_OptionList> specification = new DirectSpecification<TBL_Admin_OptionList>(u => u.Key.Equals(key));

            return _optionListRepository.GetEntityBySpec(specification);

        }

        Compromisos GetCompromiso(long idCompromiso)
        {
            Specification<Compromisos> specification = new DirectSpecification<Compromisos>(u => u.IdCompromiso == idCompromiso);

            return _compromisoRepository.GetCompleteEntity(specification);
        }

        Radicados GetRadicado(long idRadicado)
        {
            Specification<Radicados> specification = new DirectSpecification<Radicados>(u => u.IdRadicado == idRadicado);

            return _radicadoRepository.GetCompleteEntity(specification);
        }

        #endregion

        #region IContratoMailService Members

        public void SendCompromisoMailNotification(object parameters)
        {
            var arrayParameters = (Array)parameters;
            long idCompromiso = (long)arrayParameters.GetValue(0);
            int idModule = (int)arrayParameters.GetValue(1);
            string baseUrl = (string)arrayParameters.GetValue(2);

            var compromiso = GetCompromiso(idCompromiso);

            if (compromiso == null)
                return;

            var opBody = ObtenerOpcionBykey("Compromiso_BodyNotification");

            var body = opBody.Value;

            body = body.Replace("[#Contrato]", compromiso.Fases.Contratos.NumeroContrato)
                       .Replace("[#Fase]", compromiso.Fases.Nombre)
                       .Replace("[#Bloque]", compromiso.Fases.Contratos.Bloques.Descripcion)
                       .Replace("[#FechaCompromiso]", string.Format("{0:dd/MM/yyyy}", compromiso.CreateOn))
                       .Replace("[#FechaVencimiento]", string.Format("{0:dd/MM/yyyy}", compromiso.FechaCumplimiento))
                       .Replace("[#Observaciones]", compromiso.Descripcion)
                       .Replace("[#UrlBase]", string.Format("{0}/Pages/Modules/Contratos/Admin/FrmAdminCompromisoContrato.aspx?ModuleId={1}&IdContrato={2}&IdCompromiso={3}&from=miscompendiente",
                                                            baseUrl, idModule, compromiso.Fases.IdContrato, compromiso.IdCompromiso));


            _iMailHelper.SMTP_Host = ConfigurationManager.AppSettings.Get("host");
            _iMailHelper.SMTP_EnableSSL = Convert.ToBoolean(ConfigurationManager.AppSettings.Get("enableSsl"));
            _iMailHelper.SMTP_User = ConfigurationManager.AppSettings.Get("smtpUsername");
            _iMailHelper.SMTP_Password = ConfigurationManager.AppSettings.Get("smtpPassword");
            _iMailHelper.SMTP_Port = Convert.ToInt32(ConfigurationManager.AppSettings.Get("port"));
            _iMailHelper.SMTP_From = ConfigurationManager.AppSettings.Get("mailFrom");
            _iMailHelper.SMTP_Subject = string.Format("Asignación Compromiso Contrato: [{0}], Fase: [{1}], Bloque: [{2}]",
                                                        compromiso.Fases.Contratos.NumeroContrato,
                                                        compromiso.Fases.Nombre,
                                                        compromiso.Fases.Contratos.Bloques.Descripcion);
            _iMailHelper.SMTP_Body = body;
            _iMailHelper.SMTP_To = new string[] { compromiso.TBL_Admin_Usuarios.Email };

            try
            {
                _iMailHelper.SendMail();
            }
            catch (Exception ex)
            {
                _iTraceManager.LogInfo(string.Format("Error al enviar mail de notificación de compromiso.Cls:ContratoMailService,Mtd:SendCompromisoMailNotification, Error: {0}", ex.InnerException == null ? ex.Message : ex.InnerException.Message), LogType.Notify);
            }
        }

        public void SendRadicadoMailNotification(object parameters)
        {
            var arrayParameters = (Array)parameters;
            long idRadicado = (long)arrayParameters.GetValue(0);
            int idModule = (int)arrayParameters.GetValue(1);
            string baseUrl = (string)arrayParameters.GetValue(2);

            var radicado = GetRadicado(idRadicado);

            if (radicado == null)
                return;

            var opBody = ObtenerOpcionBykey("Radicado_BodyNotification");

            var body = opBody.Value;

            body = body.Replace("[#Contrato]", radicado.Contratos.NumeroContrato)
                       .Replace("[#Bloque]", radicado.Contratos.Bloques.Descripcion)
                       .Replace("[#FechaRadicado]", string.Format("{0:dd/MM/yyyy}", radicado.CreateOn))
                       .Replace("[#FechaVencimiento]", string.Format("{0:dd/MM/yyyy}", radicado.FechaRespuesta))
                       .Replace("[#Observaciones]", string.Format("{0} - {1}", radicado.Asunto, radicado.Resumen))
                       .Replace("[#UrlBase]", string.Format("{0}/Pages/Modules/Contratos/Admin/FrmAdminRadicadoContrato.aspx?ModuleId={1}&IdContrato={2}&IdRadicado={3}&from=misradpendiente",
                                                            baseUrl, idModule, radicado.IdContrato, radicado.IdRadicado));


            _iMailHelper.SMTP_Host = ConfigurationManager.AppSettings.Get("host");
            _iMailHelper.SMTP_EnableSSL = Convert.ToBoolean(ConfigurationManager.AppSettings.Get("enableSsl"));
            _iMailHelper.SMTP_User = ConfigurationManager.AppSettings.Get("smtpUsername");
            _iMailHelper.SMTP_Password = ConfigurationManager.AppSettings.Get("smtpPassword");
            _iMailHelper.SMTP_Port = Convert.ToInt32(ConfigurationManager.AppSettings.Get("port"));
            _iMailHelper.SMTP_From = ConfigurationManager.AppSettings.Get("mailFrom");
            _iMailHelper.SMTP_Subject = string.Format("Asignación Radicado Contrato: [{0}], Bloque: [{1}]",
                                                        radicado.Contratos.NumeroContrato,
                                                        radicado.Contratos.Bloques.Descripcion);
            _iMailHelper.SMTP_Body = body;
            _iMailHelper.SMTP_To = new string[] { radicado.TBL_Admin_Usuarios2.Email };

            try
            {
                _iMailHelper.SendMail();
            }
            catch (Exception ex)
            {
                _iTraceManager.LogInfo(string.Format("Error al enviar mail de notificación de radicado.Cls:ContratoMailService,Mtd:SendRadicadoMailNotification, Error: {0}", ex.InnerException == null ? ex.Message : ex.InnerException.Message), LogType.Notify);
            }
        }

        #endregion        
    }
}