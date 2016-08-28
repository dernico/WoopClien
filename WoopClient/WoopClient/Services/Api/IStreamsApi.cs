using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WoopClient.Models;

namespace WoopClient.Services.Api
{
    public interface IStreamsApi
    {
        Task<StreamModel[]> GetFavorites();
        Task<StreamModel[]> Search(string query);
    }
}
