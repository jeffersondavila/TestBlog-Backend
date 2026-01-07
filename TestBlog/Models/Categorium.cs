using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TestBlog.Models;

public partial class Categorium
{
    [Key]
    public int CodigoCategoria { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string? Nombre { get; set; }

    public bool? Estado { get; set; }

    [InverseProperty("CodigoCategoriaNavigation")]
    public virtual ICollection<Blog> Blogs { get; set; } = new List<Blog>();
}
