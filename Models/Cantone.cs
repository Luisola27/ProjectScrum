using System;
using System.Collections.Generic;

namespace proyectoscrum.Models;

public partial class Cantone
{
    public int IdCanton { get; set; }

    public int IdProvincia { get; set; }

    public string? NombreCanton { get; set; }

    public virtual ICollection<CentrosEducativo> CentrosEducativos { get; set; } = new List<CentrosEducativo>();

    public virtual ICollection<Distrito> Distritos { get; set; } = new List<Distrito>();

    public virtual Provincia IdProvinciaNavigation { get; set; } = null!;
}
