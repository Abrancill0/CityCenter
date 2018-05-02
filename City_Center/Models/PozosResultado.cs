using System;
using System.Collections.Generic;

namespace City_Center.Models
{
    public class PozosResultado
    {
        
        public class pozosDetalle
        {
            public int poz_id { get; set; }
            public string poz_monto { get; set; }
            public string poz_descripcion { get; set; }
            public string poz_fecha_entrega { get; set; }
            public string poz_imagen { get; set; }
            public int poz_id_usuario_creo { get; set; }
            public string poz_fecha_hora_creo { get; set; }
            public int poz_id_usuario_modifico { get; set; }
            public string poz_fecha_hora_modifico { get; set; }
            public string poz_estatus { get; set; }
            public string poz_eliminado { get; set; }
        }

        public class PozosReturn
        {
            public int estatus { get; set; }
            public string mensaje { get; set; }
            public List<pozosDetalle> resultado { get; set; }
        }
    }
}
