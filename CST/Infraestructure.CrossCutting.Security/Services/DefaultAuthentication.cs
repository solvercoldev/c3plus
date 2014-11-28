namespace Infraestructure.CrossCutting.Security.Services
{
    public class DefaultAuthentication //: IAutentication
    {
    
        //#region Fields

        //private readonly ITBL_Maestra_UsuariosRepository _userRepository;
        //private readonly ITBL_Maestra_OpcionesMenuRepository _nodeRepository;
        //private readonly ITraceManager _traceManager;

        //#endregion

        //public DefaultAuthentication(ITraceManager traceManager,
        //                             ITBL_Maestra_UsuariosRepository userRepository,
        //                             ITBL_Maestra_OpcionesMenuRepository nodeRepository)
        //{
        //    _traceManager = traceManager;
        //    _nodeRepository = nodeRepository;
        //    _userRepository = userRepository;
        //}

        //public TBL_Maestra_Usuarios AuthenticatedUser(string userName, string password, bool persistLogin)
        //{
        //    var spec = new UserCodeSpecification(userName, password);
        //    var user = _userRepository.FindTblMaestraUsuariosByName(spec);
        //    if(user != null)
        //    {
        //        if(!user.Activo)
        //        {
        //          _traceManager.LogInfo(string.Format(CultureInfo.InvariantCulture,
        //                                "El usuario {0} se encuentra inactivo. Trato de iniciar sesión.",
        //                                user.NombreUsuario),
        //                                LogType.Notify);
        //          throw new ArgumentNullException("AuthenticatedUser", 
        //              string.Format("El usuario {0} que se encuentra inactivo trato de iniciar sesión.",
        //              user.NombreUsuario));
        //        }

        //        if (user.TBL_Maestra_Roles.Count == 0)
        //        {
        //            throw new ArgumentNullException("RoleNotFound", 
        //              string.Format("El usuario {0} no tiene un rol asociado.",
        //              user.NombreUsuario));
        //        }

        //        user.IsAuthenticated = true;
        //        var currentIp = HttpContext.Current.Request.UserHostAddress;
        //        user.lastlogin = DateTime.Now;
        //        user.lastip = currentIp;
        //        //// Save login date and IP
        //        var unitOfWork = _userRepository.UnitOfWork;
        //        _userRepository.Modify(user);
        //        unitOfWork.CommitAndRefreshChanges();

        //        //// Create the authentication ticket
        //        var roles = user.TBL_Maestra_Roles.Aggregate("", (current, r) => current + (r.NombreRol + "|"));

        //        var authTicket = new FormsAuthenticationTicket(
        //            user.IdUsuario,                 // version
        //            userName,                   // user name
        //            DateTime.Now,               // creation
        //            DateTime.Now.AddMinutes(20),// Expiration
        //            false,                      // Persistent
        //            roles.Substring(0, roles.Length-1)); // User data

        //        //// Encrypt the ticket.
        //        var encryptedTicket = FormsAuthentication.Encrypt(authTicket);
        //        //// Create a cookie and add the encrypted ticket to the cookie as data.
        //        var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
        //        //// Add the cookie to the outgoing cookies collection. 
        //        HttpContext.Current.Response.Cookies.Add(authCookie);
                
        //        //HttpContext.Current.User = new SolutionFrameworkPrincipal(user);
        //        //FormsAuthentication.SetAuthCookie(user.Name, persistLogin);
        //        return user;
        //    }

        //    _traceManager.LogInfo(string.Format(CultureInfo.InvariantCulture,
        //                                "Combinación Login - Password es inválida : {0} - {1}",
        //                                userName,password),
        //                                LogType.Notify);
        //    return null;
        //}
        
        //public TBL_Maestra_Usuarios AuthenticatedUser(string userName, bool persistLogin)
        //{
        //    var spec = new UserCodeSpecification(userName, "");
        //    var user = _userRepository.FindTblMaestraUsuariosByName(spec);
        //    if (user != null)
        //    {
        //        if (!user.Activo)
        //        {
        //            _traceManager.LogInfo(string.Format(CultureInfo.InvariantCulture,
        //                                  "El usuario {0} se encuentra inactivo. Trato de iniciar sesión.",
        //                                  user.NombreUsuario),
        //                                  LogType.Notify);
        //            throw new ArgumentNullException("AuthenticatedUser",
        //                string.Format("El usuario {0} se encuentra inactivo. Trato de iniciar sesión.",
        //                user.NombreUsuario));
        //        }

        //        if (user.TBL_Maestra_Roles.Count == 0)
        //        {
        //            throw new ArgumentNullException("RoleNotFound",
        //              string.Format("El usuario {0} no tiene un rol asociado.",
        //              user.NombreUsuario));
        //        }

        //        user.IsAuthenticated = true;
        //        //var currentIp = HttpContext.Current.Request.UserHostAddress;
        //        user.lastlogin = DateTime.Now;
        //        //user.lastip = currentIp;
        //        //// Save login date and IP
        //        var unitOfWork = _userRepository.UnitOfWork;
        //        _userRepository.Modify(user);
        //        unitOfWork.CommitAndRefreshChanges();                

        //        return user;
        //    }

        //    _traceManager.LogInfo(string.Format(CultureInfo.InvariantCulture,
        //                               "El nombre de Usuario es inválido : {0}",
        //                               userName),
        //                               LogType.Notify);
        //    return null;
        //}

        //public TBL_Maestra_Usuarios AuthenticatedByUserId(int userId, bool persistLogin)
        //{
        //    var specification = new DirectSpecification<TBL_Maestra_Usuarios>(u => u.Activo  && u.IdUsuario == userId);
        //    var user = _userRepository.FindTblMaestraUsuariosByName(specification);
        //    if (user != null)
        //    {
        //        if (!user.Activo)
        //        {
        //            _traceManager.LogInfo(string.Format(CultureInfo.InvariantCulture,
        //                                  "El usuario {0} se encuentra inactivo. Trato de iniciar sesión.",
        //                                  user.NombreUsuario),
        //                                  LogType.Notify);
        //            throw new ArgumentNullException("AuthenticatedUser",
        //                string.Format("El usuario {0} se encuentra inactivo. Trato de iniciar sesión.",
        //                user.NombreUsuario));
        //        }

        //        if (user.TBL_Maestra_Roles.Count == 0)
        //        {
        //            throw new ArgumentNullException("RoleNotFound",
        //              string.Format("El usuario {0} no tiene un rol asociado.",
        //              user.NombreUsuario));
        //        }

        //        user.IsAuthenticated = true;
        //        var currentIp = HttpContext.Current.Request.UserHostAddress;
        //        user.lastlogin = DateTime.Now;
        //        user.lastip = currentIp;
        //        //// Save login date and IP
        //        var unitOfWork = _userRepository.UnitOfWork;
        //        _userRepository.Modify(user);
        //        unitOfWork.CommitAndRefreshChanges();
        //        //// Create the authentication ticket
        //        //HttpContext.Current.User = new SolutionFrameworkPrincipal(user);
        //        //FormsAuthentication.SetAuthCookie(user.Name, persistLogin);
        //        //return user;
        //        //// Create the authentication ticket
        //        var roles = user.TBL_Maestra_Roles.Aggregate("", (current, r) => current + (r.NombreRol + "|"));

        //        var authTicket = new FormsAuthenticationTicket(
        //            user.IdUsuario,                 // version
        //            userId.ToString(),          // user name
        //            DateTime.Now,               // creation
        //            DateTime.Now.AddMinutes(20),// Expiration
        //            false,                      // Persistent
        //            roles.Substring(0, roles.Length - 1));                        // User data

        //        //// Encrypt the ticket.
        //        var encryptedTicket = FormsAuthentication.Encrypt(authTicket);
        //        //// Create a cookie and add the encrypted ticket to the cookie as data.
        //        var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
        //        //// Add the cookie to the outgoing cookies collection. 
        //        HttpContext.Current.Response.Cookies.Add(authCookie);

        //        //HttpContext.Current.User = new SolutionFrameworkPrincipal(user);
        //        //FormsAuthentication.SetAuthCookie(user.Name, persistLogin);
        //        return user;
        //    }

        //    _traceManager.LogInfo(string.Format(CultureInfo.InvariantCulture,
        //                               "El Id de Usuario es inválido : {0}",
        //                               userId),
        //                               LogType.Notify);
        //    return null;
        //}

        //public bool ValidarAutorizacion(string className)
        //{
        //    try
        //    {
                  
        //        if (string.IsNullOrEmpty(className)) throw new ArgumentNullException("className");
               
        //        var spec = new DirectSpecification<TBL_Maestra_OpcionesMenu>(u => u.LinkUrl.ToUpper().Equals(className.ToUpper()));
        //        var node = _nodeRepository.FindNodeBySpec(spec);

        //        return node != null && node.TBL_Maestra_Roles.Any(rol => HttpContext.Current.User.IsInRole(rol.NombreRol));
        //    }
        //    catch (Exception)
        //    {
        //        _traceManager.LogInfo(string.Format(CultureInfo.InvariantCulture,
        //                                "error en la validacion de la autorización"),
        //                                LogType.Notify);
        //    }
        //    return false;
        //}

        //public int GetIdUserFromTicket()
        //{
        //    var formsIdentity = (FormsIdentity)HttpContext.Current.User.Identity;
        //    return formsIdentity.Ticket.Version;
        //}
    }
}
