using Confluent.Kafka;
using N5Now.Api.IService;
using N5Now.Api.ViewModel;
using N5Now.Api.ViewModel.DTO;
using System.Text.Json;

namespace N5Now.PermissionConsumer.Services
{
    public class ConsumerService : BackgroundService
    {
        private readonly IConsumer<Ignore, string> _consumer;

        private readonly ILogger<ConsumerService> _logger;

        private readonly IPermissionService _PermissionService;

        public ConsumerService(IConfiguration configuration, ILogger<ConsumerService> logger)
        {
            _logger = logger;

            var consumerConfig = new ConsumerConfig
            {
                BootstrapServers = configuration["Kafka:BootstrapServers"],
                GroupId = "PermissionsConsumerGroup",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            _consumer = new ConsumerBuilder<Ignore, string>(consumerConfig).Build();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _consumer.Subscribe("Permissions");

            while (!stoppingToken.IsCancellationRequested)
            {
                ProcessKafkaMessage(stoppingToken);
            }

            _consumer.Close();
        }

        public void ProcessKafkaMessage(CancellationToken stoppingToken)
        {
            try
            {
                var consumeResult = _consumer.Consume(stoppingToken);

                var message = consumeResult.Message.Value;

                if (!string.IsNullOrEmpty(message))
                {
                    PermissionDTO permission = JsonSerializer.Deserialize<PermissionDTO>(message);
                    _logger.LogInformation(string.Format("Id: {0} | Date: {1} | Action: {2} | Parameter: {3} | IpAdress: {4}", permission.Id, DateTime.Now.ToString(), permission.NameOperation, message, GetIpAddress()));

                    switch (permission.NameOperation)
                    {
                        case "get":
                            RespuestaViewModel<PermissionDTO> respuestaGet = _PermissionService.GetPermission(permission).Result;
                            _logger.LogInformation(string.Format("Id: {0} | Date: {1} | Action: {2} | Parameter: {3} | IpAdress: {4}", permission.Id, DateTime.Now.ToString(), permission.NameOperation, JsonSerializer.Serialize(respuestaGet), GetIpAddress()));
                            break;
                        case "request":
                            RespuestaViewModel<int> respuestaRequest = _PermissionService.RequestPermission(permission, GetIpAddress()).Result;
                            _logger.LogInformation(string.Format("Id: {0} | Date: {1} | Action: {2} | Parameter: {3} | IpAdress: {4}", permission.Id, DateTime.Now.ToString(), permission.NameOperation, JsonSerializer.Serialize(respuestaRequest), GetIpAddress()));
                            break;
                        case "modify":
                            RespuestaViewModel<bool> respuestaModify = _PermissionService.ModifyPermission(permission, GetIpAddress()).Result;
                            _logger.LogInformation(string.Format("Id: {0} | Date: {1} | Action: {2} | Parameter: {3} | IpAdress: {4}", permission.Id, DateTime.Now.ToString(), permission.NameOperation, JsonSerializer.Serialize(respuestaModify), GetIpAddress()));
                            break;
                        default:
                            _logger.LogInformation(string.Format("Id: {0} | Date: {1} | Action: Acción no permitida | Parameter: {2} | IpAdress: {3}", permission.Id, DateTime.Now.ToString(), message, GetIpAddress()));
                            break;
                    }
                }
                else
                {
                    _logger.LogInformation("Mensaje de Kafka no recibido");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error processing Kafka message: {ex.Message}");
            }
        }

        private string GetIpAddress()
        {
            string ipAdd;
            //try
            //{
            //    ipAdd = Request.Headers["HTTP_X_FORWARDED_FOR"];

            //    if (string.IsNullOrEmpty(ipAdd))
            //    {
            //        ipAdd = Request.HttpContext.Connection.RemoteIpAddress.ToString();

            //        if (string.IsNullOrEmpty(ipAdd))
            //            ipAdd = "N5Now.PermissionConsumer";
            //    }
            //}
            //catch (Exception)
            {
                ipAdd = "N5Now.PermissionConsumer";
            }

            return ipAdd;
        }
    }
}
