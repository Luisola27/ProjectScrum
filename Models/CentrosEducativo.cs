using System;
using System.Collections.Generic;

namespace proyectoscrum.Models;

public partial class CentrosEducativo
{
    public int IdCentro { get; set; }

    public string? NombreCentro { get; set; }

    public int? IdTipo { get; set; }

    public int? IdProvincia { get; set; }

    public int? IdCanton { get; set; }

    public int? IdDistrito { get; set; }

    public string? Telefono { get; set; }

    public virtual Cantone? Cantone { get; set; }

    public virtual Distrito? Distrito { get; set; }

    public virtual ICollection<Grupo> Grupos { get; set; } = new List<Grupo>();

    public virtual Provincia? IdProvinciaNavigation { get; set; }

    public virtual TipoCentro? IdTipoNavigation { get; set; }
}
