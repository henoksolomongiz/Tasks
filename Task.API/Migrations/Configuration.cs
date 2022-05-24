namespace Task.API.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Task.API.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<Task.API.Models.TaskContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Task.API.Models.TaskContext context)
        {
            
               context.Users.AddOrUpdate(
               p => p.FirstName,
               new User { FirstName = "test ", LastName= "Peters" , EmailId="test@gmail.com"},
                new User { FirstName = "Test2 ", LastName = "Peters", EmailId = "test2@gmail.com" },
                 new User { FirstName = "Test3 ", LastName = "Peters", EmailId = "test1@gmail.com" }
                );
            //
        }
    }
}
