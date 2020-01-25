namespace DojoMyBusiness.Models
{
    public class PlaylistMusica
    {
        public string PlaylistId { get; set; }
        public Playlist Playlist { get; set; }
        public string MusicaId { get; set; }
        public Musica Musica { get; set; }
    }
}