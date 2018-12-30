using System;
using System.Collections.Generic;
using System.Text;
using TODO.MODELS.ResponseModel;
using TODO.MODELS.DataModels;
namespace TODO.MODELS.Contracts
{
   public interface IBusinessLogic
    {
        ToDoResponse Create( ToDoDataModel objtodo);
        ToDoResponse Update(int id,ToDoDataModel objtodo);
        ToDoResponse Get(int id);
        ToDoResponse GetAll(PaginationModel.Paging paging);
        ToDoResponse Delete(int id);
    }

}
