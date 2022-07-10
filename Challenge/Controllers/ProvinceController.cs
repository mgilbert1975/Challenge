using Challenge.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Challenge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [SwaggerTag("Obtener info de las provincias")]
    public class ProvinceController : Controller
    {
        private readonly IProvince provinceService;
        private readonly Ilog4net log;
        public ProvinceController(IProvince _provinceService, Ilog4net _log)
        {
            provinceService = _provinceService;
            log = _log;
        }

        [HttpGet]
        [Route("getGeolocation")]
        [SwaggerOperation(Summary = "Ingresar Nombre de un Provincia", Description = "Se Obtiene la Geolocalización")]
        [SwaggerResponse(200, "OK")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(401, "Unauthorized")]
        [SwaggerResponse(404, "Not Found")]
        [SwaggerResponse(500, "Error General")]
        public IActionResult GetGeolocationByProvinceName(string province)
        {
            log.LogInfo("getGeolocation");
            log.LogDebug("getGeolocation - Start");
            log.LogDebug($"Request -> Province={province}");
            Models.Province.ProvinceResult provinceResult = provinceService.GetGeolocationByProvinceName(province);
            Models.Province.Coordenadas result = new Models.Province.Coordenadas();
            log.LogDebug($"Response -> IdResponse={provinceResult.IdResponse} Response={provinceResult.Response} lat={provinceResult.provincias[0].centroide.lat} lon={provinceResult.provincias[0].centroide.lon}");

            switch (provinceResult.IdResponse)
            {
                case StatusCodes.Status200OK:
                    result = provinceResult.provincias[0].centroide;
                    return Ok(result);

                case StatusCodes.Status400BadRequest:
                    return BadRequest(result);

                case StatusCodes.Status404NotFound:
                    return NotFound(result);

                default:
                    log.LogError($"Response -> IdResponse={provinceResult.IdResponse} Response={provinceResult.Response}");
                    return Problem(provinceResult.Response);
            }
        }
    }
}
