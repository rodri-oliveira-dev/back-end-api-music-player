using DojoMyBusiness.Interfaces;
using DojoMyBusiness.Models;
using DojoMyData.Context;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace DojoMyData.Repository
{
    public class UsuarioRepository : Repository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(ApplicationDbContext context) : base(context) { }

        public async Task<Usuario> ObterUsuario(string usuario)
        {
            return await Db.Usuarios.AsNoTracking().FirstOrDefaultAsync(u => u.Nome == usuario);
        }
    }
}
