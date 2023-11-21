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
            public int UserID { get; set;}  
        }

        public class TareasAsignadas
        {
            public int tareaID { get; set; }
            public string? nombre { get; set; }
            public string? descripcion { get; set; }
            public string? colonia { get; set; }
            public string? codigoPostal { get; set; }
            public int estatus { get; set; }
        }
        public class ChartModel
        {
            public string? name { get; set; }
            public int completed { get; set; }
            public int incompleted { get; set; }
            public string? status { get; set; }
        }
    }
}
