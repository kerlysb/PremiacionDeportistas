using System.Collections.Generic;

namespace PremiacionDeportistas
{
    public class Deportista
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Pais { get; set; }
        public string Disciplina { get; set; }
        public List<TipoMedalla> Medallas { get; set; }

        public Deportista(int id, string nombre, string pais, string disciplina)
        {
            Id = id;
            Nombre = nombre;
            Pais = pais;
            Disciplina = disciplina;
            Medallas = new List<TipoMedalla>();
        }

        public override string ToString()
        {
            return $"[{Id}] {Nombre} - {Pais} - {Disciplina} - Medallas: {Medallas.Count}";
        }
    }
}