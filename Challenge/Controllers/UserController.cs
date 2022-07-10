using Challenge.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Challenge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [SwaggerTag("Obtener un token de autorización")]
    public class UserController : Controller
    {
        private readonly IUserLogin userService;
        private readonly Ilog4net log;
        public UserController(IUserLogin _userService, Ilog4net _log)
        {
            userService = _userService;
            log = _log;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        [SwaggerOperation(Summary = "Ingresar Usuario y Contraseña", Description = "Se Obtiene Token")]
        [SwaggerResponse(200, "Login OK")]
        [SwaggerResponse(400, "Invalid User")]
        [SwaggerResponse(500, "Error General")]
        public IActionResult Login(Models.User.LoginSet loginSet)
        {
            log.LogInfo("login");
            log.LogDebug("login - Start");
            log.LogDebug($"Request -> User={loginSet.User} Pass={loginSet.Pass}");
            Models.User.LoginResult userResult = userService.Login(loginSet);
            log.LogDebug($"Response -> IdResponse={userResult.IdResponse} Response={userResult.Response} Token={userResult.Token}");

            switch (userResult.IdResponse)
            {
                case StatusCodes.Status200OK:
                    return Ok(userResult);

                case StatusCodes.Status400BadRequest:
                    return BadRequest(userResult);

                default:
                    log.LogError($"Response -> IdResponse={userResult.IdResponse} Response={userResult.Response} Token={userResult.Token}");
                    return Problem(userResult.Response);
            }
        }
    }
}
