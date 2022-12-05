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
    public class emergencia_resumensController : ControllerBase
    {
        private readonly Iemergencia_resumenService _service;
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;

        public emergencia_resumensController(Iemergencia_resumenService service, IMapper mapper, IUriService uriService)
        {
            _service = service;
            _mapper = mapper;
            _uriService = uriService;
        }

        /// <summary>
        /// Retrieve all emergencia_resumen
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet(Name = nameof(Getemergencia_resumens))]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<IEnumerable<emergencia_resumenDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public IActionResult Getemergencia_resumens([FromQuery] emergencia_resumenQueryFilter filter)
        {
            var emergencia_resumen = _service.Getemergencia_resumens(filter);
            var emergencia_resumenDtos = _mapper.Map<IEnumerable<emergencia_resumenDto>>(emergencia_resumen);

            var metadata = new Metadata
            {
                TotalCount = emergencia_resumen.TotalCount,
                PageSize = emergencia_resumen.PageSize,
                CurrentPage = emergencia_resumen.CurrentPage,
                TotalPages = emergencia_resumen.TotalPages,
                HasNextPage = emergencia_resumen.HasNextPage,
                HasPreviousPage = emergencia_resumen.HasPreviousPage,
                NextPageUrl = _uriService.Getemergencia_resumenPaginationUri(filter, Url.RouteUrl(nameof(Getemergencia_resumens))).ToString(),
                PreviousPageUrl = _uriService.Getemergencia_resumenPaginationUri(filter, Url.RouteUrl(nameof(Getemergencia_resumens))).ToString()
            };

            var response = new ApiResponse<IEnumerable<emergencia_resumenDto>>(emergencia_resumenDtos)
            {
                Meta = metadata
            };

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(metadata));

            if (emergencia_resumenDtos.Count() == 0)
                return StatusCode((int)HttpStatusCode.NotFound, response);

            return Ok(response);
        }

        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<emergencia_resumenDto>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Getemergencia_resumen(int id)
        {
            var emergencia_resumen = await _service.Getemergencia_resumen(id);
            if (emergencia_resumen == null)
                throw new RestException(HttpStatusCode.NotFound, new { emergencia_resumen = GlobalConstants.NOT_FOUND });

            var emergencia_resumenDto = _mapper.Map<emergencia_resumenDto>(emergencia_resumen);
            var response = new ApiResponse<emergencia_resumenDto>(emergencia_resumenDto);
            return Ok(response);
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created, Type = typeof(ApiResponse<IEnumerable<emergencia_resumenDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Postemergencia_resumen([FromBody] emergencia_resumenDto request)
        {
            var emergencia_resumen = _mapper.Map<emergencia_resumen>(request);

            await _service.Insertemergencia_resumen(emergencia_resumen);

            var created = _mapper.Map<emergencia_resumenDto>(emergencia_resumen);
            var response = new ApiResponse<emergencia_resumenDto>(created);
            return StatusCode((int)HttpStatusCode.Created, response);
        }


        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<bool>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Putemergencia_resumen([FromRoute] int id, [FromBody] emergencia_resumenDto request)
        {
            var emergencia_resumen = await _service.Getemergencia_resumen(id);
            if (emergencia_resumen == null)
                throw new RestException(HttpStatusCode.NotFound, new { emergencia_resumen = GlobalConstants.NOT_FOUND });

            _mapper.Map(request, emergencia_resumen);
            emergencia_resumen = _mapper.Map<emergencia_resumen>(emergencia_resumen);
            //emergencia_resumen.Spresolution = id;

            var result = await _service.Updateemergencia_resumen(emergencia_resumen);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<bool>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Deleteemergencia_resumen([FromRoute] int id)
        {
            var result = await _service.Deleteemergencia_resumen(id);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }
    }
}
