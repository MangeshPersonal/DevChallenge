using System;
using System.Collections.Generic;
using System.Text;
using TODO.API.Controllers;
using Xunit;
using TODO.MODELS.DataModels;
using TODO.MODELS.ResponseModel;
using Microsoft.AspNetCore.Mvc;
using TODO.LOGIC;

namespace TODO.TEST
{
    public class AddUpdateTest
    {

        private readonly ToDoController todotestcotroller;
        private readonly ToDoTestService todoservice;
        private readonly ToDoDataLogic todobuisnesslogic;


        public AddUpdateTest()
        {
            todoservice = new ToDoTestService();
            todobuisnesslogic = new TODO.LOGIC.ToDoDataLogic(todoservice, null);
            todotestcotroller = new ToDoController(null,todobuisnesslogic);
        }

        [Fact]
        public void Add_Validate_Entity()
        {
            var tododataModel = new TODO.MODELS.DataModels.ToDoDataModel();
            todotestcotroller.ModelState.AddModelError("Title", "Required");
            var todoresponse = todotestcotroller.Create(tododataModel);

            Assert.IsType<BadRequestObjectResult>(todoresponse);
        }
        [Fact]
        public void Add_Success_Result()
        {
            var tododataModel = new TODO.MODELS.DataModels.ToDoDataModel() { Title = "testtitle", Description = "testdescription", Status = true };

            var todoresponse = todotestcotroller.Create(tododataModel);
            int returnedid = ((ToDoDataModel)((ToDoResponse)((ObjectResult)todoresponse).Value).Data).ID;

            Assert.IsType<OkObjectResult>(todoresponse);
            Assert.Equal(5, returnedid);

        }


        [Fact]
        public void Update_Record_NotFound()
        {
            var todoresponse = todotestcotroller.Update(10, new ToDoDataModel());
            Assert.IsType<NotFoundObjectResult>(todoresponse);
        }
        [Fact]
        public void Update_RecordFound_Update_SuccessFull()
        {
            var todoresponse = todotestcotroller.Update(1, new ToDoDataModel() { Description = "test", Title = "" });
            Assert.IsType<OkObjectResult>(todoresponse);
            string actual = ((ToDoDataModel)((ToDoResponse)((ObjectResult)todoresponse).Value).Data).Description;
            Assert.Equal("test", actual);
        }

        [Fact]

        public void Delete_Record_NotFound()
        {
            var todoresponse = todotestcotroller.Delete(10);
            Assert.IsType<NotFoundObjectResult>(todoresponse);
        }

        [Fact]
        public void Delete_Record_Success()
        {
            var todoresponse = todotestcotroller.Delete(1);
            Assert.IsType<OkObjectResult>(todoresponse);
        }
    }
}
