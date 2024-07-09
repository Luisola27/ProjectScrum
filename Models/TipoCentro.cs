using System;
using System.Collections.Generic;

namespace proyectoscrum.Models;

public partial class TipoCentro
{
    public int IdTipo { get; set; }

    public string? NombreTipo { get; set; }

    public virtual ICollection<CentrosEducativo> CentrosEducativos { get; set; } = new List<CentrosEducativo>();
}
