using System;
using System.Collections.Generic;
using System.Text;

namespace City_Center.Models
{
   public class LoginResultado
    {

        public class Attributes
        {
        }

        public class Request
        {
        }

        public class Query
        {
        }

        public class Server
        {
        }

        public class Files
        {
        }

        public class Cookies
        {
        }

        public class Headers
        {
        }

        public class Datos
        {
            public Attributes attributes { get; set; }
            public Request request { get; set; }
            public Query query { get; set; }
            public Server server { get; set; }
            public Files files { get; set; }
            public Cookies cookies { get; set; }
            public Headers headers { get; set; }
        }


        public class Resultado
        {
            public int usu_id { get; set; }
            public string usu_usuario { get; set; }
            public string usu_contrasena { get; set; }
            public string usu_tipo_contrasena { get; set; }
            public string usu_usuario_bloquedado { get; set; }
            public string usu_nombre { get; set; }
            public string usu_apellidos { get; set; }
            public string usu_email { get; set; }
            public string usu_telefono { get; set; }
            public string usu_celular { get; set; }
            public string usu_fecha_registro { get; set; }
            public string usu_fecha_nacimiento { get; set; }
            public int usu_id_tarjeta_socio { get; set; }
            public string usu_imagen { get; set; }
            public string usu_ciudad { get; set; }
            public int usu_id_rol { get; set; }
            public int usu_estatus { get; set; }
            public string created_at { get; set; }
            public string updated_at { get; set; }
            public string usu_tipo_documento { get; set; }
         
            public string usu_no_documento { get; set; }
        }

        public class LoginReturn
        {
            public int estatus { get; set; }
            public string mensaje { get; set; }
            public Datos datos { get; set; }
            public Resultado resultado { get; set; }
        }
    }
}
