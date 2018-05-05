using System;
namespace City_Center.Models
{
    public class ValidaUsuarioResultado
    {
        public class ValidaUsuarioReturn
        {
            public int estatus { get; set; }
            public string resultado { get; set; }
            public int usu_id { get; set; }
            public string usu_usuario { get; set; }
            public string usu_usuario_bloquedado { get; set; }
            public string usu_nombre { get; set; }
            public string usu_apellidos { get; set; }
            public string usu_email { get; set; }
            public string usu_telefono { get; set; }
            public string usu_celular { get; set; }
            public string usu_fecha_registro { get; set; }
            public string usu_fecha_nacimiento { get; set; }
            public int usu_id_tarjeta_socio { get; set; }
            public string usu_ciudad { get; set; }
            public string usu_imagen { get; set; }
            public int usu_id_rol { get; set; }
            public int usu_estatus { get; set; }
        }
    }
}
