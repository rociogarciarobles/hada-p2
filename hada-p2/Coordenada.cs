using System;

namespace Hada
{
    public class Coordenada
    {
        public int Fila { get; private set; }
        public int Columna { get; private set; }

        public Coordenada()
        {
            Fila = 0;
            Columna = 0;
        }

        public Coordenada(int fila, int columna)
        {
            Fila = fila;
            Columna = columna;
        }

        public Coordenada(string fila, string columna)
        {
            Fila = Convert.ToInt32(fila);
            Columna = Convert.ToInt32(columna);
        }

        public Coordenada(Coordenada otraCoordenada)
        {
            Fila = otraCoordenada.Fila;
            Columna = otraCoordenada.Columna;
        }

        public override string ToString()
        {
            return $"({Fila},{Columna})";
        }

        public override int GetHashCode()
        {
            return this.Fila.GetHashCode() ^ this.Columna.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            return Equals(obj as Coordenada);
        }

        public bool Equals(Coordenada otraCoordenada)
        {
            return Fila == otraCoordenada.Fila && Columna == otraCoordenada.Columna;
        }
    }
}

