using FCManager.DataAccess.Data;
using FCManager.IdentityManager.Contexts;
using FCManager.Models.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using static FCManager.Models.Wrappers.Enum;

namespace FCManager.DataAccess.DbInitializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _context;
        private readonly IdentityContext _identitycontext;
        public DbInitializer(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            ApplicationDbContext context,
            IdentityContext identitycontext)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _context = context;
            _identitycontext = identitycontext;
        }


        public void Initialize()
        {
            //migrations if they are not applied
            try
            {
                if (_context.Database.GetPendingMigrations().Count() > 0)
                {
                    _context.Database.Migrate();
                }
            }
            catch (Exception ex)
            {

            }
            Console.WriteLine("---> Checking if Db exist");
            var exist = (_context.Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator).Exists();

            if (exist)
            {
                //Seed Roles
                _roleManager.CreateAsync(new IdentityRole(Roles.GlobalAdmin.ToString())).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(Roles.Coach.ToString())).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(Roles.Player.ToString())).GetAwaiter().GetResult();

                //Seed Super admin
                var defaultUser = new ApplicationUser
                {
                    UserName = "superadmin",
                    Email = "superadmin@gmail.com",
                    SuperAdminName = "Admin Account",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    IsActive = true,
                };
                if (_userManager.Users.All(u => u.Id != defaultUser.Id))
                {
                    var user = _identitycontext.Users.FirstOrDefault(x => x.Email == defaultUser.Email);
                    if (user == null)
                    {
                        var createUser = _userManager.CreateAsync(defaultUser, "Test12345!").Result;
                        var roles = _userManager.AddToRoleAsync(defaultUser, Roles.GlobalAdmin.ToString()).Result;
                    }
                }

                ////Seed Coach
                //var defaultCoach = new ApplicationUser
                //{
                //    UserName = "coach",
                //    Email = "coach@gmail.com",
                //    SuperAdminName = "Coach Account",
                //    EmailConfirmed = true,
                //    PhoneNumberConfirmed = true,
                //    IsActive = true,
                //    PlayerNumber = null
                //};
                //if (_userManager.Users.All(u => u.Id != defaultCoach.Id))
                //{
                //    var user = _identitycontext.Users.FirstOrDefault(x => x.Email == defaultCoach.Email);
                //    if (user == null)
                //    {
                //        var createUser = _userManager.CreateAsync(defaultCoach, "Test12345!").Result;
                //        var roles = _userManager.AddToRoleAsync(defaultCoach, Roles.Coach.ToString()).Result;
                //    }
                //}

                ////Seed Player
                //var defaultPlayer = new ApplicationUser
                //{
                //    UserName = "player",
                //    Email = "player@gmail.com",
                //    SuperAdminName = "Player Account",
                //    EmailConfirmed = true,
                //    PhoneNumberConfirmed = true,
                //    IsActive = true,
                //    PlayerNumber =
                //};
                //if (_userManager.Users.All(u => u.Id != defaultPlayer.Id))
                //{
                //    var user = _identitycontext.Users.FirstOrDefault(x => x.Email == defaultPlayer.Email);
                //    if (user == null)
                //    {
                //        var createUser = _userManager.CreateAsync(defaultPlayer, "Test12345!").Result;
                //        var roles = _userManager.AddToRoleAsync(defaultPlayer, Roles.Player.ToString()).Result;
                //    }
                //}

                //create genders if they are not created
                if (!_context.Genders.Any())
                {
                    Console.WriteLine("--->Seeding data for Gender table....");

                    _context.Genders.AddRange
                        (
                        new Gender() { GenderName = "Male" },
                        new Gender() { GenderName = "Female" },
                        new Gender() { GenderName = "Unspecified" }
                        );
                    _context.SaveChanges();
                    Console.WriteLine("---> Seeding Completed successfully for gender table");
                }
                else
                {
                    Console.WriteLine("---> We already have a data");
                }

                //Create team Member Category
                if (!_context.MemberCategories.Any())
                {
                    Console.WriteLine("--->Seeding data for MemberCategory table....");

                    _context.MemberCategories.AddRange
                        (
                        new MemberCategory() { TeamMemberCategory = "Coach" },
                        new MemberCategory() { TeamMemberCategory = "Player" }
                        );
                    _context.SaveChanges();
                    Console.WriteLine("---> Seeding Completed successfully for MemberCategory table");
                }
                else
                {
                    Console.WriteLine("---> We already have a data");
                }
            }
        }
    }
}
