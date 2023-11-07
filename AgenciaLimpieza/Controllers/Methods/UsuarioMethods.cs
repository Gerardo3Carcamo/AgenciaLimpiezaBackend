using AgenciaLimpieza.Controllers.Models;
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

        public static string Login(RegisterUser data)
        {
            string query = $@"select * from dbo.users where userMail = '{data.Email}' and userPassword = '{data.Password}'";
            try
            {
                List<RegisterUser> list = SQLService.SelectMethod<RegisterUser>(query);
                return list.Count() > 0 ? "DONE" : "REJECTED";
            }
            catch (Exception ex)
            {
                return "REJECTED";
            }
        }
    }
}
