using DojoMyBusiness.Interfaces;
using DojoMyBusiness.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace DojoMyPlaylist.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class PlaylistController : MainController
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IPlaylistRepository _playlistRepository;
        private readonly IMusicaRepository _musicaRepository;
        private readonly IPlaylistService _playlistService;

        public PlaylistController(IUsuarioRepository usuarioRepository,
            IPlaylistRepository playlistRepository,
            IMusicaRepository musicaRepository,
            IPlaylistService playlistService,
            INotificador notificador) : base(notificador)
        {
            _usuarioRepository = usuarioRepository;
            _playlistRepository = playlistRepository;
            _musicaRepository = musicaRepository;

            _playlistService = playlistService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        [HttpGet("{usuario}")]
        [ProducesResponseType(typeof(Playlist), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetByUser(string usuario)
        {
            if (string.IsNullOrWhiteSpace(usuario))
            {
                return NoContent();
            }

            var usuarioEncontrado = await _usuarioRepository.ObterUsuario(usuario);

            if (usuarioEncontrado == null)
            {
                return NoContent();
            }

            var playlist = await _playlistRepository.RetornaPlaylistComMusicas(usuarioEncontrado.PlaylistId);

            if (playlist == null)
            {
                return NoContent();
            }

            return Ok(playlist);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="playlistId"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPut("AddMusica/{playlistId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutAddMusic(string playlistId, [FromBody]Musica value)
        {
            if (string.IsNullOrWhiteSpace(playlistId))
            {
                NotificarErro("PlaylistId não pode ser vazio");
                return CustomResponse(value);
            }

            var playlist = await _playlistRepository.RetornaPlaylistComMusicas(playlistId);

            if (playlist == null)
            {
                NotificarErro("PlaylistId não encontrada");
                return CustomResponse(value);
            }

            var musica = await _musicaRepository.ObterPorId(value.Id);

            if (musica == null)
            {
                NotificarErro("Musica não encontrada");
                return CustomResponse(value);
            }

            playlist.PlaylistMusicas.Add(new PlaylistMusica
            {
                MusicaId = musica.Id,
                Musica = musica,
                PlaylistId = playlistId,
                Playlist = playlist
            });

            await _playlistService.Atualizar(playlist);

            return CustomResponse(value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="playlistId"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPut("RemoveMusica/{playlistId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutRemoveMusic(string playlistId, [FromBody]Musica value)
        {
            if (string.IsNullOrWhiteSpace(playlistId))
            {
                NotificarErro("PlaylistId não pode ser vazio");
                return CustomResponse(value);
            }

            var playlist = await _playlistRepository.RetornaPlaylistComMusicas(playlistId);

            if (playlist == null)
            {
                NotificarErro("PlaylistId não encontrada");
                return CustomResponse(value);
            }

            var musica = await _musicaRepository.ObterPorId(value.Id);

            if (musica == null)
            {
                NotificarErro("Musica não encontrada");
                return CustomResponse(value);
            }

            var musicaNaLista = playlist.PlaylistMusicas.FirstOrDefault(m => m.MusicaId == musica.Id);

            if (musicaNaLista != null)
            {
                playlist.PlaylistMusicas.Remove(musicaNaLista);
                await _playlistService.Atualizar(playlist);

                return CustomResponse(value);
            }

            NotificarErro("Musica não encontrada na lista");
            return CustomResponse(value);
        }
    }
}
