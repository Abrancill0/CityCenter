using System;
using System.Collections.Generic;

namespace City_Center.Models
{
    public class TorneoResultado
    {
        public class TorneoDetalle
        {
            public int tor_id { get; set; }
            public string tor_nombre { get; set; }
            public string tor_descripcion { get; set; }
            public string tor_imagen { get; set; }
            public string tor_fecha_hora_inicio { get; set; }
            public string tor_fecha_hora_fin { get; set; }
            public int tor_destacado { get; set; }
            public int tor_id_usuario_creo { get; set; }
            public string tor_fecha_hora_creo { get; set; }
            public int? tor_id_usuario_modifico { get; set; }
            public string tor_fecha_hora_modifico { get; set; }
            public int tor_estatus { get; set; }
        }

        public class TorneoReturn
        {
            public int estatus { get; set; }
            public string mensaje { get; set; }
            public List<TorneoDetalle> resultado { get; set; }
        }
    }
}
