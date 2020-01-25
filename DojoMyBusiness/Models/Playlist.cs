using System.Collections.Generic;

namespace DojoMyBusiness.Models
{
    public class Playlist : Entity
    {
        public Playlist()
        {
            PlaylistMusicas = new List<PlaylistMusica>();
        }

        public ICollection<PlaylistMusica> PlaylistMusicas { get; set; }
        public Usuario Usuario { get; set; }
    }
}