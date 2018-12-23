using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TODO.MODELS.REPOSITORY;
using TODO.REPOSITORY;
using TODO.MODELS.DataModels;
using TODO.MODELS.APIModel;
using TODO.MODELS.ResponseModel;

namespace TODO.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoController : ControllerBase
    {

        private readonly ILogger _loggerservice;
        private readonly IToDoRespository<ToDoDataModel> _todorepo;
        public ToDoController(IToDoRespository<ToDoDataModel> todorepo, ILogger loggerservice)
        {
            _todorepo = todorepo;
            _loggerservice = loggerservice;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }
        [HttpPost]
        public IActionResult Create([FromBody]ToDoApiModel todoitem)
        {
            throw new System.Exception();
            int result = _todorepo.Add(new ToDoDataModel() { Title = todoitem.Title, Description = todoitem.Description, Status = todoitem.Status });
            if (result > 0)
            {
                todoitem.ID = result;
                return Ok(new ToDoResponse(statusCode: (int)System.Net.HttpStatusCode.OK, result: todoitem, errorMessage: ""));
            }
            else
            {
                return BadRequest(new ToDoResponse(statusCode: (int)System.Net.HttpStatusCode.BadRequest, result: todoitem, errorMessage: ""));
            }
        }


    }
}