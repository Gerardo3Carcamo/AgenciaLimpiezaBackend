using AgenciaLimpieza.Controllers.Methods;
using Microsoft.AspNetCore.Mvc;
using static AgenciaLimpieza.Controllers.Models.CuadrillaModels;

namespace AgenciaLimpieza.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CuadrillaController : ControllerBase
    {
        [HttpPost]
        public ActionResult InsertCuadrilla(Cuadrilla data)
        {
            try
            {
                var result = CuadrillaMethods.InsertCuadrilla(data);
                return Ok(new { apiName = "InsertCuadrilla", error = false, data = result, msg = "OK" });
            }
            catch (Exception ex)
            {
                return Ok(new { error = true, msg = ex.Message, apiName = "InsertCuadrilla" });
            }
        }
        [HttpPost]
        public ActionResult GetCuadrillaByUser(Cuadrilla data)
        {
            try
            {
                var result = CuadrillaMethods.GetCuadrillaByUser(data);
                return Ok(new { apiName = "GetCuadrillaByUser", error = false, data = result, msg = "OK" });
            }
            catch (Exception ex)
            {
                return Ok(new { error = true, msg = ex.Message, apiName = "InsertCuadrilla" });
            }
        }
        [HttpPost]
        public ActionResult InsertUserIntoCuadrilla(Cuadrilla data)
        {
            try
            {
                var result = CuadrillaMethods.InsertUserIntoCuadrilla(data);
                return Ok(new { apiName = "InsertUserIntoCuadrilla", error = false, data = result, msg = "OK" });
            }
            catch (Exception ex)
            {
                return Ok(new { error = true, msg = ex.Message, apiName = "InsertUserIntoCuadrilla" });
            }
        }
        [HttpPost]
        public ActionResult GetUsersOnMyCuadrilla(Cuadrilla data)
        {
            try
            {
                var result = CuadrillaMethods.GetUsersOnMyCuadrilla(data);
                return Ok(new { apiName = "GetUsersOnMyCuadrilla", error = false, data = result, msg = "OK" });
            }
            catch (Exception ex)
            {
                return Ok(new { error = true, msg = ex.Message, apiName = "GetUsersOnMyCuadrilla" });
            }
        }
    }
}
