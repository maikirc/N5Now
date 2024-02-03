using Microsoft.Extensions.Logging;
using N5Now.Api.IService;
using N5Now.Api.Logic;
using N5Now.Api.ViewModel;
using N5Now.Api.ViewModel.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace N5Now.Api.Service
{
    public class PermissionService : IPermissionService
    {
        private readonly PermissionLogic _PermissionLogic;
        private readonly ILogger<PermissionService> _logger;

        public PermissionService(PermissionLogic permissionLogic, ILogger<PermissionService> logger)
        {
            _PermissionLogic = permissionLogic;
            _logger = logger;
        }

        public async Task<RespuestaViewModel<PermissionDTO>> GetPermission(PermissionDTO permission)
        {
            RespuestaViewModel<PermissionDTO> respuesta = new();
            string mensajeErrorValidacion = string.Empty;
            string idLog;

            try
            {
                mensajeErrorValidacion = "";
                if (permission.IdPermission <= 0)
                    mensajeErrorValidacion = "El parámetro Id Permiso es requerido.";

                if (!string.IsNullOrEmpty(mensajeErrorValidacion))
                {
                    idLog = string.IsNullOrEmpty(permission.Id)? Guid.NewGuid().ToString() : permission.Id;
                    var parametros = $"GetPermission Service Layer: {permission.IdPermission.ToString()} Mensajes {mensajeErrorValidacion}";
                    var props = new Dictionary<string, object>(){
                            { "Metodo", "GetPermission" },
                            { "Sitio", "PERMISSION-API" },
                            { "Parametros", parametros },
                            { "IdLog", idLog }
                    };

                    using (_logger.BeginScope(props))
                        _logger.LogWarning("Api.Permission: Registro en SeriLog");

                    respuesta.Resultado.Ok = true;
                    respuesta.Resultado.ErrorValidacion = true;
                    respuesta.Resultado.Mensajes.Add(string.Format(PermissionMessages.EXCEPCION_VALIDACION, mensajeErrorValidacion + " " + idLog));
                    respuesta.Resultado.StatusCode = 400;
                    return respuesta;
                }

                PermissionDTO permissionResult = await _PermissionLogic.GetPermission(permission.IdPermission);

                respuesta.DataResult = (permissionResult != null) ? permissionResult : null;
                respuesta.Resultado.Ok = true;
                respuesta.Resultado.StatusCode = (permissionResult != null) ? 200 : 404;
                respuesta.Resultado.Mensajes = (permissionResult != null) ? null : new List<string>(new string[] { PermissionMessages.SIN_DATOS });
                return respuesta;             
            }
            catch (Exception ex)
            {                
                idLog = string.IsNullOrEmpty(permission.Id) ? Guid.NewGuid().ToString() : permission.Id;
                var parametros = $"GetPermission Service Layer: {permission.IdPermission.ToString()}";
                var props = new Dictionary<string, object>(){
                            { "Metodo", "GetPermission" },
                            { "Sitio", "PERMISSION-API" },
                            { "Parametros", parametros },
                            { "IdLog", idLog }
                    };

                respuesta.Resultado.Mensajes.Add(string.Format(PermissionMessages.EXCEPCION_NO_CONTROLADA, idLog.ToString() + "-" + ex.ToString()));
                respuesta.DataResult = null;
                respuesta.Resultado.Ok = false;
                respuesta.Resultado.StatusCode = 500;
                using (_logger.BeginScope(props))
                    _logger.LogError(ex, "Api.Permission: Registro en SeriLog");
                return respuesta;
            }            
        }

        public async Task<RespuestaViewModel<int>> RequestPermission(PermissionDTO permission, string ipAdress)
        {
            RespuestaViewModel<int> respuesta = new();
            string mensajeErrorValidacion = string.Empty;
            string idLog;
            int idPermission;

            try
            {
                #region Validación de Propiedades
                if (permission.IdEmployee <= 0)
                    mensajeErrorValidacion = "La propiedad Id Employee debe ser mayor a 0.";
                else if (permission.IdTypePermission <= 0)
                    mensajeErrorValidacion = "La propiedad Id Type Permission debe ser mayor a 0.";
                else if (permission.DateFrom > permission.DateUntil)
                    mensajeErrorValidacion = "La propiedad Date From no debe ser mayor a la propiedad Date Until.";
                else if (string.IsNullOrEmpty(permission.Observation))
                    mensajeErrorValidacion = "La propiedad Observation no debe ser nula o vacia.";

                if (!string.IsNullOrEmpty(mensajeErrorValidacion))
                {
                    idLog = string.IsNullOrEmpty(permission.Id) ? Guid.NewGuid().ToString() : permission.Id;
                    var parametros = $"RequestPermission Service Layer: {ipAdress} Mensajes {mensajeErrorValidacion}";
                    var props = new Dictionary<string, object>(){
                            { "Metodo", "RequestPermission" },
                            { "Sitio", "PERMISSION-API" },
                            { "Parametros", parametros },
                            { "IdLog", idLog }
                    };

                    using (_logger.BeginScope(props))
                        _logger.LogWarning("Api.Permission: Registro en SeriLog");

                    respuesta.Resultado.Ok = true;
                    respuesta.Resultado.ErrorValidacion = true;
                    respuesta.Resultado.Mensajes.Add(string.Format(PermissionMessages.EXCEPCION_VALIDACION, mensajeErrorValidacion + " " + idLog));
                    respuesta.Resultado.StatusCode = 400;
                    return respuesta;
                }                
                #endregion

                permission.State = true;
                permission.CreationDate = DateTime.Now;
                permission.CreationUser = ipAdress;
                permission.LastModificationDate = permission.CreationDate;
                permission.LastModificationUser = ipAdress;

                idPermission = await _PermissionLogic.RequestPermission(permission);

                respuesta.DataResult = idPermission;
                respuesta.Resultado.Ok = (idPermission > 0) ? true : false;
                respuesta.Resultado.StatusCode = (idPermission > 0) ? 200 : 500;
                respuesta.Resultado.Mensajes = (idPermission > 0) ? null : new List<string>(new string[] { PermissionMessages.EXCEPCION_NO_CONTROLADA });
                return respuesta;
            }
            catch (Exception ex)
            {
                idLog = string.IsNullOrEmpty(permission.Id) ? Guid.NewGuid().ToString() : permission.Id;
                var parametros = $"RequestPermission Service Layer:";
                var props = new Dictionary<string, object>(){
                            { "Metodo", "RequestPermission" },
                            { "Sitio", "PERMISSION-API" },
                            { "Parametros", parametros },
                            { "IdLog", idLog }
                    };

                respuesta.Resultado.Mensajes.Add(string.Format(PermissionMessages.EXCEPCION_NO_CONTROLADA, idLog.ToString() + "-" + ex.ToString()));
                respuesta.DataResult = 0;
                respuesta.Resultado.Ok = false;
                respuesta.Resultado.StatusCode = 500;
                using (_logger.BeginScope(props))
                    _logger.LogError(ex, "Api.Permission: Registro en SeriLog");
                return respuesta;
            }            
        }

        public async Task<RespuestaViewModel<bool>> ModifyPermission(PermissionDTO permission, string ipAdress)
        {
            RespuestaViewModel<bool> respuesta = new();
            string mensajeErrorValidacion = string.Empty;
            string idLog;
            bool result;

            try
            {
                #region Validación de Propiedades
                if (permission.IdPermission <= 0)
                    mensajeErrorValidacion = "La propiedad Id Permission debe ser mayor a 0.";
                else if (permission.DateFrom > permission.DateUntil)
                    mensajeErrorValidacion = "La propiedad Date From no debe ser mayor a la propiedad Date Until.";
                else if (string.IsNullOrEmpty(permission.Observation))
                    mensajeErrorValidacion = "La propiedad Observation no debe ser nula o vacia.";
               
                if (!string.IsNullOrEmpty(mensajeErrorValidacion))
                {
                    idLog = string.IsNullOrEmpty(permission.Id) ? Guid.NewGuid().ToString() : permission.Id;
                    var parametros = $"ModifyPermission Service Layer: {ipAdress} Mensajes {mensajeErrorValidacion}";
                    var props = new Dictionary<string, object>(){
                            { "Metodo", "ModifyPermission" },
                            { "Sitio", "PERMISSION-API" },
                            { "Parametros", parametros },
                            { "IdLog", idLog }
                    };

                    using (_logger.BeginScope(props))
                        _logger.LogWarning("Api.Permission: Registro en SeriLog");

                    respuesta.Resultado.Ok = true;
                    respuesta.Resultado.ErrorValidacion = true;
                    respuesta.Resultado.Mensajes.Add(string.Format(PermissionMessages.EXCEPCION_VALIDACION, mensajeErrorValidacion + " " + idLog));
                    respuesta.Resultado.StatusCode = 400;
                    return respuesta;
                }
                #endregion

                result = await _PermissionLogic.ModifyPermission(permission, ipAdress);

                respuesta.DataResult = result;
                respuesta.Resultado.Ok = result;
                respuesta.Resultado.StatusCode = (result) ? 200 : 500;
                respuesta.Resultado.Mensajes = (result) ? null : new List<string>(new string[] { PermissionMessages.EXCEPCION_NO_CONTROLADA });
                return respuesta;
            }
            catch (Exception ex)
            {
                idLog = string.IsNullOrEmpty(permission.Id) ? Guid.NewGuid().ToString() : permission.Id;
                var parametros = $"ModifyPermission Service Layer:";
                var props = new Dictionary<string, object>(){
                            { "Metodo", "ModifyPermission" },
                            { "Sitio", "PERMISSION-API" },
                            { "Parametros", parametros },
                            { "IdLog", idLog }
                    };

                respuesta.Resultado.Mensajes.Add(string.Format(PermissionMessages.EXCEPCION_NO_CONTROLADA, idLog.ToString() + "-" + ex.ToString()));
                respuesta.DataResult = false;
                respuesta.Resultado.Ok = false;
                respuesta.Resultado.StatusCode = 500;
                using (_logger.BeginScope(props))
                    _logger.LogError(ex, "Api.Permission: Registro en SeriLog");
                return respuesta;
            }            
        }
    }
}
