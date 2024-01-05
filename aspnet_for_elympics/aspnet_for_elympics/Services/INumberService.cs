using Aspnet_for_elympics.Entities;
using Aspnet_for_elympics.Models;

namespace Aspnet_for_elympics.Services
{
    public interface INumberService
    {
        public IEnumerable<NumberDTO> GetLatest();
    }
}
