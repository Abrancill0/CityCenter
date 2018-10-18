using System;
namespace City_Center.Models
{
	public class CambiaContrasenaResultado
	{
		public class CambiacontrasenaDetalle
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
			public string usu_id_tarjeta_socio { get; set; }
			public string usu_ciudad { get; set; }
			public string usu_tipo_documento { get; set; }
			public string usu_no_documento { get; set; }
			public string usu_contrasena_temp { get; set; }
			public string usu_imagen { get; set; }
			public int usu_id_rol { get; set; }
			public int usu_estatus { get; set; }
			public string created_at { get; set; }
			public string updated_at { get; set; }
		}

		public class CambiaContrasenaReturn
		{
			public int estatus { get; set; }
			public string mensaje { get; set; }
			public CambiacontrasenaDetalle resultado { get; set; }
		}
	}
}
