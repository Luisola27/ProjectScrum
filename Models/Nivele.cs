using System;
using System.Collections.Generic;

namespace proyectoscrum.Models;

public partial class Nivele
{
    public int IdNivel { get; set; }

    public string? NombreNivel { get; set; }

    public virtual ICollection<Grupo> Grupos { get; set; } = new List<Grupo>();
}
