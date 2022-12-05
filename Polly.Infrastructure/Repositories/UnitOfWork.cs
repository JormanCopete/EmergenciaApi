using Polly.Core.Interfaces;
using Polly.Core.Interfaces.ML;
using Polly.Infrastructure.Data;
using Polly.Infrastructure.Repositories.ML;
using System.Threading.Tasks;

namespace Polly.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PollyContext _context;


        private readonly Iemergencia_resumenRepository _emergencia_resumenRepository;
        private readonly Iemergencia_detalleRepository _emergencia_detalleRepository;
        
        public UnitOfWork(PollyContext context)
        {
            _context = context;
        }

        public Iemergencia_resumenRepository emergencia_resumenRepository => _emergencia_resumenRepository ?? new emergencia_resumenRepository(_context);
        public Iemergencia_detalleRepository emergencia_detalleRepository => _emergencia_detalleRepository ?? new emergencia_detalleRepository(_context);
        
        public void Dispose()
        {
            if (_context != null)
            {
                _context.Dispose();
            }
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
