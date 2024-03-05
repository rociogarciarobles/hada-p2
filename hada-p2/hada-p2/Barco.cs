using Hada;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hada
{
    public class Barco
    {
        public event EventHandler<TocadoArgs> eventoTocado;
        public event EventHandler<HundidoArgs> eventoHundido;

        public Dictionary<Coordenada, string> CoordenadasBarco { get; private set; }
        public string Nombre { get; private set; }
        public int NumDanyos { get; private set; }

        public Barco(string nombre, int longitud, char orientacion, Coordenada coordenadaInicio)
        {
            CoordenadasBarco = new Dictionary<Coordenada, string>();
            Nombre = nombre;
            NumDanyos = 0;

            InicializarCoordenadas(longitud, orientacion, coordenadaInicio);
        }

        private void InicializarCoordenadas(int longitud, char orientacion, Coordenada coordenadaInicio)
        {
            for (int i = 0; i < longitud; i++)
            {
                Coordenada nuevaCoordenada;

                if (orientacion == 'h')
                {
                    nuevaCoordenada = new Coordenada(coordenadaInicio.Fila, coordenadaInicio.Columna + i);
                }
                else // 'v'
                {
                    nuevaCoordenada = new Coordenada(coordenadaInicio.Fila + i, coordenadaInicio.Columna);
                }

                CoordenadasBarco.Add(nuevaCoordenada, Nombre);
            }
        }

        public void Disparo(Coordenada c)
        {
            if (CoordenadasBarco.ContainsKey(c))
            {
                string etiqueta = CoordenadasBarco[c];

                if (!etiqueta.EndsWith("_T"))
                {
                    CoordenadasBarco[c] = etiqueta + "_T";
                    NumDanyos++;

                    eventoTocado?.Invoke(this, new TocadoArgs(Nombre, c, etiqueta));

                    if (Hundido())
                    {
                        eventoHundido?.Invoke(this, new HundidoArgs($"El barco {Nombre} ha sido hundido."));
                    }
                }
            }
        }

        public bool Hundido()
        {
            foreach (var etiqueta in CoordenadasBarco.Values)
            {
                if (!etiqueta.EndsWith("_T"))
                {
                    return false;
                }
            }

            return true;
        }

        public override string ToString()
        {
            string result = $"Barco: {Nombre}\nDaños: {NumDanyos}\nEstado: {(Hundido() ? "Hundido" : "A flote")}\nCoordenadas:\n";

            foreach (var kvp in CoordenadasBarco)
            {
                result += $"{kvp.Key.Fila},{kvp.Key.Columna} - {kvp.Value}\n";
            }

            return result;
        }
    }
}

