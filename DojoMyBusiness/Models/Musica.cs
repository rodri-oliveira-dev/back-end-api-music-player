using Newtonsoft.Json;
using System.Collections.Generic;

namespace DojoMyBusiness.Models
{
    public class Musica : Entity
    {
        public string Nome { get; set; }
        public string ArtistaId { get; set; }
        public Artista Artista { get; set; }

        [JsonIgnore]
        public ICollection<PlaylistMusica> PlaylistMusicas { get; set; }
    }
}