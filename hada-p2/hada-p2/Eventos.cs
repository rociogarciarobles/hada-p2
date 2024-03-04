using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hada
{
    class Eventos
    {

    }
    public class TocadoArgs : EventArgs
    {
        public string Nombre { get; }
        public Coordenada CoordenadaImpacto { get; }
        public string Etiqueta { get; }

        public TocadoArgs(string nombre, Coordenada coordenadaImpacto, string etiqueta)
        {
            Nombre = nombre;
            CoordenadaImpacto = coordenadaImpacto;
            Etiqueta = etiqueta;
        }
    }

    public class HundidoArgs : EventArgs
    {
        public string Nombre { get; }

        public HundidoArgs(string nombre)
        {
            Nombre = nombre;
        }
    }
}
