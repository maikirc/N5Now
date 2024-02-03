using Microsoft.AspNetCore.Mvc.Testing;
using N5Now.Api.ViewModel;
using N5Now.Api.ViewModel.DTO;
using System.Text.Json;

namespace N5Now.Api.UnitTest
{
    [TestClass]
    public class PermissionControllerTest
    {
        private HttpClient _httpClient;

        public PermissionControllerTest()
        {
            var webApplicationFactory = new WebApplicationFactory<Program>();
            _httpClient = webApplicationFactory.CreateDefaultClient();
        }

        [TestMethod]
        public async Task GetPermissionFound()
        {
            PermissionDTO permission = new PermissionDTO
            {
                Id = new Guid().ToString(),
                NameOperation = "get",
                IdPermission = 2,
                IdEmployee = 1,
                IdTypePermission = 1,
                DateFrom = DateTime.Now,
                DateUntil = DateTime.Now,
                Observation = "OBSERVACION",
                State = true,
                CreationDate = DateTime.Now,
                CreationUser = "MREGALADO",
                LastModificationDate = DateTime.Now,
                LastModificationUser = "MREGALADO"
            };

            var response = await _httpClient.PostAsJsonAsync("/api/Permission/GetPermission", permission);
            var result = await response.Content.ReadAsStringAsync();
            Assert.IsNotNull(result);            

            if (result != null)
            {
                RespuestaViewModel<PermissionDTO> respuesta = JsonSerializer.Deserialize<RespuestaViewModel<PermissionDTO>>(result);
                Assert.IsNotNull(respuesta);
                Assert.IsNotNull(respuesta.DataResult);
                Assert.IsNotNull(respuesta.Resultado);
                Assert.AreEqual(respuesta.Resultado.Ok, true);
                Assert.AreEqual(respuesta.Resultado.StatusCode, 200);
                Assert.IsNotNull(respuesta.Resultado.Mensajes);
                Assert.AreEqual(respuesta.Resultado.Mensajes.Count, 0);
            }
        }

        [TestMethod]
        public async Task GetPermissionNotFound()
        {
            PermissionDTO permission = new PermissionDTO
            {
                Id = new Guid().ToString(),
                NameOperation = "get",
                IdPermission = 99999,
                IdEmployee = 1,
                IdTypePermission = 1,
                DateFrom = DateTime.Now,
                DateUntil = DateTime.Now,
                Observation = "OBSERVACION",
                State = true,
                CreationDate = DateTime.Now,
                CreationUser = "MREGALADO",
                LastModificationDate = DateTime.Now,
                LastModificationUser = "MREGALADO"
            };

            var response = await _httpClient.PostAsJsonAsync("/api/Permission/GetPermission", permission);
            var result = await response.Content.ReadAsStringAsync();
            Assert.IsNotNull(result);

            if (result != null)
            {
                RespuestaViewModel<PermissionDTO> respuesta = JsonSerializer.Deserialize<RespuestaViewModel<PermissionDTO>>(result);
                Assert.IsNotNull(respuesta);
                Assert.IsNull(respuesta.DataResult);
                Assert.IsNotNull(respuesta.Resultado);
                Assert.AreEqual(respuesta.Resultado.Ok, true);
                Assert.AreEqual(respuesta.Resultado.StatusCode, 404);
                Assert.IsNotNull(respuesta.Resultado.Mensajes);
                Assert.AreEqual(respuesta.Resultado.Mensajes.Count, 1);
            }
        }

        [TestMethod]
        public async Task GetPermissionBadRequest()
        {
            PermissionDTO permission = new PermissionDTO
            {
                Id = new Guid().ToString(),
                NameOperation = "get",
                IdPermission = 0,
                IdEmployee = 1,
                IdTypePermission = 1,
                DateFrom = DateTime.Now,
                DateUntil = DateTime.Now,
                Observation = "OBSERVACION",
                State = true,
                CreationDate = DateTime.Now,
                CreationUser = "MREGALADO",
                LastModificationDate = DateTime.Now,
                LastModificationUser = "MREGALADO"
            };

            var response = await _httpClient.PostAsJsonAsync("/api/Permission/GetPermission", permission);
            var result = await response.Content.ReadAsStringAsync();
            Assert.IsNotNull(result);

            if (result != null)
            {
                RespuestaViewModel<PermissionDTO> respuesta = JsonSerializer.Deserialize<RespuestaViewModel<PermissionDTO>>(result);
                Assert.IsNotNull(respuesta);
                Assert.IsNull(respuesta.DataResult);
                Assert.IsNotNull(respuesta.Resultado);
                Assert.AreEqual(respuesta.Resultado.Ok, true);
                Assert.AreEqual(respuesta.Resultado.ErrorValidacion, true);
                Assert.AreEqual(respuesta.Resultado.StatusCode, 400);
                Assert.IsNotNull(respuesta.Resultado.Mensajes);
                Assert.AreEqual(respuesta.Resultado.Mensajes.Count, 1);
            }
        }

        [TestMethod]
        public async Task RequestPermissionInsert()
        {
            PermissionDTO permission = new PermissionDTO
            {
                Id = new Guid().ToString(),
                NameOperation = "request",
                IdPermission = 0,
                IdEmployee = 1,
                IdTypePermission = 1,
                DateFrom = DateTime.Now,
                DateUntil = DateTime.Now,
                Observation = "OBSERVACION",
                State = true,
                CreationDate = DateTime.Now,
                CreationUser = "MREGALADO",
                LastModificationDate = DateTime.Now,
                LastModificationUser = "MREGALADO"
            };

            var response = await _httpClient.PostAsJsonAsync("/api/Permission/RequestPermission", permission);
            var result = await response.Content.ReadAsStringAsync();
            Assert.IsNotNull(result);

            if (result != null)
            {
                RespuestaViewModel<int> respuesta = JsonSerializer.Deserialize<RespuestaViewModel<int>>(result);
                Assert.IsNotNull(respuesta);
                Assert.AreNotEqual(respuesta.DataResult, -1);
                Assert.AreNotEqual(respuesta.DataResult, 0);
                Assert.IsNotNull(respuesta.Resultado);
                Assert.AreEqual(respuesta.Resultado.Ok, true);
                Assert.AreEqual(respuesta.Resultado.StatusCode, 200);
                Assert.IsNotNull(respuesta.Resultado.Mensajes);
                Assert.AreEqual(respuesta.Resultado.Mensajes.Count, 0);
            }
        }

        [TestMethod]
        public async Task RequestPermissionBadRequest()
        {
            PermissionDTO permission = new PermissionDTO
            {
                Id = new Guid().ToString(),
                NameOperation = "request",
                IdPermission = 0,
                IdEmployee = 0,
                IdTypePermission = 0,
                DateFrom = DateTime.Now,
                DateUntil = DateTime.Now,
                Observation = "OBSERVACION",
                State = true,
                CreationDate = DateTime.Now,
                CreationUser = "MREGALADO",
                LastModificationDate = DateTime.Now,
                LastModificationUser = "MREGALADO"
            };

            var response = await _httpClient.PostAsJsonAsync("/api/Permission/RequestPermission", permission);
            var result = await response.Content.ReadAsStringAsync();
            Assert.IsNotNull(result);

            if (result != null)
            {
                RespuestaViewModel<int> respuesta = JsonSerializer.Deserialize<RespuestaViewModel<int>>(result);
                Assert.IsNotNull(respuesta);
                Assert.AreEqual(respuesta.DataResult, 0);
                Assert.IsNotNull(respuesta.Resultado);
                Assert.AreEqual(respuesta.Resultado.Ok, true);
                Assert.AreEqual(respuesta.Resultado.ErrorValidacion, true);
                Assert.AreEqual(respuesta.Resultado.StatusCode, 400);
                Assert.IsNotNull(respuesta.Resultado.Mensajes);
                Assert.AreEqual(respuesta.Resultado.Mensajes.Count, 1);
            }
        }

        [TestMethod]
        public async Task RequestPermissionException()
        {
            PermissionDTO permission = new PermissionDTO
            {
                Id = new Guid().ToString(),
                NameOperation = "request",
                IdPermission = 0,
                IdEmployee = 1,
                IdTypePermission = 1,
                DateFrom = DateTime.Now,
                DateUntil = DateTime.Now,
                Observation = "OBSERVACION OBSERVACION OBSERVACION OBSERVACION OBSERVACION OBSERVACION OBSERVACION OBSERVACION OBSERVACION ",
                State = true,
                CreationDate = DateTime.Now,
                CreationUser = "MREGALADO",
                LastModificationDate = DateTime.Now,
                LastModificationUser = "MREGALADO"
            };

            var response = await _httpClient.PostAsJsonAsync("/api/Permission/RequestPermission", permission);
            var result = await response.Content.ReadAsStringAsync();
            Assert.IsNotNull(result);

            if (result != null)
            {
                RespuestaViewModel<int> respuesta = JsonSerializer.Deserialize<RespuestaViewModel<int>>(result);
                Assert.IsNotNull(respuesta);
                Assert.AreEqual(respuesta.DataResult, 0);
                Assert.IsNotNull(respuesta.Resultado);
                Assert.AreEqual(respuesta.Resultado.Ok, false);
                Assert.AreEqual(respuesta.Resultado.StatusCode, 500);
                Assert.IsNotNull(respuesta.Resultado.Mensajes);
                Assert.AreEqual(respuesta.Resultado.Mensajes.Count, 1);
            }
        }

        [TestMethod]
        public async Task ModifyPermissionUpdate()
        {
            PermissionDTO permission = new PermissionDTO
            {
                Id = new Guid().ToString(),
                NameOperation = "modify",
                IdPermission = 2,
                IdEmployee = 1,
                IdTypePermission = 1,
                DateFrom = DateTime.Now,
                DateUntil = DateTime.Now,
                Observation = "MODIFICACIÓN",
                State = true,
                CreationDate = DateTime.Now,
                CreationUser = "MREGALADO",
                LastModificationDate = DateTime.Now,
                LastModificationUser = "MREGALADO"
            };

            var response = await _httpClient.PutAsJsonAsync("/api/Permission/ModifyPermission", permission);
            var result = await response.Content.ReadAsStringAsync();
            Assert.IsNotNull(result);

            if (result != null)
            {
                RespuestaViewModel<bool> respuesta = JsonSerializer.Deserialize<RespuestaViewModel<bool>>(result);
                Assert.IsNotNull(respuesta);
                Assert.AreEqual(respuesta.DataResult, true);
                Assert.IsNotNull(respuesta.Resultado);
                Assert.AreEqual(respuesta.Resultado.Ok, true);
                Assert.AreEqual(respuesta.Resultado.StatusCode, 200);
                Assert.IsNotNull(respuesta.Resultado.Mensajes);
                Assert.AreEqual(respuesta.Resultado.Mensajes.Count, 0);
            }
        }

        [TestMethod]
        public async Task ModifyPermissionBadRequest()
        {
            PermissionDTO permission = new PermissionDTO
            {
                Id = new Guid().ToString(),
                NameOperation = "modify",
                IdPermission = -1,
                IdEmployee = 0,
                IdTypePermission = 0,
                DateFrom = DateTime.Now,
                DateUntil = DateTime.Now,
                Observation = "OBSERVACION",
                State = true,
                CreationDate = DateTime.Now,
                CreationUser = "MREGALADO",
                LastModificationDate = DateTime.Now,
                LastModificationUser = "MREGALADO"
            };

            var response = await _httpClient.PutAsJsonAsync("/api/Permission/ModifyPermission", permission);
            var result = await response.Content.ReadAsStringAsync();
            Assert.IsNotNull(result);

            if (result != null)
            {
                RespuestaViewModel<bool> respuesta = JsonSerializer.Deserialize<RespuestaViewModel<bool>>(result);
                Assert.IsNotNull(respuesta);
                Assert.AreEqual(respuesta.DataResult, false);
                Assert.IsNotNull(respuesta.Resultado);
                Assert.AreEqual(respuesta.Resultado.Ok, true);
                Assert.AreEqual(respuesta.Resultado.ErrorValidacion, true);
                Assert.AreEqual(respuesta.Resultado.StatusCode, 400);
                Assert.IsNotNull(respuesta.Resultado.Mensajes);
                Assert.AreEqual(respuesta.Resultado.Mensajes.Count, 1);
            }
        }

        [TestMethod]
        public async Task ModifyPermissionException()
        {
            PermissionDTO permission = new PermissionDTO
            {
                Id = new Guid().ToString(),
                NameOperation = "modify",
                IdPermission = 2,
                IdEmployee = 1,
                IdTypePermission = 1,
                DateFrom = DateTime.Now,
                DateUntil = DateTime.Now,
                Observation = "OBSERVACION OBSERVACION OBSERVACION OBSERVACION OBSERVACION OBSERVACION OBSERVACION OBSERVACION OBSERVACION ",
                State = true,
                CreationDate = DateTime.Now,
                CreationUser = "MREGALADO",
                LastModificationDate = DateTime.Now,
                LastModificationUser = "MREGALADO"
            };

            var response = await _httpClient.PutAsJsonAsync("/api/Permission/ModifyPermission", permission);
            var result = await response.Content.ReadAsStringAsync();
            Assert.IsNotNull(result);

            if (result != null)
            {
                RespuestaViewModel<bool> respuesta = JsonSerializer.Deserialize<RespuestaViewModel<bool>>(result);
                Assert.IsNotNull(respuesta);
                Assert.IsNotNull(respuesta.DataResult);
                Assert.AreEqual(respuesta.DataResult, false);
                Assert.IsNotNull(respuesta.Resultado);
                Assert.AreEqual(respuesta.Resultado.Ok, false);
                Assert.AreEqual(respuesta.Resultado.StatusCode, 500);
                Assert.IsNotNull(respuesta.Resultado.Mensajes);
                Assert.AreEqual(respuesta.Resultado.Mensajes.Count, 1);
            }
        }
    }
}