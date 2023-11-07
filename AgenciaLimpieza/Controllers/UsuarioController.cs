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
                return Ok(new { apiName = apiName, data = result, msg = "ok", error = false });
            }
            catch (Exception ex)
            {
                return Ok(new { apiName = apiName, msg = ex.Message, error = true });
            }
        }

    }
}
