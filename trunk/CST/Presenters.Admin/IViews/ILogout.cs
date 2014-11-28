namespace Presenters.Admin.IViews
{
    public interface ILogoutView : Application.Core.IView
    {
       string User { set; }
        string Role { set; }

    }
}