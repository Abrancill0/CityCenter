using System;
using System.Collections.Generic;

namespace City_Center.Models
{
    public class MoaSpaResultado
    {
        public class MoaSpaDetalle
        {
            public int gal_id { get; set; }
            public string gal_descripcion { get; set; }
            public string gal_imagen { get; set; }
            public string gal_galeria { get; set; }
            public int gal_id_usuario_creo { get; set; }
            public string gal_fecha_hora_creo { get; set; }
            public object gal_id_usuario_modifico { get; set; }
            public string gal_fecha_hora_modifico { get; set; }
            public string gal_estatus { get; set; }
            public string gal_eliminado { get; set; }
        }

        public class MoaSpaReturn
        {
            public int estatus { get; set; }
            public string mensaje { get; set; }
            public List<MoaSpaDetalle> resultado { get; set; }
        }
    }
}
