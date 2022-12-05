using Microsoft.EntityFrameworkCore;
using Polly.Core.Entities.ML;
using Polly.Core.Interfaces.ML;
using Polly.Infrastructure.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Polly.Infrastructure.Repositories.ML
{
    public class emergencia_detalleRepository : BaseRepository<emergencia_detalle>, Iemergencia_detalleRepository
    {
        public emergencia_detalleRepository(PollyContext context) : base(context) { }

        public async Task<IEnumerable<emergencia_detalle>> Getemergencia_detalleAllActivos()
        {
            return await _entities.Where(x => x.organinismo == x.organinismo).ToListAsync();
        }
    }
}
