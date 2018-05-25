using System;
namespace City_Center.Models
{
    public class GuardaFavoritosResultado
    {
		public class GuardaFavoritosDetalle
        {
            public string gua_id_usuario { get; set; }
            public string gua_id_usuario_creo { get; set; }
            public int gua_id_evento { get; set; }
            public int gua_id_promocion { get; set; }
            public int gua_id_torneo { get; set; }
            public int gua_id_destacado { get; set; }
            public string gua_fecha_hora_creo { get; set; }
            public string gua_fecha_hora_modifico { get; set; }
            public int gua_id { get; set; }
        }

        public class GuardaFavoritosReturn
        {
            public int estatus { get; set; }
            public string mensaje { get; set; }
            public GuardaFavoritosDetalle resultado { get; set; }
        }
    }
}
