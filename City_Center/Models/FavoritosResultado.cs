using System;
using System.Collections.Generic;

namespace City_Center.Models
{
    public class FavoritosResultado
    {
        public class FavoritosDetalle
        {
            public int gua_id { get; set; }
            public int gua_id_usuario { get; set; }
            public int gua_id_evento { get; set; }
            public int gua_id_promocion { get; set; }
            public int gua_id_usuario_creo { get; set; }
            public string gua_fecha_hora_creo { get; set; }
            public object gua_id_usuario_modifico { get; set; }
            public string gua_fecha_hora_modifico { get; set; }
            public int eve_id { get; set; }
            public string eve_nombre { get; set; }
            public string eve_descripcion { get; set; }
            public string eve_fecha_hora_inicio { get; set; }
            public string eve_fecha_hora_fin { get; set; }
            public int eve_id_usuario_creo { get; set; }
            public string eve_fecha_hora_creo { get; set; }
            public int? eve_id_usuario_modifico { get; set; }
            public object eve_fecha_hora_modifico { get; set; }
            public int eve_num_usuarios_inscritos { get; set; }
            public int eve_num_compartidos { get; set; }
            public int eve_num_favoritos { get; set; }
            public string eve_lista { get; set; }
            public string eve_carrucel { get; set; }
            public int eve_id_locacion { get; set; }
            public object eve_descripcion_locacion { get; set; }
            public string eve_imagen { get; set; }
            public int eve_destacado { get; set; }
            public string updated_at { get; set; }
            public string created_at { get; set; }
            public string eve_link { get; set; }
            public string eve_telefono { get; set; }
            public string eve_tipo { get; set; }

        }

        public class FavoritoReturn
        {
            public int estatus { get; set; }
          
            public List<FavoritosDetalle> resultado { get; set; }
        }
    }
}
