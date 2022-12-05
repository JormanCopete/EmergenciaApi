using Polly.Core.QueryFilters.ML;
using System;

namespace Polly.Infrastructure.Interfaces
{
    public interface IUriService
    {
        Uri Getemergencia_resumenPaginationUri(emergencia_resumenQueryFilter filter, string actionUrl);
        Uri Getemergencia_detallePaginationUri(emergencia_detalleQueryFilter filter, string actionUrl);
    }
}