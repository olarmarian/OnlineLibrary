namespace OnlineLibrary.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using OnlineLibrary.Models;
    using OnlineLibrary.Services;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<OnlineLibrary.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "OnlineLibrary.Models.ApplicationDbContext";
        }

        protected override void Seed(ApplicationDbContext context)
        {
            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);

            if(!context.Users.Any(t=>t.UserName == "admin@gmail.com"))
            {
                var admin = new ApplicationUser
                {
                    UserName = "admin@gmail.com",
                    Email = "admin@gmail.com"
                };
                userManager.Create(admin, "!1asdasdasD");

                var service = new BookingAccountServices(context);
                service.CreateBookingAccount("admin", "admin", admin.Id);

                context.Roles.AddOrUpdate(r => r.Name, new IdentityRole
                {
                    Name = "admin"
                });
                context.SaveChanges();
                userManager.AddToRole(admin.Id, "admin");
            }
        }
    }
}
