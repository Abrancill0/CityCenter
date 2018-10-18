using System;
using System.Collections.Generic;

namespace City_Center.Models
{
    public class MensajesPendientesResultado
    {
		public class MensajesPendientesDetalle
        {
            public string ccn_mensaje { get; set; }
        }

        public class MensajesPendientesReturn
        {
            public List<MensajesPendientesDetalle> msn { get; set; }
        }
    }
}
