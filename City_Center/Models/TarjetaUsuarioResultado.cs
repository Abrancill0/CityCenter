using System;
using System.Collections.Generic;

namespace City_Center.Models
{
    public class TarjetaUsuarioResultado
    {
        public class TarjetaUsuarioDetalle
        {
            public string tar_id { get; set; }
            public string tar_id_tipo { get; set; }
            public int tar_puntos { get; set; }
            public string tar_fecha_hora_creo { get; set; }
            public string tar_fecha_hora_modifico { get; set; }
            public string tar_imagen { get; set; }
        }

        public class TarjetaUsuarioReturn
        {
            public int estatus { get; set; }
            public string mensaje { get; set; }
            public TarjetaUsuarioDetalle resultado { get; set; }
        }


    }
}
