using DojoMyBusiness.Interfaces;
using DojoMyBusiness.Models;
using DojoMyBusiness.Models.Validations;
using System.Threading.Tasks;

namespace DojoMyBusiness.Services
{
    public class PlaylistService : BaseService, IPlaylistService
    {
        private readonly IPlaylistRepository _playlistRepository;

        public PlaylistService(INotificador notificador, IPlaylistRepository playlistRepository) : base(notificador)
        {
            _playlistRepository = playlistRepository;
        }

        public async Task<bool> Atualizar(Playlist playlist)
        {
            if (!ExecutarValidacao(new PlaylistValidation(), playlist)) return false;

            await _playlistRepository.Atualizar(playlist);
            return true;
        }

        public void Dispose()
        {
            _playlistRepository?.Dispose();
        }
    }
}
