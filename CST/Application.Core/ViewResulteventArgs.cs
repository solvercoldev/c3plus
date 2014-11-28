using System;

namespace Application.Core
{
    public class ViewResulteventArgs : EventArgs
    {
        private readonly object _message ;
        
        public ViewResulteventArgs(object message)
        {
            _message = message;
        }

        public object MessageView { get { return _message; } }
        public object Sender { get; set; }
    }
}