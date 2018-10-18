using System;
using System.Collections.Generic;

namespace City_Center.Models
{
    public class NotificacionesRecibidasResultado
    {
        public class NotificacionesRecibidasDetalle
        {
            public int nen_id { get; set; }
            public string nen_equipo { get; set; }
            public int nen_id_usuario { get; set; }
            public string nen_titulo { get; set; }
            public string nen_mensaje { get; set; }
            public string nen_fecha_hora_creo { get; set; }
            public string nen_fecha_hora_modifico { get; set; }
            public string nen_resultado { get; set; }
        }

        public class NotificacionesRecibidasReturn
        {
            public int estatus { get; set; }
            public string mensaje { get; set; }
            public List<NotificacionesRecibidasDetalle> respuesta { get; set; }
        }
    }
}
