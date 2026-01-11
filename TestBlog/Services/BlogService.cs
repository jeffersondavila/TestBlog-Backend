using Microsoft.EntityFrameworkCore;
using TestBlog.DTOs;
using TestBlog.Models;

namespace TestBlog.Services
{
    public class BlogService : IBlogService
    {
        private readonly BlogDbContext _context;
        public BlogService(BlogDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<BlogDTO>> GetBlogs()
        {
            var blogs = await _context.Blogs
                .Where(b => b.Estado == true)
                .Select(b => new BlogDTO 
                { 
                    CodigoBlog = b.CodigoBlog, 
                    Titulo = b.Titulo, 
                    Contenido = b.Contenido,
                    Estado = b.Estado,
                    CodigoCategoria = b.CodigoCategoria, 
                    CodigoUsuario = b.CodigoUsuario 
                })
                .Take(10)
                .ToListAsync() ?? new List<BlogDTO>();

            return blogs;
        }

        public async Task<BlogDTO> GetBlog(int id)
        {
            var blog = await _context.Blogs
                .Where(b => b.CodigoBlog == id)
                .Select(b => new BlogDTO
                {
                    CodigoBlog = b.CodigoBlog,
                    Titulo = b.Titulo,
                    Contenido = b.Contenido,
                    Estado = b.Estado,
                    CodigoCategoria = b.CodigoCategoria,
                    CodigoUsuario = b.CodigoUsuario
                })
                .FirstOrDefaultAsync() ?? new BlogDTO();

            return blog;
        }

        public async Task CreateBlog(BlogDTO blogDTO)
        {
            var blog = new Blog();

            blog.Titulo = blogDTO.Titulo;
            blog.Contenido = blogDTO.Contenido;
            blog.Estado = true;
            blog.CodigoCategoria = blogDTO.CodigoCategoria;
            blog.CodigoUsuario = blogDTO.CodigoUsuario;

            await _context.Blogs.AddAsync(blog);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateBlog(BlogDTO blogDTO)
        {
            var blog = await _context.Blogs
                .Where(b => b.CodigoBlog == blogDTO.CodigoBlog)
                .FirstOrDefaultAsync() ?? new Blog();

            blog.Titulo = blogDTO.Titulo;
            blog.Contenido = blogDTO.Contenido;
            blog.Estado = blogDTO.Estado;
            blog.CodigoCategoria = blogDTO.CodigoCategoria;
            blog.CodigoUsuario = blogDTO.CodigoUsuario;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteBlog(int id)
        {
            var blog = await _context.Blogs
                .Where(b => b.CodigoBlog == id)
                .FirstOrDefaultAsync() ?? new Blog();

            _context.Blogs.Remove(blog);
            await _context.SaveChangesAsync();
        }
    }

    public interface IBlogService
    {
        Task<IEnumerable<BlogDTO>> GetBlogs();
        Task<BlogDTO> GetBlog(int id);
        Task CreateBlog(BlogDTO blogDTO);
        Task UpdateBlog(BlogDTO blogDTO);
        Task DeleteBlog(int id);
    }
}
