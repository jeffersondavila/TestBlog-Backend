using TestBlog.DTOs;
using TestBlog.Models;
using FluentAssertions;
using TestBlog.Services;
using Microsoft.EntityFrameworkCore;

namespace TestBlog.Tests
{
    public class BlogServiceTest
    {
        private static BlogDbContext CreateContextInMemory(string databaseName)
        {
            var options = new DbContextOptionsBuilder<BlogDbContext>()
                .UseInMemoryDatabase(databaseName)
                .Options;

            var context = new BlogDbContext(options);

            return context;
        }

        private async static Task InserDataInMemory(BlogDbContext context, IEnumerable<Blog> blogsInsert)
        {
            context.AddRange(blogsInsert);
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

            await InserDataInMemory(context, blogs);

            var service = new BlogService(context);

            // ACT
            var result = await service.GetBlogs();

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

            await InserDataInMemory(context, blogs);

            var service = new BlogService(context);

            // ACT
            var result = await service.GetBlog(9);

            // ASSERT
            result.Should().NotBeNull();
            result.CodigoBlog.Should().Be(9);
            result.Titulo.Should().Be("Titulo 9");
            result.Contenido.Should().Be("Contenido 9");
            result.Estado.Should().BeTrue();
            result.CodigoUsuario.Should().Be(9);
            result.CodigoCategoria.Should().Be(9);
        }

        [Fact]
        public async Task CreateBlogTest()
        {
            // ARRENGE
            var context = CreateContextInMemory(Guid.NewGuid().ToString());
            var service = new BlogService(context);

            // ACT
            var blog = new BlogDTO
            {
                CodigoBlog = 1,
                Titulo = "Titulo 16",
                Contenido = "Contenido 16",
                Estado = true,
                CodigoUsuario = 16,
                CodigoCategoria = 16
            };

            await service.CreateBlog(blog);

            // ASSERT
            var result = await service.GetBlog(1);
            result.CodigoBlog.Should().Be(1);
            result.Titulo.Should().Be("Titulo 16");
            result.Contenido.Should().Be("Contenido 16");
            result.Estado.Should().BeTrue();
            result.CodigoUsuario.Should().Be(16);
            result.CodigoCategoria.Should().Be(16);
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

            await InserDataInMemory(context, blogs);

            var service = new BlogService(context);

            // ACT
            var blog = new BlogDTO
            {
                CodigoBlog = 9,
                Titulo = "Titulo 120",
                Contenido = "Contenido 123",
                Estado = false,
                CodigoUsuario = 9,
                CodigoCategoria = 10
            };

            await service.UpdateBlog(blog);

            // ASSERT
            var result = await service.GetBlog(9);
            result.CodigoBlog.Should().Be(9);
            result.Titulo.Should().Be("Titulo 120");
            result.Contenido.Should().Be("Contenido 123");
            result.Estado.Should().BeFalse();
            result.CodigoUsuario.Should().Be(9);
            result.CodigoCategoria.Should().Be(10);
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

            await InserDataInMemory(context, blogs);

            var service = new BlogService(context);

            // ACT
            await service.DeleteBlog(2);

            // ASSERT
            var result = await service.GetBlogs();
            result.Should().HaveCount(1);
        }
    }
}