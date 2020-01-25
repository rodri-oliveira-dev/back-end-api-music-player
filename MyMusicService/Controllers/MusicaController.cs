using DojoMyBusiness.Extensions;
using DojoMyBusiness.Interfaces;
using DojoMyBusiness.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DojoMyMusic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class MusicaController : MainController
    {
        private readonly IMusicaRepository _musicaRepository;

        public MusicaController(IMusicaRepository musicaRepository, INotificador notificador) : base(notificador)
        {
            _musicaRepository = musicaRepository;
        }

        /// <summary>
        /// A busca é feita no nome da musica ou no nome do artista.
        /// Todas as pesquisas efetuadas são cacheadas por 10 minutos
        /// </summary>
        /// <param name="filtro">palavra-chave de pesquisa, não pode ser vazia ou menor de três caracteres</param>
        /// <returns>Retorna uma lista de musicas que atendam o filtro</returns>
        [HttpGet("{filtro}")]
        [ProducesResponseType(typeof(List<Musica>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get(string filtro,
            [FromServices] IConfiguration config,
            [FromServices] IDistributedCache cache)
        {
            if (string.IsNullOrWhiteSpace(filtro) || filtro.Trim().Length < 3)
            {
                return BadRequest(new
                {
                    error = "Campo de filtro não pode ser vazio ou menor de três caracteres"
                });
            }

            var campoBusca = filtro.Trim();
            var hashCache = campoBusca.CalculateHash();

            var musicas = await _musicaRepository.ResultadoPesquisaMusicas(filtro);

            if (musicas.Count > 0)
            {
                if (cache.Get(hashCache) == null)
                {
                    SalvarCache(cache, musicas, hashCache);
                }

                return Ok(musicas);
            }

            return NoContent();
        }

        /// <summary>
        /// A busca é feita, em cache, baseada em uma pesquisa realizadas anteriormente.
        /// </summary>
        /// <param name="filtro">palavra-chave de pesquisa, não pode ser vazia ou menor de três caracteres</param>
        /// <returns>Retorna uma lista de musicas que atendam o filtro</returns>
        [HttpGet("GetCached/{filtro}")]
        [ProducesResponseType(typeof(List<Musica>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetCached(string filtro,
            [FromServices] IConfiguration config,
            [FromServices] IDistributedCache cache)
        {
            if (string.IsNullOrWhiteSpace(filtro) || filtro.Trim().Length < 3)
            {
                return BadRequest(new
                {
                    error = "Campo de filtro não pode ser vazio ou menor de três caracteres"
                });
            }

            var campoBusca = filtro.Trim();
            var hashCache = campoBusca.CalculateHash();

            var valorJson = await cache.GetStringAsync(hashCache);
            if (valorJson == null)
            {
                return NoContent();
            }

            var musicas = JsonConvert.DeserializeObject<List<Musica>>(valorJson);

            return Ok(musicas);
        }

        private static void SalvarCache(IDistributedCache cache, List<Musica> musicas, string hashCache)
        {
            var svalorJson = JsonConvert.SerializeObject(musicas);

            var opcoesCache = new DistributedCacheEntryOptions();
            opcoesCache.SetAbsoluteExpiration(TimeSpan.FromMinutes(10));

            cache.SetString(hashCache, svalorJson, opcoesCache);
        }
    }
}