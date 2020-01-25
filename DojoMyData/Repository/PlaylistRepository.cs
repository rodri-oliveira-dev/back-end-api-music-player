using DojoMyBusiness.Interfaces;
using DojoMyBusiness.Models;
using DojoMyData.Context;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace DojoMyData.Repository
{
    public class PlaylistRepository : Repository<Playlist>, IPlaylistRepository
    {
        public PlaylistRepository(ApplicationDbContext context) : base(context) { }


        public async Task<Playlist> RetornaPlaylistComMusicas(string playlistId)
        {
            return await Db.Playlists
                .Include(p => p.PlaylistMusicas)
                .Include("PlaylistMusicas.Musica")
                .FirstOrDefaultAsync(p => p.Id == playlistId);
        }
    }
}
