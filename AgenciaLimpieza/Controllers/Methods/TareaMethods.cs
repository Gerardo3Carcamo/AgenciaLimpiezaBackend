using AgenciaLimpieza.Controllers.Models;
using AgenciaLimpieza.DataBase;
using static AgenciaLimpieza.Controllers.Models.TareaModels;
namespace AgenciaLimpieza.Controllers.Methods
{
    public class TareaMethods
    {

        public static bool InsertTarea(Tarea data)
        {
            try
            {
                string query = $@"Insert into Tareas(Nombre, ColoniaID, Descripcion, Disponible) values(@Nombre, @ColoniaID, @Descripcion, @Disponible)";
                Dictionary<string, object> param = new();
                param.Add("Nombre", data.Nombre);
                param.Add("ColoniaID", data.ColoniaID);
                param.Add("Descripcion", data.Descripcion);
                param.Add("Disponible", 0);
                SQLService.InsertMethod(query, param);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static List<Tarea> GetTareas()
        {
            List<Tarea> list = new();
            try
            {
                 string query = $@"SELECT TOP (1000) tarea.[TareaID]
                                        ,tarea.[Nombre]
                                        ,tarea.[Descripcion]
                                        ,tarea.[Disponible]
	                                    ,colonia.[Nombre] as Colonia
                                    FROM [GestionLimpieza].[dbo].[Tareas] AS tarea
                                    Inner join [GestionLimpieza].[dbo].Colonias as colonia on
                                    tarea.ColoniaID = colonia.ColoniaID
                                    LEFT JOIN dbo.TareasAsignadas AS ta ON tarea.TareaID = ta.TareaID
                                    WHERE ta.TareaID IS NULL and tarea.Disponible = 0";
                list = SQLService.SelectMethod<Tarea>(query);
                return list;
            }
            catch (Exception ex)
            {
                return list;
            }
        }

        public static bool AsignarTareasCuadrilla(Asignar data)
        {
            string query = $@"Insert into TareasAsignadas(CuadrillaID, TareaID, Estatus) values(@CuadrillaID, @TareaID, @Estatus)";
            try
            {
                Dictionary<string, object> param = new();
                param.Add("CuadrillaID", data.CuadrillaID);
                param.Add("TareaID", data.TareaID);
                param.Add("Estatus", 1);
                SQLService.InsertMethod(query, param);
                string update = $@"Update Tareas set Disponible = 1 where TareaID = @TareaID";
                param = new Dictionary<string, object>();
                param.Add("TareaID", data.TareaID);
                SQLService.InsertMethod(query, param);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
