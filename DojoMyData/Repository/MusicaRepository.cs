using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DojoMyBusiness.Interfaces;
using DojoMyBusiness.Models;
using DojoMyData.Context;
using Microsoft.EntityFrameworkCore;

namespace DojoMyData.Repository
{
    public class MusicaRepository : Repository<Musica>, IMusicaRepository
    {
        public MusicaRepository(ApplicationDbContext context) : base(context) { }

        public Task<List<Musica>> ResultadoPesquisaMusicas(string palavraChave)
        {
            return Db.Musicas.Include(a => a.Artista).AsNoTracking().Where(m =>
                    (m.Nome.IndexOf(palavraChave, StringComparison.OrdinalIgnoreCase) >= 0) ||
                    (m.Artista.Nome.IndexOf(palavraChave, StringComparison.OrdinalIgnoreCase) >= 0))
                .OrderBy(m => m.Artista.Nome).ThenBy(m => m.Nome).ToListAsync();
        }
    }
}
