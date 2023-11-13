using AgenciaLimpieza.Controllers.Methods;
using AgenciaLimpieza.Controllers.Models;
using Microsoft.AspNetCore.Mvc;
using static AgenciaLimpieza.Controllers.Models.ColoniaModels;
namespace AgenciaLimpieza.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ColoniaController : ControllerBase
    {

        [HttpGet]
        public ActionResult GetColonias()
        {
            try
            {
                var result = ColoniaMethods.GetColonias();
                return Ok(new { apiName = "GetTareas", error = false, data = result, msg = "OK" });
            }
            catch (Exception ex)
            {
                return Ok(new { error = true, msg = ex.Message, apiName = "GetTareas" });
            }
        }

        [HttpPost]
        public ActionResult InsertColonia(Colonia data)
        {
            try
            {
                var result = ColoniaMethods.InsertColonia(data);
                return Ok(new { apiName = "GetTareas", error = false, data = result, msg = "OK" });
            }
            catch (Exception ex)
            {
                return Ok(new { error = true, msg = ex.Message, apiName = "GetTareas" });
            }
        }

    }
}
