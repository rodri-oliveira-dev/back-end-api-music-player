using DojoMyBusiness.Models;
using System.Threading.Tasks;

namespace DojoMyBusiness.Interfaces
{
    public interface IPlaylistRepository : IRepository<Playlist>
    {
        Task<Playlist> RetornaPlaylistComMusicas(string playlistId);
    }
}
