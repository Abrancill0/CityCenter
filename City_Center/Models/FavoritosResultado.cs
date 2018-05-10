using System;
using System.Collections.Generic;

namespace City_Center.Models
{
    public class FavoritosResultado
    {
        
        public class FavoritoDetalle
        {
            public int gua_id { get; set; }
            public int gua_id_usuario { get; set; }
            public int gua_id_evento { get; set; }
            public int gua_id_promocion { get; set; }
            public int gua_id_torneo { get; set; }
            public int gua_id_destacado { get; set; }
            public int gua_id_usuario_creo { get; set; }
            public string gua_fecha_hora_creo { get; set; }
            public int gua_id_usuario_modifico { get; set; }
            public string gua_fecha_hora_modifico { get; set; }
            public string nombre { get; set; }
            public string descripcion { get; set; }
            public string imagen { get; set; }
            public string imagen_2 { get; set; }
            public string link { get; set; }
            public string fecha { get; set; }
			public bool ocultallamada { get; set; }
			public bool ocultaonline { get; set; }
			public bool ocultatorneo { get; set; }
			public string telefono { get; set; }
        }

        public class FavoritoReturn
        {
            public int estatus { get; set; }
            public List<FavoritoDetalle> resultado { get; set; }
        }
    }
}
