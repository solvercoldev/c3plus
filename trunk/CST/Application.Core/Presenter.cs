using System;

namespace Application.Core
{
    public abstract class Presenter<TView>
    {
        /// <summary>
        /// Propiedad que referencia la Vista.
        /// </summary>
        public TView View
        {
            protected get; set;
        }

        /// <summary>
        /// Metodo que debe implementarse en las clases que hereden de Presenter<T/>
        /// para que registren los eventos que deben utilizarse desde la vista.
        /// </summary>
        public abstract void SubscribeViewToEvents();

        /// <summary>
        /// Evento para cuando se necesite enviar un mensaje a la vista.
        /// </summary>
        public event EventHandler<MessageBoxEventArgs> MessageBox;
        
        /// <summary>
        /// Metodo que invoca al evento.
        /// </summary>
        /// <param name="e"></param>
        protected void InvokeMessageBox(MessageBoxEventArgs e)
        {
            var handler = MessageBox;
            if (handler != null) handler(this, e);
        }

        /// <summary>
        /// Evento utiliazdo para lanzar mensages a la vista.
        /// </summary>
        public event EventHandler<ViewResulteventArgs> ViewResult;

        /// <summary>
        /// Invoca el evento que lanza los mensajes a la view
        /// </summary>
        /// <param name="e"></param>
        protected void ReturnMessageView(ViewResulteventArgs e)
        {
            var handler = ViewResult;
            if (handler != null) handler(this, e);
        }

        /// <summary>
        /// evento que permite registrar un evento dentro del log de procesamiento de la aplicacion
        /// </summary>
        public event EventHandler<LogProcesamientoEventArgs> LogProcesamientoEvent;

        /// <summary>
        /// Metodo que utiliza el evento LogProcesamiento para dispararlo en la capa de presentaci�n
        /// </summary>
        /// <param name="e"></param>
        protected void CrearEntradaLogProcesamiento(LogProcesamientoEventArgs e)
        {
            var handler = LogProcesamientoEvent;
            if (handler != null) handler(this, e);
        }

        protected string UppercaseFirst(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }
            char[] a = s.ToCharArray();
            a[0] = char.ToUpper(a[0]);
            return new string(a);
        }

    }
}