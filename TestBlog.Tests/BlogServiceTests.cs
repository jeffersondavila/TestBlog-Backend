using TestBlog.DTOs;
using TestBlog.Models;
using FluentAssertions;
using TestBlog.Services;
using Microsoft.EntityFrameworkCore;

namespace TestBlog.Tests
{
    public class BlogServiceTests
    {
        private static BlogDbContext CreateContextInMemory(string databaseName)
        {
            var opcions = new DbContextOptionsBuilder<BlogDbContext>()
                .UseInMemoryDatabase(databaseName)
                .Options;

            return new BlogDbContext(opcions);
        }

        private async static Task InsertDataInMemory(BlogDbContext context, IEnumerable<Blog> blogs)
        {
            context.Blogs.AddRange(blogs);
            await context.SaveChangesAsync();
        }

        [Fact]
        public async Task GetBlogsTest()
        {
            // ARRANGE
            var context = CreateContextInMemory(Guid.NewGuid().ToString());

            var blogs = Enumerable.Range(1, 15).Select(b => new Blog
            {
                CodigoBlog = b,
                Titulo = $"Titulo {b}",
                Contenido = $"Contenido {b}",
                Estado = true,
                CodigoUsuario = b,
                CodigoCategoria = b
            });

            await InsertDataInMemory(context, blogs);

            var blogService = new BlogService(context);

            // ACT
            var result = await blogService.GetBlogs();

            // ASSERT
            result.Should().NotBeNull();
            result.Should().HaveCount(10);
        }

        [Fact]
        public async Task GetBlogTest()
        {
            // ARRANGE
            var context = CreateContextInMemory(Guid.NewGuid().ToString());

            var blogs = Enumerable.Range(1, 15).Select(b => new Blog
            {
                CodigoBlog = b,
                Titulo = $"Titulo {b}",
                Contenido = $"Contenido {b}",
                Estado = true,
                CodigoUsuario = b,
                CodigoCategoria = b
            });

            await InsertDataInMemory(context, blogs);

            var blogService = new BlogService(context);

            // ACT
            var result = await blogService.GetBlog(13);

            // ASSERT
            result.Should().NotBeNull();
            result.CodigoBlog.Should().Be(13);
            result.Titulo.Should().Be("Titulo 13");
            result.Contenido.Should().Be("Contenido 13");
            result.Estado.Should().BeTrue();
            result.CodigoUsuario.Should().Be(13);
            result.CodigoCategoria.Should().Be(13);
        }

        [Fact]
        public async Task CreateBlogTest()
        {
            // ARRANGE
            var context = CreateContextInMemory(Guid.NewGuid().ToString());
            var blogService = new BlogService(context);

            var blogInsert = Enumerable
                .Range(1, 1)
                .Select(b => new BlogDTO
                {
                    CodigoBlog = b,
                    Titulo = $"Titulo {b}",
                    Contenido = $"Contenido {b}",
                    Estado = true,
                    CodigoUsuario = b,
                    CodigoCategoria = b
                })
                .FirstOrDefault() ?? new BlogDTO();

            // ACT
            await blogService.CreateBlog(blogInsert);

            // ASSERT
            var result = await blogService.GetBlog(1);

            result.Should().NotBeNull();
            result.CodigoBlog.Should().Be(1);
            result.Titulo.Should().Be("Titulo 1");
            result.Contenido.Should().Be("Contenido 1");
            result.Estado.Should().BeTrue();
            result.CodigoUsuario.Should().Be(1);
            result.CodigoCategoria.Should().Be(1);
        }

        [Fact]
        public async Task UpdateBlogTest()
        {
            // ARRANGE
            var context = CreateContextInMemory(Guid.NewGuid().ToString());

            var blogs = Enumerable.Range(1, 15).Select(b => new Blog
            {
                CodigoBlog = b,
                Titulo = $"Titulo {b}",
                Contenido = $"Contenido {b}",
                Estado = true,
                CodigoUsuario = b,
                CodigoCategoria = b
            });

            await InsertDataInMemory(context, blogs);

            var blogService = new BlogService(context);

            var blogUpdate = new BlogDTO
            {
                CodigoBlog = 5,
                Titulo = "Titulo 188",
                Contenido = "Contenido 189",
                Estado = false,
                CodigoUsuario = 1,
                CodigoCategoria = 190
            };

            // ACT
            await blogService.UpdateBlog(blogUpdate);

            // ASSERT
            var result = await blogService.GetBlog(5);

            result.Should().NotBeNull();
            result.CodigoBlog.Should().Be(5);
            result.Titulo.Should().Be("Titulo 188");
            result.Contenido.Should().Be("Contenido 189");
            result.Estado.Should().BeFalse();
            result.CodigoUsuario.Should().Be(1);
            result.CodigoCategoria.Should().Be(190);
        }

        [Fact]
        public async Task DeleteBlogTest()
        {
            // ARRANGE
            var context = CreateContextInMemory(Guid.NewGuid().ToString());

            var blogs = Enumerable.Range(1, 2).Select(b => new Blog
            {
                CodigoBlog = b,
                Titulo = $"Titulo {b}",
                Contenido = $"Contenido {b}",
                Estado = true,
                CodigoUsuario = b,
                CodigoCategoria = b
            });

            await InsertDataInMemory(context, blogs);

            var blogService = new BlogService(context);

            // ACT
            await blogService.DeleteBlog(2);

            // ASSERT
            var result = await blogService.GetBlogs();

            result.Should().NotBeNull();
            result.Should().HaveCount(1);
        }
    }
}