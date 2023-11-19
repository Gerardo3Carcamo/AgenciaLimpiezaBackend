using static AgenciaLimpieza.Controllers.Models.UsuarioModels;
using AgenciaLimpieza.DataBase;

namespace AgenciaLimpieza.Controllers.Methods
{
    public class UsuarioMethods
    {
        public static bool InsertUser(RegisterUser data)
        {
            string query = $@"insert into dbo.users (UserName, UserMail, UserPhone, UserPassword) values(@Name, @Email, @Phone, @Password)";
            try
            {
                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("Name", data?.Name);
                param.Add("Email", data?.Email);
                param.Add("Phone", data?.Phone);
                param.Add("Password", data?.Password);
                SQLService.InsertMethod(query, param);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static Session Login(RegisterUser data)
        {
            string query = $@"SELECT TOP (1000) [UserID] as UserID
                              ,[UserName] as Name
                              ,[UserMail] as Email
                              ,[UserPassword] as Password
                              ,[UserPhone] as Phone
                              ,[RoleID] as RoleID
                          FROM [GestionLimpieza].[dbo].[users] where userMail = '{data.Email}' and userPassword = '{data.Password}'";
            try
            {
                List<RegisterUser> list = SQLService.SelectMethod<RegisterUser>(query);
                Session dataResult = new Session();
                dataResult.Auth = list.Count() > 0 ? "DONE" : "REJECTED";
                dataResult.User = list.FirstOrDefault();
                return dataResult;
            }
            catch (Exception ex)
            {
                return new Session();
            }
        }

        public static List<RegisterUser> GetAllUsers()
        {
            string query = $@"Select UserID as UserID, UserName as Name, UserMail as Email, UserPhone as Phone from dbo.users";
            List<RegisterUser> users = new();
            try
            {
                users = SQLService.SelectMethod<RegisterUser>(query);
                return users;
            }
            catch (Exception ex)
            {
                return users;
            }
        }

        public static List<Roles>? GetAllRoles()
        {
            try
            {
                return SQLService.SelectMethod<Roles>("Select * from Roles");
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static bool UpdateUser(RegisterUser data)
        {
            string query = $@"Update users set RoleID = @RoleID where UserID = @UserID";
            try
            {
                Dictionary<string, object>? param = new();
                param.Add("RoleID", data.RoleID);
                param.Add("UserID", data.UserID);
                SQLService.InsertMethod(query, param);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static List<RegisterUser> GetUsersWithOutCuadrilla()
        {
            try
            {
                return SQLService.SelectMethod<RegisterUser>($@"Select u.UserID, u.UserName as Name, u.UserMail as Email, u.UserPhone as Phone, u.RoleID as RoleID from users as u 
                                                                LEFT JOIN UserCuadrilla AS c ON u.UserID = c.UserID
                                                                WHERE c.UserID IS NULL;");
            }
            catch (Exception ex)
            {
                return null;
            }
        }

    }
}
