using System.Web;
using System.Web.UI;
using System.Collections.Generic;

namespace ASP.NETCLIENTE.Utils
{
    public class ValidationError : IValidator
    {
        private ValidationError(string message)
        {
            ErrorMessage = message;
            IsValid = false;
        }

        public string ErrorMessage { get; set; }

        public bool IsValid { get; set; }

        public void Validate()
        {
            // no action required
        }

        public static void Display(List<string> messages)
        {
            Page currentPage = HttpContext.Current.Handler as Page;
            foreach (var msg in messages)
            {
                currentPage.Validators.Add(new ValidationError(msg));
            }            
        }
    }
}