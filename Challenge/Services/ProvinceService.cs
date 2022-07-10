using Challenge.Interfaces;
using Newtonsoft.Json;

namespace Challenge.Services
{
    public class ProvinceService : IProvince
    {
        private readonly IConfiguration configuration;
        private readonly Ilog4net log;
        public ProvinceService(IConfiguration _configuration, Ilog4net _log)
        {
            configuration = _configuration;
            log = _log;
        }

        public Models.Province.ProvinceResult GetGeolocationByProvinceName(string province)
        {
            string apiURL = configuration.GetValue<string>("URLApiProvincias") + province;
            Models.Province.ProvinceResult result = new Models.Province.ProvinceResult();
            log.LogDebug($"apiURL={apiURL}");

            HttpClient client = new HttpClient();
            try
            {
                HttpResponseMessage response = client.GetAsync(apiURL).Result;
                switch(response.StatusCode)
                {
                    case System.Net.HttpStatusCode.OK:
                        result = JsonConvert.DeserializeObject<Models.Province.ProvinceResult>(response.Content.ReadAsStringAsync().Result);
                        if(result.cantidad > 0)
                        {
                            result.IdResponse = StatusCodes.Status200OK;
                            result.Response = "OK";
                        }
                        else
                        {
                            result.IdResponse = StatusCodes.Status404NotFound;
                            result.Response = "No se encontró la provincia";
                        }
                        break;

                    case System.Net.HttpStatusCode.NoContent:
                        result.IdResponse = StatusCodes.Status404NotFound;
                        result.Response = "No se encontró la provincia";
                        break;

                    case System.Net.HttpStatusCode.BadRequest:
                        result.IdResponse = StatusCodes.Status400BadRequest;
                        result.Response = "Error en los datos ingresados";
                        break;

                    default:
                        result.IdResponse = StatusCodes.Status500InternalServerError;
                        result.Response = "Error General";
                        break;
                }
            }
            catch (Exception ex)
            {
                result.IdResponse = StatusCodes.Status500InternalServerError;
                result.Response = ex.Message;
            }

            return result;
        }
    }
}
