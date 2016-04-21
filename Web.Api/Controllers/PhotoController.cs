using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Web.Api.Infrastructure.Repository;
using Web.Api.Models;

namespace Web.Api.Controllers
{
    public class PhotoController : ApiController
    {
        private IRepository _repository;

        public PhotoController()
        {
            _repository = new Repository();
        }

        public PhotoController(IRepository repository)
        {
            _repository = repository;
        }

        // GET: api/Photo
        public IEnumerable<Object> Get()
        {
            var items = _repository.ReadPhotoItems();                       

            return items;
        }

        // GET: api/Photo/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Photo
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Photo/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Photo/5
        public void Delete(int id)
        {
        }
    }
}
