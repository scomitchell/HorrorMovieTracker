using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HorrorMovieBackend.Controllers;
using HorrorMovieBackend.Models;
using HorrorMovieBackend.Data;

namespace HorrorMovieBackend.Tests.Controllers
{
    public class UserMoviesControllerTests
    {
        private readonly UserMoviesController _controller;
        private readonly ApplicationDbContext _dbContext;

        public UserMoviesControllerTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;


        }
    }
}