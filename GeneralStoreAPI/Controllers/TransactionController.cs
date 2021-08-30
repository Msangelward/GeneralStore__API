using GeneralStoreAPI.Models;
using GeneralStorePI.Models;
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
    public class TransactionController : ApiController
    {
        private readonly ApplicationDbContext _context = new ApplicationDbContext();

        //Post (create)
        //api
        [HttpPost]
        public async Task<IHttpActionResult> CreateTransaction([FromBody] Transaction model)
        {
            if (model is null)
            {
                return BadRequest("Your request body cannot be empty.");
            }

            var customer = await _context.Customer.FindAsync(model.CustomerId);
            var product = await _context.Product.FindAsync(model.ProductSKU);
            var itemcount = await _context.Transaction.FindAsync(model.ItemCount);

            var validationResult = this.ValidateTransaction (model, product, customer);


            if (ModelState.IsValid)
            {
                //Store the model in the database
                _context.Transaction.Add(model);
                product.NumberInInventory = product.NumberInInventory - model.ItemCount;
                int changeCount = await _context.SaveChangesAsync();
                return Ok("Your Transaction was created and Inventory has been adjusted.");
               
            }

            // The model is not valid, go ahead and reject it
            return BadRequest(ModelState);
        }

        // GET ALL
        // api/Transaction
        [HttpGet]
        public async Task<IHttpActionResult> GetAll()
        {
            List<Transaction> transactions = await _context.Transaction.ToListAsync();
            return Ok(transactions);
        }

        // GET BY ID
        // api/Transaction/{id}
        [HttpGet]

        public async Task<IHttpActionResult> GetById([FromUri] int id)
        {
            Transaction transaction = await _context.Transaction.FindAsync(id);

            if (transaction != null)
            {
                return Ok(transaction);
            }

            return NotFound();

        }

        private string ValidateTransaction(Transaction transaction, Product product, Customer customer)

        {
            if (customer == null)
                Console.WriteLine("Invalid Transaction: Customer does not exist.");

            if (product == null)
                Console.WriteLine("Invalid Transaction: Product does not exist.");

            if (!product.IsInStock)
                Console.WriteLine("Invalid Transaction: Product is not in stock.");

            if (product.NumberInInventory < transaction.ItemCount)
                Console.WriteLine("Invalid Transaction: Not enough product in stock to complete the order.");

            return string.Empty;
        }

    }
}
