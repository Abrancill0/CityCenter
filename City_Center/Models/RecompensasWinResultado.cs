using System;
using System.Collections.Generic;

namespace City_Center.Models
{
    public class RecompensasWinResultado
    {
        public class RecompesasWinDetalle
        {
            public int wre_id { get; set; }
            public string wre_nombre { get; set; }
            public string wre_descripcion { get; set; }
            public int wre_puntos { get; set; }
            public int wre_id_usuario_creo { get; set; }
            public string wre_fecha_hora_creo { get; set; }
            public int? wre_id_usuario_modifico { get; set; }
            public string wre_fecha_hora_modifico { get; set; }
            public string wre_estatus { get; set; }
            public string wre_eliminado { get; set; }
            public string Color { get; set; }
        }



        public class RecompesasWinReturn
        {
            public int estatus { get; set; }
            public string mensaje { get; set; }
            public List<RecompesasWinDetalle> resultado { get; set; }
        }
    }
}
