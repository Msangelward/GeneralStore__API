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

            if (ModelState.IsValid)
            {
                //Store the model in the database
                _context.Transaction.Add(model);
                int changeCount = await _context.SaveChangesAsync();
                return Ok("Your Transaction was created.");
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

    }
}
