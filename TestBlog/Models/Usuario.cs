using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TestBlog.Models;

[Table("Usuario")]
public partial class Usuario
{
    [Key]
    public int CodigoUsuario { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string? Nombre { get; set; }

    public int? Edad { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string? Correo { get; set; }

    public bool? Estado { get; set; }

    [Column("Usuario")]
    [StringLength(500)]
    [Unicode(false)]
    public string? Usuario1 { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string? Contrasenia { get; set; }

    [InverseProperty("CodigoUsuarioNavigation")]
    public virtual ICollection<Blog> Blogs { get; set; } = new List<Blog>();
}
