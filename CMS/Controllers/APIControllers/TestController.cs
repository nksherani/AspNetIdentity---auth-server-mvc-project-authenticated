using CMS.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CMS.Controllers.APIControllers
{
    public class TestController : ApiController
    {
        // GET: api/Test
        public Test Get()
        {
            Test obj = new Test();
            obj.name = "mariam";
            obj.password = "123";
                
            return obj ;
        }
        [HttpGet]
        public HttpResponseMessage bag()
        {
            List<Test> testList = new List<Test>();
            Test obj = new Test();
            obj.name = "mariam";
            obj.password = "123";
            testList.Add(obj);
            obj = new Test();
            obj.name = "mariam1";
            obj.password = "123";
            testList.Add(obj);
            return Request.CreateResponse(HttpStatusCode.NotFound, testList);

        }

        // GET: api/Test/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Test
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Test/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Test/5
        public void Delete(int id)
        {
        }
    }
}
