using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TODO.MODELS.REPOSITORY;
using TODO.REPOSITORY;
using TODO.MODELS.DataModels;
using TODO.MODELS.APIModel;
using TODO.MODELS.ResponseModel;
using TODO.MODELS.PaginationModel;
using System.Web.Http;
using TODO.MODELS.Contracts;

namespace TODO.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoController : ControllerBase
    {

        private readonly ILogger _loggerservice;
        private readonly IBusinessLogic _buisnesslogic;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="todorepo"></param>
        /// <param name="loggerservice"></param>
        public ToDoController(ILogger loggerservice, IBusinessLogic logic)
        {
            _loggerservice = loggerservice;
            _buisnesslogic = logic;
        }

        [HttpGet]
        [Route("GetAll")]
        public ActionResult GetAll([FromQuery]Paging paging)
        {

            var objresponse = _buisnesslogic.GetAll(paging);
            return SetResponse(objresponse);
        }

        [HttpGet]
        public ActionResult Get([FromQuery]int id)
        {
            var objresponse = _buisnesslogic.Get(id);
            return SetResponse(objresponse);
        }


        [HttpPost]
        public ActionResult Create([FromBody]ToDoDataModel todoitem)
        {
            var objresponse = _buisnesslogic.Create(todoitem);
            return SetResponse(objresponse);
        }

        [HttpPut]
        public ActionResult Update([FromQuery] int id, [FromBody] ToDoDataModel todoitem)
        {
            var objresponse = _buisnesslogic.Update(id, todoitem);
            return SetResponse(objresponse);

        }
        [HttpDelete]
        public ActionResult Delete([FromQuery] int Id)
        {
            var objresponse = _buisnesslogic.Delete(Id);
            return SetResponse(objresponse);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="objresponse"></param>
        /// <returns></returns>
        private ActionResult SetResponse(ToDoResponse objresponse)
        {

            switch (objresponse.StatusCode)
            {
                case (int)System.Net.HttpStatusCode.OK:
                    return Ok(objresponse);
                case (int)System.Net.HttpStatusCode.BadRequest:
                    return BadRequest(objresponse);
                case (int)System.Net.HttpStatusCode.NotFound:
                    return NotFound(objresponse);
                default:
                    return BadRequest(objresponse);
            }

        }
    }

}