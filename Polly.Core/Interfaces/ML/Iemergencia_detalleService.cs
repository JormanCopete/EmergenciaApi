using Polly.Core.CustomEntities;
using Polly.Core.Entities.ML;
using Polly.Core.QueryFilters.ML;
using System.Threading.Tasks;

namespace Polly.Core.Interfaces.ML
{
    public interface Iemergencia_detalleService
    {
        PagedList<emergencia_detalle> Getemergencia_detalles(emergencia_detalleQueryFilter filters);
        Task<emergencia_detalle> Getemergencia_detalle(int id);
        Task Insertemergencia_detalle(emergencia_detalle post);
        Task<bool> Updateemergencia_detalle(emergencia_detalle emergencia_detalle);
        Task<bool> Deleteemergencia_detalle(int id);
    }
}
