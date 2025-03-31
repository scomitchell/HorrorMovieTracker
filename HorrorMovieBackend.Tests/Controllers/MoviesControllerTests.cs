using Xunit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HorrorMovieBackend.Controllers;
using HorrorMovieBackend.Models;
using HorrorMovieBackend.Data;
using System.Linq;
using System.Collections.Generic;
using System;
using System.Reflection;

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

        SeedDatabase();

        // Initialize the controller with the in-memory dbContext
        _controller = new MoviesController(_dbContext);
    }

    // Clear the database after each test
    private void Dispose()
    {
        _dbContext.Movies.RemoveRange(_dbContext.Movies);
        _dbContext.SaveChanges();
    }

    private void SeedDatabase()
    {
        if (!_dbContext.Movies.Any())
        {
            _dbContext.Movies.Add(new Movie
            {
                Id = 1,
                Title = "The Shining",
                Subgenre = "Horror",
                ReleaseDate = new DateTime(1980, 5, 23)
            });

            _dbContext.Movies.Add(new Movie
            {
                Id = 2,
                Title = "Scream",
                Subgenre = "Slasher",
                ReleaseDate = new DateTime(1996, 12, 19)
            });

            _dbContext.SaveChanges();
        }
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

    [Fact]
    public async Task PutMovie_PutsMovie()
    {
        // Arrange: Retrieve the existing movie from the database
        var existingMovie = await _dbContext.Movies.FindAsync(2);
        Assert.NotNull(existingMovie); // Ensure the movie exists before updating

        // Modify the retrieved movie
        existingMovie.Title = "Scream - Updated";

        // Act: Send updated movie to the controller
        var result = await _controller.PutMovie(2, existingMovie);

        // Assert: Check the response type
        var noContentResult = Assert.IsType<NoContentResult>(result);
        Assert.Equal(204, noContentResult.StatusCode);

        // Verify that the movie was updated in the database
        var updatedMovie = await _dbContext.Movies.FindAsync(2);
        Assert.NotNull(updatedMovie);
        Assert.Equal("Scream - Updated", updatedMovie.Title);
    }

    [Fact]
    public async Task DeleteMovie_DeletesMovie()
    {
        var result = await _controller.DeleteMovie(1);

        // Ensure the response is NoContent
        var noContentResult = Assert.IsType<NoContentResult>(result);
        Assert.Equal(204, noContentResult.StatusCode);

        // Ensure movie was removed
        var deletedMovie = await _dbContext.Movies.FindAsync(1);
        Assert.Null(deletedMovie);
    }
}