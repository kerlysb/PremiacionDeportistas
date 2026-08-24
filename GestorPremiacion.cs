using System.Collections.Generic;
using System.Linq;

namespace PremiacionDeportistas
{
    public class GestorPremiacion
    {
        private HashSet<string> disciplinas;
        private Dictionary<int, Deportista> deportistas;
        private Dictionary<string, List<Deportista>> deportistasPorDisciplina;
        private Dictionary<string, HashSet<string>> paisesPorDisciplina;
        private SortedDictionary<string, int> medallero;
        private Dictionary<TipoMedalla, int> conteoMedallasPorTipo;

        public GestorPremiacion()
        {
            disciplinas = new HashSet<string>();
            deportistas = new Dictionary<int, Deportista>();
            deportistasPorDisciplina = new Dictionary<string, List<Deportista>>();
            paisesPorDisciplina = new Dictionary<string, HashSet<string>>();
            medallero = new SortedDictionary<string, int>();
            conteoMedallasPorTipo = new Dictionary<TipoMedalla, int>
            {
                { TipoMedalla.Oro, 0 },
                { TipoMedalla.Plata, 0 },
                { TipoMedalla.Bronce, 0 }
            };
        }

        public bool RegistrarDisciplina(string nombreDisciplina)
        {
            bool agregada = disciplinas.Add(nombreDisciplina);
            if (agregada)
            {
                deportistasPorDisciplina[nombreDisciplina] = new List<Deportista>();
                paisesPorDisciplina[nombreDisciplina] = new HashSet<string>();
            }
            return agregada;
        }

        public bool RegistrarDeportista(Deportista deportista)
        {
            if (deportistas.ContainsKey(deportista.Id))
                return false;

            if (!disciplinas.Contains(deportista.Disciplina))
                RegistrarDisciplina(deportista.Disciplina);

            deportistas[deportista.Id] = deportista;
            deportistasPorDisciplina[deportista.Disciplina].Add(deportista);
            paisesPorDisciplina[deportista.Disciplina].Add(deportista.Pais);

            if (!medallero.ContainsKey(deportista.Pais))
                medallero[deportista.Pais] = 0;

            return true;
        }
    }
}