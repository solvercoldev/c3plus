using System;
using System.ServiceModel;
using DistributedServices.Core.ErrorHandlers;
using DistributedServices.MainModule.IntanceProviders;
using Infraestructure.CrossCutting.Security.IServices;

namespace DistributedServices.MainModule
{
    [ApplicationErrorHandlerAttribute()]
    [UnityInstanceProviderServiceBehavior()]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class AuthenticationService : IAuthenticationService
    {
        #region Members

        private readonly IAutentication _autentication;

        #endregion

        #region Builders

        public AuthenticationService(IAutentication autentication)
        {
            if (autentication == null)
                throw new ArgumentNullException("autentication");

            _autentication = autentication;
        }

        #endregion

        #region IAutenticationService Members

        //public TBL_Maestra_Usuarios AuthenticatedUserByUserNamePassword(string userName, string password, bool persistLogin)
        //{
        //    return _autentication.AuthenticatedUser(userName, password, persistLogin);
        //}

        //public TBL_Maestra_Usuarios AuthenticatedUser(string userName, bool persistLogin)
        //{
        //    return _autentication.AuthenticatedUser(userName, persistLogin);
        //}

        //public TBL_Maestra_Usuarios AuthenticatedByUserId(int userId, bool persistLogin)
        //{
        //    return _autentication.AuthenticatedByUserId(userId, persistLogin);
        //}

        //public bool ValidarAutorizacion(string className)
        //{
        //    return _autentication.ValidarAutorizacion(className);
        //}

        //public int GetIdUserFromTicket()
        //{
        //    return _autentication.GetIdUserFromTicket();
        //}

        #endregion

        #region IDisposable Member

        public void Dispose()
        {
            GC.Collect();
        }

        #endregion
    }
}