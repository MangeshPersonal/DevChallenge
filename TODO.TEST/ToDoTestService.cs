using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TODO.MODELS.DataModels;
using TODO.MODELS.PaginationModel;
using TODO.MODELS.REPOSITORY;

namespace TODO.TEST
{
    class ToDoTestService : IToDoRespository<ToDoDataModel>
    {
        private readonly List<TODO.MODELS.DataModels.ToDoDataModel> todoItems;

        public ToDoTestService()
        {
            todoItems = new List<ToDoDataModel>()
          {
              new ToDoDataModel(){ ID=1, Title="test title 1",Status=false,Description="test Description 1"},
              new ToDoDataModel(){ ID=2 ,Title="test title 2",Status=true,Description="test Description 2"},
              new ToDoDataModel(){ ID=3 ,Title="test title 3",Status=false,Description="test Description 3"}

          };
        }

        public ToDoDataModel Add(ToDoDataModel entity)
        {
            entity.ID = 5;
            todoItems.Add(entity);
            return entity;
        }

        public bool Delete(int Id)
        {
            var existingitem = FindById(Id);
            if (existingitem != null)
            {
                todoItems.Remove(existingitem);
                return true;
            }
            else { return false; }
        }

        public ToDoDataModel FindById(int Id)
        {
            var todomodel = todoItems.FirstOrDefault(a => a.ID == Id);
            return todomodel;
        }

        public IEnumerable<ToDoDataModel> GetAll()
        {
            return todoItems;
        }

        public int GetCount()
        {
            return todoItems.Count();
        }

        public IEnumerable<ToDoDataModel> list(Paging paging)
        {
            int Count = todoItems.Count();
            int CurrentPage = paging.pageNumber;
            int PageSize = paging.pageSize;
            int TotalPages = (int)Math.Ceiling(Count / (double)PageSize);
            var todolist = todoItems.Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToList();
            return todolist;
        }

        public bool Update(int id, ToDoDataModel entity)
        {
            bool result = false;
            var todoitem = FindById(id);
            if(todoitem!=null)
            {
                foreach (var obj in todoItems)
                {
                    if (obj.ID == id)
                    {
                        obj.Description = obj.Description;
                        result= true;
                        break;
                    }
            
                }
            }
            return result;


        }


    }




}
