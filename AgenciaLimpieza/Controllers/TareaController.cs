using AgenciaLimpieza.Controllers.Methods;
using AgenciaLimpieza.DataBase;
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

        [HttpPost]
        public ActionResult GetSpecificHomeworks(Asignar data)
        {
            try
            {
                var result = TareaMethods.GetSpecificHomeworks(data);
                return Ok(new { apiName = "GetSpecificHomeworks", error = false, data = result, msg = "OK" });
            }
            catch (Exception ex)
            {
                return Ok(new { error = true, msg = ex.Message, apiName = "GetTareas" });
            }
        }

        [HttpPost]
        public ActionResult GetSpecificHomeworksByUserID(Asignar data)
        {
            try
            {
                var result = TareaMethods.GetSpecificHomeworksByUserID(data);
                return Ok(new { apiName = "GetSpecificHomeworksByUserID", error = false, data = result, msg = "OK" });
            }
            catch (Exception ex)
            {
                return Ok(new { error = true, msg = ex.Message, apiName = "GetSpecificHomeworksByUserID" });
            }
        }

        [HttpPost]
        public ActionResult MarkAsSolvedHomework(Asignar data)
        {
            try
            {
                var result = TareaMethods.MarkAsSolvedHomework(data);
                return Ok(new { apiName = "MarkAsSolvedHomework", error = false, data = result, msg = "OK" });
            }
            catch (Exception ex)
            {
                return Ok(new { error = true, msg = ex.Message, apiName = "GetTareas" });
            }
        }

        [HttpPost]
        public ActionResult ChartCompletedHomeworks(Asignar data)
        {
            try
            {
                var result = TareaMethods.GetChartData(data);
                return Ok(new { apiName = "GetChartData", error = false, data = result, msg = "OK" });
            }
            catch (Exception ex)
            {
                return Ok(new { error = true, msg = ex.Message, apiName = "GetChartData" });
            }
        }

        [HttpGet]
        public ActionResult GetChartDataByCuadrillas()
        {
            try
            {
                var result = TareaMethods.GetChartDataByCuadrillas();
                return Ok(new { apiName = "GetChartDataByCuadrillas", error = false, data = result, msg = "OK" });
            }
            catch (Exception ex)
            {
                return Ok(new { error = true, msg = ex.Message, apiName = "GetChartDataByCuadrillas" });
            }
        }

        [HttpPost]
        public ActionResult InsertImage([FromForm] IFormFile file, [FromForm] int tareaID)
        {
            try
            {
                return Ok(new { error = false, apiName = "InsertImage", msg = "Ok", data = TareaMethods.UploadImage(file, tareaID) });
            }
            catch (Exception ex)
            {
                return Ok(new { error = true, apiName = "InsertImage",  msg = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public ActionResult GetImage(int id)
        {
            List<ImageModel> images = new List<ImageModel>();
            images = SQLService.SelectMethod<ImageModel>($"Select [Data] as ImageData from IMAGES where TareaID = {id}");
            byte[]? imageData = SQLService.SelectMethod<ImageModel>($"Select [Data] as ImageData from IMAGES where TareaID = {id}").FirstOrDefault().ImageData;  
            string? contentType = SQLService.SelectMethod<ImageModel>($"Select [Type] as ImageData from IMAGES where TareaID = {id}").FirstOrDefault().ContentType;

            if (imageData == null)
            {
                return NotFound();
            }

            if (string.IsNullOrWhiteSpace(contentType))
            {
                contentType = "image/png"; // Tipo MIME genérico para datos binarios
            }

            return File(imageData, contentType);
        }

    }
}
