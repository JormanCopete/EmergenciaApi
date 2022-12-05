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
    public class emergencia_detalleService : Iemergencia_detalleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;

        public emergencia_detalleService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
        }

        public async Task<emergencia_detalle> Getemergencia_detalle(int id)
        {
            return await _unitOfWork.emergencia_detalleRepository.GetById(id);
        }

        public PagedList<emergencia_detalle> Getemergencia_detalles(emergencia_detalleQueryFilter filters)
        {
            filters.PageNumber = filters.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filters.PageNumber;
            filters.PageSize = filters.PageSize == 0 ? _paginationOptions.DefaultPageSize : filters.PageSize;

            var emergencia_detalles = _unitOfWork.emergencia_detalleRepository.GetAll();
            
            var pagedemergencia_detalles = PagedList<emergencia_detalle>.Create(emergencia_detalles, filters.PageNumber, filters.PageSize);
            return pagedemergencia_detalles;
        }
        
        public async Task Insertemergencia_detalle(emergencia_detalle emergencia_detalle)
        {
            await _unitOfWork.emergencia_detalleRepository.Add(emergencia_detalle);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> Updateemergencia_detalle(emergencia_detalle emergencia_detalle)
        {
            _unitOfWork.emergencia_detalleRepository.Update(emergencia_detalle);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Deleteemergencia_detalle(int id)
        {
            var emergencia_detalle = await Getemergencia_detalle(id);
            if (emergencia_detalle == null)
                throw new RestException(HttpStatusCode.NotFound, new { emergencia_detalle = GlobalConstants.NOT_FOUND });

            await _unitOfWork.emergencia_detalleRepository.Delete(id);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
