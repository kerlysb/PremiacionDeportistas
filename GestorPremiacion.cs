using System;
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
            disciplinas = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            deportistas = new Dictionary<int, Deportista>();
            deportistasPorDisciplina = new Dictionary<string, List<Deportista>>(StringComparer.OrdinalIgnoreCase);
            paisesPorDisciplina = new Dictionary<string, HashSet<string>>(StringComparer.OrdinalIgnoreCase);
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
            if (string.IsNullOrWhiteSpace(nombreDisciplina)) return false;
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
            if (deportista == null || deportistas.ContainsKey(deportista.Id))
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

        public bool OtorgarPremio(int idDeportista, TipoMedalla medalla)
        {
            if (!deportistas.ContainsKey(idDeportista))
                return false;

            Deportista deportista = deportistas[idDeportista];
            deportista.Medallas.Add(medalla);

            medallero[deportista.Pais] = medallero[deportista.Pais] + 1;
            conteoMedallasPorTipo[medalla] = conteoMedallasPorTipo[medalla] + 1;

            return true;
        }

        public IEnumerable<string> ObtenerDisciplinas()
        {
            return disciplinas.OrderBy(d => d);
        }

        public List<Deportista> ObtenerDeportistasPorDisciplina(string disciplina)
        {
            if (disciplina != null && deportistasPorDisciplina.ContainsKey(disciplina))
                return deportistasPorDisciplina[disciplina];
            return new List<Deportista>();
        }

        public HashSet<string> ObtenerPaisesPorDisciplina(string disciplina)
        {
            if (disciplina != null && paisesPorDisciplina.ContainsKey(disciplina))
                return paisesPorDisciplina[disciplina];
            return new HashSet<string>();
        }

        public Deportista? BuscarDeportistaPorId(int id)
        {
            deportistas.TryGetValue(id, out Deportista? deportista);
            return deportista;
        }

        public SortedDictionary<string, int> ObtenerMedallero()
        {
            return medallero;
        }

        public Dictionary<TipoMedalla, int> ObtenerConteoPorTipoMedalla()
        {
            return conteoMedallasPorTipo;
        }

        public int TotalDeportistas()
        {
            return deportistas.Count;
        }

        public int TotalDisciplinas()
        {
            return disciplinas.Count;
        }
    }
}