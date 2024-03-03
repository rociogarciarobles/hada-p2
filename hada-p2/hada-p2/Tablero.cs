using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hada
{
    public class Tablero
    {
        private const string AGUA = "AGUA";
        private const string TOCADO = "_T";
        private const string HUNDIDO = "HUNDIDO";

        private int TamTablero { get; set; }
        private List<Coordenada> coordenadasDisparadas;
        private List<Coordenada> coordenadasTocadas;
        private List<Barco> barcos;
        private List<Barco> barcosEliminados;
        private Dictionary<Coordenada, string> casillasTablero;

        public event EventHandler<EventArgs> eventoFinPartida;

        public Tablero(int tamTablero, List<Barco> barcos)
        {
            TamTablero = tamTablero;
            coordenadasDisparadas = new List<Coordenada>();
            coordenadasTocadas = new List<Coordenada>();
            barcos = barcos;
            barcosEliminados = new List<Barco>();
            casillasTablero = new Dictionary<Coordenada, string>();

            foreach (var barco in barcos)
            {
                barco.EventoTocado += cuandoEventoTocado;
                barco.EventoHundido += cuandoEventoHundido;
            }

            inicializaCasillasTablero();
        }

        private void inicializaCasillasTablero()
        {
            for (int fila = 0; fila < TamTablero; fila++)
            {
                for (int columna = 0; columna < TamTablero; columna++)
                {
                    var coordenada = new Coordenada(fila, columna);
                    var estado = AGUA;

                    foreach (var barco in barcos)
                    {
                        if (barco.Casillas.Contains(coordenada))
                        {
                            estado = barco.Nombre;
                            break;
                        }
                    }

                    casillasTablero.Add(coordenada, estado);
                }
            }
        }

        public void Disparar(Coordenada c)
        {
            if (c.Fila < 0 || c.Fila >= TamTablero || c.Columna < 0 || c.Columna >= TamTablero)
            {
                Console.WriteLine($"La coordenada {c.ToString()} está fuera de las dimensiones del tablero.");
                return;
            }

            coordenadasDisparadas.Add(c);

            foreach (var barco in barcos)
            {
                barco.ComprobarTocado(c);
            }
        }

        private void cuandoEventoTocado(object sender, BarcoEventArgs e)
        {
            coordenadasTocadas.Add(e.Coordenada);
            casillasTablero[e.Coordenada] += TOCADO;
            Console.WriteLine($"TABLERO: Barco [{e.Barco.Nombre}] tocado en Coordenada: {e.Coordenada.ToString()}");
        }

        private void cuandoEventoHundido(object sender, BarcoEventArgs e)
        {
            Console.WriteLine($"TABLERO: Barco [{e.Barco.Nombre}] hundido!!");
            barcosEliminados.Add(e.Barco);

            if (barcosEliminados.Count == barcos.Count)
            {
                eventoFinPartida?.Invoke(this, EventArgs.Empty);
            }
        }

        public string DibujarTablero()
        {
            string tablero = "";
            for (int fila = 0; fila < TamTablero; fila++)
            {
                for (int columna = 0; columna < TamTablero; columna++)
                {
                    var coordenada = new Coordenada(fila, columna);
                    tablero += casillasTablero[coordenada].PadRight(10);
                }
                tablero += "\n";
            }
            return tablero;
        }

        public override string ToString()
        {
            string info = "Información de Barcos:\n";
            foreach (var barco in barcos)
            {
                info += barco.ToString() + "\n";
            }

            info += "\nCoordenadas Disparadas:\n";
            foreach (var coordenada in coordenadasDisparadas)
            {
                info += coordenada.ToString() + "\n";
            }

            info += "\nCoordenadas Tocadas:\n";
            foreach (var coordenada in coordenadasTocadas)
            {
                info += coordenada.ToString() + "\n";
            }

            info += "\nTablero:\n";
            info += DibujarTablero();

            return info;
        }
    }
}
