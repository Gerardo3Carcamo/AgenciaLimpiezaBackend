using AgenciaLimpieza.DataBase;
using static AgenciaLimpieza.Controllers.Models.CuadrillaModels;
using static AgenciaLimpieza.Controllers.Models.UsuarioModels;
namespace AgenciaLimpieza.Controllers.Methods
{
    public class CuadrillaMethods
    {
        public static bool InsertCuadrilla(Cuadrilla data)
        {
            string query = $@"Insert into Cuadrilla(CuadrillaName, UserID) values(@CuadrillaName, @UserID)";
            Dictionary<string, object> param = new();
            param.Add("CuadrillaName", data.CuadrillaName);
            param.Add("UserID", data.UserID);
            SQLService.InsertMethod(query, param);
            return true;
        }

        public static Cuadrilla? GetCuadrillaByUser(Cuadrilla data)
        {
            string query = $@"Select * from Cuadrilla where UserID = {data.UserID}";
            return SQLService.SelectMethod<Cuadrilla>(query)?.FirstOrDefault();
        }
        public static bool InsertUserIntoCuadrilla(Cuadrilla data)
        {
            string query = $@"Insert into UserCuadrilla(UserID, CuadrillaID) values(@UserID, @CuadrillaID)";
            Dictionary<string, object> param = new();
            param.Add("UserID", data.UserID);
            param.Add("CuadrillaID", data.CuadrillaID);
            SQLService.InsertMethod(query, param);
            return true;
        }

        public static List<RegisterUser> GetUsersOnMyCuadrilla(Cuadrilla data)
        {
            try
            {
                return SQLService.SelectMethod<RegisterUser>($@"Select u.UserID, u.UserName as Name, u.UserMail as Email, u.UserPhone as Phone, u.RoleID as RoleID
                                                                FROM users AS u
                                                                JOIN UserCuadrilla AS uc ON u.UserID = uc.UserID
                                                                JOIN Cuadrilla AS c ON uc.CuadrillaID = c.CuadrillaID
                                                                WHERE c.UserID = {data.UserID};");
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
