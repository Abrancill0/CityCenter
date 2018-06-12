using System;
using System.Collections.Generic;

namespace City_Center.Models
{
    public class HabitacionesResultado
    {
        public class HabitacionesDetalle
        {
            public int hab_id { get; set; }
            public string hab_nombre { get; set; }
            public string hab_descripcion { get; set; }
            public string hab_imagen { get; set; }
            public string hab_titulo_1 { get; set; }
            public string hab_descripcion_1 { get; set; }
            public string hab_imagen_1 { get; set; }
            public string hab_titulo_2 { get; set; }
            public string hab_descripcion_2 { get; set; }
            public string hab_imagen_2 { get; set; }
            public string hab_titulo_3 { get; set; }
            public string hab_descripcion_3 { get; set; }
            public string hab_imagen_3 { get; set; }
            public string hab_imagen_4 { get; set; }
            public string hab_imagen_5 { get; set; }
            public string hab_imagen_6 { get; set; }
            public int hab_id_usuario_creo { get; set; }
            public string hab_fecha_hora_creo { get; set; }
            public int hab_id_usuario_modifico { get; set; }
            public string hab_fecha_hora_modifico { get; set; }
            public string hab_estatus { get; set; }

        }

        public class HabitacionesReturn
        {
            public int estatus { get; set; }
            public string mensaje { get; set; }
            public List<HabitacionesDetalle> resultado { get; set; }
        }
    }
}
