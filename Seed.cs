using FrogExebitionAPI.Database;
using FrogExebitionAPI.Models;
using System.Diagnostics.Metrics;

namespace FrogExebitionAPI
{
    public class Seed
    {
        private readonly ApplicationContext _context;
        public Seed(ApplicationContext context)
        {
            _context = context;
        }
        public void SeedApplicationContext()
        {
            var exhibitions = new List<Exebition>
            {
                new Exebition
                {
                    Name = "Exhibition 1",
                    Date = DateTime.Now.AddDays(7),
                    Country = "Country 1",
                    City = "City 1",
                    Street = "Street 1",
                    House = "House 1"
                },
                new Exebition
                {
                    Name = "Exhibition 2",
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
                    Genus = "Genus 1",
                    Species = "Species 1",
                    Color = "Color 1",
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
                    Genus = "Genus 2",
                    Species = "Species 2",
                    Color = "Color 2",
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
                }
                // Add more frogs if needed
            };

            var users = new List<User>
            {
                new User
                {
                    Name = "User 1",
                    Email = "user1@example.com",
                    Password = "password1",
                    Vote = null
                },
                new User
                {
                    Name = "User 2",
                    Email = "user2@example.com",
                    Password = "password2",
                    Vote = null
                }
                // Add more users if needed
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

            foreach (var user in users)
            {
                _context.Set<User>().Add(user);
             
            }

            _context.SaveChanges();

            //for (int i = 0; i < (_context.Users.Count()) || (i < _context.FrogsOnExebitions.Count()); i++)
            //{
            //    _context.Votes.Add(new Vote() { User = _context.Users[i], FrogOnExebition = _context.FrogsOnExebitions[i] });
            //}
        }
    }
}
