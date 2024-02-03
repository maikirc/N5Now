using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using N5Now.Api.IService;
using N5Now.Api.ViewModel;
using N5Now.Api.ViewModel.DTO;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace N5Now.Api.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class PermissionController : ControllerBase
    {
        private readonly IPermissionService _PermissionService;
        private readonly ILogger<PermissionController> _logger;

        public PermissionController(IPermissionService PermissionService, ILogger<PermissionController> logger)
        {
            _PermissionService = PermissionService;
            _logger = logger;
        }
        
        [HttpPost]
        [Route("GetPermission")]
        public async Task<IActionResult> GetPermission([FromBody] PermissionDTO permission)
        {
            _logger.LogInformation(string.Format("Date: {0} | Action: GetPermission | Parameter: {1} | IpAdress: {2}", DateTime.Now.ToString(), JsonSerializer.Serialize(permission), GetIpAddress()));
            RespuestaViewModel<PermissionDTO> respuesta = _PermissionService.GetPermission(permission).Result;
            return this.StatusCode(respuesta.Resultado.StatusCode, respuesta);
        }

        [HttpPost]
        [Route("RequestPermission")]
        public async Task<IActionResult> RequestPermission([FromBody] PermissionDTO permission)
        {
            _logger.LogInformation(string.Format("Date: {0} | Action: RequestPermission | Parameter: {1} | IpAdress: {2}", DateTime.Now.ToString(), JsonSerializer.Serialize(permission), GetIpAddress()));
            RespuestaViewModel<int> respuesta = _PermissionService.RequestPermission(permission, GetIpAddress()).Result;
            return this.StatusCode(respuesta.Resultado.StatusCode, respuesta);          
        }

        [HttpPut]
        [Route("ModifyPermission")]
        public async Task<IActionResult> ModifyPermission([FromBody] PermissionDTO permission)
        {
            _logger.LogInformation(string.Format("Date: {0} | Action: ModifyPermission | Parameter: {1} | IpAdress: {2}", DateTime.Now.ToString(), JsonSerializer.Serialize(permission), GetIpAddress()));
            RespuestaViewModel<bool> respuesta = _PermissionService.ModifyPermission(permission, GetIpAddress()).Result;
            return this.StatusCode(respuesta.Resultado.StatusCode, respuesta);
        }
        
        private string GetIpAddress()
        {
            string ipAdd;
            try
            {
                ipAdd = Request.Headers["HTTP_X_FORWARDED_FOR"];

                if (string.IsNullOrEmpty(ipAdd))
                {
                    ipAdd = Request.HttpContext.Connection.RemoteIpAddress.ToString();

                    if (string.IsNullOrEmpty(ipAdd))
                        ipAdd = "N5Now.Api";
                }
            }
            catch (Exception)
            {
                ipAdd = "PermissionAPI";
            }            

            return ipAdd;
        }
    }
}
