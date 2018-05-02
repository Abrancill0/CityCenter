using System;
using System.Collections.Generic;

namespace City_Center.Models
{
    public class RegistroUsuario
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

        public class RegistroResult
        {
            public int estatus { get; set; }
            public Datos datos { get; set; }
            public List<string> resultado { get; set; }
        }
    }
}
