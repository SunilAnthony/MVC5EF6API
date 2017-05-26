using Application.Data;
using Application.Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Application.WebApi.Controllers
{
    public class v2Controller : ApiController
    {
        private IProductRepository productService = new ProductRepository();

        [HttpGet]
        [Route("v2/getproducts")]
        public IHttpActionResult GetStoreProducts()
        {
            return Ok(productService.GetProducts());
        }
        [HttpGet]
        [Route("v2/{id}/getproduct")]  //id must match the GetProduct(params name)
        [ActionName("GetProduct")] //Method overloading
        public IHttpActionResult GetProduct(int id)
        {

            var product = productService.GetProduct(id);
            if (product == null)
                return NotFound();
            return Ok(product);
        }
        [HttpPost]
        public IHttpActionResult Post(Product product)
        {
            var isSave = productService.SaveProduct(product);
            if (isSave)
                return Ok(); //Return 200
            return BadRequest(); //Return 400
        }
        //[AcceptVerbs("PUT", "POST")] //Use for multiple METHOD: Just an example
        [HttpPut]
        public IHttpActionResult Put(int id, Product product)
        {
            if (!ModelState.IsValid)
                return BadRequest("Sorry, something is wrong. Couldn't update the record");
            if (id != product.Id)
                return BadRequest("Sorry, cannot find a record with id: " + id);

            var isUpdated = productService.UpdateProduct(product);
            if (isUpdated)
                return Ok();
            return BadRequest();
        }
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var isDelete = productService.DeleteProduct(id);
            if (isDelete)
                return Ok();
            return BadRequest();
        }
    }
}
