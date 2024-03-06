using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Hada
{
    public class Tablero
    {
        private const string AGUA = "[AGUA]";

        public int TamTablero { get; }
        private List<Coordenada> coordenadasDisparadas;
        private List<Coordenada> coordenadasTocadas;
        private List<Barco> barcos;
        private List<Barco> barcosEliminados;
        private Dictionary<Coordenada, string> casillasTablero;

        public event EventHandler<EventArgs> eventoFinPartida;

        public Tablero(int tamTablero, List<Barco> barcos)
        {
            TamTablero = tamTablero;
            this.barcos = barcos;
            coordenadasDisparadas = new List<Coordenada>();
            coordenadasTocadas = new List<Coordenada>();
            barcosEliminados = new List<Barco>();
            casillasTablero = new Dictionary<Coordenada, string>();

            InicializaCasillasTablero();
            InicializarEventosBarcos();
        }

        private void InicializaCasillasTablero()
        {
            for (int fila = 0; fila < TamTablero; fila++)
            {
                for (int columna = 0; columna < TamTablero; columna++)
                {
                    Coordenada coordenada = new Coordenada(fila, columna);
                    casillasTablero[coordenada] = AGUA;
                }
            }

            foreach (Barco barco in barcos)
            {
                foreach (Coordenada coordenada in barco.CoordenadasBarco.Keys)
                {
                    casillasTablero[coordenada] = $"[{barco.Nombre}]";
                    
                }
            }
        }

        private void InicializarEventosBarcos()
        {
            foreach (Barco barco in barcos)
            {
                barco.eventoTocado += CuandoEventoTocado;
                barco.eventoHundido += CuandoEventoHundido;
            }
        }

        public void Disparar(Coordenada c)
        {
            if (c.Fila < 0 || c.Fila >= TamTablero || c.Columna < 0 || c.Columna >= TamTablero)
            {
                Console.WriteLine($"La coordenada ({c.Fila},{c.Columna}) está fuera de las dimensiones del tablero.");
                return;
            }

            coordenadasDisparadas.Add(c);

            foreach (Barco barco in barcos)
            {
                barco.Disparo(c);
            }

            Console.WriteLine(DibujarTablero());
        }
        public void AgregarBarco(Barco barco)
        {
            foreach (var coordenada in barco.CoordenadasBarco.Keys)
            {
                if (coordenadasTocadas.Contains(coordenada))
                {
                    throw new Exception("No se puede agregar un barco en una casilla ya tocada.");
                }
            }

            foreach (var coordenada in barco.CoordenadasBarco.Keys)
            {
                casillasTablero[coordenada] = $"[{barco.Nombre}]";
            }

            barcos.Add(barco);
            barco.eventoTocado += CuandoEventoTocado;
            barco.eventoHundido += CuandoEventoHundido;
        }

        public string DibujarTablero()
        {
            string tablero = "";
            
            for (int fila = 0; fila < TamTablero; fila++)
            {
                for (int columna = 0; columna < TamTablero; columna++)
                {
                    Coordenada coordenada = new Coordenada(fila, columna);
                    tablero += $"{casillasTablero[coordenada]} ";
                }
                tablero += "\n";
            }
            return tablero;
        }

        public override string ToString()
        {
            string infoBarcos = "";
            foreach (Barco barco in barcos)
            {
                infoBarcos += barco.ToString() + "\n";
            }

            string infoCoordenadasDisparadas = $"Coordenadas Disparadas: {string.Join(", ", coordenadasDisparadas)}\n";
            string infoCoordenadasTocadas = $"Coordenadas Tocadas: {string.Join(", ", coordenadasTocadas)}\n";
            

            return infoBarcos + infoCoordenadasDisparadas + infoCoordenadasTocadas + $"\n\n\nCASILLAS TABLERO\n-------\n" + DibujarTablero();
        }

        private void CuandoEventoTocado(object sender, TocadoArgs e)
        {
            coordenadasTocadas.Add(e.CoordenadaImpacto);
            casillasTablero[e.CoordenadaImpacto] = $"[{e.Nombre}_T]";
            Console.WriteLine($"TABLERO: Barco [{e.Nombre}] tocado en Coordenada: [{e.CoordenadaImpacto}]");
        }

        private void CuandoEventoHundido(object sender, HundidoArgs e)
        {
            barcosEliminados.Add((Barco)sender);
            Console.WriteLine($"TABLERO: Barco [{e.Nombre}] hundido!!");
            if (TodosBarcosHundidos())
            {
                eventoFinPartida?.Invoke(this, EventArgs.Empty);
            }
        }

        public bool TodosBarcosHundidos()
        {
            return barcosEliminados.Count == barcos.Count;
        }
    }
}
