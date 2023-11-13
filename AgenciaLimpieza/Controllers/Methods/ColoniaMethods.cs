using AgenciaLimpieza.DataBase;
using static AgenciaLimpieza.Controllers.Models.ColoniaModels;
namespace AgenciaLimpieza.Controllers.Methods
{
    public class ColoniaMethods
    {
        public static bool InsertColonia(Colonia data)
        {
            string query = $@"Insert into Colonias(Nombre, CodigoPostal) values(@Nombre, @CodigoPostal)";
            try
            {
                Dictionary<string, object> param = new();
                param.Add("Nombre", data.Nombre);
                param.Add("CodigoPostal", data.CodigoPostal);
                SQLService.InsertMethod(query, param);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static List<Colonia> GetColonias()
        {
            List<Colonia> list = new();
            try
            {
                string query = $@"select * from Colonias";
                list = SQLService.SelectMethod<Colonia>(query);
                return list;
            }catch (Exception ex)
            {
                return list;
            }
        }

    }
}
