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

        public static List<TareasAsignadas>? GetSpecificHomeworks(Asignar data)
        {
            try
            {
                string query = $@"Select tarea.tareaID, tarea.Nombre as nombre, tarea.Descripcion as descripcion, colonia.Nombre as colonia,
                                        colonia.CodigoPostal as codigoPostal, asignada.Estatus as estatus
                                        from TareasAsignadas as asignada
                                        inner join Tareas as tarea on asignada.TareaID = tarea.TareaID
                                        inner join Colonias as colonia on tarea.ColoniaID = colonia.ColoniaID
                                        where asignada.CuadrillaID = {data.CuadrillaID}";
                List<TareasAsignadas>? list = SQLService.SelectMethod<TareasAsignadas>(query);
                return list;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public static List<TareasAsignadas>? GetSpecificHomeworksByUserID(Asignar data)
        {
            try
            {
                string query = $@"SELECT tarea.TareaID, 
                                       tarea.Nombre AS nombre, 
                                       tarea.Descripcion AS descripcion, 
                                       colonia.Nombre AS colonia,
                                       colonia.CodigoPostal AS codigoPostal, 
                                       asignada.Estatus AS estatus
                                FROM TareasAsignadas AS asignada
                                INNER JOIN Tareas AS tarea ON asignada.TareaID = tarea.TareaID
                                INNER JOIN Colonias AS colonia ON tarea.ColoniaID = colonia.ColoniaID
                                INNER JOIN UserCuadrilla AS uc ON asignada.CuadrillaID = uc.CuadrillaID
                                WHERE uc.UserID = {data.UserID}";
                List<TareasAsignadas>? list = SQLService.SelectMethod<TareasAsignadas>(query);
                return list;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public static bool? MarkAsSolvedHomework(Asignar data)
        {
            try
            {
                string query = @$"Update TareasAsignadas set Estatus = 0 where TareaID = {data.TareaID}";
                SQLService.InsertMethod(query);
                return true;
            }catch (Exception ex)
            {
                return false;
            }
        }
        public static List<ChartModel> GetChartData(Asignar data)
        {
            string query = $@"SELECT 
	                            sum(case when Estatus = 0 then 1 end)as completed,
	                            sum(case when Estatus = 1 then 1 end) as incompleted
                            FROM TareasAsignadas
                            WHERE CuadrillaID = {data.CuadrillaID}";
            return SQLService.SelectMethod<ChartModel>(query);
        }
        public static object GetChartDataByCuadrillas()
        {
            string query = $@"SELECT 
                              cuadrilla.CuadrillaName as name,
                              SUM(CASE WHEN tareas.Estatus = 0 THEN 1 ELSE 0 END) AS completed,
                              SUM(CASE WHEN tareas.Estatus = 1 THEN 1 ELSE 0 END) AS incompleted
                            FROM TareasAsignadas as tareas
                            INNER JOIN Cuadrilla cuadrilla
                            on cuadrilla.CuadrillaID = tareas.CuadrillaID
                            GROUP BY cuadrilla.CuadrillaName";
            List<ChartModel> list = SQLService.SelectMethod<ChartModel>(query);
            object dataResult = new
            {
                names = list.OrderBy(x=> x.name).Select(x=> x.name).ToList(),
                completed = list.OrderBy(x => x.name).Select(x=> x.completed).ToList(),
                incompleted = list.OrderBy(x => x.name).Select(x => x.incompleted).ToList(),
                count = list.Select(x=> x.name).Count()
            };
            return dataResult;
        }

    }
}
