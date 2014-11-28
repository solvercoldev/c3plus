#region Using

using System;
using System.Globalization;
using System.IO;
using System.Web;
using System.Text;
using System.Security.Cryptography;
using System.Web.Security;
using ASP.NETCLIENTE.Utils;
using Infrastructure.CrossCutting;
using Infrastructure.CrossCutting.IoC;
using Infrastructure.CrossCutting.Logging;

#endregion

namespace ASP.NETCLIENTE.HTTPModules
{
    /// <summary>
    /// Summary description for QueryStringModule
    /// </summary>
    public class QueryStringModule : IHttpModule
    {
        private ITraceManager _traceManager; 

        #region IHttpModule Members

        public void Dispose()
        {
            // Nothing to dispose
        }

        public void Init(HttpApplication context)
        {
            context.BeginRequest += ContextBeginRequest;
            context.EndRequest += ContextEndRequest;
            _traceManager = IoC.Resolve<ITraceManager>();
        }

        #endregion

        private const string ParameterName = "enc=";
        private const string ItemParentName = "KUS=";
        private const string EncryptionKey = "key";

        void ContextBeginRequest(object sender, EventArgs e)
        {
            var context = HttpContext.Current;
            context.Items["RequestStart"] = DateTime.Now;

            ValidarUrl(context);
        }


        private static void ValidarUrl(HttpContext context)
        {
            if (context.Request.Url.OriginalString.Contains("aspx") && context.Request.RawUrl.Contains("?"))
            {
                var query = ExtractQuery(context.Request.RawUrl);
                var path = GetVirtualPath();
               
                if (query.StartsWith(ParameterName, StringComparison.OrdinalIgnoreCase))
                {
                    var rawQuery = query.Replace(ParameterName, string.Empty);
                    var decryptedQuery = Decrypt(rawQuery);
                    context.RewritePath(path, string.Empty, decryptedQuery);
                }
                else if (context.Request.HttpMethod == "GET")
                {
                   
                    var encryptedQuery = Encrypt(query);
                    context.Response.Redirect(path + encryptedQuery);
                }
            }
        }

       /// <summary>
       /// 
       /// </summary>
       /// <returns></returns>

        private static string GetVirtualPath()
        {
            var path = HttpContext.Current.Request.RawUrl;
            path = path.Substring(0, path.IndexOf("?"));
            path = path.Substring(path.LastIndexOf("/") + 1);
            return path;
        }

        private static string GetVirtualPathNotQuery()
        {
            var path = HttpContext.Current.Request.Path;
            var recurso = path.Substring(path.LastIndexOf("/") + 1);
            return recurso;
        }

        /// <summary>
        /// Obtiene el queryString de la URL
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private static string ExtractQuery(string url)
        {
            var index = url.IndexOf("?") + 1;
            return url.Substring(index);
        }

        /// <summary>
        /// Obtiene el Valor del QueryString Solamente cuando es Key es itemParent
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private static string ExtractQueryValue(string url)
        {
            var index = url.IndexOf("=") + 1;
            return url.Substring(index);
        }
        #region Encryption/decryption

        /// <summary>
        /// 
        /// </summary>
        private readonly static byte[] Salt = Encoding.ASCII.GetBytes(EncryptionKey.Length.ToString());

        /// <summary>
        /// Encripta la cadena usando el algoritmo  Rijndael.
        /// </summary>
        /// <param name="inputText">Cadena a encriptar.</param>
        /// <returns>Cadena encriptada en Base64.</returns>
        private static string Encrypt(string inputText)
        {
            var rijndaelCipher = new RijndaelManaged();
            var plainText = Encoding.Unicode.GetBytes(inputText);
            var secretKey = new PasswordDeriveBytes(EncryptionKey, Salt);

            using (var encryptor = rijndaelCipher.CreateEncryptor(secretKey.GetBytes(32), secretKey.GetBytes(16)))
            {
                using (var memoryStream = new MemoryStream())
                {
                    using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        cryptoStream.Write(plainText, 0, plainText.Length);
                        cryptoStream.FlushFinalBlock();
                        return "?" + ParameterName + Convert.ToBase64String(memoryStream.ToArray());
                    }
                }
            }
        }

        /// <summary>
        /// Desencripta la cadena que previamente fue encriptada
        /// </summary>
        /// <param name="inputText">Cadena Encriptada</param>
        /// <returns>Una Cadena sesencriptada.</returns>
        private static string Decrypt(string inputText)
        {
            try
            {
                var rijndaelCipher = new RijndaelManaged();
                var encryptedData = Convert.FromBase64String(inputText.Replace(" ","+"));
                var secretKey = new PasswordDeriveBytes(EncryptionKey, Salt);

                using (var decryptor = rijndaelCipher.CreateDecryptor(secretKey.GetBytes(32), secretKey.GetBytes(16)))
                {
                    using (var memoryStream = new MemoryStream(encryptedData))
                    {
                        using (var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                        {
                            var plainText = new byte[encryptedData.Length];
                            var decryptedCount = cryptoStream.Read(plainText, 0, plainText.Length);
                            return Encoding.Unicode.GetString(plainText, 0, decryptedCount);
                        }
                    }
                }
            }
            catch 
            {
                //Se envia este codigo de error cuando el usuario intenta manipular el QueryString y el metodo 
                //para Descifrar no es capaz de efectuar la operacion.. 
                return "enc=-1";
            }
      
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ContextEndRequest(object sender, EventArgs e)
        {
            // Log duration
            var context = ((HttpApplication)sender).Context;
            var rawUrl = context.Request.RawUrl;
            var startTime = (DateTime)context.Items["RequestStart"];
            var duration = DateTime.Now - startTime;
            //_traceManager.LogInfo(string.Format(CultureInfo.InvariantCulture,
            //                            "Solicitud Finalizada para el recurso [{0}]. Duración Total: {1} ms.",
            //                            rawUrl,
            //                            duration.Milliseconds),
            //                            LogType.Notify);
        }

        #endregion

    }
}
