using System;
using Xunit;
using TODO.API.Controllers;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using TODO.MODELS.ResponseModel;
using TODO.LOGIC;

namespace TODO.TEST
{
    public class GetMethodTests
    {

        private readonly ToDoController todotestcotroller;
        private readonly ToDoTestService todoservice;
        private readonly ToDoDataLogic todobuisnesslogic;

        public GetMethodTests()
        {
            todoservice = new ToDoTestService();
            todobuisnesslogic = new TODO.LOGIC.ToDoDataLogic(todoservice,null);
            todotestcotroller = new ToDoController(null, todobuisnesslogic);
        }

        [Fact]
        public void Get_OK_Result()
        {
            // Act
            var okResult = todotestcotroller.Get(1);

            // Assert
            Assert.IsType<Microsoft.AspNetCore.Mvc.OkObjectResult>(okResult);


        }
        [Fact]
        public void Get_NotFound_Result()
        {
            // Act
            var NotFoundresult = todotestcotroller.Get(5);

            // Assert
            Assert.IsType<Microsoft.AspNetCore.Mvc.NotFoundObjectResult>(NotFoundresult);


        }

        [Fact]
        public void Get_Ok_IItemReturned()
        {
            // Act
            var okResult = todotestcotroller.Get(1);
            var founditem = ((ObjectResult)okResult).Value;
            // Assert
            //var todoitem=< MODELS.DataModels.ToDoDataModel>founditem;
            int statuscode = ((TODO.MODELS.ResponseModel.ToDoResponse)((Microsoft.AspNetCore.Mvc.ObjectResult)okResult).Value).StatusCode;
            Assert.Equal("200", statuscode.ToString());

        }
        [Fact]
        public void Get_All_Success()
        {
            // Act
            var okResult = todotestcotroller.GetAll(new MODELS.PaginationModel.Paging() { pageNumber = 1, pageSize = 1 });
            var resultset = (ToDoResponse)((ObjectResult)okResult).Value;

            int resultcount = ((System.Collections.Generic.List<TODO.MODELS.DataModels.ToDoDataModel>)resultset.Data).Capacity;
            // Assert
            Assert.Equal("200", resultset.StatusCode.ToString());
            Assert.Equal(1, resultcount);

        }
        [Fact]
        public void Get_All_Failed()
        {
            // Act
            var okResult = todotestcotroller.GetAll(new MODELS.PaginationModel.Paging() { pageNumber = 5, pageSize = 1 });
            var resultset = (ToDoResponse)((ObjectResult)okResult).Value;
            // Assert
            int resultcount = ((System.Collections.Generic.List<TODO.MODELS.DataModels.ToDoDataModel>)resultset.Data).Capacity;

            Assert.Equal("200", resultset.StatusCode.ToString());
            Assert.Equal(0, resultcount);

        }


    }
}
