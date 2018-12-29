using System;
using System.Collections.Generic;
using System.Text;
using TODO.BaseEntity;

namespace TODO.MODELS.REPOSITORY
{
    public interface IToDoRespository<T>  
    {
        IEnumerable<T> list(PaginationModel.Paging paging);
        T Add(T entity);
        bool Delete(int id);
        bool Update(int id,T entity);
        T FindById(int Id);
        IEnumerable<T> GetAll();
        int GetCount();
    }
}
