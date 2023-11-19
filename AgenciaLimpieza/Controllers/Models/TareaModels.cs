namespace AgenciaLimpieza.Controllers.Models
{
    public class TareaModels
    {

        public class Tarea
        {
            public int TareaID { get; set; }
            public string? Nombre { get; set; }
            public int ColoniaID { get; set; }
            public string? Descripcion { get; set;}
            public string? Colonia { get; set;}
        }

        public class Asignar
        {
            public int CuadrillaID { get; set;}
            public int TareaID { get; set;}
        }

    }
}
