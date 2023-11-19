using Microsoft.AspNetCore.Mvc;
using AgenciaLimpieza.Controllers.Models;
using AgenciaLimpieza.Controllers.Methods;
using static AgenciaLimpieza.Controllers.Models.UsuarioModels;

namespace AgenciaLimpieza.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {

        [HttpPost]
        public ActionResult InsertUser(RegisterUser data)
        {
            string apiName = "Insert User";
            try
            {
                var result = UsuarioMethods.InsertUser(data);
                return Ok(new { apiName = apiName, data = result, msg = "ok", error = false });
            }
            catch (Exception ex)
            {
                return Ok(new {apiName = apiName, msg =  ex.Message, error = true});
            }
        }

        [HttpPost]
        public ActionResult Login(RegisterUser data)
        {
            string apiName = "Login";
            try
            {
                var result = UsuarioMethods.Login(data);
                return Ok(new { apiName = apiName, data = result.Auth, session = result.User?.RoleID,
                    userID = result.User?.UserID, msg = "ok", error = false });
            }
            catch (Exception ex)
            {
                return Ok(new { apiName = apiName, msg = ex.Message, error = true });
            }
        }
        [HttpGet]
        public ActionResult GetAllUsers()
        {
            string apiName = "Get All Users";
            try
            {
                var result = UsuarioMethods.GetAllUsers();
                return Ok(new { apiName = apiName, data = result, msg = "ok", error = false });
            }
            catch (Exception ex)
            {
                return Ok(new { apiName = apiName, msg = ex.Message, error = true });
            }
        }

        [HttpGet]
        public ActionResult GetAllRoles()
        {
            string apiName = "Get All Roles";
            try
            {
                var result = UsuarioMethods.GetAllRoles();
                return Ok(new { apiName = apiName, data = result, msg = "ok", error = false });
            }
            catch (Exception ex)
            {
                return Ok(new { apiName = apiName, msg = ex.Message, error = true });
            }
        }
        [HttpPost]
        public ActionResult UpdateUser(RegisterUser data)
        {
            string apiName = "Update User";
            try
            {
                var result = UsuarioMethods.UpdateUser(data);
                return Ok(new { apiName = apiName, data = result, msg = "ok", error = false });
            }
            catch (Exception ex)
            {
                return Ok(new { apiName = apiName, msg = ex.Message, error = true });
            }
        }
        [HttpGet]
        public ActionResult GetUsersWithOutCuadrilla()
        {
            string apiName = "Update User";
            try
            {
                var result = UsuarioMethods.GetUsersWithOutCuadrilla();
                return Ok(new { apiName = apiName, data = result, msg = "ok", error = false });
            }
            catch (Exception ex)
            {
                return Ok(new { apiName = apiName, msg = ex.Message, error = true });
            }
        }
    }
}
