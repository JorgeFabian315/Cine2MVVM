using System;
using System.Collections.Generic;
using System.Text;

namespace Cine2MVVM.Models
{
    public class Pelicula
    {
        public string Titulo { get; set; } = "";
        public string Portada { get; set; } = "";
        public string Director { get; set; } = "";
        public string Descripcion { get; set; } = "";
        public byte Puntuacion { get; set; }
        public int  Año { get; set; }

    }
}
