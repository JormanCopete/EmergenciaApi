using Polly.Core.Entities.ML;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Polly.Core.Interfaces.ML
{
    public interface Iemergencia_resumenRepository : IRepository<emergencia_resumen>
    {
        Task<IEnumerable<emergencia_resumen>> Getemergencia_resumenAllActivos();
    }
}
