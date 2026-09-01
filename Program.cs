using System;

namespace PremiacionDeportistas
{
    class Program
    {
        static GestorPremiacion gestor = new GestorPremiacion();

        static void Main(string[] args)
        {
            CargarDatosIniciales();
            int opcion;

            do
            {
                MostrarMenu();
                opcion = LeerOpcion();

                switch (opcion)
                {
                    case 1:
                        RegistrarDisciplina();
                        break;
                    case 2:
                        RegistrarDeportista();
                        break;
                    case 3:
                        OtorgarPremio();
                        break;
                    case 4:
                        ConsultarPorDisciplina();
                        break;
                    case 5:
                        MostrarMedallero();
                        break;
                    case 6:
                        MostrarDisciplinas();
                        break;
                    case 7:
                        BuscarPorId();
                        break;
                    case 8:
                        MostrarReporteGeneral();
                        break;
                    case 0:
                        Console.WriteLine("Saliendo del sistema...");
                        break;
                    default:
                        Console.WriteLine("Opcion invalida.");
                        break;
                }

                if (opcion != 0)
                {
                    Console.WriteLine("\nPresione ENTER para continuar...");
                    Console.ReadLine();
                }

            } while (opcion != 0);
        }

        static void CargarDatosIniciales()
        {
            gestor.RegistrarDisciplina("Atletismo");
            gestor.RegistrarDisciplina("Natacion");
            gestor.RegistrarDisciplina("Ciclismo");

            gestor.RegistrarDeportista(new Deportista(1, "Carlos Perez", "Ecuador", "Atletismo"));
            gestor.RegistrarDeportista(new Deportista(2, "Maria Lopez", "Colombia", "Natacion"));
            gestor.RegistrarDeportista(new Deportista(3, "Juan Rodriguez", "Ecuador", "Ciclismo"));

            gestor.OtorgarPremio(1, TipoMedalla.Oro);
            gestor.OtorgarPremio(2, TipoMedalla.Plata);
            gestor.OtorgarPremio(3, TipoMedalla.Bronce);
        }

        static void MostrarMenu()
        {
            Console.Clear();
            Console.WriteLine("=== SISTEMA DE PREMIACION DE DEPORTISTAS ===");
            Console.WriteLine("1. Registrar disciplina");
            Console.WriteLine("2. Registrar deportista");
            Console.WriteLine("3. Otorgar premio a deportista");
            Console.WriteLine("4. Consultar deportistas por disciplina");
            Console.WriteLine("5. Mostrar medallero por pais");
            Console.WriteLine("6. Mostrar disciplinas registradas");
            Console.WriteLine("7. Buscar deportista por ID");
            Console.WriteLine("8. Reporte general");
            Console.WriteLine("0. Salir");
            Console.Write("Seleccione una opcion: ");
        }

        static int LeerOpcion()
        {
            bool valido = int.TryParse(Console.ReadLine(), out int opcion);
            return valido ? opcion : -1;
        }

        static void RegistrarDisciplina()
        {
            Console.Write("Nombre de la disciplina: ");
            string? disciplina = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(disciplina))
            {
                Console.WriteLine("Nombre de disciplina inválido.");
                return;
            }

            bool resultado = gestor.RegistrarDisciplina(disciplina.Trim());
            Console.WriteLine(resultado
                ? "Disciplina registrada correctamente."
                : "La disciplina ya existe en el conjunto.");
        }

        static void RegistrarDeportista()
        {
            Console.Write("ID: ");
            if (!int.TryParse(Console.ReadLine(), out int id)) { Console.WriteLine("ID inválido."); return; }

            Console.Write("Nombre: ");
            string? nombre = Console.ReadLine();
            Console.Write("Pais: ");
            string? pais = Console.ReadLine();
            Console.Write("Disciplina: ");
            string? disciplina = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(pais) || string.IsNullOrWhiteSpace(disciplina))
            {
                Console.WriteLine("Todos los campos son obligatorios.");
                return;
            }

            Deportista nuevo = new Deportista(id, nombre.Trim(), pais.Trim(), disciplina.Trim());
            bool resultado = gestor.RegistrarDeportista(nuevo);
            Console.WriteLine(resultado
                ? "Deportista registrado correctamente."
                : "Ya existe un deportista con ese ID.");
        }

        static void OtorgarPremio()
        {
            Console.Write("ID del deportista: ");
            if (!int.TryParse(Console.ReadLine(), out int id)) { Console.WriteLine("ID inválido."); return; }

            Console.WriteLine("Tipo de medalla: 1-Oro 2-Plata 3-Bronce");
            if (!int.TryParse(Console.ReadLine(), out int tipo)) { Console.WriteLine("Opción inválida."); return; }

            TipoMedalla medalla;
            switch (tipo)
            {
                case 1: medalla = TipoMedalla.Oro; break;
                case 2: medalla = TipoMedalla.Plata; break;
                case 3: medalla = TipoMedalla.Bronce; break;
                default:
                    Console.WriteLine("Opcion de medalla invalida.");
                    return;
            }

            bool resultado = gestor.OtorgarPremio(id, medalla);
            Console.WriteLine(resultado
                ? "Premio otorgado correctamente."
                : "No se encontro el deportista.");
        }

        static void ConsultarPorDisciplina()
        {
            Console.Write("Disciplina a consultar: ");
            string? disciplina = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(disciplina)) return;

            var deportistas = gestor.ObtenerDeportistasPorDisciplina(disciplina.Trim());
            var paises = gestor.ObtenerPaisesPorDisciplina(disciplina.Trim());

            Console.WriteLine($"\nDeportistas en {disciplina}:");
            if (deportistas.Count == 0)
                Console.WriteLine("No hay deportistas registrados en esta disciplina.");

            foreach (var d in deportistas)
                Console.WriteLine(d);

            Console.WriteLine($"\nPaises participantes en {disciplina}: {string.Join(", ", paises)}");
        }

        static void MostrarMedallero()
        {
            var medallero = gestor.ObtenerMedallero();
            Console.WriteLine("\n=== MEDALLERO POR PAIS ===");
            foreach (var par in medallero)
                Console.WriteLine($"{par.Key}: {par.Value} medallas");
        }

        static void MostrarDisciplinas()
        {
            Console.WriteLine("\n=== DISCIPLINAS REGISTRADAS ===");
            foreach (var disciplina in gestor.ObtenerDisciplinas())
                Console.WriteLine("- " + disciplina);
        }

        static void BuscarPorId()
        {
            Console.Write("ID del deportista: ");
            if (!int.TryParse(Console.ReadLine(), out int id)) { Console.WriteLine("ID inválido."); return; }

            var deportista = gestor.BuscarDeportistaPorId(id);
            if (deportista != null)
            {
                Console.WriteLine($"\nDeportista encontrado: {deportista}");
            }
            else
            {
                Console.WriteLine("\nNo se encontró ningún deportista con ese ID.");
            }
        }

        static void MostrarReporteGeneral()
        {
            Console.WriteLine("\n=== REPORTE GENERAL ===");
            Console.WriteLine($"Total de Disciplinas: {gestor.TotalDisciplinas()}");
            Console.WriteLine($"Total de Deportistas: {gestor.TotalDeportistas()}");
            
            var conteoMedallas = gestor.ObtenerConteoPorTipoMedalla();
            Console.WriteLine("\nMedallas entregadas por tipo:");
            foreach (var item in conteoMedallas)
            {
                Console.WriteLine($"- {item.Key}: {item.Value}");
            }
        }
    }
}