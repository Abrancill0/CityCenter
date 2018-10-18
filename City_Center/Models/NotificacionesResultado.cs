using System;
using System.Collections.Generic;

namespace City_Center.Models
{
    public class NotificacionesResultado
    {
		public class NotificacionesDetalle
        {
            public int nus_id { get; set; }
            public int nus_id_usuario { get; set; }
            public int nus_id_notificacion { get; set; }
            public string nus_fecha_hora_creo { get; set; }
            public string nus_fecha_hora_modifico { get; set; }
            public int nus_activa { get; set; }
            public string not_nombre { get; set; }
            public string not_decripcion { get; set; }
        }

        public class NotificacionesReturn
        {
            public int estatus { get; set; }
            public string mensaje { get; set; }
            public List<NotificacionesDetalle> respuesta { get; set; }
        }
    }
}
