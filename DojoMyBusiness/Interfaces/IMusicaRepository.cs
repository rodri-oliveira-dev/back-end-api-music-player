using System.Collections.Generic;
using System.Threading.Tasks;
using DojoMyBusiness.Models;

namespace DojoMyBusiness.Interfaces
{
    public interface IMusicaRepository : IRepository<Musica>
    {
        Task<List<Musica>> ResultadoPesquisaMusicas(string palavraChave);
    }
}