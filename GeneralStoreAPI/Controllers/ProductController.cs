using GeneralStoreAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace GeneralStoreAPI.Controllers
{
    public class ProductController : ApiController
    {
        private readonly ApplicationDbContext _context = new ApplicationDbContext();

        // POST (create)
        // api/Product
        [HttpPost]
        public async Task<IHttpActionResult> CreateProduct([FromBody] Product model)
        {
            if (model is null)
            {
                return BadRequest("Your request body cannot be empty.");
            }

            if (ModelState.IsValid)
            {
                if item
                //Store the model in the database
                _context.Product.Add(model);
                int changeCount = await _context.SaveChangesAsync();
                return Ok("Your Product was created.");
            }

            // The model is not valid, go ahead and reject it
            return BadRequest(ModelState);
        }

        // GET ALL
        // api/Product
        [HttpGet]
        public async Task<IHttpActionResult> GetAll()
        {
            List<Product> products = await _context.Product.ToListAsync();
            return Ok(products);
        }

        // GET BY ID
        // api/Product/{sku}
        [HttpGet]

        public async Task<IHttpActionResult> GetBySKU([FromUri] string sku)
        {
            Product product = await _context.Product.FindAsync(sku);

            if (product != null)
            {
                return Ok(product);
            }

            return NotFound();

        }

    }
}
