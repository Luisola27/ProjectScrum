using System;
using System.Collections.Generic;

namespace proyectoscrum.Models;

public partial class Distrito
{
    public int IdDistrito { get; set; }

    public int IdCanton { get; set; }

    public int IdProvincia { get; set; }

    public string? NombreDistrito { get; set; }

    public virtual Cantone Cantone { get; set; } = null!;

    public virtual ICollection<CentrosEducativo> CentrosEducativos { get; set; } = new List<CentrosEducativo>();
}
