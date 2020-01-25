using DojoMyBusiness.Notificacoes;
using System.Collections.Generic;

namespace DojoMyBusiness.Interfaces
{
    public interface INotificador
    {
        bool TemNotificacao();
        List<Notificacao> ObterNotificacoes();
        void Handle(Notificacao notificacao);
    }
}
