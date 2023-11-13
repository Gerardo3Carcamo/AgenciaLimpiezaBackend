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
                string query = $@"Insert into Tareas(Nombre, ColoniaID, Descripcion) values(@Nombre, @ColoniaID, @Descripcion)";
                Dictionary<string, object> param = new();
                param.Add("Nombre", data.Nombre);
                param.Add("ColoniaID", data.ColoniaID);
                param.Add("Descripcion", data.Descripcion);
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
                string query = $@"Select t.TareaID, t.Nombre, t.Descripcion, c.Nombre as Colonia from Tareas as t
                                    inner join Colonias as c
                                    on t.ColoniaID = c.ColoniaID";
                list = SQLService.SelectMethod<Tarea>(query);
                return list;
            }
            catch (Exception ex)
            {
                return list;
            }
        }

    }
}
