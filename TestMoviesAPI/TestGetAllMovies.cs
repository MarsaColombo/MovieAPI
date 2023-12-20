using Microsoft.VisualStudio.TestTools.UnitTesting;
using MoviesAPI;
using System;

namespace TestMoviesAPI;

[TestClass]
public class TestGetAllMovies
{
    [TestMethod]
    public void returns_list_of_all_movies_with_valid_connection_string()
    {
        // Arrange
        string connectionString =
            @"Host=localhost;Port=5436;Username=postgres;Password=PirateBiaou@123;Database=movie_db;Pooling=true;";
        ;
        MovieRepository movieRepository = new MovieRepository(connectionString);
        // Act
        List<Movie> result = movieRepository.GetAllMovies();

        // Assert
        Assert.IsNotNull(result);
        Assert.IsTrue(result.Count > 3);
    }
}