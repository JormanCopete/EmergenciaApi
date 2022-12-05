using Polly.Core.Entities.ML;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Polly.Core.Interfaces.ML
{
    public interface Iemergencia_detalleRepository : IRepository<emergencia_detalle>
    {
        Task<IEnumerable<emergencia_detalle>> Getemergencia_detalleAllActivos();
    }
}
