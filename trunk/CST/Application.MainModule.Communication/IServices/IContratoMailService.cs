namespace Application.MainModule.Communication.IServices
{
    public interface IContratoMailService
    {
        void SendCompromisoMailNotification(object parameters);
        void SendRadicadoMailNotification(object parameters);
    }
}