using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Workintech02RestApiDemo.Business.Blog;
using Workintech02RestApiDemo.Controllers;
using Workintech02RestApiDemo.Domain.Entities;

namespace Workintech02ResApiDemo.Test
{
    public class BlogControllerTest
    {
        private readonly Mock<IBlogService> blogServiceMock;
        private readonly BlogController blogController;

        public BlogControllerTest()
        {
            blogServiceMock = new Mock<IBlogService>();
            blogController = new BlogController(blogServiceMock.Object);
        }

        [Fact]
        public async Task GetBlogs_Should_OkResult()
        {
            //Arrange
            var fakeData = new List<Blog>()
            {
                new Blog(){Id= 1, Title = "Test Blog 1", Description = "Test Content 1"},
                new Blog(){Id= 2, Title = "Test Blog 2", Description = "Test Content 2"},
            };
            blogServiceMock.Setup(x=>x.GetBlogsAsync()).ReturnsAsync(fakeData);
            //Act
            var result = await blogController.GetBlogs();
            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<List<Blog>>(okResult.Value);
            //Assert.Single(returnValue);
            returnValue.Count.Should().Be(2);
            Assert.NotEmpty(returnValue);
            okResult.StatusCode.Should().Be(StatusCodes.Status200OK);
            
        }

        [Fact]
        public async Task GetBlog_Should_OkResult()
        {
            //Arrange
            var fakeData = new Blog() { Id= 1, Title = "Test Blog 1", Description = "Test Content 1"};
            blogServiceMock.Setup(x=>x.GetBlogAsync(1)).ReturnsAsync(fakeData);
            //Act
            var result = await blogController.GetBlog(1);
            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<Blog>(okResult.Value);
            Assert.Equal(fakeData, returnValue);
            okResult.StatusCode.Should().Be(StatusCodes.Status200OK);
        }

        [Fact]
        public async Task CreateBlog_Should_CreatedAtAction()
        {
            //Arrange
            var fakeData = new Blog() { Id= 1, Title = "Test Blog 1", Description = "Test Content 1"};
            blogServiceMock.Setup(x=>x.CreateBlogAsync(fakeData)).ReturnsAsync(fakeData);
            //Act
            var result = await blogController.CreateBlog(fakeData);
            //Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            var returnValue = Assert.IsType<Blog>(createdAtActionResult.Value);
            Assert.Equal(fakeData, returnValue);
            createdAtActionResult.StatusCode.Should().Be(StatusCodes.Status201Created);
        }
        
    }
}