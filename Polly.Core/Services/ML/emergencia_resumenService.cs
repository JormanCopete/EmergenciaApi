using Microsoft.Extensions.Options;
using Polly.Core.Constants;
using Polly.Core.CustomEntities;
using Polly.Core.Entities.ML;
using Polly.Core.Exceptions;
using Polly.Core.Interfaces;
using Polly.Core.Interfaces.ML;
using Polly.Core.QueryFilters.ML;
using System.Net;
using System.Threading.Tasks;

namespace Polly.Core.Services.ML
{
    public class emergencia_resumenService : Iemergencia_resumenService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;

        public emergencia_resumenService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
        }

        public async Task<emergencia_resumen> Getemergencia_resumen(int id)
        {
            return await _unitOfWork.emergencia_resumenRepository.GetById(id);
        }

        public PagedList<emergencia_resumen> Getemergencia_resumens(emergencia_resumenQueryFilter filters)
        {
            filters.PageNumber = filters.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filters.PageNumber;
            filters.PageSize = filters.PageSize == 0 ? _paginationOptions.DefaultPageSize : filters.PageSize;

            var emergencia_resumens = _unitOfWork.emergencia_resumenRepository.GetAll();
            
            var pagedemergencia_resumens = PagedList<emergencia_resumen>.Create(emergencia_resumens, filters.PageNumber, filters.PageSize);
            return pagedemergencia_resumens;
        }
        
        public async Task Insertemergencia_resumen(emergencia_resumen emergencia_resumen)
        {
            await _unitOfWork.emergencia_resumenRepository.Add(emergencia_resumen);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> Updateemergencia_resumen(emergencia_resumen emergencia_resumen)
        {
            _unitOfWork.emergencia_resumenRepository.Update(emergencia_resumen);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Deleteemergencia_resumen(int id)
        {
            var emergencia_resumen = await Getemergencia_resumen(id);
            if (emergencia_resumen == null)
                throw new RestException(HttpStatusCode.NotFound, new { emergencia_resumen = GlobalConstants.NOT_FOUND });

            await _unitOfWork.emergencia_resumenRepository.Delete(id);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
