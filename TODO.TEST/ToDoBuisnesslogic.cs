using System;
using System.Collections.Generic;
using System.Text;
using TODO.MODELS.Contracts;
using TODO.MODELS.DataModels;
using TODO.MODELS.PaginationModel;
using TODO.MODELS.ResponseModel;

namespace TODO.TEST
{
    public class ToDoBuisnesslogic : IBusinessLogic
    {
        private readonly ToDoTestService todoservice;
        public ToDoBuisnesslogic()
        {
            todoservice = new ToDoTestService();

        }

        public ToDoResponse Create(ToDoDataModel objtodo)
        {
            
        }

        public ToDoResponse Delete(int id)
        {
            throw new NotImplementedException();
        }

        public ToDoResponse Get(int id)
        {
            throw new NotImplementedException();
        }

        public ToDoResponse GetAll(Paging paging)
        {
            throw new NotImplementedException();
        }

        public ToDoResponse Update(int id, ToDoDataModel objtodo)
        {
            throw new NotImplementedException();
        }
    }
}
