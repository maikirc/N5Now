using System.Collections.Generic;

namespace N5Now.Api.ViewModel
{
    public class ResultadoViewModel
    {
        public bool Ok { get; set; }

        public string Titulo { get; set; }

        public Enumerados.TipoMensaje TipoMensaje { get; set; }

        public List<string> Mensajes { get; set; }

        public bool ErrorValidacion { get; set; }

        public int StatusCode { get; set; }

        public ResultadoViewModel()
        {
            Ok = false;
            Titulo = "Mensajes de la Aplicación";
            TipoMensaje = Enumerados.TipoMensaje.Exito;
            Mensajes = new List<string>();
            ErrorValidacion = false;
        }
    }
}
