using System;

namespace Application.Core
{
    public enum TypeError
    {
        Ok,
        Error
    }

    public class MessageBoxEventArgs : EventArgs
    {
        private readonly string _mensaje;
        private readonly TypeError _error;
        

        public MessageBoxEventArgs(string mensaje, TypeError error)
        {
            _mensaje = mensaje;
            _error = error;
        }
        public string Message { get { return _mensaje; } }
        public TypeError Tipo { get { return _error; } }
        public object Sender { get; set; }
    }
}