 
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
    public class UsersController : ApiController
    {
        string cs = ConfigurationManager.ConnectionStrings["TaskContext"].ConnectionString;

        private TaskContext db = new TaskContext();

        // GET: api/Users 
        public IQueryable<User> GetUsers()
        {
            return db.Users;
        }

        public IHttpActionResult GetUser(int id)
        {
            return Ok(db.Users.Find(id));
        }


        // POST: api/Users
        [ResponseType(typeof(User))]
        public IHttpActionResult PostUser(User user)
        {
            int i;
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                SqlCommand com = new SqlCommand("User_Update", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Id", user.Id);
                com.Parameters.AddWithValue("@EmailId", user.EmailId);
                com.Parameters.AddWithValue("@FirstName", user.FirstName);
                com.Parameters.AddWithValue("@LastName", user.LastName);
                com.Parameters.AddWithValue("@Address", user.Address);
                com.Parameters.AddWithValue("@DateofBirth", user.DateofBirth);
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

       
    }
}