using System;
using System.Collections.Generic;

namespace proyectoscrum.Models;

public partial class Provincia
{
    public int IdProvincia { get; set; }

    public string? NombreProvincia { get; set; }

    public virtual ICollection<Cantone> Cantones { get; set; } = new List<Cantone>();

    public virtual ICollection<CentrosEducativo> CentrosEducativos { get; set; } = new List<CentrosEducativo>();
}
