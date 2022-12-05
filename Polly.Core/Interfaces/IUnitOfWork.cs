using Polly.Core.Interfaces.ML;
using System;
using System.Threading.Tasks;

namespace Polly.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        Iemergencia_resumenRepository emergencia_resumenRepository { get; }
        Iemergencia_detalleRepository emergencia_detalleRepository { get; }

        void SaveChanges();
        Task SaveChangesAsync();
    }
}
