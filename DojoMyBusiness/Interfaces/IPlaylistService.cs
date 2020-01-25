using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DojoMyBusiness.Models;

namespace DojoMyBusiness.Interfaces
{
    public interface IPlaylistService : IDisposable
    {
        Task<bool> Atualizar(Playlist playlist);
    }
}
