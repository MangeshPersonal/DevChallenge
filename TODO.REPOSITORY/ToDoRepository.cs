using System;
using System.Collections.Generic;
using System.Text;
using TODO.MODELS.DataModels;
using TODO.MODELS.REPOSITORY;
using System.Linq;
using TODO.MODELS.PaginationModel;

namespace TODO.REPOSITORY
{
    public class ToDoRepository : IToDoRespository<ToDoDataModel>
    {

        private readonly ToDoContext _ctx;

        public ToDoRepository(ToDoContext ctx)
        {
            _ctx = ctx;
        }

        /// <summary>
        /// Adds the TODO Item in the Database
        /// </summary>
        /// <param name="entity">ToDo Item </param>
        /// <returns></returns>
        public int Add(ToDoDataModel entity)
        {
            _ctx.ToDo.Add(entity);
            int result = _ctx.SaveChanges();
            return entity.ID;
        }
        /// <summary>
        /// Deletes the Item From the Database
        /// </summary>
        /// <param name="id">Id of the Item to be deleted</param>
        /// <returns></returns>
        public bool Delete(int id)
        {
            int result = 0;
            var todoitemremove = FindById(id);
            if (todoitemremove != null)
            {
                _ctx.ToDo.Remove(todoitemremove);
                result = _ctx.SaveChanges();

            }

            return result > 0;

        }

        /// <summary>
        /// Gets the Item From the Db 
        /// </summary>
        /// <param name="Id">id of the Item to find out<param>
        /// <returns></returns>
        public ToDoDataModel FindById(int Id)
        {
            var todoitem = (from t in _ctx.ToDo
                            where t.ID == Id
                            select t).FirstOrDefault();
            return todoitem;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="paging"></param>
        /// <returns></returns>
        public IEnumerable<ToDoDataModel> list(Paging paging)
        {
            int Count = _ctx.ToDo.Count();
            int CurrentPage = paging.pageNumber;
            int PageSize = paging.pageSize;
            int TotalPages = (int)Math.Ceiling(Count / (double)PageSize);
            var todolist = _ctx.ToDo.Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToList();
            return todolist;
        }

        public bool Update(int id, ToDoDataModel entity)
        {
            int result = 0;
            var itemtoupdate = FindById(id);
            if (itemtoupdate != null)
            {
                itemtoupdate.Description = entity.Description;
                itemtoupdate.Title = entity.Title;
                itemtoupdate.Status = entity.Status;
                itemtoupdate.ModifiedOn = DateTime.Now;

                _ctx.ToDo.Update(itemtoupdate);
                result = _ctx.SaveChanges();
            }
            return result > 0;
        }
    }
}
