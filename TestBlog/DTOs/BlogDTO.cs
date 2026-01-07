using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace TestBlog.DTOs
{
    public class BlogDTO
    {
        public int CodigoBlog { get; set; } = 0;
        public string? Titulo { get; set; } = string.Empty;
        public string? Contenido { get; set; } = string.Empty;
        public bool? Estado { get; set; } = true;
        public int? CodigoUsuario { get; set; } = 0;
        public int? CodigoCategoria { get; set; } = 0;
    }
}
