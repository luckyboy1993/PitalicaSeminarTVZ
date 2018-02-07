using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PitalicaSeminar.DAL.Entities;
using PitalicaSeminar.GmailAuth.ApiControllers;
using Xunit;

namespace PitalicaSeminar.Tests.UnitTests
{
    public class Schools_Should
    {
        DbContextOptions<PitalicaDbContext> _dbContextOptions;

        public Schools_Should()
        {
            _dbContextOptions = new DbContextOptionsBuilder<PitalicaDbContext>()
                            .UseInMemoryDatabase(databaseName: "Test_database")
                            .Options;
        }

        // Testiranje dodavanja novih dobavljaca u bazu podataka
        [Fact]
        public async void PostSchools()
        {
            using (var context = new PitalicaDbContext(_dbContextOptions))
            {
                var schoolsAPI = new SchoolsController(context);
                for (int i = 0; i < 10; ++i)
                {
                    Schools tmpSchool = new Schools
                    {
                        Name = $"Skola { i + 1 }",
                        Address = new Addresses
                        {
                            CityName = "Zagreb",
                            Country = "Hrvatska",
                            StreetName = $"Ulica{ i }"
                        }
                    };

                    var result = await schoolsAPI.PostSchools(tmpSchool);
                    var badRequest = result as BadRequestObjectResult;

                    Assert.Null(badRequest); // Ako API ne vraca BadRequest, to znaci da je poziv uspjesan
                }      
            }
        }

        // Testiranje dohvata skole
        [Fact]
        public async void GetSchools()
        {
            // Dodavanje skola prije dohvacanja
            using (var context = new PitalicaDbContext(_dbContextOptions))
            {
                var schoolsAPI = new SchoolsController(context);
                for (int i = 0; i < 10; ++i)
                {
                Schools tmpSchool = new Schools
                {
                    Name = $"Skola { i + 1 }",
                    Address = new Addresses
                    {
                        CityName = "Zagreb",
                        Country = "Hrvatska",
                        StreetName = $"Ulica{ i }"
                    }
                };

                schoolsAPI.PostSchools(tmpSchool).Wait();
                }
            }

            using (var context = new PitalicaDbContext(_dbContextOptions))
            {
                var schoolsAPI = new SchoolsController(context);
                var result = await schoolsAPI.GetSchools(2);
                var okResult = result as OkObjectResult;

                // Ako je rezultat Ok i status kod je 200, tada je poziv uspjesan
                Assert.NotNull(okResult);
                Assert.Equal(200, okResult.StatusCode);

                // Ako je dohvacena skola sa ispravnim imenom, poziv je uspjesan
                Schools school = okResult.Value as Schools;
                Assert.NotNull(school);
                Assert.Equal("Skola 2", school.Name);
            }
        }

        // Testiranje dohvata skole
        [Fact]
        public async void DeleteSchools()
        {
            // Dodavanje skola prije dohvacanja
            using (var context = new PitalicaDbContext(_dbContextOptions))
            {
                var schoolsAPI = new SchoolsController(context);
                for (int i = 0; i < 10; ++i)
                {
                    Schools tmpSchool = new Schools
                    {
                        Name = $"Skola { i + 1 }",
                        Address = new Addresses
                        {
                            CityName = "Zagreb",
                            Country = "Hrvatska",
                            StreetName = $"Ulica{ i }"
                        }
                    };

                    schoolsAPI.PostSchools(tmpSchool).Wait();
                }
            }

            using (var context = new PitalicaDbContext(_dbContextOptions))
            {
                var schoolsAPI = new SchoolsController(context);
                var result = await schoolsAPI.DeleteSchools(2);
                var okResult = result as OkObjectResult;

                // Status kod je 200, tada je poziv uspjesan
                Assert.Equal(200, okResult.StatusCode);

                // Ako mu ne vrati ok result (jer ne postoji objekt), onda je test uspjesan

                var result2 = await schoolsAPI.GetSchools(2);
                var okResult2 = result2 as OkObjectResult;

                Assert.Null(okResult2);
            }

        }
    }
}
