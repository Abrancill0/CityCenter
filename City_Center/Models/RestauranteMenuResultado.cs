using System;
using System.Collections.Generic;

namespace City_Center.Models
{
    public class RestauranteMenuResultado
    {
        public class Restaurant
        {
            public int reb_id { get; set; }
            public string reb_nombre { get; set; }
            public string reb_descripcion { get; set; }
            public string reb_descripcion_horario { get; set; }
            public int reb_ver_hotel_spa { get; set; }
            public int reb_reservas { get; set; }
            public string reb_tipo { get; set; }
            public string reb_imagen_1 { get; set; }
            public string reb_imagen_2 { get; set; }
            public string reb_imagen_3 { get; set; }
            public string reb_imagen_4 { get; set; }
            public int reb_id_usuario_creo { get; set; }
            public string reb_fecha_hora_creo { get; set; }
            public int reb_id_usuario_modifico { get; set; }
            public string reb_fecha_hora_modifico { get; set; }
            public string reb_estatus { get; set; }
            public string reb_chk_ver_hotel_spa { get; set; }
            public string reb_chk_reservas { get; set; }
        }

        public class MenuNombre
        {
            public int men_id { get; set; }
            public string men_nombre { get; set; }
            public string men_descripcion { get; set; }
            public int men_id_restaurant_bar { get; set; }
            public int men_id_usuario_creo { get; set; }
            public string men_fecha_hora_creo { get; set; }
            public int men_id_usuario_modifico { get; set; }
            public string men_fecha_hora_modifico { get; set; }
            public string men_estatus { get; set; }
        }

        public class MenuDetalle
        {
            public int mde_id { get; set; }
            public int mde_id_menu { get; set; }
            public int mde_id_restaurant { get; set; }
            public string mde_nombre { get; set; }
            public string mde_descripcion { get; set; }
            public string mde_imagen { get; set; }
            public string mde_precio { get; set; }
            public int mde_id_usuario_creo { get; set; }
            public string mde_fecha_hora_creo { get; set; }
            public int mde_id_usuario_modifico { get; set; }
            public string mde_fecha_hora_modifico { get; set; }
            public string mde_estatus { get; set; }
            public string NombreMenu { get; set; }
        }

        public class RestauranteMenuDetalle
        {
            public string restaurant_bar { get; set; }
            public Restaurant restaurant { get; set; }
            public List<MenuNombre> menu { get; set; }
            public List<MenuDetalle> menu_detalle { get; set; }
        }

        public class RestauranteMenuReturn
        {
            public int estatus { get; set; }
            public string mensaje { get; set; }
            public RestauranteMenuDetalle resultado { get; set; }
        }

    }
}
