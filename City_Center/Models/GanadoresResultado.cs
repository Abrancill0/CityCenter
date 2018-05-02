using System;
using System.Collections.Generic;

namespace City_Center.Models
{
    public class GanadoresResultado
    {
        public class GanadoresDetalle
        {
            public int gan_id { get; set; }
            public string gan_nombre { get; set; }
            public string gan_premio { get; set; }
            public string gan_imagen { get; set; }
            public int gan_id_usuario_creo { get; set; }
            public string gan_fecha_hora_creo { get; set; }
            public int gan_id_usuario_modifico { get; set; }
            public string gan_fecha_hora_modifico { get; set; }
            public string gan_estatus { get; set; }
            public string gan_eliminado { get; set; }
        }

        public class GanadoresReturn
        {
            public int estatus { get; set; }
            public string mensaje { get; set; }
            public List<GanadoresDetalle> resultado { get; set; }
        }
    }
}
