using Polly.Core.CustomEntities;
using Polly.Core.Entities.ML;
using Polly.Core.QueryFilters.ML;
using System.Threading.Tasks;

namespace Polly.Core.Interfaces.ML
{
    public interface Iemergencia_resumenService
    {
        PagedList<emergencia_resumen> Getemergencia_resumens(emergencia_resumenQueryFilter filters);
        Task<emergencia_resumen> Getemergencia_resumen(int id);
        Task Insertemergencia_resumen(emergencia_resumen post);
        Task<bool> Updateemergencia_resumen(emergencia_resumen emergencia_resumen);
        Task<bool> Deleteemergencia_resumen(int id);
    }
}
