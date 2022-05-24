
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using Task.API.Models;

namespace Task.API
{
    [EnableCors("*", "*", "*", SupportsCredentials = true)]
    public class TransactionsController : ApiController
    {
        private TaskContext db = new TaskContext();
        string cs = ConfigurationManager.ConnectionStrings["TaskContext"].ConnectionString;

        // GET: api/Transactions
        public IQueryable<Transaction> GetTransactions()
        {
            return db.Transactions;
        }
        public IQueryable<Transaction> GetTransactions(int userId)
        {
            return db.Transactions.Where(x=>x.UserId == userId).AsQueryable();
        }


        // POST: api/Transactions
        [ResponseType(typeof(Transaction))]
        public IHttpActionResult PostTransaction(Transaction transaction)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            int i;
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                SqlCommand com = new SqlCommand("Transaction_Insert", con);
                com.CommandType = CommandType.StoredProcedure; 
                com.Parameters.AddWithValue("@Position", transaction.Position);
                com.Parameters.AddWithValue("@FromDate", transaction.FromDate);
                com.Parameters.AddWithValue("@ToDate", transaction.ToDate);
                com.Parameters.AddWithValue("@UserId", transaction.UserId);
                com.Parameters.AddWithValue("@CompanyName", transaction.CompanyName);
                i = com.ExecuteNonQuery();
            }

            return Ok(i);
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TransactionExists(int id)
        {
            return db.Transactions.Count(e => e.Id == id) > 0;
        }
    }
}