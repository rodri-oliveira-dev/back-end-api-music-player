using DojoMyBusiness.Models;
using System.Threading.Tasks;

namespace DojoMyBusiness.Interfaces
{
    public interface IUsuarioRepository : IRepository<Usuario>
    {
        Task<Usuario> ObterUsuario(string usuario);
    }
}
