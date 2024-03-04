using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hada_p2
{
    class Barco
    {
        // Propiedad para almacenar las coordenadas y etiquetas del barco
        public Dictionary<Coordenada, string> CoordenadasBarco { get; private set; }

        // Propiedad para el nombre del barco
        public string Nombre { get; }

        // Constructor que recibe el nombre del barco y las coordenadas iniciales
        public Barco(string nombre, List<Coordenada> coordenadasIniciales)
        {
            Nombre = nombre;
            CoordenadasBarco = new Dictionary<Coordenada, string>();

            // Inicializar las etiquetas de cada coordenada con el nombre del barco
            foreach (var coordenada in coordenadasIniciales)
            {
                CoordenadasBarco.Add(coordenada, nombre);
            }
        }

        // Método que simula un disparo en una coordenada
        public void Disparo(Coordenada coordenada)
        {
            // Verificar si la coordenada pertenece al barco
            if (CoordenadasBarco.ContainsKey(coordenada))
            {
                // Modificar la etiqueta de la coordenada al ser tocada
                CoordenadasBarco[coordenada] += "_T";
            }
        }
    }

    // Clase para representar las coordenadas
    public class Coordenada
    {
        public int X { get; }
        public int Y { get; }

        public Coordenada(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}

