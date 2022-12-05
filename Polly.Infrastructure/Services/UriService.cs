using Polly.Core.QueryFilters.ML;
using Polly.Infrastructure.Interfaces;
using System;

namespace Polly.Infrastructure.Services
{
    public class UriService : IUriService
    {
        private readonly string _baseUri;

        public UriService(string baseUri)
        {
            _baseUri = baseUri;
        }

        public Uri Getemergencia_detallePaginationUri(emergencia_detalleQueryFilter filter, string actionUrl)
        {
            string baseUrl = $"{_baseUri}{actionUrl}";
            return new Uri(baseUrl);
        }

        public Uri Getemergencia_resumenPaginationUri(emergencia_resumenQueryFilter filter, string actionUrl)
        {
            string baseUrl = $"{_baseUri}{actionUrl}";
            return new Uri(baseUrl);
        }
    }
}
