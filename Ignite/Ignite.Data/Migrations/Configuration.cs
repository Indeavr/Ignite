namespace Ignite.Data.Migrations
{
    using Ignite.Data.Enums;
    using Ignite.Data.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public sealed class Configuration : DbMigrationsConfiguration<Ignite.Data.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Ignite.Data.ApplicationDbContext context)
        {
            //var ab = context.Assignments;
            //foreach (var item in ab)
            //{
            //    item.State = AssignmentState.Pending;
            //}
            //context.SaveChanges();

            //This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.

            //var assignment = new Assignment()
            //{
            //    CourseId = 1,
            //    DateOfAssignment = DateTime.Now,
            //    DueDate = DateTime.Now,
            //    State = AssignmentState.Started,
            //    Type = "Mandatory",
            //    UserId = context.Users.First(u => u.UserName == "goshkata@go.go").Id
            //};

            //context.Assignments.Add(assignment);


            //var ans = new HashSet<Answer>();
            //ans.Add(new Answer() { Text = "2", Letter = "A" });
            //ans.Add(new Answer() { Text = "23", Letter = "B" });
            //ans.Add(new Answer() { Text = "33", Letter = "C" });

            //context.Questions.Add(new Question() { Id = 15, Answers = ans, Statement = "1 + 1 = ?", CorrectAnswer = "A", CourseId = 1 });

            //context.SaveChanges();

            if (!context.Roles.Any())
            {   
                var role = new IdentityRole("Admin");
                context.Roles.Add(role);

                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

                // Create Admin Role 
                roleManager.Create(role);

                // Create an admin SUPERMAN user to Rule them all

                var user = new ApplicationUser();
                var usernameAndEmail = "admin@ignite.com";

                user.Email = usernameAndEmail;
                user.UserName = usernameAndEmail;

                string password = "admin1";

                var createUser = userManager.Create(user, password);

                if (createUser.Succeeded)
                {
                    var result = userManager.AddToRole(user.Id, "Admin");
                }

                // context.SaveChanges();
            }

        }
    }
}
