using Microsoft.EntityFrameworkCore;
using Polly.Core.Entities.ML;
using Polly.Core.Interfaces.ML;
using Polly.Infrastructure.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Polly.Infrastructure.Repositories.ML
{
    public class emergencia_resumenRepository : BaseRepository<emergencia_resumen>, Iemergencia_resumenRepository
    {
        public emergencia_resumenRepository(PollyContext context) : base(context) { }

        public async Task<IEnumerable<emergencia_resumen>> Getemergencia_resumenAllActivos()
        {
            return await _entities.Where(x => x.id == x.id).ToListAsync();
        }
    }
}
