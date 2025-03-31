using Xunit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HorrorMovieBackend.Controllers;
using HorrorMovieBackend.Models;
using HorrorMovieBackend.Data;
using System.Linq;
using System.Collections.Generic;
using System;

public class MoviesControllerTests
{
    private readonly MoviesController _controller;
    private readonly ApplicationDbContext _dbContext;

    public MoviesControllerTests()
    {
        // Use an in-memory database for testing
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        _dbContext = new ApplicationDbContext(options);

        // Seed the database with a sample movie
        _dbContext.Movies.Add(new Movie
        {
            Id = 1,
            Title = "The Shining",
            Subgenre = "Horror",
            ReleaseDate = new DateTime(1980, 5, 23)
        });
        _dbContext.SaveChanges();

        // Initialize the controller with the in-memory dbContext
        _controller = new MoviesController(_dbContext);
    }

    [Fact]
    public async Task GetMovie_ReturnsMovie()
    {
        var result = await _controller.GetMovie(1);
        var okResult = Assert.IsType<ActionResult<Movie>>(result);
        var returnedMovie = Assert.IsType<Movie>(okResult.Value);

        Assert.Equal(1, returnedMovie.Id);
        Assert.Equal("The Shining", returnedMovie.Title);
        Assert.Equal("Horror", returnedMovie.Subgenre);
        Assert.Equal(new DateTime(1980, 5, 23), returnedMovie.ReleaseDate);
    }
}