using FluentAssertions;
using Workintech02RestApiDemo.Business.Movie;

namespace Workintech02RestApiDemo.Business.Test
{
    public class MovieServiceTest
    {
        protected readonly MovieService movieService;

        public MovieServiceTest()
        {
            var mockDb = TestHelper.GetInMemoryDbContext();
            movieService = new MovieService(mockDb);
        }


        [Fact]
        public async Task CreateMovieAsync_Should_ReturnInsertedValue()
        {
            //Arrange
            var movieSample = new Workintech02RestApiDemo.Domain.Entities.Movie()
            {
                Title = "Test Movie",
                CreatedDate = DateTime.Now,
                IsActive = true
            };
            //Act
            var insertedMovie = await movieService.CreateMovieAsync(movieSample);
            //Assert
            Assert.NotNull(insertedMovie);
            Assert.Equal(movieSample.Title, insertedMovie.Title);
            insertedMovie.Id.Should().BeGreaterThan(0);
        }

        [Fact]
        public async Task GetMoviesAsync_Should_ReturnAllMovies()
        {
            //Arrange
            var movieSample = new Workintech02RestApiDemo.Domain.Entities.Movie()
            {
                Title = "Test Movie",
                CreatedDate = DateTime.Now,
                IsActive = true
            };

            //Act
            await movieService.CreateMovieAsync(movieSample);
            var movies = await movieService.GetMoviesAsync();

            //Assert
            Assert.NotNull(movies);
            Assert.NotEmpty(movies);
            movies.Should().HaveCount(1);
        }
    }
}
