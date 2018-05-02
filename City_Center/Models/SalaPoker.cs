using System;
using System.Collections.Generic;

namespace City_Center.Models
{
    public class SalaPokerResultado
    {
        public class SalaPokerDetalle
        {
            public int spo_id { get; set; }
            public string spo_descripcion { get; set; }
            public string spo_imagen { get; set; }
            public int spo_id_usuario_creo { get; set; }
            public string spo_fecha_hora_creo { get; set; }
            public int spo_id_usuario_modifico { get; set; }
            public string spo_fecha_hora_modifico { get; set; }
            public string spo_estatus { get; set; }
            public string spo_eliminado { get; set; }
        }

        public class SalaPokerReturn
        {
            public int estatus { get; set; }
            public string mensaje { get; set; }
            public List<SalaPokerDetalle> resultado { get; set; }
        }
    }
}
