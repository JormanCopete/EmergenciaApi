using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Polly.Api.Responses;
using Polly.Core.Constants;
using Polly.Core.CustomEntities;
using Polly.Core.DTOs.ML;
using Polly.Core.Entities.ML;
using Polly.Core.Exceptions;
using Polly.Core.Interfaces.ML;
using Polly.Core.QueryFilters.ML;
using Polly.Infrastructure.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace polly.Api.Controllers.ML
{
    // [Authorize]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class emergencia_detallesController : ControllerBase
    {
        private readonly Iemergencia_detalleService _service;
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;

        public emergencia_detallesController(Iemergencia_detalleService service, IMapper mapper, IUriService uriService)
        {
            _service = service;
            _mapper = mapper;
            _uriService = uriService;
        }

        /// <summary>
        /// Retrieve all emergencia_detalle
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet(Name = nameof(Getemergencia_detalles))]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<IEnumerable<emergencia_detalleDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public IActionResult Getemergencia_detalles([FromQuery] emergencia_detalleQueryFilter filter)
        {
            var emergencia_detalle = _service.Getemergencia_detalles(filter);
            var emergencia_detalleDtos = _mapper.Map<IEnumerable<emergencia_detalleDto>>(emergencia_detalle);

            var metadata = new Metadata
            {
                TotalCount = emergencia_detalle.TotalCount,
                PageSize = emergencia_detalle.PageSize,
                CurrentPage = emergencia_detalle.CurrentPage,
                TotalPages = emergencia_detalle.TotalPages,
                HasNextPage = emergencia_detalle.HasNextPage,
                HasPreviousPage = emergencia_detalle.HasPreviousPage,
                NextPageUrl = _uriService.Getemergencia_detallePaginationUri(filter, Url.RouteUrl(nameof(Getemergencia_detalles))).ToString(),
                PreviousPageUrl = _uriService.Getemergencia_detallePaginationUri(filter, Url.RouteUrl(nameof(Getemergencia_detalles))).ToString()
            };

            var response = new ApiResponse<IEnumerable<emergencia_detalleDto>>(emergencia_detalleDtos)
            {
                Meta = metadata
            };

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(metadata));

            if (emergencia_detalleDtos.Count() == 0)
                return StatusCode((int)HttpStatusCode.NotFound, response);

            return Ok(response);
        }

        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<emergencia_detalleDto>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Getemergencia_detalle(int id)
        {
            var emergencia_detalle = await _service.Getemergencia_detalle(id);
            if (emergencia_detalle == null)
                throw new RestException(HttpStatusCode.NotFound, new { emergencia_detalle = GlobalConstants.NOT_FOUND });

            var emergencia_detalleDto = _mapper.Map<emergencia_detalleDto>(emergencia_detalle);
            var response = new ApiResponse<emergencia_detalleDto>(emergencia_detalleDto);
            return Ok(response);
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created, Type = typeof(ApiResponse<IEnumerable<emergencia_detalleDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Postemergencia_detalle([FromBody] emergencia_detalleDto request)
        {
            var emergencia_detalle = _mapper.Map<emergencia_detalle>(request);

            await _service.Insertemergencia_detalle(emergencia_detalle);

            var created = _mapper.Map<emergencia_detalleDto>(emergencia_detalle);
            var response = new ApiResponse<emergencia_detalleDto>(created);
            return StatusCode((int)HttpStatusCode.Created, response);
        }


        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<bool>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Putemergencia_detalle([FromRoute] int id, [FromBody] emergencia_detalleDto request)
        {
            var emergencia_detalle = await _service.Getemergencia_detalle(id);
            if (emergencia_detalle == null)
                throw new RestException(HttpStatusCode.NotFound, new { emergencia_detalle = GlobalConstants.NOT_FOUND });

            _mapper.Map(request, emergencia_detalle);
            emergencia_detalle = _mapper.Map<emergencia_detalle>(emergencia_detalle);
            //emergencia_detalle.Spresolution = id;

            var result = await _service.Updateemergencia_detalle(emergencia_detalle);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<bool>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Deleteemergencia_detalle([FromRoute] int id)
        {
            var result = await _service.Deleteemergencia_detalle(id);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }
    }
}
