using AutoMapper;
using FrogExebitionAPI.Database;
using FrogExebitionAPI.Interfaces;
using FrogExebitionAPI.Models;
using FrogExebitionAPI.Services;
using FrogExebitionAPI.UoW;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;


namespace FrogExebitionAPI
{
    public class Seed
    {
        private readonly ApplicationContext _context;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IAuthService _authService;
        private readonly ILogger<Seed> _logger;
        private readonly IMapper _mapper;
        public Seed(ApplicationContext context, IUnitOfWork unitOfWork, ILogger<Seed> logger, IMapper mapper, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IAuthService authService)
        {
            _context = context;
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
            _userManager = userManager;
            _roleManager = roleManager;
            _authService = authService;
        }
        public void SeedApplicationContext()
        {
            var exhibitions = new List<Exebition>
            {
                new Exebition
                {
                    Name = "Exhibition of Beautifull frogs",
                    Date = DateTime.Now.AddDays(7),
                    Country = "Country 1",
                    City = "City 1",
                    Street = "Street 1",
                    House = "House 1"
                },
                new Exebition
                {
                    Name = "FrogFEST",
                    Date = DateTime.Now.AddDays(14),
                    Country = "Country 2",
                    City = "City 2",
                    Street = "Street 2",
                    House = "House 2"
                }
            // Add more exhibitions if needed
            };

            var frogs = new List<Frog>
            {
                new Frog
                {
                    Genus = "Red Frog",
                    Species = "Species 1",
                    Color = "Red",
                    Habitat = "Habitat 1",
                    Poisonous = false,
                    Sex = "Male",
                    HouseKeepable = true,
                    Size = 5.0f,
                    Weight = 10.0f,
                    CurrentAge = 3,
                    MaxAge = 12,
                    Features = "Features 1",
                    Diet = "Diet 1",
                    Photo = "Photo 1"
                },
                new Frog
                {
                    Genus = "Green Frog",
                    Species = "Species 2",
                    Color = "Green",
                    Habitat = "Habitat 2",
                    Poisonous = true,
                    Sex = "Female",
                    HouseKeepable = false,
                    Size = 4.5f,
                    Weight = 8.0f,
                    CurrentAge = 2,
                    MaxAge = 10,
                    Features = "Features 2",
                    Diet = "Diet 2",
                    Photo = "Photo 2"
                },
                new Frog
                {
                    Genus = "Purple Frog",
                    Species = "Species 3",
                    Color = "Purple",
                    Habitat = "Habitat 3",
                    Poisonous = false,
                    Sex = "Female",
                    HouseKeepable = false,
                    Size = 8.5f,
                    Weight = 8.0f,
                    CurrentAge = 2,
                    MaxAge = 10,
                    Features = "Features 3",
                    Diet = "Diet 3",
                    Photo = "Photo 3"
                },
                new Frog
                {
                    Genus = "Yellow Frog",
                    Species = "Species 4",
                    Color = "Yellow",
                    Habitat = "Habitat 4",
                    Poisonous = true,
                    Sex = "Hermaphrodite",
                    HouseKeepable = false,
                    Size = 4.5f,
                    Weight = 8.0f,
                    CurrentAge = 2,
                    MaxAge = 10,
                    Features = "Features 4",
                    Diet = "Diet 4",
                    Photo = "Photo 4"
                }
                // Add more frogs if needed
            };

            

            foreach (var exhibition in exhibitions)
            {
                exhibition.Frogs.AddRange(frogs);
                _context.Set<Exebition>().Add(exhibition);
            }

            foreach (var frog in frogs)
            {
                frog.Exebitions.AddRange(exhibitions);
                _context.Set<Frog>().Add(frog);
            }

            _context.SaveChanges();

            var exe = _unitOfWork.Exebitions.GetAll();
            var frgs = _unitOfWork.Frogs.GetAll();
            foreach (var exhibition in exe)
            {
                foreach (var frog in frgs)
                {
                    var temp = new FrogOnExebition()
                    {
                        FrogId = frog.Id,
                        ExebitionId = exhibition.Id
                    };
                    _unitOfWork.FrogOnExebitions.Create(temp);
                }
            }

            //var roles = new[] { "Admin", "User" };
            //foreach (var role in roles)
            //{
            //    if (!await _roleManager.RoleExistsAsync(role))
            //    {
            //        await _roleManager.CreateAsync(new IdentityRole(role));
            //    }
            //}

            //var adminUser = new LoginUser() { UserName = "Admin", Email = "Admin@mail.com", Password = "P@ssw0rd" };
            //var regularUser = new LoginUser() { UserName = "User", Email = "User@mail.com", Password = "P@ssw0rd" };
            //await _authService.RegisterUser(adminUser);
            //await _authService.RegisterUser(regularUser);

            
            //await _userManager.AddToRoleAsync(await _userManager.FindByEmailAsync(adminUser.Email), "Admin");
            //await _userManager.AddToRoleAsync(await _userManager.FindByEmailAsync(regularUser.Email), "User");
        }
    }
}
