using System;
using System.Collections.Generic;
using System.Text;
using TODO.BaseEntity;

namespace TODO.CONTRACTS
{
    public interface IToDoRespository
    {
        IEnumerable<ToDo> list(int pagesize,int pagenumber);
        bool Add(T entity);
        bool Delete(int id);
        bool Update(int id,T entity);
        T FindById(int Id);

    }
}
