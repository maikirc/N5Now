using Microsoft.AspNetCore.Mvc;
using N5Now.Api.ViewModel.DTO;
using N5Now.PermissionProducer.Services;
using System.Text.Json;

namespace N5Now.PermissionProducer.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class PermissionController : ControllerBase
    {
        private readonly ProducerService _producerService;
        private readonly ILogger<PermissionController> _logger;

        public PermissionController(ProducerService producerService, ILogger<PermissionController> logger)
        {
            _producerService = producerService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> GetPermission([FromBody] PermissionDTO permission)
        {
            permission.Id = new Guid().ToString();
            var message = JsonSerializer.Serialize(permission);
            _logger.LogInformation(string.Format("Id: {0} | Date: {1} | Action: GetPermission | Parameter: {2} | IpAdress: {3}", permission.Id, DateTime.Now.ToString(), message, GetIpAddress()));
            await _producerService.ProduceAsync("Permission", message);

            return Ok("Permission Get Successfully...");
        }

        [HttpPost]
        public async Task<IActionResult> RequestPermission([FromBody] PermissionDTO permission)
        {
            permission.Id = new Guid().ToString();
            var message = JsonSerializer.Serialize(permission);
            _logger.LogInformation(string.Format("Id: {0} | Date: {1} | Action: RequestPermission | Parameter: {2} | IpAdress: {3}", permission.Id, DateTime.Now.ToString(), message, GetIpAddress()));
            await _producerService.ProduceAsync("Permission", message);

            return Ok("Permission Requets Successfully...");
        }

        [HttpPut]
        public async Task<IActionResult> ModifyPermission([FromBody] PermissionDTO permission)
        {
            permission.Id = new Guid().ToString();
            var message = JsonSerializer.Serialize(permission);
            _logger.LogInformation(string.Format("Id: {0} | Date: {1} | Action: ModifyPermission | Parameter: {2} | IpAdress: {3}", permission.Id, DateTime.Now.ToString(), message, GetIpAddress()));
            await _producerService.ProduceAsync("Permission", message);

            return Ok("Permission Modify Successfully...");
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
                        ipAdd = "N5Now.PermissionProducer";
                }
            }
            catch (Exception)
            {
                ipAdd = "N5Now.PermissionProducer";
            }

            return ipAdd;
        }
    }
}