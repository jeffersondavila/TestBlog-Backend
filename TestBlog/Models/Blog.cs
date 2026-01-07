using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TestBlog.Models;

[Table("Blog")]
public partial class Blog
{
    [Key]
    public int CodigoBlog { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string? Titulo { get; set; }

    [Unicode(false)]
    public string? Contenido { get; set; }

    public bool? Estado { get; set; }

    public int? CodigoUsuario { get; set; }

    public int? CodigoCategoria { get; set; }

    [ForeignKey("CodigoCategoria")]
    [InverseProperty("Blogs")]
    public virtual Categorium? CodigoCategoriaNavigation { get; set; }

    [ForeignKey("CodigoUsuario")]
    [InverseProperty("Blogs")]
    public virtual Usuario? CodigoUsuarioNavigation { get; set; }
}
