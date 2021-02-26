using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConsumeApi.Models
{
    public class Usuario
    {
        public int ID { get; set; }
        public string usuario1 { get; set; }
        public string apellidos { get; set; }
        public string email { get; set; }
        public string direcion { get; set; }
        public string password { get; set; }
    }
}