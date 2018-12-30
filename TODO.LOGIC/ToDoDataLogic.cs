using System;
using System.Collections.Generic;
using System.Text;
using TODO.MODELS.Contracts;
using TODO.MODELS.DataModels;
using TODO.MODELS.PaginationModel;
using TODO.MODELS.REPOSITORY;
using TODO.MODELS.ResponseModel;
using Microsoft.Extensions.Logging;
namespace TODO.LOGIC
{
    public class ToDoDataLogic : IBusinessLogic
    {
        private readonly ILogger _loggerservice;
        private readonly IToDoRespository<ToDoDataModel> _todorepo;

        public ToDoDataLogic(IToDoRespository<ToDoDataModel> todorepo, ILogger loggerservice)
        {
            _todorepo = todorepo;
            _loggerservice = loggerservice;
        }

        public ToDoResponse Create(ToDoDataModel objtodo)
        {
            _todorepo.Add(objtodo);
            if (objtodo.ID > 0)
            {
                return new ToDoResponse(statusCode: (int)System.Net.HttpStatusCode.OK, result: objtodo, errorMessage: "");
            }
            else
            {
                return new ToDoResponse(statusCode: (int)System.Net.HttpStatusCode.InternalServerError, result: objtodo, errorMessage: "Problem while adding Item");
            }
        }
        public ToDoResponse Delete(int id)
        {
            if (_todorepo.Delete(id))
            {
                return new ToDoResponse(statusCode: (int)System.Net.HttpStatusCode.OK, result: null, errorMessage: "");
            }
            else
            {
                return new ToDoResponse(statusCode: (int)System.Net.HttpStatusCode.InternalServerError, result: null, errorMessage: "Problem while deleting Item");
            }

        }

        public ToDoResponse Get(int id)
        {
            var objtodo = _todorepo.FindById(id);
            if(objtodo!=null)
            {
                return new ToDoResponse(statusCode: (int)System.Net.HttpStatusCode.OK, result: objtodo, errorMessage: "");
            }
            else
            {
                return new ToDoResponse(statusCode: (int)System.Net.HttpStatusCode.NotFound, result: objtodo, errorMessage: "");
            }
           
        }

        public ToDoResponse GetAll(Paging paging)
        {
            int DataCount = _todorepo.GetCount();

            return new ToDoResponse(statusCode: (int)System.Net.HttpStatusCode.OK, result: _todorepo.list(paging), errorMessage: "", _DataCount: DataCount);
        }

        public ToDoResponse Update(int id, ToDoDataModel objtodo)
        {
            if (_todorepo.Update(id, objtodo))
            {
                return new ToDoResponse(statusCode: (int)System.Net.HttpStatusCode.OK, result: objtodo, errorMessage: "");
            }
            else
            {
                return new ToDoResponse(statusCode: (int)System.Net.HttpStatusCode.NotFound, result: null, errorMessage: "Update Unsuccessfull !!");
            }
        }
    }
}

