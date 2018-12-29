using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TODO.MODELS.REPOSITORY;
using TODO.REPOSITORY;
using TODO.MODELS.DataModels;
using TODO.MODELS.APIModel;
using TODO.MODELS.ResponseModel;
using TODO.MODELS.PaginationModel;
using System.Web.Http;

namespace TODO.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoController : ControllerBase
    {

        private readonly ILogger _loggerservice;
        private readonly IToDoRespository<ToDoDataModel> _todorepo;
        ///// <summary>
        ///// constructor for the Unit Testing
        ///// </summary>
        ///// <param name="todorepo"></param>
        //public ToDoController(IToDoRespository<ToDoDataModel> todorepo)
        //{
        //    _todorepo = todorepo;
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="todorepo"></param>
        /// <param name="loggerservice"></param>
        public ToDoController(IToDoRespository<ToDoDataModel> todorepo, ILogger loggerservice)
        {
            _todorepo = todorepo;
            _loggerservice = loggerservice;
        }

        [HttpGet]
        [Route("GetAll")]
        public ActionResult GetAll([FromQuery]Paging paging)
        {
            int DataCount = _todorepo.GetCount();
            return Ok(new ToDoResponse(statusCode: (int)System.Net.HttpStatusCode.OK, result: _todorepo.list(paging), errorMessage: "",_DataCount:DataCount));
        }

        [HttpGet]
        public ActionResult Get([FromQuery]int id)
        {
            
            var itemtofind = _todorepo.FindById(id);
            if (itemtofind != null)
                return Ok(new ToDoResponse(statusCode: (int)System.Net.HttpStatusCode.OK, result: itemtofind, errorMessage: ""));
            else
                return NotFound(new ToDoResponse(statusCode: (int)System.Net.HttpStatusCode.NotFound, result: null, errorMessage: "Not Found"));

        }


        [HttpPost]
        public ActionResult Create([FromBody]ToDoDataModel todoitem)
        {
            if (ModelState.IsValid)
            {
                _todorepo.Add(todoitem);
                if (todoitem.ID > 0)
                {

                    return Ok(new ToDoResponse(statusCode: (int)System.Net.HttpStatusCode.OK, result: todoitem, errorMessage: ""));
                }
               
            }
            return BadRequest(new ToDoResponse(statusCode: (int)System.Net.HttpStatusCode.BadRequest, result: todoitem, errorMessage: "Error While Adding"));
            
        }

        [HttpPut]
        public ActionResult Update([FromQuery] int id, [FromBody] ToDoDataModel todoitem)
        {
            if (_todorepo.Update(id, todoitem))
            {
                return Ok(new ToDoResponse(statusCode: (int)System.Net.HttpStatusCode.OK, result: todoitem, errorMessage: ""));
            }
            else
            {
                return NotFound(new ToDoResponse(statusCode: (int)System.Net.HttpStatusCode.NotFound, result: null, errorMessage: "Update Unsuccessfull !!"));
            }
        }

        [HttpDelete]
        public ActionResult Delete([FromQuery] int Id)
        {
            if (_todorepo.Delete(Id))
            {
                return Ok(new ToDoResponse(statusCode: (int)System.Net.HttpStatusCode.OK, result: null, errorMessage: ""));
            }
            else
            {
                return NotFound(new ToDoResponse(statusCode: (int)System.Net.HttpStatusCode.NotFound, result: null, errorMessage: "Item Cannot be deleted !"));
            }
        }

    }
}