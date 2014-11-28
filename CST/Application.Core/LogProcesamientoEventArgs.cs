using System;
using Infrastructure.CrossCutting.NetFramework.Enums;

namespace Application.Core
{
    public class LogProcesamientoEventArgs : EventArgs
    {
        private readonly Exception _error;
        private readonly string _nombreMetodo;
        private readonly Logtype _tipoLog;
        private readonly string _idPedido;
        private readonly Acciones _accion;
        /// <summary>
        /// Crea una entrada en el archivo Log.txt de la aplicación
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="nombreMetodo"></param>
        /// <param name="tipoLog"></param>
        public LogProcesamientoEventArgs(Exception ex, string nombreMetodo, Logtype tipoLog)
        {
            _error = ex;
            _tipoLog = tipoLog;
            _nombreMetodo = nombreMetodo;
        }

        /// <summary>
        /// Constructor utilizado para inicializar las variables con el fin de insertar un nuevo registro en la Base de Datos.
        /// </summary>
        /// <param name="idPedido"></param>
        /// <param name="tipoLog"></param>
        /// <param name="accion"></param>
        public LogProcesamientoEventArgs(string idPedido, Logtype tipoLog,Acciones accion)
        {
            _idPedido = idPedido;
            _tipoLog = tipoLog;
            _accion = accion;
        }

        public Acciones LogAccion
        {
            get { return _accion; }
        }

        public string IdPedido
        {
            get { return _idPedido; }
        }

        public Logtype TipoLog
        {
            get { return _tipoLog; }
        }
        public Exception Error
        {
            get { return _error; }
        }

        public string NombreMetodo
        {
            get { return _nombreMetodo; }
        }
    }

    
}