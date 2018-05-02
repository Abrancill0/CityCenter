using System;
using System.Collections.Generic;

namespace City_Center.Models
{
    public class RestaurantResultado
    {

        public class RestaurantDetalle
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
        }

        public class RestaurantReturn
        {
            public int estatus { get; set; }
            public string mensaje { get; set; }
            public List<RestaurantDetalle> resultado { get; set; }
        }
    }
}
