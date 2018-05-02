using System;
using System.Collections.Generic;

namespace City_Center.Models
{
    public class TarjetasResultado
    {
        public class TarjetasDetalle
        {
            public int tar_id { get; set; }
            public string tar_nombre { get; set; }
            public string tar_descripcion { get; set; }
            public string tar_imagen { get; set; }
            public int tar_id_usuario_creo { get; set; }
            public string tar_fecha_hora_creo { get; set; }
            public object tar_id_usuario_modifico { get; set; }
            public object tar_fecha_hora_modifico { get; set; }
            public string tar_estatus { get; set; }
        }

        public class TarjetasReturn
        {
            public int estatus { get; set; }
        
            public List<TarjetasDetalle> resultado { get; set; }
        }
    }
}
