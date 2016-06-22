using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using hackaton_engine.Models;

namespace hackaton_engine.Controllers
{
    public class TestController : ApiController
    {
        private static TestModel[] _data = new TestModel[]
        {
            new TestModel() {Id = 1, Name = "a"},
            new TestModel() {Id = 2, Name = "b"},
        };

        public IEnumerable<TestModel> GetAll()
        {
            return _data;
        }

        public IHttpActionResult Get(int id)
        {
            var testModel = _data.FirstOrDefault(t => t.Id == id);
            if (testModel != null)
            {
                return Ok(testModel);
            }
            return NotFound();
        }
    }
}
