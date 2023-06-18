using AutoMapper;
using FrogExebitionAPI.Database;
using FrogExebitionAPI.DTO.VoteDtos;
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
        private readonly IVoteService _voteService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IAuthService _authService;
        private readonly ILogger<Seed> _logger;
        private readonly IMapper _mapper;
        public Seed(ApplicationContext context, IUnitOfWork unitOfWork,IVoteService voteService, ILogger<Seed> logger, IMapper mapper, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IAuthService authService)
        {
            _context = context;
            _unitOfWork = unitOfWork;
            _voteService = voteService;
            _logger = logger;
            _mapper = mapper;
            _userManager = userManager;
            _roleManager = roleManager;
            _authService = authService;
        }
        public async Task SeedApplicationContextAsync()
        {
            var exhibitions = new List<Exebition>
            {
                new Exebition
                {
                    Name = "Exhibition of Beautifull frogs",
                    Date = DateTime.Now.AddDays(7),
                    Country = "Gernamy",
                    City = "Berlin",
                    Street = "Kharnchahte 34",
                    House = "17/b"
                },
                new Exebition
                {
                    Name = "FrogFEST",
                    Date = DateTime.Now.AddDays(14),
                    Country = "Poland",
                    City = "Krakov",
                    Street = "Pervolino 21",
                    House = "19"
                }
            // Add more exhibitions if needed
            };

            var frogs = new List<Frog>
            {
                new Frog
                {
                    Genus = "Red Frog",
                    Species = "Bull-frog",
                    Color = "Red",
                    Habitat = "North America",
                    Poisonous = false,
                    Sex = "Male",
                    HouseKeepable = true,
                    Size = 5.0f,
                    Weight = 10.0f,
                    CurrentAge = 3,
                    MaxAge = 12,
                    Features = "Bulls everyone around",
                    Diet = "Insects"
                },
                new Frog
                {
                    Genus = "Green Frog",
                    Species = "Jumping frog",
                    Color = "Green",
                    Habitat = "Asia",
                    Poisonous = true,
                    Sex = "Female",
                    HouseKeepable = false,
                    Size = 4.5f,
                    Weight = 8.0f,
                    CurrentAge = 2,
                    MaxAge = 10,
                    Features = "Can do triple backflip",
                    Diet = "flies"
                },
                new Frog
                {
                    Genus = "Purple Frog",
                    Species = "Programmer frog",
                    Color = "Purple",
                    Habitat = "Dark offices",
                    Poisonous = false,
                    Sex = "Female",
                    HouseKeepable = false,
                    Size = 8.5f,
                    Weight = 8.0f,
                    CurrentAge = 2,
                    MaxAge = 10,
                    Features = "She is capable of anything",
                    Diet = "Code bugs"
                },
                new Frog
                {
                    Genus = "Yellow Frog",
                    Species = "Toxic frog",
                    Color = "Yellow",
                    Habitat = "Africa",
                    Poisonous = true,
                    Sex = "Hermaphrodite",
                    HouseKeepable = false,
                    Size = 4.5f,
                    Weight = 8.0f,
                    CurrentAge = 2,
                    MaxAge = 10,
                    Features = "Peolpe say biological weapon isn't toxic as he/she is",
                    Diet = "Your suffering"
                }
                // Add more frogs if needed
            };

            

            foreach (var exhibition in exhibitions)
            {
                _unitOfWork.Exebitions.Create(exhibition);
            }

            foreach (var frog in frogs)
            {
                _unitOfWork.Frogs.Create(frog);
            }

            _unitOfWork.Save();

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
                    
                    try
                    {
                        _unitOfWork.FrogOnExebitions.Create(temp);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError("Db update exception");
                        continue;
                    }
                }
            }

            _unitOfWork.Save();

           
        }
    }
}
