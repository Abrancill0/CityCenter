using System;
using System.Collections.Generic;

namespace City_Center.Models
{
    public class DestacadosResultado
    {
        public class DestacadosDetalle
        {
            public int des_id { get; set; }
            public int des_id_evento { get; set; }
            public int des_id_promocion { get; set; }
            public object des_tipo_promocion { get; set; }
            public object des_tipo_evento { get; set; }
            public int des_id_torneo { get; set; }
            public string des_nombre { get; set; }
            public string des_descripcion { get; set; }
            public string des_imagen { get; set; }
            public DateTime des_fecha_hora_inicio { get; set; }
            public string des_fecha_hora_fin { get; set; }
            public int des_id_usuario_creo { get; set; }
            public string des_fecha_hora_creo { get; set; }
            public int des_id_usuario_modifico { get; set; }
            public string des_fecha_hora_modifico { get; set; }
            public bool oculta { get; set; }
            public bool des_guardado { get; set; }
            public int des_id_guardado { get; set; }
            public string des_link { get; set; }
            public string des_telefono { get; set; }
            public bool ocultallamada { get; set; }
            public bool ocultaonline { get; set; }
        }

        public class DestacadosReturn
        {
            public int estatus { get; set; }
            public string mensaje { get; set; }
            public List<DestacadosDetalle> resultado { get; set; }
        }
    }
}
