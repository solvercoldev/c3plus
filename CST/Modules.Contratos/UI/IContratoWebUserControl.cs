using System;
namespace Modules.Contratos.UI
{
    public interface IContratoWebUserControl
    {
        void LoadControlData();
        event Action RiseFatherPostback;
    }
}