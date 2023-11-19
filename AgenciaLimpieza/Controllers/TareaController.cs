using AgenciaLimpieza.Controllers.Methods;
using Microsoft.AspNetCore.Mvc;
using static AgenciaLimpieza.Controllers.Models.TareaModels;
namespace AgenciaLimpieza.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TareaController : ControllerBase
    {

        [HttpGet]
        public ActionResult GetTareas()
        {
            try
            {
                var result = TareaMethods.GetTareas();
                return Ok(new { apiName = "GetTareas", error = false, data = result, msg = "OK" });
            }catch (Exception ex)
            {
                return Ok(new { error = true, msg = ex.Message, apiName = "GetTareas" });
            }
        }

        [HttpPost]
        public ActionResult InsertTarea(Tarea data)
        {
            try
            {
                var result = TareaMethods.InsertTarea(data);
                return Ok(new { apiName = "InsertTarea", error = false, data = result, msg = "OK" });
            }
            catch (Exception ex)
            {
                return Ok(new { error = true, msg = ex.Message, apiName = "GetTareas" });
            }
        }

        [HttpPost]
        public ActionResult AsignarTareasCuadrilla(Asignar data)
        {
            try
            {
                var result = TareaMethods.AsignarTareasCuadrilla(data);
                return Ok(new { apiName = "InsertTarea", error = false, data = result, msg = "OK" });
            }
            catch (Exception ex)
            {
                return Ok(new { error = true, msg = ex.Message, apiName = "GetTareas" });
            }
        }

    }
}
