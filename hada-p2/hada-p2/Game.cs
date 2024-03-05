using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Hada
{
    public class Game
    {
        private bool finPartida;
        private Tablero tablero;

        public Game()
        {
            finPartida = false;
            int tamTablero = new Random().Next(4, 10);
            List<Barco> barcos = new List<Barco>();
            tablero = new Tablero(tamTablero, barcos);

            gameLoop();
        }

        private void gameLoop()
        {
            InicializarBarcos();

            while (!finPartida)
            {
                Console.WriteLine(tablero.ToString());

                Console.Write("Introduce la coordenada a la que disparar FILA,COLUMNA ('S' para Salir): ");
                string input = Console.ReadLine();

                if (input.ToLower() == "s")
                {
                    finPartida = true;
                }
                else
                {
                    if (TryParseCoordenada(input, out Coordenada coordenada))
                    {
                        tablero.Disparar(coordenada);
                        if (tablero.TodosBarcosHundidos())
                        {
                            Console.WriteLine("¡Todos los barcos han sido hundidos!");
                            finPartida = true;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Formato de coordenada incorrecto. Vuelve a intentarlo.");
                    }
                }
            }

            Console.WriteLine("Fin del juego.");
        }

        private void InicializarBarcos()
        {
            // Crea al menos tres barcos
            // Puedes ajustar la posición y orientación según tus necesidades
            Barco barco1 = new Barco("THOR", 3, 'h', new Coordenada(0, 0));
            Barco barco2 = new Barco("LOKI", 4, 'v', new Coordenada(2, 3));
            Barco barco3 = new Barco("MAYA", 2, 'h', new Coordenada(5, 1));

           tablero.AgregarBarco(barco1); 
           tablero.AgregarBarco(barco2);
           tablero.AgregarBarco(barco3);
        }

        private bool TryParseCoordenada(string input, out Coordenada coordenada)
        {
            coordenada = null;

            string[] partes = input.Split(',');

            if (partes.Length == 2 && int.TryParse(partes[0], out int fila) && int.TryParse(partes[1], out int columna))
            {
                coordenada = new Coordenada(fila, columna);
                return true;
            }

            return false;
        }
    }
}