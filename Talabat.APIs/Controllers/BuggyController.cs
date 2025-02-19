using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.Errors;
using Talabat.Repository.Data;

namespace Talabat.APIs.Controllers
{
   
    public class BuggyController : BaseApiController
    {
        private readonly StoreContext _context;

        public BuggyController(StoreContext context)
        {
            _context = context;
        }

        [HttpGet("notfound")] //Get : Buggy/notfound
        public ActionResult GetNotFoundRequest()
        {
            var product = _context.Products.Find(100);
            if(product == null)return NotFound( new ApiResponse(404));

            return Ok(product);
        }

        [HttpGet("servererror")] //Get :Buggy/servererror
        public ActionResult GetServerError()
        {
            var product = _context.Products.Find(100);
            var producttoreturn = product.ToString();
            return Ok(producttoreturn);
        }

        [HttpGet("badrequest")] // Get:Buggy/badrequest
        public ActionResult GetBadRequest()
        {
            return BadRequest(new ApiResponse(400));
        }

        [HttpGet("badrequest/{id}")] // Get:Buggy/badrequest/five
        public ActionResult GetBadRequest(int id)//ValidationErroe
        {
            return Ok();
        }
    }
}
