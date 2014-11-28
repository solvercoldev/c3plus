using System;
using System.Collections.Generic;
using System.ServiceModel;

using DistributedServices.Core.ErrorHandlers;
using DistributedServices.MainModule.IntanceProviders;
using Domain.MainModules.Entities;

namespace DistributedServices.MainModule
{
    [ServiceContract]
    public interface IAuthenticationService : IDisposable
    {
        //[OperationContract]
        //[FaultContract(typeof(ApplicationServiceError))]
        //TBL_Maestra_Usuarios AuthenticatedUserByUserNamePassword(string userName, string password, bool persistLogin);

        //[OperationContract]
        //[FaultContract(typeof(ApplicationServiceError))]
        //TBL_Maestra_Usuarios AuthenticatedUser(string userName, bool persistLogin);

        //[OperationContract]
        //[FaultContract(typeof(ApplicationServiceError))]
        //TBL_Maestra_Usuarios AuthenticatedByUserId(int userId, bool persistLogin);

        //[OperationContract]
        //[FaultContract(typeof(ApplicationServiceError))]
        //bool ValidarAutorizacion(string className);

        //[OperationContract]
        //[FaultContract(typeof(ApplicationServiceError))]
        //int GetIdUserFromTicket();
    }
}