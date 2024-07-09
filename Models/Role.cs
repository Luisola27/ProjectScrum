using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace proyectoscrum.Models;

public partial class Role
{
    [Display(Name = "ID Rol")]
    public int IdRol { get; set; }

    [Display(Name = "Nombre ROL")]
    public string? NombreRol { get; set; }

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
