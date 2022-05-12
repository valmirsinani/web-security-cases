
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Http;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        public HttpResponseMessage Post(Product product)
        {
            if (ModelState.IsValid)
            {
                //Insterimi/select/... i produktit ose 
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            else
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }
    }
}
